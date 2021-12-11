using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.DataAccessLayer;
using MyLibrary.Models.Entities;
using MyLibrary.Views;

namespace MyLibrary.Presenters
{
    public class AddMediaItemPresenter
    {
        private MediaItemRepository _mediaItemRepo;
        private TagRepository _tagRepo;

        private IAddMediaItemForm _view;

        // ctor
        public AddMediaItemPresenter(MediaItemRepository mediaItemRepository, TagRepository tagRepository, 
            IAddMediaItemForm view)
        {
            this._mediaItemRepo = mediaItemRepository;
            this._tagRepo = tagRepository;

            this._view = view;

            // subscribe to the view's events
            this._view.SaveButtonClicked += SaveButtonClicked;
            this._view.InputFieldsUpdated += InputFieldsUpdated;
        }

        #region View event handlers
        public void SaveButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void InputFieldsUpdated(object sender, EventArgs e)
        {
            bool sane = true;
            sane = sane && !string.IsNullOrWhiteSpace(this._view.TitleFieldText);
            if (!string.IsNullOrWhiteSpace(this._view.NumberFieldText))
            {
                int number;
                sane = sane && int.TryParse(this._view.NumberFieldText, out number);
            }
            if (!string.IsNullOrWhiteSpace(this._view.RunningTimeFieldEntry))
            {
                int runningTime;
                sane = sane && int.TryParse(this._view.RunningTimeFieldEntry, out runningTime);
            }
            if (!string.IsNullOrWhiteSpace(this._view.YearFieldEntry))
            {
                int year;
                sane = sane && int.TryParse(this._view.YearFieldEntry, out year);
            }

            this._view.SaveButtonEnabled = sane;
        }
        #endregion
    }//class
}
