using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Views
{
    public interface ISearchByIsbn
    {
        string IsbnFieldText { get; set; }
        bool ScanModeChecked { get; set; }
        bool SearchButtonEnabled { get; set; }
        bool CancelButtonEnabled { get; set; }
        string StatusLabelText { get; set; }

        event EventHandler IsbnFieldTextChanged;
        event EventHandler SearchButtonClicked;

        void ShowCouldNotFindBookDialog(string isbn);

        Task ClickSearchButton();
    }
}
