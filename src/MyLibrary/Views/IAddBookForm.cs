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

namespace MyLibrary.Views
{
    public interface IAddBookForm : IAddItemForm
    {
        string LongTitleFieldText { get; set; }
        string IsbnFieldText { get; set; }
        string Isbn13FieldText { get; set; }
        string DeweyDecimalFieldText { get; set; }
        string FormatFieldText { get; set; }
        string LanguageFieldText { get; set; }
        string DatePublishedFieldText { get; set; }
        string PlaceOfPublicationFieldText { get; set; }
        string EditionFieldText { get; set; }
        string PagesFieldText { get; set; }
        string DimensionsFieldText { get; set; }
        string OverviewFieldText { get; set; }
        string MsrpFieldText { get; set; }
        string ExcerptFieldText { get; set; }
        string SynopsysFieldText { get; set; }

        string NewAuthorFirstNameFieldText { get; set; }
        string NewAuthorLastNameFieldText { get; set; }
        bool AddNewAuthorButtonEnabled { get; set; }

        string NewPublisherFieldText { get; set; }
        bool AddNewPublisherButtonEnabled { get; set; }

        IEnumerable<string> SelectedAuthors { get; }
        string SelectedPublisher { get; }

        void SetAuthor(Author author, bool selected);
        void SetPublisher(Publisher publisher, bool selected);

        void PopulateAuthorList(IEnumerable<string> names);
        void PopulatePublisherList(IEnumerable<string> publisherNames);

        void ShowIsbnAlreadyExistsDialog(string isbn);

        void ShowAsDialog();

        event EventHandler NewAuthorFieldsUpdated;
        event EventHandler NewPublisherFieldUpdated;
    }
}
