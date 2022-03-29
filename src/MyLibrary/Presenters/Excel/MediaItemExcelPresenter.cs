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

        public async override Task WriteEntities(IProgress<int> numberExported)
        {
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

            await this._view.SaveAsync();
        }
    }
}