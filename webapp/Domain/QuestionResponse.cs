using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class QuestionResponse
    {
        [Key]
        public int QuestionResponseId { get; set; }
        public ICollection<QuestionResponseLine> QuestionResponseLines { get; set; }
        [JsonIgnore]
        public virtual GameData GameData { get; set; }
    }
}
