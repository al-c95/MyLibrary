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
    public class AuthorExcelPresenter : ExcelPresenterBase
    {
        protected readonly IAuthorService _authorService;
        protected readonly AuthorsExcel _view;

        public AuthorExcelPresenter(IAuthorService authorService, AuthorsExcel view)
        {
            this._authorService = authorService;
            this._view = view;
        }

        public AuthorExcelPresenter(IExcelFile file)
        {
            this._authorService = new AuthorService();
            this._view = new AuthorsExcel(file);
        }

        public async override Task RenderExcel(IProgress<int> numberExported)
        {
            var allAuthors = await this._authorService.GetAll();

            await Task.Run(() =>
            {
                int count = 0;
                foreach (var author in allAuthors)
                {
                    this._view.WriteEntity(author);
                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            this._view.AutofitColumn(2);
            this._view.AutofitColumn(3);

            await this._view.SaveAsync();
        }
    }
}