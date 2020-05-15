using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public static class Extensions
    {
        public static List<ChartData<int>> GetChartData(this PromoChartData data)
        {
            List<ChartData<int>> result = new List<ChartData<int>> { };

            for (int i = 0; i < data.Data.Count; i++)
                result.Add(new ChartData<int> { Category = data.Labels[i], Value = (int)data.Data[i] });

            return result;
        }

        public static List<string> GetExceleFilesInPath(this string path)
        {
            List<string> result = Directory.GetFiles(path)
                                           .Where(x => x.EndsWith(".xls") || x.EndsWith(".xlsx"))
                                           .ToList();

            return result;
        }

        public static PromoChartData GetPerStoreMetrics(this IEnumerable<PromoData> @this)
        {
            var result = new PromoChartData { DisplayLabel = "Entries Per Store" };

            if (@this != null)
            {
                var x = @this.GroupBy(p => p.Store);

                foreach (var group in x)
                {
                    result.Labels.Add(group.Key);
                    result.Data.Add(group.Sum(p => p.NumberOfEntries));
                }
            }

            return result;
        }

        public static PromoChartData GetPerGeoLocationMetrics(this IEnumerable<PromoData> @this)
        {
            var result = new PromoChartData { DisplayLabel = "Entries Per Geo Location" };

            if (@this != null)
            {
                var x = @this.GroupBy(p => p.GeoLocation);

                foreach (var group in x)
                {
                    result.Labels.Add(group.Key);
                    result.Data.Add(group.Sum(p => p.NumberOfEntries));
                }
            }

            return result;
        }

        public static PromoChartData GetPerEntryMetrics(this IEnumerable<PromoData> @this)
        {
            var result = new PromoChartData { DisplayLabel = "Entries Per Number" };

            if (@this != null)
            {
                var x = @this.GroupBy(p => p.Entry);

                foreach (var group in x)
                {
                    result.Labels.Add(group.Key);
                    result.Data.Add(group.Sum(p => p.NumberOfEntries));
                }
            }

            return result;
        }

        public static PromoChartData GetPerDateMetrics(this IEnumerable<PromoData> @this)
        {
            var result = new PromoChartData { DisplayLabel = "Entries Per Date" };

            if (@this != null)
            {
                var x = @this.GroupBy(p => p.Date);

                foreach (var group in x)
                {
                    result.Labels.Add(group.Key.ToShortDateString());
                    result.Data.Add(group.Sum(p => p.NumberOfEntries));
                }
            }

            return result;
        }


        public static PlotyData GetPlotyDataByDate(this IEnumerable<PromoData> @this)
        {
            var result = new PlotyData();

            if (@this != null && @this?.Count() > 0)
            {
                var x = @this.GroupBy(p => p.Date);

                foreach (var group in x)
                {
                    var xTemp = group.Key.ToShortDateString();
                    var yTemp = group.Sum(p => p.NumberOfEntries);

                    result.x.Add(xTemp);
                    result.y.Add(yTemp.ToString());
                }
            }

            return result;
        }


    }
}
