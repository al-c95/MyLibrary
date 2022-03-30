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
    public class BookExcelPresenter : ExcelPresenterBase
    {
        protected readonly IBookService _bookService;
        protected readonly BooksExcel _excel;

        public BookExcelPresenter(IBookService bookService, BooksExcel excel)
        {
            this._bookService = bookService;
            this._excel = excel;
        }

        public BookExcelPresenter(IExcelFile file)
        {
            this._bookService = new BookService();
            this._excel = new BooksExcel(file);
        }

        public async override Task RenderExcel(IProgress<int> numberExported)
        {
            // write data
            var allItems = await this._bookService.GetAll();
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

            // autofit some columns
            int col = 2;
            this._excel.AutofitColumn(col);
            while (col <= 15)
            {
                this._excel.AutofitColumn(col);
                col++;
            }

            // wrap text and set column width for some columns
            // Dimensions, Overview, Excerpt, Synopsys, Notes
            col = 16;
            while (col <= 19)
            {
                this._excel.WrapText(col);
                this._excel.SetColumnWidth(col, 30);

                col++;
            }

            await this._excel.SaveAsync();
        }
    }
}