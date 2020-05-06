using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class ExcelDatasourceBase : ComponentBase
    {
        protected List<string> _folderPaths;

        protected List<string> _files;

        protected string _selectedFolder;

        protected string _selectedFile { get; set; }

        protected List<string> Sheets { get { return ExcelService.Sheets; } }

        [Inject] public ExcelService ExcelService { get; set; }

        [Inject] IOptions<Settings> Settings { get; set; }


        [Parameter] public List<PromoData> PromoDataList { get { return ExcelService.PromoData; } set { } }

        [Parameter] public EventCallback<List<PromoData>> PromoDataListChanged { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _folderPaths = Settings.Value.Paths.ToList();
            _selectedFolder = _folderPaths.First();
            _files = _selectedFolder.GetExceleFilesInPath().ToList();

            _selectedFile = _files.First();

            ExcelService.SetPromoData(_selectedFile);

            await PromoDataListChanged.InvokeAsync(PromoDataList);

            await base.OnInitializedAsync();
        }

        protected void OnSelectedFileChanged(string value)
        {
            _selectedFile = value;

            ExcelService.SetPromoData(_selectedFile);

            PromoDataListChanged.InvokeAsync(PromoDataList).Wait();
        }
    }
}
