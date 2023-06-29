//MIT License

//Copyright (c) 2021-2023

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
using System.Data;
using MyLibrary.Models.Entities;

namespace MyLibrary.Views
{
    public interface IWishlistForm
    {
        string TitleFilterText { get; set; }
        bool CassetteFilterSelected { get; set; }
        bool BookFilterSelected { get; set; }
        bool CdFilterSelected { get; set; }
        bool DvdFilterSelected { get; set; }
        bool BlurayFilterSelected { get; set; }
        bool UhdBlurayFilterSelected { get; set; }
        bool VhsFilterSelected { get; set; }
        bool VinylFilterSelected { get; set; }
        bool FloppyDiskFilterSelected { get; set; }
        bool FlashDriveFilterSelected { get; set; }
        bool OtherFilterSelected { get; set; }

        string SelectedNotes { get; set; }
        string NewNotes { get; set; }

        string StatusText { get; set; }

        void ShowItemAlreadyExistsDialog(string title);

        DataTable DisplayedItems { get; set; }
        WishlistItem SelectedItem { get; }
        WishlistItem ModifiedItem { get; }
        WishlistItem NewItem { get; }
        string NewItemTitle { get; set; }

        bool SaveSelectedButtonEnabled { get; set; }
        bool DeleteSelectedButtonEnabled { get; set; }
        bool AddToLibraryButtonEnabled { get; set; }
        bool DiscardChangesButtonEnabled { get; set; }
        bool SaveNewButtonEnabled { get; set; }

        int NumberItemsSelected { get; }

        ItemType NewItemType { get; }

        event EventHandler WindowCreated;
        event EventHandler ApplyFilters;
        event EventHandler ClearTitleFilterButtonClicked;
        event EventHandler ItemSelected;
        event EventHandler SaveSelectedClicked;
        event EventHandler DiscardChangesClicked;
        event EventHandler DeleteClicked;
        event EventHandler AddToLibraryClicked;
        event EventHandler SaveNewClicked;
        event EventHandler NewItemFieldsUpdated;
        event EventHandler SelectedItemFieldsUpdated;
    }
}