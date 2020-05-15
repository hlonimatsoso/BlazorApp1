using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class PlotyChartBarBase : PlotyChartBase
    {
        [Parameter] public bool IsHorzontal { get; set; }
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (IsHorzontal)
            {
                if (base._plotyData == null)
                    base._plotyData = new PlotyData { };
                
                base._plotyData.orientation = "h";
            }
                

            return base.OnAfterRenderAsync(firstRender);
        }
    }
}
