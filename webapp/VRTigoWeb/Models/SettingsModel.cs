using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VRTigoWeb.Models
{
    public class SettingsModel
    {
        public int GameDataId { get; set; }
        public string Title { get; set; }
        public string IntroText { get; set; }
        public TeleportData[] TeleportDatas { get; set; }
        public QuestionData[] QuestionDatas { get; set; }
        public QuestionData QuestionData { get; set; }
        public List<SelectListItem> Items { get; set; }
        public int SelectedItem { get; set; }
        public bool NewQuestion { get; set; }
        public string NewTypeName { get; set; }
        public bool NewType { get; set; }
    }
}
