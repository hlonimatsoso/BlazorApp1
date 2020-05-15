using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class PlotyChartGaugeBase : PlotyChartBase
    {
        public override PlotyData Data => new PlotyData
        {

            type = ChartType.ToString(),
            value = Value,
            mode = Modes.GaugePlusNumber,
            title = new PlotyData.Title { text = Title }

            //domain = new PlotyData.Domain { x = { 0, 1 },y= { 0, 1 } }
        };
    }
}
