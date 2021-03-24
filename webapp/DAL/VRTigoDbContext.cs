using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    internal class VRTigoDbContext : DbContext
    {
        private static bool hasRunDuringAppExecution = false;

        public DbSet<GameData> GameDatas { get; set; }
        public DbSet<TeleportData> TeleportDatas { get; set; }
        public DbSet<QuestionData> QuestionDatas { get; set; }
        public DbSet<QuestionResponse> QuestionResponses { get; set; }
        public DbSet<QuestionResponseLine> QuestionResponseLines { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=VandenEyndeDb_EFCodeFirst.db");
            optionsBuilder.UseMySql("server=localhost;database=db;user=kdg;password=kdg123");
        }

        public static void Initialize(VRTigoDbContext context, bool dropCreateDatabase = false)
        {
            if (!hasRunDuringAppExecution)
            {
                // Delete database if requested
                if (dropCreateDatabase)
                    context.Database.EnsureDeleted();
                // Create database and initial data if needed
                if (context.Database.EnsureCreated())
                    Seed(context);
                hasRunDuringAppExecution = true;
            }
        }

        private static void Seed(VRTigoDbContext context)
        {
            GameData data = new GameData
            {
                Title = "HOE MOET ZAVENTEM ER VOOR JOU UITZIEN?",
                IntroText = "Nadat we gepeild hebben naar jouw gedrag, willen we graag weten hoe je de mobiliteitsmix in Zaventem ziet. Waar moet Zaventem meer op inzetten? Wat is een goede mix voor de mobiliteit in onze gemeente? \n Voeg één voor één de manieren toe om je te verplaatsen in Zaventem, tot je de ideale mix hebt.",
                TeleportDatas = new List<TeleportData>(),
                QuestionDatas = new List<QuestionData>(),
                QuestionResponses = new List<QuestionResponse>()
            };
            context.GameDatas.Add(data);
            context.SaveChanges();
            TeleportData tpData1 = new TeleportData
            {
                X = -5.652814f, Y = 9.308958f, Z = 4.369f, Name = "Testpunt 1"
            };
            TeleportData tpData2 = new TeleportData
            {
                X = 2.656139f,
                Y = 9.853806f,
                Z = 4.369f,
                Name = "Testpunt 2"
            };
            context.TeleportDatas.AddRange(new[] { tpData1, tpData2 });
            context.SaveChanges();

            QuestionData questionData = new QuestionData()
            {
                Title = "WAT IS DE ROL VAN DE AUTO IN ZAVENTEM?",
                Question = "Op dit moment verplaatst 61% zich in Zaventem met de auto voor verplaatsingen tussen woonplaats en werk of school. Hoeveel % zou dat in de toekomst moeten zijn? Via de knoppen kan je meer of minder auto’s in het straatbeeld plaatsen.",
                QuestionType = QuestionType.Auto,
                Position = 1
            };
            QuestionData questionData2 = new QuestionData()
            {
                Title = "PLAATS VOOR FIETSERS?",
                Question = "Een alternatief voor de wagen is de fiets. Wat is voor jou een goede balans tussen auto’s en fietsers? Via de knoppen kan je meer of minder fietsen in het straatbeeld plaatsen.",
                QuestionType = QuestionType.Fiets,
                Position = 2
            };
            QuestionData questionData3 = new QuestionData()
            {
                Title = "WAT KAN ER TE VOET?",
                Question = "Ook te voet kunnen er heel wat verplaatsingen gebeuren. Voeg ook voetgangers toe aan je mix.",
                QuestionType = QuestionType.Voetganger,
                Position = 3
            };
            QuestionData questionData4 = new QuestionData()
            {
                Title = "BUSSEN EN TREINEN",
                Question = "Er zijn verschillende buslijnen die Zaventem doorkruisen. Zie je het zitten om voor bepaalde verplaatsingen de bus of de trein te nemen? Hoeveel van de verplaatsingen kunnen er volgens jou met de bus gebeuren?",
                QuestionType = QuestionType.Bus,
                Position = 4
            };
            QuestionData questionData5 = new QuestionData()
            {
                Title = "KUNNEN WE AUTO’S DELEN?",
                Question = "Verschillende onderzoeken hebben aangetoond dat één deelwagen 8 tot 12 voertuigen kan vervangen. Zou jij het zien zitten om een auto weg te doen en te vervangen door een deelvoertuig? Test hier wat de impact is van het aandeel deelvoertuigen op de noodzaak aan parkeerplaatsen en op het straatbeeld in het algemeen.",
                QuestionType = QuestionType.Deelauto,
                Position = 5
            };
            QuestionData questionData6 = new QuestionData()
            {
                Title = "JOUW IDEALE MIX!",
                Question = "Heb je nu je ideale mix van vervoersmiddelen voor Zaventem gemaakt? Kijk het nog even na en pas eventueel nog een beetje aan en geef je mix dan door.",
                QuestionType = QuestionType.Geen,
                Position = 6
            };
            context.QuestionDatas.AddRange(new[] { questionData, questionData2, questionData3, questionData4, questionData5, questionData6 });
            context.SaveChanges();

            QuestionResponse response1 = new QuestionResponse { QuestionResponseLines = new List<QuestionResponseLine>() };
            response1.QuestionResponseLines.Add(new QuestionResponseLine { QuestionResult = 10, QuestionType = QuestionType.Auto });
            QuestionResponse response2 = new QuestionResponse { QuestionResponseLines = new List<QuestionResponseLine>() };
            response2.QuestionResponseLines.Add(new QuestionResponseLine { QuestionResult = 30, QuestionType = QuestionType.Bus });
            context.QuestionResponses.AddRange(new[] { response1, response2 });
            context.SaveChanges();

            data.QuestionResponses.Add(response1);
            data.QuestionResponses.Add(response2);
            data.QuestionDatas.Add(questionData);
            data.QuestionDatas.Add(questionData2);
            data.QuestionDatas.Add(questionData3);
            data.QuestionDatas.Add(questionData4);
            data.QuestionDatas.Add(questionData5);
            data.QuestionDatas.Add(questionData6);
            data.TeleportDatas.Add(tpData1);
            data.TeleportDatas.Add(tpData2);
            context.Update(data);
            context.SaveChanges();

            

            foreach (EntityEntry entry in context.ChangeTracker.Entries().ToList())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
