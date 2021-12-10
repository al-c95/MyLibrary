using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Views
{
    public interface IAddMediaItemForm
    {
        int CategoryDropDownSelectedIndex { get; set; }
        string TitleFieldText { get; set; }
        string NumberFieldText { get; set; }
        int? RunningTimeFieldEntry { get; set; }
        int YearFieldEntry { get; set; }
        string NotesFieldText { get; set; }
        IEnumerable<string> SelectedFilterTags { get; }

        bool SaveButtonEnabled { get; set; }
        bool CancelButtonEnabled { get; set; }

        event EventHandler SaveButtonClicked;
    }
}
