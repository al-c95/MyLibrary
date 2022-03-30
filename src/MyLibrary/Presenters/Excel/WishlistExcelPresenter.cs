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
    public class WishlistExcelPresenter : ExcelPresenterBase
    {
        protected readonly IWishlistService _wishlistService;
        protected readonly WishlistExcel _excel;

        public WishlistExcelPresenter(IWishlistService wishlistService, WishlistExcel excel)
        {
            this._wishlistService = wishlistService;
            this._excel = excel;
        }

        public WishlistExcelPresenter(IExcelFile file)
        {
            this._wishlistService = new WishlistService();
            this._excel = new WishlistExcel(file);
        }

        public async override Task RenderExcel(IProgress<int> numberExported)
        {
            var allItems = await this._wishlistService.GetAll();

            await Task.Run(() =>
            {
                int count = 0;
                foreach (var item in allItems)
                {
                    this._excel.WriteEntity(item);
                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            this._excel.AutofitColumn(3);
            this._excel.WrapText(4);
            this._excel.SetColumnWidth(4, 50);

            await this._excel.SaveAsync();
        }
    }
}
