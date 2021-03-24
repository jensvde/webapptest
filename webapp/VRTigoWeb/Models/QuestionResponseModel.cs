using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VRTigoWeb.Models
{
    public class QuestionResponseModel
    {
        public int GameDataId { get; set; }
        public QuestionResponseLine[] QuestionResponseLines { get; set; }
    }
}
