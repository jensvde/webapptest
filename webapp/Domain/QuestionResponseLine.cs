using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class QuestionResponseLine
    {
        [Key]
        public int QuestionResponseLineId { get; set; }
        public QuestionType QuestionType { get; set; }
        public int QuestionResult { get; set; }
        public virtual QuestionResponse QuestionResponse { get; set; }
    }
}
