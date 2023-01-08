//MIT License

//Copyright (c) 2021-2023

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
using MyLibrary.Views;

namespace MyLibrary.Presenters
{
    public class NewTagOrPublisherInputPresenter
    {
        private INewTagOrPublisher _view;

        private InputBoxMode _mode;

        public NewTagOrPublisherInputPresenter(INewTagOrPublisher view, InputBoxMode mode)
        {
            this._view = view;

            this._view.OkButtonEnabled = false;

            this._mode = mode;
            switch (mode)
            {
                case InputBoxMode.Publisher:
                    this._view.Label = "New Publisher:";
                    this._view.DialogTitle = "New Publisher";
                    break;
                case InputBoxMode.Tag:
                    this._view.Label = "New Tag:";
                    this._view.DialogTitle = "New Tag";
                    break;
            }

            // register view event handlers
            this._view.InputChanged += InputChanged;
        }//ctor

        #region view event handlers
        public void InputChanged(object sender, EventArgs args)
        {
            // validate input
            if (string.IsNullOrWhiteSpace(this._view.Entry))
            {
                this._view.OkButtonEnabled = false;
            }
            else
            {
                switch (this._mode)
                {
                    case InputBoxMode.Tag:
                        this._view.OkButtonEnabled = (Models.Entities.Tag.Validate(this._view.Entry));
                        break;
                    case InputBoxMode.Publisher:
                        this._view.OkButtonEnabled = (Models.Entities.Publisher.ValidateName(this._view.Entry));
                        break;
                }
            }
        }//InputChanged
        #endregion

        public enum InputBoxMode
        {
            Tag,
            Publisher
        }
    }//class
}
