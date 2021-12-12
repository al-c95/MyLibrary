using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Models.Entities;
using MyLibrary.Models.Entities.Builders;
using MyLibrary.Views;

namespace MyLibrary.Presenters
{
    public class AddBookPresenter
    {
        private BookRepository _bookRepo;
        private TagRepository _tagRepo;

        private IAddBookForm _view;

        public AddBookPresenter(BookRepository bookRepository, TagRepository tagRepository,
            IAddBookForm view)
        {
            this._bookRepo = bookRepository;
            this._tagRepo = tagRepository;

            this._view = view;

            // subscribe to the view's events
            this._view.SaveButtonClicked += SaveButtonClicked;
            this._view.InputFieldsUpdated += InputFieldsUpdated;
        }

        public async Task PopulateTagsList()
        {
            var allTags = await this._tagRepo.GetAll();
            List<string> tagNames = new List<string>();
            foreach (var tag in allTags)
                tagNames.Add(tag.Name);

            this._view.PopulateTagsList(tagNames);
        }

        public async Task PopulateAuthorList()
        {
            throw new NotImplementedException();
        }

        public async Task PopulatePublisherList()
        {
            throw new NotImplementedException();
        }

        #region View event handlers
        public async void SaveButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void InputFieldsUpdated(object sender, EventArgs e)
        {
            bool sane = true;
            sane = sane && !string.IsNullOrWhiteSpace(this._view.TitleFieldText);
            sane = sane && !string.IsNullOrWhiteSpace(this._view.LongTitleFieldText);
            sane = sane && !string.IsNullOrWhiteSpace(this._view.LanguageFieldText);
            sane = sane && !string.IsNullOrWhiteSpace(this._view.PagesFieldText);
            int pages;
            sane = sane && (int.TryParse(this._view.PagesFieldText, out pages));

            this._view.SaveButtonEnabled = sane;
        }
        #endregion
    }//class
}
