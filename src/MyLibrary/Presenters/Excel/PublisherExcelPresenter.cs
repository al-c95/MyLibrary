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
        protected readonly PublishersExcel _excel;

        public PublisherExcelPresenter(IPublisherService publisherService, PublishersExcel excel)
        {
            this._publisherService = publisherService;
            this._excel = excel;
        }

        public PublisherExcelPresenter(IExcelFile file)
        {
            this._publisherService = new PublisherService();
            this._excel = new PublishersExcel(file);
        }

        public async override Task RenderExcel(IProgress<int> numberExported)
        {
            var allPublishers = await this._publisherService.GetAll();

            await Task.Run(() =>
            {
                int count = 0;
                foreach (var publisher in allPublishers)
                {
                    this._excel.WriteEntity(publisher);
                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            this._excel.AutofitColumn(2);

            await this._excel.SaveAsync();
        }
    }
}