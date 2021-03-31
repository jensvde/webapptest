using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class VRTigoRepository : IVrtigoRepository
    {
        private VRTigoDbContext ctx = null;
        public VRTigoRepository()
        {
            ctx = new VRTigoDbContext();
            VRTigoDbContext.Initialize(ctx, dropCreateDatabase:true);
        }
        public GameData CreateGameData(GameData gameData)
        {
            ctx.GameDatas.Add(gameData);
            ctx.SaveChanges();
            return gameData;
        }

        public TeleportData CreateTeleportData(TeleportData teleportData)
        {
            ctx.TeleportDatas.Add(teleportData);
            ctx.SaveChanges();
            return teleportData;
        }

        public void DeleteGameData(GameData gameData)
        {
            ctx.GameDatas.Remove(gameData);
            ctx.SaveChanges();
        }

        public void DeleteTeleportData(TeleportData teleportData)
        {
            ctx.TeleportDatas.Remove(teleportData);
            ctx.SaveChanges();    
        }

        public void DeleteTeleportDatas(int gameDataId)
        {
            List<TeleportData> tpData = ctx.TeleportDatas.Where(x => x.GameData.GameDataId == gameDataId).ToList();
            ctx.TeleportDatas.RemoveRange(tpData);
            ctx.SaveChanges();
        }


        public GameData ReadGameData(int gameDataId)
        {
            return ctx.GameDatas
                .Include(x => x.TeleportDatas)
                .Include(x => x.QuestionDatas)
                .Include(x => x.QuestionResponses).ThenInclude(x => x.QuestionResponseLines)
                .Single(x => x.GameDataId == gameDataId);
        }

        public TeleportData ReadTeleportData(int teleportDataId)
        {
            return ctx.TeleportDatas.Single(x => x.TeleportDataId == teleportDataId);
        }

        public void UpdateGameData(GameData gameData)
        {
            ctx.GameDatas.Update(gameData);
            ctx.SaveChanges();
        }

        public void UpdateTeleportData(TeleportData teleportData)
        {
            ctx.TeleportDatas.Update(teleportData);
            ctx.SaveChanges();
        }

        public IEnumerable<TeleportData> ReadTeleportDatas(int gameDataId)
        {
            return ctx.TeleportDatas.Where(x => x.GameData.GameDataId == gameDataId).AsEnumerable();
        }
        public QuestionData CreateQuestionData(QuestionData QuestionData)
        {
            ctx.QuestionDatas.Add(QuestionData);
            ctx.SaveChanges();
            return QuestionData;
        }
        public void DeleteQuestionData(QuestionData QuestionData)
        {
            ctx.QuestionDatas.Remove(QuestionData);
            ctx.SaveChanges();
        }
        public QuestionData ReadQuestionData(int QuestionDataId)
        {
            return ctx.QuestionDatas.Single(x => x.QuestionDataId == QuestionDataId);
        }
        public IEnumerable<QuestionData> ReadQuestionDatas(int gameDataId)
        {
            return ctx.QuestionDatas.Include(x => x.QuestionType).Where(x => x.GameData.GameDataId == gameDataId).AsEnumerable();
        }
        public void UpdateQuestionData(QuestionData QuestionData)
        {
            ctx.QuestionDatas.Update(QuestionData);
            ctx.SaveChanges();
        }
        public QuestionResponse CreateQuestionResponse(QuestionResponse QuestionResponse)
        {
            ctx.QuestionResponses.Add(QuestionResponse);
            ctx.SaveChanges();
            return QuestionResponse;
        }
        public void DeleteQuestionResponse(QuestionResponse QuestionResponse)
        {
            ctx.QuestionResponses.Remove(QuestionResponse);
            ctx.SaveChanges();
        }
        public QuestionResponse ReadQuestionResponse(int QuestionResponseId)
        {
            return ctx.QuestionResponses
                .Include(x => x.QuestionResponseLines)
                .Single(x => x.QuestionResponseId == QuestionResponseId);
        }
        public IEnumerable<QuestionResponse> ReadQuestionResponses(int gameDataId)
        {
            return ctx.QuestionResponses.Include(x => x.QuestionResponseLines).Where(x => x.GameData.GameDataId == gameDataId).AsEnumerable();
        }
        public void UpdateQuestionResponse(QuestionResponse QuestionResponse)
        {
            ctx.QuestionResponses.Update(QuestionResponse);
            ctx.SaveChanges();
        }

        public QuestionType CreateQuestionType(QuestionType QuestionType)
        {
            ctx.QuestionTypes.Add(QuestionType);
            ctx.SaveChanges();
            return QuestionType;
        }
        public void DeleteQuestionType(QuestionType QuestionType)
        {
            ctx.QuestionTypes.Remove(QuestionType);
            ctx.SaveChanges();
        }
        public QuestionType ReadQuestionType(int QuestionTypeId)
        {
            return ctx.QuestionTypes
                .Single(x => x.QuestionTypeId == QuestionTypeId);
        }
        public IEnumerable<QuestionType> ReadQuestionTypes(int gameDataId)
        {
            return ctx.QuestionTypes.Where(x => x.GameData.GameDataId == gameDataId).AsEnumerable();
        }
        public void UpdateQuestionType(QuestionType QuestionType)
        {
            ctx.QuestionTypes.Update(QuestionType);
            ctx.SaveChanges();
        }
    }
}
