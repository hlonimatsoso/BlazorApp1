using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class PlotyChartBase : ComponentBase
    {
        [Parameter] public string DivName { get; set; }

        [Parameter] public PlotyData InputData { get; set; }

        [Parameter] public int Value { get; set; }

        [Parameter] public string Title { get; set; }


        [Parameter]
        public MyChartType ChartType { get; set; } = MyChartType.None;

        [Inject] public IJSRuntime jsRuntime { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            jsRuntime.InvokeVoidAsync("DrawPolyChart", DivName, Data);
            return base.OnAfterRenderAsync(firstRender);
        }

        protected PlotyData _plotyData;

        public virtual PlotyData Data
        {
            get
            {
                if (_plotyData == null)
                    _plotyData = new PlotyData();

                if ((this as PlotyChartBarBase).IsHorzontal)
                {
                    _plotyData.x = InputData.y;
                    _plotyData.y = InputData.x;
                }
                else
                {
                    _plotyData.x = InputData.x;
                    _plotyData.y = InputData.y;
                }

                _plotyData.type = ChartType.ToString().ToLower();
                
                _plotyData.title = new PlotyData.Title { text = Title };

                return _plotyData;
            }
            set { }
        }
    }
}
