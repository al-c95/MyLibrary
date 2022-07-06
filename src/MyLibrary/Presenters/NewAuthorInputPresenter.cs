//MIT License

//Copyright (c) 2022

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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyLibrary.Views;
using MyLibrary.Models.Entities;

namespace MyLibrary.Presenters
{
    public class NewAuthorInputPresenter
    {
        private INewAuthor _view;

        public NewAuthorInputPresenter(INewAuthor view)
        {
            this._view = view;

            this._view.OkButtonEnabled = false;

            // register view event handlers
            this._view.InputChanged += InputChanged;
        }

        public void InputChanged(object sender, EventArgs e)
        {
            bool sane = true;
            string firstName = this._view.FirstNameEntry;
            string lastName = this._view.LastNameEntry;
            sane = sane && (Regex.IsMatch(firstName, Author.NAME_PATTERN) || Regex.IsMatch(firstName, Author.WITH_MIDDLE_NAME_PATTERN));
            sane = sane && Regex.IsMatch(lastName, Author.NAME_PATTERN);

            this._view.OkButtonEnabled = sane;
        }
    }//class
}
