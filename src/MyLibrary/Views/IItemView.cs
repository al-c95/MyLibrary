using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;

namespace MyLibrary.Views
{
    public interface IItemView
    {
        int SelectedItemId { get; set; }
        Item SelectedItem { get; set; }
        DataTable DisplayedItems { get; set; }
        int CategoryDropDownSelectedIndex { get; set; }
        string StatusBarText { get; set; }
        string TitleFilterText { get; set; }

        event EventHandler ItemSelectionChanged;
        event EventHandler CategorySelectionChanged;
        event EventHandler FiltersUpdated;
        event EventHandler ApplyFilterButtonClicked;
        event EventHandler DeleteButtonClicked;
    }
}
