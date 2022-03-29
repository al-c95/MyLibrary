using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Views.Excel;

namespace MyLibrary.Presenters.Excel
{
    public class MediaItemExcelPresenter : ExcelPresenterBase
    {
        protected readonly IMediaItemService _mediaItemService;
        protected readonly MediaItemsExcel _view;

        public MediaItemExcelPresenter(IMediaItemService tagService, MediaItemsExcel view)
        {
            this._mediaItemService = tagService;
            this._view = view;
        }

        public MediaItemExcelPresenter(IExcelFile file)
        {
            this._mediaItemService = new MediaItemService();
            this._view = new MediaItemsExcel(file);
        }

        public async override Task RenderExcel(IProgress<int> numberExported)
        {
            // write data
            var allItems = await this._mediaItemService.GetAll();
            await Task.Run(() =>
            {
                int count = 0;
                foreach (var item in allItems)
                {
                    this._view.WriteEntity(item);
                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            // autofit some columns
            this._view.AutofitColumn(2);
            this._view.AutofitColumn(4);
            this._view.AutofitColumn(5);
            this._view.AutofitColumn(6);
            this._view.AutofitColumn(7);

            // wrap text and set width for Notes column
            this._view.WrapText(8);
            this._view.SetColumnWidth(8, 30);

            await this._view.SaveAsync();
        }
    }
}