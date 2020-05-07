using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class RadzenComponentBase : ComponentBase
    {
        protected bool smooth;
        
        protected List<ChartData<int>> Series { get => Source.GetChartData(); }

        [Parameter] public PromoChartData Source { get; set; }

        [Parameter] public string Title { get; set; } = "Not Set";

        [Parameter] public MyChartType ChartType { get; set; } = MyChartType.Line;

    }
}
