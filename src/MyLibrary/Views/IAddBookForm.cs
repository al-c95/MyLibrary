using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        string EditionFieldText { get; set; }
        string PagesFieldText { get; set; }
        string DimensionsFieldText { get; set; }
        string OverviewFieldText { get; set; }
        string MsrpFieldText { get; set; }
        string ExcerptFieldText { get; set; }
        string SynopsysFieldText { get; set; }

        IEnumerable<string> SelectedAuthors { get; }
        string SelectedPublisher { get; }

        void PopulateAuthorList(IEnumerable<string> names);
        void PopulatePublisherList(IEnumerable<string> publisherNames);
    }
}
