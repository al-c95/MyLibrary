﻿using System;
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
        protected readonly AuthorsExcel _excel;

        public AuthorExcelPresenter(IAuthorService authorService, AuthorsExcel excel)
        {
            this._authorService = authorService;
            this._excel = excel;
        }

        public AuthorExcelPresenter(IExcelFile file)
        {
            this._authorService = new AuthorService();
            this._excel = new AuthorsExcel(file);
        }

        public async override Task RenderExcel(IProgress<int> numberExported)
        {
            var allAuthors = await this._authorService.GetAll();

            await Task.Run(() =>
            {
                int count = 0;
                foreach (var author in allAuthors)
                {
                    this._excel.WriteEntity(author);
                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            this._excel.AutofitColumn(2);
            this._excel.AutofitColumn(3);

            await this._excel.SaveAsync();
        }
    }
}