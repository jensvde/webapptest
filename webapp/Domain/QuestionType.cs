using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public enum QuestionTypes
    {
        Auto, Fiets, Voetganger, Bus, Transport
    }

    public class QuestionType
    {
        [Key]
        public int QuestionTypeId { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public virtual GameData GameData { get; set; }
    }
}
