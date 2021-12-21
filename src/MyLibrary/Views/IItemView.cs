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
        int SelectedItemId { get; }
        Item SelectedItem { get; set; }
        bool UpdateSelectedItemButtonEnabled { get; set; }
        bool DiscardSelectedItemChangesButtonEnabled { get; set; }
        DataTable DisplayedItems { get; set; }
        int CategoryDropDownSelectedIndex { get; set; }
        string StatusText { get; set; }
        string ItemsDisplayedText { get; set; }
        string TitleFilterText { get; set; }
        IEnumerable<string> SelectedFilterTags { get; }
        bool IsItemSelected { get; }
        bool DeleteItemButtonEnabled { get; set; }
        int NumberOfItemsSelected { get; }
        string SelectedItemDetailsBoxEntry { get; set; }

        void LoadWindow();
        void PopulateFilterTags(Dictionary<string, bool> tagNamesAndCheckedStatuses);
        void ShowErrorDialog(string title, string message);

        event EventHandler ItemSelectionChanged;
        event EventHandler CategorySelectionChanged;
        event EventHandler FiltersUpdated;
        event EventHandler ApplyFilterButtonClicked;
        event EventHandler DeleteButtonClicked;
        event EventHandler UpdateSelectedItemButtonClicked;
        event EventHandler SelectedItemModified;
        event EventHandler DiscardSelectedItemChangesButtonClicked;
        event EventHandler TagsUpdated;
        event EventHandler AddNewMediaItemClicked;
        event EventHandler AddNewBookClicked;
    }
}
