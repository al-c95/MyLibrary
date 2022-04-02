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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Views.Excel;

namespace MyLibrary.Presenters.Excel
{
    public class PublisherExcelPresenter : ExcelPresenterBase
    {
        protected readonly IPublisherService _publisherService;

        public PublisherExcelPresenter(IPublisherService publisherService, IExcelFile file, Views.IExportDialog dialog)
            :base("Publisher",file,dialog)
        {
            this._publisherService = publisherService;
            WriteHeaders();
        }

        public PublisherExcelPresenter(Views.IExportDialog dialog)
            : base("Publisher", new ExcelFile(), dialog)
        {
            this._publisherService = new PublisherService();
            WriteHeaders();
        }

        protected override void WriteHeaders()
        {
            WriteHeaderCell("B", "Publisher");
        }

        public async override Task RenderExcel(IProgress<int> numberExported)
        {
            var allPublishers = await this._publisherService.GetAll();

            await Task.Run(() =>
            {
                int count = 0;
                foreach (var publisher in allPublishers)
                {
                    WriteEntityRow(new object[]
                    {
                        publisher.Id,
                        publisher.Name
                    });
                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            AutoFitColumn(2);

            await this._excel.SaveAsync(this._file, this._dialog.Path);
        }
    }
}