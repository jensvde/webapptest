using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Threading.Tasks;
using VRTigoWeb.Models;
using BL;
using Domain;
using Microsoft.AspNetCore.Authorization;

namespace VRTigoWeb.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private IVRTigoManager mgr;
        public SettingsController()
        {
            mgr = new VRTigoManager();
        }

        public IActionResult Teleport()
        {
            return View();
        }

        public IActionResult Responses()
        {
            ResponseModel model = new ResponseModel();
            List<ResponseChartItem> responseChartItems = new List<ResponseChartItem>();
            List<QuestionType> questionTypes = mgr.GetQuestionTypes(1).ToList();
            foreach(QuestionType questionType in questionTypes)
            {
                int totalResult = 0;
                int counter = 0;
                foreach(QuestionResponse response in mgr.GetQuestionResponses(1))
                {
                    foreach(QuestionResponseLine line in response.QuestionResponseLines)
                    {
                        if (line.QuestionType.Type.Equals(questionType.Type))
                        {
                            totalResult += line.QuestionResult;
                            counter++;
                        }
                    }
                }
                responseChartItems.Add(new ResponseChartItem
                {
                    Name = questionType.Type, Result = (totalResult/counter)
                });
            }
            model.responseChartItems = responseChartItems;
            model.Title = "Resultaten van de quiz";
            model.TotalSubmissions = mgr.GetQuestionResponses(1).Count();
            return View(model);
        }

        public IActionResult General()
        {
            GameData model = mgr.GetGameData();
            return View(model);
        }

        public IActionResult Game()
        {
            GameData model = mgr.GetGameData();
            model.QuestionDatas = model.QuestionDatas.AsEnumerable().OrderBy(x => x.Position).ToList();
            return View(model);
        }

        public IActionResult Question()
        {
            SettingsModel model = new SettingsModel();
            model.QuestionDatas = mgr.GetQuestionDatas(1).AsEnumerable().OrderBy(x => x.Position).ToArray();
            model.QuestionData = mgr.GetQuestionData(1);
            model.GameDataId = 1;
            model.Items = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            //var stringedTypes = ((QuestionType[])Enum.GetValues(typeof(QuestionType))).Select(qt => qt.ToString()).ToArray();
            
            foreach(QuestionType type in mgr.GetQuestionTypes(1))
            {
                model.Items.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = type.Type, Value = ""+type.QuestionTypeId
                });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult EditGeneralInfo(GameDataModel model)
        {
            if (model.Title != null)
            {
                GameData gameDat = mgr.GetGameData();
                gameDat.Title = model.Title;
                gameDat.IntroText = model.IntroText;
                mgr.ChangeGameData(gameDat);
                return RedirectToAction("General");
            }
            return BadRequest();
        }

        public IActionResult DeleteQuestion(int id)
        {
            QuestionData toRemove = mgr.GetQuestionData(1);
            mgr.RemoveQuestionData(toRemove);
            return RedirectToAction("Question");
        }

        public IActionResult DeleteQuestionType(int id)
        {
            List<QuestionData> questionDatas = mgr.GetQuestionDatas(1).ToList();
            QuestionType toRemove = mgr.GetQuestionType(id);

            foreach(QuestionData question in questionDatas)
            {
                if (question.QuestionType != null)
                {
                    if (question.QuestionType.QuestionTypeId == toRemove.QuestionTypeId)
                    {
                        question.QuestionType = null;
                        mgr.ChangeQuestionData(question);
                    }
                }
            }
            List<QuestionResponse> questionResponses = mgr.GetQuestionResponses(1).ToList();
            foreach (QuestionResponse questionResponse in questionResponses)
            {
                if (questionResponse.QuestionResponseLines != null)
                {
                    foreach (QuestionResponseLine questionResponseLine in questionResponse.QuestionResponseLines)
                    {
                        if (questionResponseLine.QuestionType != null)
                        {
                            if (questionResponseLine.QuestionType.QuestionTypeId == toRemove.QuestionTypeId)
                            {
                                questionResponseLine.QuestionType = null;
                            }
                            mgr.ChangeQuestionResponse(questionResponse);
                        }
                    }
                }
            }
            toRemove.GameData = null;
            mgr.RemoveQuestionType(toRemove);
            return RedirectToAction("Question");
        }

        [HttpPost]
        public IActionResult EditQuestion(SettingsModel model)
        {
            if (ModelState.IsValid)
            {
                QuestionType addedType = new QuestionType();
                if (model.NewType)
                { //Add new questionType first
                    GameData gameData = mgr.GetGameData();
                    QuestionType questionType = new QuestionType
                    {
                        Type = model.NewTypeName, GameData = gameData
                    };
                    addedType = mgr.AddQuestionType(questionType);
                }

                if (!model.NewQuestion)
                { //Change question
                    if (model.NewType) { //New type? use that one
                        model.QuestionData.QuestionType = addedType;
                    }
                    else //Else search and use one from the list
                    {
                        QuestionType selectedType = mgr.GetQuestionType(model.SelectedItem);
                        model.QuestionData.QuestionType = selectedType;
                    }
                    QuestionData toChange = mgr.GetQuestionData(model.QuestionData.QuestionDataId);
                    toChange.QuestionType = model.QuestionData.QuestionType;
                    toChange.Question = model.QuestionData.Question;
                    toChange.KeepPreviousValue = model.QuestionData.KeepPreviousValue;
                    toChange.Title = model.QuestionData.Title;
                    mgr.ChangeQuestionData(toChange);
                    return RedirectToAction("Question");
                }
                else
                {//add question
                    if (model.NewType)
                    { //New type? use that one
                        model.QuestionData.QuestionType = addedType;
                    }
                    else //Else search and use one from the list
                    {
                        model.QuestionData.QuestionType = mgr.GetQuestionType(model.SelectedItem);
                    }
                    model.QuestionData.GameData = mgr.GetGameData();//model.GameDataId);
                    mgr.AddQuestionData(model.QuestionData);
                    return RedirectToAction("Question");
                }
                
            }
            return BadRequest();
        }
    }
}
