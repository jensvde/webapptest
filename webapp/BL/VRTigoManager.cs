using DAL;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class VRTigoManager : IVRTigoManager
    {
        private readonly IVrtigoRepository repo;
        public VRTigoManager()
        {
            repo = new VRTigoRepository(); 
        }

        public TeleportData AddTeleportData(TeleportData teleportData)
        {
            return repo.CreateTeleportData(teleportData);
        }

        public void ChangeGameData(GameData gameData)
        {
            repo.UpdateGameData(gameData);
        }

        public void ChangeTeleportData(TeleportData teleportData)
        {
            repo.UpdateTeleportData(teleportData);
        }

        public GameData GetGameData()
        {
            return repo.ReadGameData(1);
        }

        public TeleportData GetTeleportData(int teleportDataId)
        {
            return repo.ReadTeleportData(teleportDataId);
        }

        public void RemoveTeleportData(TeleportData teleportData)
        {
            repo.DeleteTeleportData(teleportData);
        }

        public void RemoveTeleportDatas(int gameDataId)
        {
            repo.DeleteTeleportDatas(gameDataId);
        }
        public IEnumerable<TeleportData> GetTeleportDatas(int gameDataId)
        {
            return repo.ReadTeleportDatas(gameDataId);
        }

        public QuestionData AddQuestionData(QuestionData QuestionData)
        {
            return repo.CreateQuestionData(QuestionData);
        }

        public void ChangeQuestionData(QuestionData QuestionData)
        {
            repo.UpdateQuestionData(QuestionData);
        }

        public QuestionData GetQuestionData(int QuestionDataId)
        {
            return repo.ReadQuestionData(QuestionDataId);
        }

        public IEnumerable<QuestionData> GetQuestionDatas(int gameDataId)
        {
            return repo.ReadQuestionDatas(gameDataId);
        }

        public void RemoveQuestionData(QuestionData QuestionData)
        {
            repo.DeleteQuestionData(QuestionData);
        }
        public QuestionResponse AddQuestionResponse(QuestionResponse QuestionResponse)
        {
            return repo.CreateQuestionResponse(QuestionResponse);
        }

        public void ChangeQuestionResponse(QuestionResponse QuestionResponse)
        {
            repo.UpdateQuestionResponse(QuestionResponse);
        }

        public QuestionResponse GetQuestionResponse(int QuestionResponseId)
        {
            return repo.ReadQuestionResponse(QuestionResponseId);
        }

        public IEnumerable<QuestionResponse> GetQuestionResponses()
        {
            return repo.ReadQuestionResponses();
        }

        public void RemoveQuestionResponse(QuestionResponse QuestionResponse)
        {
            repo.DeleteQuestionResponse(QuestionResponse);
        }
    }
}
