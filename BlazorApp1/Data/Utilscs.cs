using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public enum MyChartType
    {
        None,Bar, Line, Pie, Donut, Area, indicator
    }

    public static class Modes
    {
        public const string gauge_plus_number = "gauge+number";

        public static string GaugePlusNumber { get { return "gauge+number"; } }
    }
}
