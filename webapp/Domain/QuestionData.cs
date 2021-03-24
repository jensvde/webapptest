using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class QuestionData : IComparable<QuestionData>
    {
        [Key]
        public int QuestionDataId { get; set; }
        public int Position { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public QuestionType QuestionType { get; set; }
[JsonIgnore] 
        public virtual GameData GameData { get; set; }

        public int CompareTo(QuestionData dat)
        {
            if (this.Position < dat.Position) return -1;
            if (this.Position == dat.Position) return 0;
            return 1;
        }
    }
}
