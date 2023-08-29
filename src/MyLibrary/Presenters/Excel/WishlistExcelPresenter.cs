//MIT License

//Copyright (c) 2021-2023

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

using System;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Views.Excel;

namespace MyLibrary.Presenters.Excel
{
    public class WishlistExcelPresenter : ExcelPresenterBase
    {
        protected readonly IWishlistService _wishlistService;

        public WishlistExcelPresenter(IWishlistService wishlistService, IExcelFile file, Views.IExportDialog dialog, Views.Excel.Excel excel)
            :base("Wishlist item", file, dialog, excel)
        {
            this._wishlistService = wishlistService;
            WriteHeaders();
        }

        public WishlistExcelPresenter(Views.IExportDialog dialog, Views.Excel.Excel excel)
            : base("Wishlist item", new ExcelFile(), dialog, excel)
        {
            this._wishlistService = new WishlistService();
            WriteHeaders();
        }

        protected override void WriteHeaders()
        {
            WriteHeaderCell("B", "Type");
            WriteHeaderCell("C", "Title");
            WriteHeaderCell("D", "Notes");
        }

        protected async override Task RenderExcel(IProgress<int> numberExported)
        {
            var allItems = await this._wishlistService.GetAll();

            await Task.Run(() =>
            {
                int count = 0;
                foreach (var item in allItems)
                {
                    WriteEntityRow(new object[]
                    {
                        item.Id,
                        Item.GetTypeString(item.Type),
                        item.Title,
                        item.Notes
                    });

                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            AutoFitColumn(3);
            WrapText(4);
            SetColumnWidth(4, 50);

            await this._excel.SaveAsync(this._file, this._dialog.Path);
        }//RenderExcel
    }//class
}