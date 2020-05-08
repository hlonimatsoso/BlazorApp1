using Microsoft.Extensions.Options;
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
        IWorkbook workbook;

        public List<PromoData> PromoData { get; set; }

        public List<string> Sheets
        {
            get
            {
                List<string> result = new List<string>();
                if (workbook != null)
                    for (int i = 0; i < workbook.NumberOfSheets; i++)
                        result.Add(workbook.GetSheetAt(i).SheetName);
                return result;
            }
        }



        //public PromoChartData PerDay { get { return PromoData.GetPerStoreMetrics(); } }

        public ExcelService(IOptions<Settings> settings)
        {
            //_reader = reader;
            SetPromoData();
        }

        public List<PromoData> SetPromoData(string path = "file1.xlsx")
        {
            List<PromoData> result = new List<PromoData> { };
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }

            result = SetCurrentSheet();
            PromoData = result;
            return result;
        }

        public List<PromoData> SetCurrentSheet(string sheetname = null)
        {
            List<PromoData> result = new List<PromoData> { };
            ISheet sheet;
            if (sheetname == null)
                sheet = workbook.GetSheetAt(0);
            else
                sheet = workbook.GetSheet(sheetname);

            if (sheet.LastRowNum > 0)
                for (int row = 0; row <= sheet.LastRowNum; row++)
                {
                    var currentRow = sheet.GetRow(row);
                    if (currentRow.Cells[0].StringCellValue == "Date")
                        continue; // Skip headings

                    if (currentRow != null && currentRow.Cells.Count > 1) //null is when the row only contains empty cells 
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

            PromoData = result;
            return result;
        }

    }
}
