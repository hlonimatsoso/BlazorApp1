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

        [Parameter] public ChartType ChartType { get; set; } = ChartType.Bar;

        List<string> backgroundColors = new List<string> { ChartColor.FromRgba(255, 99, 132, 0.2f), ChartColor.FromRgba(54, 162, 235, 0.2f), ChartColor.FromRgba(255, 206, 86, 0.2f), ChartColor.FromRgba(75, 192, 192, 0.2f), ChartColor.FromRgba(153, 102, 255, 0.2f), ChartColor.FromRgba(255, 159, 64, 0.2f) };
        List<string> borderColors = new List<string> { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };

        protected LineChart<double> lineChart;
        protected Chart<double> barChart;
        protected Chart<double> pieChart;
        protected Chart<double> polarAreaChart;

        [Parameter] public List<PromoData> PromoData { get; set; }

        PromoChartData LineChartData { get; set; }
        PromoChartData BarChartData { get; set; }
        PromoChartData PieChartData { get; set; }
        PromoChartData PolarChartData { get; set; }




        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //lineChart = new LineChart<double>();
            LineChartData = PromoData.GetPerStoreMetrics();
            PieChartData = PromoData.GetPerGeoLocationMetrics();
            BarChartData = PromoData.GetPerEntryMetrics();
            PolarChartData = PromoData.GetPerDateMetrics();


            await Task.WhenAll(
                HandleRedraw(lineChart, GetLineChartDataset, LineChartData.Labels),
                HandleRedraw(barChart, GetBarChartDataset, BarChartData.Labels),
                HandleRedraw(pieChart, GetPieChartDataset, PieChartData.Labels),
                HandleRedraw(polarAreaChart, GetPolarAreaChartDataset, PolarChartData.Labels)
            );

            await base.OnAfterRenderAsync(firstRender);

        }

        async Task HandleRedraw<TDataSet, TItem, TOptions, TModel>(Blazorise.Charts.BaseChart<TDataSet, TItem, TOptions, TModel> chart, Func<TDataSet> getDataSet, IEnumerable<string> labels)
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

        LineChartDataset<double> GetLineChartDataset()
        {
            return new LineChartDataset<double>
            {
                Label = LineChartData.DisplayLabel,
                Data = LineChartData.Data,
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                Fill = true,
                PointRadius = 3,
                BorderWidth = 1,
                PointBorderColor = Enumerable.Repeat(borderColors.First(), 6).ToList()
            };
        }

        BarChartDataset<double> GetBarChartDataset()
        {
            return new BarChartDataset<double>
            {
                Label = BarChartData.DisplayLabel,
                Data = BarChartData.Data,
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                BorderWidth = 1
            };
        }

        PieChartDataset<double> GetPieChartDataset()
        {
            return new PieChartDataset<double>

            {
                Label = PieChartData.DisplayLabel,
                Data = PieChartData.Data,
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                BorderWidth = 1
            };
        }


        PolarAreaChartDataset<double> GetPolarAreaChartDataset()
        {
            return new PolarAreaChartDataset<double>
            {
                Label = PolarChartData.DisplayLabel,
                Data = PolarChartData.Data,
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                BorderWidth = 1
            };
        }

    }
}
