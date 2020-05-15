using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class ExcelDatasourceBase : ComponentBase
    {
        protected RadzenDropDown<string> _dropDown;

        protected RadzenRadioButtonList<string> _selectedSheetButtons;

        protected List<string> _folderPaths;

        protected List<string> _files;

        protected string _selectedFolder;

        protected bool _isSheetSelectionVisible;


        protected string _selectedFile { get; set; }

        protected List<string> Sheets { get { return ExcelService.Sheets; } }

        public string _selectedSheet { get; set; }

        [Parameter] public EventCallback<string> SelectedSheetChanged { get; set; }

        [Inject] public ExcelService ExcelService { get; set; }

        [Inject] IOptions<Settings> Settings { get; set; }

        protected string SelectedFileName { get { return new FileInfo(_selectedFile).Name; } }

        [Parameter] public List<PromoData> PromoDataList { get { return ExcelService.PromoData; } set { } }

        [Parameter] public EventCallback<List<PromoData>> PromoDataListChanged { get; set; }

        [Inject] public IJSRuntime jsRuntime { get; set; }


        protected async override Task OnInitializedAsync()
        {
            _folderPaths = Settings.Value.Paths.ToList();
            _selectedFolder = Path.Combine(Directory.GetCurrentDirectory(), _folderPaths.First());

            _files = _selectedFolder.GetExceleFilesInPath().ToList();

            _selectedFile = _files.First();

            ExcelService.SetPromoData(_selectedFile);

            ExcelService.SetCurrentSheet(Settings.Value.DefaultSheet);

            await PromoDataListChanged.InvokeAsync(PromoDataList);

            await base.OnInitializedAsync();
        }

        protected void OnSelectedSheetChanged(string value)
        {
            _selectedSheet = value;

            ExcelService.SetCurrentSheet(_selectedSheet);

            SelectedSheetChanged.InvokeAsync(value).Wait();

            PromoDataListChanged.InvokeAsync(PromoDataList).Wait();

            jsRuntime.InvokeVoidAsync("fixChartLabels");

        }

        protected void OnSelectedFileChanged(string value)
        {
            _selectedFile = value;

            ExcelService.SetPromoData(_selectedFile);

            PromoDataListChanged.InvokeAsync(PromoDataList).Wait();

            jsRuntime.InvokeVoidAsync("fixChartLabels");

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _dropDown.SelectedItem = _selectedFile;

            await jsRuntime.InvokeVoidAsync("fixChartLabels");

            //_selectedSheetButtons.Value = _selectedSheet;

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
