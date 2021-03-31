using Domain;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IVrtigoRepository
    {
        GameData CreateGameData(GameData gameData);
        GameData ReadGameData(int gameDataId);
        void UpdateGameData(GameData gameData);
        void DeleteGameData(GameData gameData);

        TeleportData CreateTeleportData(TeleportData teleportData);
        TeleportData ReadTeleportData(int teleportDataId);
        IEnumerable<TeleportData> ReadTeleportDatas(int gameDataId);
        void UpdateTeleportData(TeleportData teleportData);
        void DeleteTeleportData(TeleportData teleportData);
        void DeleteTeleportDatas(int gameDataId);
        QuestionData CreateQuestionData(QuestionData QuestionData);
        QuestionData ReadQuestionData(int QuestionDataId);
        IEnumerable<QuestionData> ReadQuestionDatas(int gameDataId);
        void UpdateQuestionData(QuestionData QuestionData);
        void DeleteQuestionData(QuestionData QuestionData);

        QuestionResponse CreateQuestionResponse(QuestionResponse QuestionResponse);
        QuestionResponse ReadQuestionResponse(int QuestionResponseId);
        IEnumerable<QuestionResponse> ReadQuestionResponses(int gameDataId);
        void UpdateQuestionResponse(QuestionResponse QuestionResponse);
        void DeleteQuestionResponse(QuestionResponse QuestionResponse);

        QuestionType CreateQuestionType(QuestionType QuestionType);
        QuestionType ReadQuestionType(int QuestionTypeId);
        IEnumerable<QuestionType> ReadQuestionTypes(int gameDataId);
        void UpdateQuestionType(QuestionType QuestionType);
        void DeleteQuestionType(QuestionType QuestionType);
    }
}
