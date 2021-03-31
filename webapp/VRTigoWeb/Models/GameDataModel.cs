using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VRTigoWeb.Models
{
    [Serializable]
    public class GameDataModel
    {
        public int GameDataId { get; set; }
        public string Title { get; set; }
        public string IntroText { get; set; }
        public TeleportData[] TeleportDatas { get; set; }
        public int[] DeletedTeleportDatas { get; set; }

        public QuestionData[] QuestionDatas { get; set; }
        public bool Reset { get; set; }

    }
}

