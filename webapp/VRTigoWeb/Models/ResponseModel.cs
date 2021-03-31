using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VRTigoWeb.Models
{
    public class ResponseModel
    {
        public string Title { get; set; }
        public List<ResponseChartItem> responseChartItems { get; set; }
        public int TotalSubmissions { get; set; }
    }

    public class ResponseChartItem
    {
        public string Name { get; set; }
        public int Result { get; set; }
    }
}
