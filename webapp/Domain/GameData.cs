using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class GameData
    {
        [Key]
        public int GameDataId { get; set; }
        public string Title { get; set; }
        public string IntroText { get; set; }
        public virtual ICollection<TeleportData> TeleportDatas { get; set; }
        public virtual ICollection<QuestionData> QuestionDatas { get; set; }
        public virtual ICollection<QuestionResponse> QuestionResponses { get; set; }
    }
}
