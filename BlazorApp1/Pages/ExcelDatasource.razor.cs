﻿using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class ExcelDatasourceBase : ComponentBase
    {
        protected RadzenDropDown<string> _dropDown;

        protected List<string> _folderPaths;

        protected List<string> _files;

        protected string _selectedFolder;

        protected string _selectedFile { get; set; }

        protected List<string> Sheets { get { return ExcelService.Sheets; } }

        public string SelectedSheet { get; set; }

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

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            _dropDown.SelectedItem = _selectedFile;

            return base.OnAfterRenderAsync(firstRender);
        }
    }
}