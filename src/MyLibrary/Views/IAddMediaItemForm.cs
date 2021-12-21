using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Views
{
    public interface IAddMediaItemForm : IAddItemForm
    {
        string SelectedCategory { get; }
        int SelectedCategoryIndex { get; set; }
        string NumberFieldText { get; set; }
        string RunningTimeFieldEntry { get; set; }
        string YearFieldEntry { get; set; }
    }
}
