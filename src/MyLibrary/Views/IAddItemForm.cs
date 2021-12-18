using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Views
{
    public interface IAddItemForm
    {
        string TitleFieldText { get; set; }
        string NotesFieldText { get; set; }
        string ImageFilePathFieldText { get; set; }

        IEnumerable<string> SelectedTags { get; }

        bool SaveButtonEnabled { get; set; }
        bool CancelButtonEnabled { get; set; }

        void PopulateTagsList(IEnumerable<string> tagNames);
        void CloseDialog();
        void ShowItemAlreadyExistsDialog(string title);
        void ItemAddedFinished();

        void ShowErrorDialog(string title, string message);

        event EventHandler InputFieldsUpdated;
        event EventHandler SaveButtonClicked;
        event EventHandler ItemAdded;
    }
}
