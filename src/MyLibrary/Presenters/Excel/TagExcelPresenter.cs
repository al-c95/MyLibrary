//MIT License

//Copyright (c) 2021

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
    public class TagExcelPresenter : ExcelPresenterBase
    {
        protected readonly ITagService _tagService;

        public TagExcelPresenter(ITagService tagService, IExcelFile file, Views.IExportDialog dialog, Views.Excel.Excel excel)
            :base("Tag", file, dialog, excel)
        {
            this._tagService = tagService;
            WriteHeaders();
        }

        public TagExcelPresenter(Views.IExportDialog dialog, Views.Excel.Excel excel)
            : base("Tag", new ExcelFile(), dialog, excel)
        {
            this._tagService = new TagService();
            WriteHeaders();
        }

        protected override void WriteHeaders()
        {
            WriteHeaderCell("B", "Tag");
        }

        protected async override Task RenderExcel(IProgress<int> numberExported, CancellationToken token)
        {
            var allTags = await this._tagService.GetAll();

            await Task.Run(() =>
            {
                int count = 0;
                foreach (var tag in allTags)
                {
                    // check for cancellation
                    if (token.IsCancellationRequested)
                    {
                        throw new OperationCanceledException();
                    }

                    WriteEntityRow(new object[]
                    {
                        tag.Id,
                        tag.Name
                    });

                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            AutoFitColumn(2);

            await this._excel.SaveAsync(this._file, this._dialog.Path);
        }//RenderExcel
    }//class
}
