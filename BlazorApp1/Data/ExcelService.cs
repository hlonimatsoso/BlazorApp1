using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlazorApp1.Data
{

    public interface IExcelService
    {


    }

    public class ExcelService
    {
       // private readonly ExcelReader _reader = null;

        public List<PromoData> PromoData { get; set; }

        public PromoChartData PerDay { get { return PromoData.GetPerStoreMetrics(); } }
        
        public ExcelService()
        {
            //_reader = reader;
            PromoData = SetPromoData();
        }

        private List<PromoData> SetPromoData(string path = "file1.xlsx")
        {
            List<PromoData> result = new List<PromoData> { };
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            IWorkbook workbook;
            
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }

            ISheet sheet = workbook.GetSheet("promo1");
            
            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                var currentRow = sheet.GetRow(row);
                if (currentRow.Cells[0].StringCellValue == "Date")
                    continue; // Skip headings

                if (currentRow != null) //null is when the row only contains empty cells 
                {
                    result.Add(new Data.PromoData
                    {
                        Date = DateTime.Parse($"{currentRow.GetCell(0)}"),
                        Entry = currentRow.GetCell(1).ToString(),
                        Time = currentRow.GetCell(2).ToString(),
                        Store = currentRow.GetCell(3).ToString(),
                        GeoLocation = currentRow.GetCell(4).ToString(),
                        NumberOfItemsBought = int.Parse(currentRow.GetCell(5).ToString()),
                        NumberOfEntries = int.Parse(currentRow.GetCell(6).ToString())
                    });
                }
            }

            return result;
        }
    }
}
