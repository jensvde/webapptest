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
        public DbSet<QuestionType> QuestionTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=VandenEyndeDb_EFCodeFirst.db");
            optionsBuilder.UseMySql("server=localhost;database=db;user=kdg;password=Kdg@202103");
            //optionsBuilder.UseMySql("server=localhost;database=db;user=kdg;password=kdg123");
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
                QuestionResponses = new List<QuestionResponse>(),
                QuestionTypes = new List<QuestionType>()
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
                Name = "Testpunt 22"
            };
            context.TeleportDatas.AddRange(new[] { tpData1, tpData2 });
            context.SaveChanges();

            //Seed questionTypes;
            QuestionType Auto = new QuestionType
            {
                Type = "Auto"
            };
            QuestionType Bus = new QuestionType
            {
                Type = "Bus"
            };
            QuestionType Fiets = new QuestionType
            {
                Type = "Fiets"
            };
            QuestionType Voetganger = new QuestionType
            {
                Type = "Voetganger"
            };
            QuestionType Transport = new QuestionType
            {
                Type = "Transport"
            };
            context.QuestionTypes.AddRange(new[] { Auto, Bus, Fiets, Voetganger, Transport });
            context.SaveChanges();

            QuestionData questionData = new QuestionData()
            {
                Title = "WAT IS DE ROL VAN DE AUTO IN ZAVENTEM?",
                Question = "Op dit moment verplaatst 61% zich in Zaventem met de auto voor verplaatsingen tussen woonplaats en werk of school. Hoeveel % zou dat in de toekomst moeten zijn? Via de knoppen kan je meer of minder auto’s in het straatbeeld plaatsen.",
                QuestionType = Auto,
                Position = 1
            };
            QuestionData questionData2 = new QuestionData()
            {
                Title = "PLAATS VOOR FIETSERS?",
                Question = "Een alternatief voor de wagen is de fiets. Wat is voor jou een goede balans tussen auto’s en fietsers? Via de knoppen kan je meer of minder fietsen in het straatbeeld plaatsen.",
                QuestionType = Fiets,
                Position = 2
            };
            QuestionData questionData3 = new QuestionData()
            {
                Title = "WAT KAN ER TE VOET?",
                Question = "Ook te voet kunnen er heel wat verplaatsingen gebeuren. Voeg ook voetgangers toe aan je mix.",
                QuestionType = Voetganger,
                Position = 3
            };
            QuestionData questionData4 = new QuestionData()
            {
                Title = "BUSSEN EN TREINEN",
                Question = "Er zijn verschillende buslijnen die Zaventem doorkruisen. Zie je het zitten om voor bepaalde verplaatsingen de bus of de trein te nemen? Hoeveel van de verplaatsingen kunnen er volgens jou met de bus gebeuren?",
                QuestionType = Bus,
                Position = 4
            };
            QuestionData questionData5 = new QuestionData()
            {
                Title = "Hoeveel plaats voor transport moet er zijn?",
                Question = "Natuurlijk moeten goederen van en naar luchthaven geraken. Deze vrachtwagens nemen veel plaats in beslag. Hoeveel plaats wil je hier voor vrijmaken?",
                QuestionType = Transport,
                Position = 5
            };
            QuestionData questionData6 = new QuestionData()
            {
                Title = "JOUW IDEALE MIX!",
                Question = "Heb je nu je ideale mix van vervoersmiddelen voor Zaventem gemaakt? Kijk het nog even na en pas eventueel nog een beetje aan en geef je mix dan door.",
                Position = 6,
                QuestionType = new QuestionType { Type = "Geen"}
            };
            context.QuestionDatas.AddRange(new[] { questionData, questionData2, questionData3, questionData4, questionData5, questionData6 });
            context.SaveChanges();

            QuestionResponse response1 = new QuestionResponse { QuestionResponseLines = new List<QuestionResponseLine>() };
            response1.QuestionResponseLines.Add(new QuestionResponseLine { QuestionResult = 10, QuestionType = Auto });
            QuestionResponse response2 = new QuestionResponse { QuestionResponseLines = new List<QuestionResponseLine>() };
            response2.QuestionResponseLines.Add(new QuestionResponseLine { QuestionResult = 20, QuestionType = Bus });
            QuestionResponse response3 = new QuestionResponse { QuestionResponseLines = new List<QuestionResponseLine>() };
            response3.QuestionResponseLines.Add(new QuestionResponseLine { QuestionResult = 40, QuestionType = Fiets });
            QuestionResponse response4 = new QuestionResponse { QuestionResponseLines = new List<QuestionResponseLine>() };
            response4.QuestionResponseLines.Add(new QuestionResponseLine { QuestionResult = 10, QuestionType = Voetganger });
            QuestionResponse response5 = new QuestionResponse { QuestionResponseLines = new List<QuestionResponseLine>() };
            response5.QuestionResponseLines.Add(new QuestionResponseLine { QuestionResult = 20, QuestionType = Transport });
            QuestionResponse response6 = new QuestionResponse { QuestionResponseLines = new List<QuestionResponseLine>() };
            response6.QuestionResponseLines.Add(new QuestionResponseLine { QuestionResult = 20, QuestionType = Transport });
            response6.QuestionResponseLines.Add(new QuestionResponseLine { QuestionResult = 20, QuestionType = Auto });
            response6.QuestionResponseLines.Add(new QuestionResponseLine { QuestionResult = 20, QuestionType = Bus });
            response6.QuestionResponseLines.Add(new QuestionResponseLine { QuestionResult = 20, QuestionType = Fiets });
            response6.QuestionResponseLines.Add(new QuestionResponseLine { QuestionResult = 20, QuestionType = Voetganger });

            context.QuestionResponses.AddRange(new[] { response1, response2, response3, response4, response5, response6 });
            context.SaveChanges();

            data.QuestionResponses.Add(response1);
            data.QuestionResponses.Add(response2);
            data.QuestionResponses.Add(response3);
            data.QuestionResponses.Add(response4);
            data.QuestionResponses.Add(response5);
            data.QuestionResponses.Add(response6);
            data.QuestionDatas.Add(questionData);
            data.QuestionDatas.Add(questionData2);
            data.QuestionDatas.Add(questionData3);
            data.QuestionDatas.Add(questionData4);
            data.QuestionDatas.Add(questionData5);
            data.QuestionDatas.Add(questionData6);
            data.TeleportDatas.Add(tpData1);
            data.TeleportDatas.Add(tpData2);
            data.QuestionTypes.Add(Auto);
            data.QuestionTypes.Add(Bus);
            data.QuestionTypes.Add(Fiets);
            data.QuestionTypes.Add(Voetganger);
            data.QuestionTypes.Add(Transport);
            context.Update(data);
            context.SaveChanges();

            

            foreach (EntityEntry entry in context.ChangeTracker.Entries().ToList())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
