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

        IEnumerable<string> SelectedFilterTags { get; }

        bool SaveButtonEnabled { get; set; }
        bool CancelButtonEnabled { get; set; }

        void PopulateTagsList(IEnumerable<string> tagNames);
        void CloseDialog();
        void ShowItemAlreadyExistsDialog(string title);
        void ItemAddedFinished();

        event EventHandler InputFieldsUpdated;
        event EventHandler SaveButtonClicked;
        event EventHandler ItemAdded;
    }
}
