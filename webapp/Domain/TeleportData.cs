using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class TeleportData
    {
        [Key]
        public int TeleportDataId { get; set; }
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        [JsonIgnore]

        public virtual GameData GameData { get; set; }

    }
}
