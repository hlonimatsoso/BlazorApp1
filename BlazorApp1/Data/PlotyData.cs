using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public class PlotyData
    {
        public PlotyData()
        {
            x = new List<string> { };
            y = new List<string> { };
            type = MyChartType.Bar.ToString();
            domain = new Domain();
            title = new Title();
        }

        public List<string> x { get; set; }
        public List<string> y { get; set; }
        public string type { get; set; }
        public string mode { get; set; }
        public Title title { get; set; }
        public Domain domain { get; set; }
        public int value { get; set; }
        public string orientation { get; set; }

        public Layout layout { get; set; }

        public class Domain
        {
            public Domain()
            {
                x = new List<int> { 0, 1 };
                y = new List<int> { 0, 1 };
            }
            public List<int> x { get; set; }
            public List<int> y { get; set; }
        }

        public class Title
        {
            public string text { get; set; } = "Title Not Set";
        }

    }

    public class Layout
    {
        public Layout()
        {
            font = new Font();
        }

        public string title { get; set; } = "Title Not Set";

        public Font font { get; set; }

    }

    public class Font
    {
        public string family { get; set; } = "Raleway, sans-serif";
    }


}
