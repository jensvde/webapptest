using Domain;
using System;
using System.Collections.Generic;

namespace BL
{
    public interface IVRTigoManager
    {
        GameData GetGameData();
        void ChangeGameData(GameData gameData);

        TeleportData AddTeleportData(TeleportData teleportData);
        TeleportData GetTeleportData(int teleportDataId);
        IEnumerable<TeleportData> GetTeleportDatas(int gameDataId);

        void ChangeTeleportData(TeleportData teleportData);
        void RemoveTeleportData(TeleportData teleportData);
        void RemoveTeleportDatas(int gameDataId);


        QuestionData AddQuestionData(QuestionData QuestionData);
        QuestionData GetQuestionData(int QuestionDataId);
        IEnumerable<QuestionData> GetQuestionDatas(int gameDataId);
        void ChangeQuestionData(QuestionData QuestionData);
        void RemoveQuestionData(QuestionData QuestionData);

        QuestionResponse AddQuestionResponse(QuestionResponse QuestionResponse);
        QuestionResponse GetQuestionResponse(int QuestionResponseId);
        IEnumerable<QuestionResponse> GetQuestionResponses(int gameDataId);
        void ChangeQuestionResponse(QuestionResponse QuestionResponse);
        void RemoveQuestionResponse(QuestionResponse QuestionResponse);

        QuestionType AddQuestionType(QuestionType QuestionType);
        QuestionType GetQuestionType(int QuestionTypeId);
        IEnumerable<QuestionType> GetQuestionTypes(int gameDataId);
        void ChangeQuestionType(QuestionType QuestionType);
        void RemoveQuestionType(QuestionType QuestionType);
    }
}
