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
        event EventHandler SearchByIsbnClicked;
        event EventHandler ShowStatsClicked;
    }
}
