using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using VRTigoWeb.Models;
using BL;
using Domain;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace VRTigoWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IVRTigoManager mgr;

        public ValuesController()
        {
            mgr = new VRTigoManager();
        }

        // GET api/values/GameData
        [HttpGet("GameData")]
        public string GameData()
        {
            GameData gameData = mgr.GetGameData();
            GameDataModel model = new GameDataModel
            {
                TeleportDatas = mgr.GetTeleportDatas(gameData.GameDataId).ToArray(),
                Title = gameData.Title,
                IntroText = gameData.IntroText,
                GameDataId = gameData.GameDataId,
                QuestionDatas = mgr.GetQuestionDatas(gameData.GameDataId).ToArray()
            };

            return JsonConvert.SerializeObject(model, Formatting.Indented);
        }

 


        // POST api/values/UpdateImportantObjects
        [HttpPost("UpdateImportantObjects")]
        public void UpdateImportantObjects(GameDataModel model) {
            if (ModelState.IsValid)
            {
                GameData gameData = mgr.GetGameData();                

                if (model.Reset)
                { // Deleting all tp points and add the new ones
                    mgr.RemoveTeleportDatas(model.GameDataId);
                    int tdId = 1;
                    foreach (TeleportData td in model.TeleportDatas)
                    {
                        td.TeleportDataId = tdId;
                        tdId++;
                        td.GameData = gameData; //Set the gamedata 
                        mgr.AddTeleportData(td); //Then create 
                    }
                }
                    if (model.DeletedTeleportDatas != null)
                    { //Delete tp points first then update / add 
                        List<TeleportData> tpDatas = mgr.GetTeleportDatas(model.GameDataId).ToList();
                        foreach (int iD in model.DeletedTeleportDatas)
                        {
                            mgr.RemoveTeleportData(tpDatas.FirstOrDefault(x => x.TeleportDataId == iD));
                        }
                    }

                //So we've deleted everything that needed to go. Now to decide to update or add
                List<TeleportData> currentTeleportDatas = mgr.GetTeleportDatas(model.GameDataId).ToList();
                foreach (TeleportData tpData in model.TeleportDatas)
                {
                    if (currentTeleportDatas.Find(x => x.TeleportDataId == tpData.TeleportDataId) != null)
                    { //Already exists, update only
                        TeleportData data = mgr.GetTeleportData(tpData.TeleportDataId);
                        data.Name = tpData.Name;
                        data.X = tpData.X;
                        data.Y = tpData.Y;
                        data.Z = tpData.Z;
                        mgr.ChangeTeleportData(data);
                    }
                    else
                    { //New one, add instead
                        tpData.GameData = gameData;
                        mgr.AddTeleportData(tpData);
                    }
                }            
                }
            }

        // POST api/values/SortPanels
        [HttpPost("SortPanels")]
        public void SortPanels(SortPanelViewModel[] questions)
        {

            foreach (SortPanelViewModel item in questions)
            {
                QuestionData qDB = mgr.GetQuestionData(item.Id);
                qDB.Position = item.Priority;
                mgr.ChangeQuestionData(qDB);
            }

        }

        // POST api/values/UploadResponse
        [HttpPost("UploadResponse")]
        public void UploadResponse(QuestionResponseModel model)
        {
            QuestionResponse response = new QuestionResponse()
            {
                GameData = mgr.GetGameData(),
                QuestionResponseLines = model.QuestionResponseLines
            };
            mgr.AddQuestionResponse(response);
        }
    }
    }

