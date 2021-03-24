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

namespace VRTigoWeb.Controllers
{
    public class SettingsController : Controller
    {
        private IVRTigoManager mgr;
        public SettingsController()
        {
            mgr = new VRTigoManager();
        }
        public IActionResult Index()
        {
            GameData model = mgr.GetGameData();
            model.QuestionDatas = model.QuestionDatas.AsEnumerable().OrderBy(x => x.Position).ToList();
            return View(model);
        }

        public IActionResult Responses()
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

        public IActionResult Test()
        {
            SettingsModel model = new SettingsModel();
            model.QuestionDatas = mgr.GetQuestionDatas(1).AsEnumerable().OrderBy(x => x.Position).ToArray();
            model.QuestionData = mgr.GetQuestionData(1);
            model.Items = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            var stringedTypes = ((QuestionType[])Enum.GetValues(typeof(QuestionType))).Select(qt => qt.ToString()).ToArray();
            for (int i = 0; i < stringedTypes.Length; i++)
            {
                model.Items.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = stringedTypes[i], Value = stringedTypes[i]
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
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult EditQuestion(SettingsModel model)
        {
            if (ModelState.IsValid)
            {
                mgr.ChangeQuestionData(model.QuestionData);
                return RedirectToAction("Test");
            }
            return BadRequest();
        }
    }
}
