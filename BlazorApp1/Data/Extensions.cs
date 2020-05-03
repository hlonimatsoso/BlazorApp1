using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public static class Extensions
    {
        public static PromoChartData GetPerStoreMetrics(this IEnumerable<PromoData> @this)
        {
            var result = new PromoChartData { DisplayLabel = "Entries Per Store" };

            var x = @this.GroupBy(p => p.Store);

            foreach (var group in x)
            {
                result.Labels.Add(group.Key);
                result.Data.Add(group.Sum(p => p.NumberOfEntries));
            }

            return result;
        }

        public static PromoChartData GetPerGeoLocationMetrics(this IEnumerable<PromoData> @this)
        {
            var result = new PromoChartData { DisplayLabel = "Entries Per Geo Location" };

            var x = @this.GroupBy(p => p.GeoLocation);

            foreach (var group in x)
            {
                result.Labels.Add(group.Key);
                result.Data.Add(group.Sum(p => p.NumberOfEntries));
            }

            return result;
        }

        public static PromoChartData GetPerEntryMetrics(this IEnumerable<PromoData> @this)
        {
            var result = new PromoChartData { DisplayLabel = "Entries Per Number" };

            var x = @this.GroupBy(p => p.Entry);

            foreach (var group in x)
            {
                result.Labels.Add(group.Key);
                result.Data.Add(group.Sum(p => p.NumberOfEntries));
            }

            return result;
        }

        public static PromoChartData GetPerDateMetrics(this IEnumerable<PromoData> @this)
        {
            var result = new PromoChartData { DisplayLabel = "Entries Per Date" };

            var x = @this.GroupBy(p => p.Date);

            foreach (var group in x)
            {
                result.Labels.Add(group.Key.ToShortDateString());
                result.Data.Add(group.Sum(p => p.NumberOfEntries));
            }

            return result;
        }
    }
}
