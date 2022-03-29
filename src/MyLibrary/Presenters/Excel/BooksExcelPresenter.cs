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
        protected readonly BooksExcel _view;

        public BookExcelPresenter(IBookService bookService, BooksExcel view)
        {
            this._bookService = bookService;
            this._view = view;
        }

        public BookExcelPresenter(IExcelFile file)
        {
            this._bookService = new BookService();
            this._view = new BooksExcel(file);
        }

        public async override Task WriteEntities(IProgress<int> numberExported)
        {
            var allItems = await this._bookService.GetAll();

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