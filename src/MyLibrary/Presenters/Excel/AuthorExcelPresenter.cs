//MIT License

//Copyright (c) 2021-2022

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
using System.Threading;
using System.Threading.Tasks;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Views.Excel;

namespace MyLibrary.Presenters.Excel
{
    public class AuthorExcelPresenter : ExcelPresenterBase
    {
        protected readonly IAuthorService _authorService;

        public AuthorExcelPresenter(IAuthorService authorService, IExcelFile file, Views.IExportDialog dialog, Views.Excel.Excel excel)
            :base("Author",file,dialog,excel)
        {
            this._authorService = authorService;
            WriteHeaders();
        }

        public AuthorExcelPresenter(Views.IExportDialog dialog, Views.Excel.Excel excel)
            :base("Author",new ExcelFile(),dialog, new Views.Excel.Excel())
        {
            this._authorService = new AuthorService();
            WriteHeaders();
        }

        protected override void WriteHeaders()
        {
            WriteHeaderCell("B", "First Name");
            WriteHeaderCell("C", "Last Name");
        }

        protected async override Task RenderExcel(IProgress<int> numberExported, CancellationToken token)
        {
            var allAuthors = await this._authorService.GetAll();

            await Task.Run(() =>
            {
                int count = 0;
                foreach (var author in allAuthors)
                {
                    // check for cancellation
                    if (token.IsCancellationRequested)
                    {
                        throw new OperationCanceledException();
                    }

                    WriteEntityRow(new object[]
                    {
                        author.Id,
                        author.FirstName,
                        author.LastName
                    });

                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            AutoFitColumn(2);
            AutoFitColumn(3);

            await this._excel.SaveAsync(this._file, this._dialog.Path);
        }//RenderExcel
    }//class
}