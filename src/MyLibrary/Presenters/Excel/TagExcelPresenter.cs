using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.Views.Excel;

namespace MyLibrary.Presenters.Excel
{
    public class TagExcelPresenter : ExcelPresenterBase
    {
        protected readonly ITagService _tagService;
        protected readonly TagsExcel _excel;

        public TagExcelPresenter(ITagService tagService, TagsExcel excel)
        {
            this._tagService = tagService;
            this._excel = excel;
        }

        public TagExcelPresenter(IExcelFile file)
        {
            this._tagService = new TagService();
            this._excel = new TagsExcel(file);
        }

        public async override Task RenderExcel(IProgress<int> numberExported)
        {
            var allTags = await this._tagService.GetAll();

            await Task.Run(() =>
            {
                int count = 0;
                foreach (var tag in allTags)
                {
                    this._excel.WriteEntity(tag);
                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            this._excel.AutofitColumn(2);

            await this._excel.SaveAsync();
        }
    }
}
