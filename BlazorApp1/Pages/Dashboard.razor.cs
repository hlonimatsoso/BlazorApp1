using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class DashboardBase : ComponentBase
    {
        List<PromoData> _promoDataList;
        [Parameter]
        public List<PromoData> PromoDataList
        {
            get { return _promoDataList; }
            set
            {
                _promoDataList = value;
                UpdateParent(value);
            }
        }

        private void UpdateParent(List<PromoData> value)
        {
            OnPromoDataListChanged.InvokeAsync(value);
        }

        [Parameter] public EventCallback<List<PromoData>> OnPromoDataListChanged { get; set; }

    }
}
