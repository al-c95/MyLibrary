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
        protected readonly TagsExcel _view;

        public TagExcelPresenter(ITagService tagService, TagsExcel view)
        {
            this._tagService = tagService;
            this._view = view;
        }

        public TagExcelPresenter(IExcelFile file)
        {
            this._tagService = new TagService();
            this._view = new TagsExcel(file);
        }

        public async override Task WriteEntities(IProgress<int> numberExported)
        {
            var allTags = await this._tagService.GetAll();

            await Task.Run(() =>
            {
                int count = 0;
                foreach (var tag in allTags)
                {
                    this._view.WriteEntity(tag);
                    if (numberExported != null)
                        numberExported.Report(++count);
                }
            });

            await this._view.SaveAsync();
        }
    }
}
