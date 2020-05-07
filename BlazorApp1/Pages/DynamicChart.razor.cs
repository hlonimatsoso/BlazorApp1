using BlazorApp1.Data;
using Blazorise.Charts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class DynamicChartBase : ComponentBase
    {
        //[Inject] ExcelService excelService { get; set; }

        PromoChartData _chartData;

        [Parameter]
        public PromoChartData ChartData
        {
            get { return _chartData; }
            set
            {
                _chartData = value;
                OnInitialized();
            }
        }

        [Parameter] public ChartType ChartType { get; set; } = ChartType.Bar;

        [Parameter] public string ChartName { get; set; } = "No Name";


        List<string> backgroundColors = new List<string> { ChartColor.FromRgba(255, 99, 132, 0.2f), ChartColor.FromRgba(54, 162, 235, 0.2f), ChartColor.FromRgba(255, 206, 86, 0.2f), ChartColor.FromRgba(75, 192, 192, 0.2f), ChartColor.FromRgba(153, 102, 255, 0.2f), ChartColor.FromRgba(255, 159, 64, 0.2f) };
        List<string> borderColors = new List<string> { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };

        //protected LineChart<double> lineChart;
        protected Chart<double> chart;
        //protected Chart<double> pieChart;
        //protected Chart<double> polarAreaChart;


        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            //if (lineChart != null)
            //   await HandleRedraw(lineChart, GetLineChartDataset, ChartData.Labels);

            if (chart != null)
                await HandleRedraw(chart, GetBarChartDataset, ChartData.Labels);

            //if (pieChart != null)
            //    await HandleRedraw(pieChart, GetPieChartDataset, ChartData.Labels);

            //if (polarAreaChart != null)
            //    await HandleRedraw(polarAreaChart, GetPolarAreaChartDataset, ChartData.Labels);

            //await Task.WhenAll(

            //    HandleRedraw(lineChart, GetLineChartDataset, ChartData.Labels),
            //    HandleRedraw(barChart, GetBarChartDataset, ChartData.Labels),
            //    HandleRedraw(pieChart, GetPieChartDataset, ChartData.Labels),
            //    HandleRedraw(polarAreaChart, GetPolarAreaChartDataset, ChartData.Labels)
            //);

            await base.OnAfterRenderAsync(firstRender);

        }

        protected async Task HandleRedraw<TDataSet, TItem, TOptions, TModel>(Blazorise.Charts.BaseChart<TDataSet, TItem, TOptions, TModel> chart, Func<TDataSet> getDataSet, IEnumerable<string> labels)
          where TDataSet : ChartDataset<TItem>
          where TOptions : ChartOptions
          where TModel : ChartModel
        {
            if (chart != null)
            {
                var dataSet = getDataSet();

                await chart.Clear();

                await chart.AddLabel(labels.ToArray());

                await chart.AddDataSet(dataSet);

                await chart.Update();
            }
        }

        protected void Update()
        {

        }

        LineChartDataset<double> GetLineChartDataset()
        {
            return new LineChartDataset<double>
            {
                Label = ChartData.DisplayLabel,
                Data = ChartData.Data,
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                Fill = true,
                PointRadius = 3,
                BorderWidth = 1,
                PointBorderColor = Enumerable.Repeat(borderColors.First(), 6).ToList()
            };
        }

       protected BarChartDataset<double> GetBarChartDataset()
        {
            return new BarChartDataset<double>
            {
                Label = ChartData.DisplayLabel,
                Data = ChartData.Data,
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                BorderWidth = 1
            };
        }

        PieChartDataset<double> GetPieChartDataset()
        {
            return new PieChartDataset<double>

            {
                Label = ChartData.DisplayLabel,
                Data = ChartData.Data,
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                BorderWidth = 1
            };
        }


        PolarAreaChartDataset<double> GetPolarAreaChartDataset()
        {
            return new PolarAreaChartDataset<double>
            {
                Label = ChartData.DisplayLabel,
                Data = ChartData.Data,
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                BorderWidth = 1
            };
        }

    }
}
