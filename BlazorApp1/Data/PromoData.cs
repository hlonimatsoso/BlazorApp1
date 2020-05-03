using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public class PromoData
    {
        public DateTime Date { get; set; }

        public string  Time { get; set; }

        public string Entry { get; set; }

        public string Store { get; set; }

        public string GeoLocation { get; set; }

        public int NumberOfItemsBought { get; set; }

        public int NumberOfEntries { get; set; }

    }

    public class PromoChartData
    {
        public PromoChartData()
        {
            Labels = new List<string>();
            Data = new List<double>();
            DisplayLabel = "Display set in Default CTOR";
        }
        public string DisplayLabel { get; set; }

        public List<string> Labels { get; set; }

        public List<double> Data { get; set; }

    }
}
