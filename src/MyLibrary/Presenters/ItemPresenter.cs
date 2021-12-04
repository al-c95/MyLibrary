using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MyLibrary.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.Views;

namespace MyLibrary.Presenters
{
    public class ItemPresenter
    {
        private IBookRepository _bookRepo;
        private IMediaItemRepository _mediaItemRepo;

        private IItemView _view;

        public ItemPresenter(IBookRepository bookRepository, IMediaItemRepository mediaItemRepository,
            IItemView view)
        {
            this._bookRepo = bookRepository;
            this._mediaItemRepo = mediaItemRepository;

            this._view = view;

            // subscribe to the view's events
            this._view.CategorySelectionChanged += CategorySelectionChanged;
            this._view.ItemSelectionChanged += ItemSelectionChanged;

            this._view.CategoryDropDownSelectedIndex = 0;
        }

        public async Task DisplayBooks()
        {
            // fetch the data
            var allBooks = await this._bookRepo.GetAll();

            // create DataTable to display and assign to the view
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Title");
            dt.Columns.Add("Long Title");
            dt.Columns.Add("ISBN");
            dt.Columns.Add("Publisher");
            dt.Columns.Add("Authors");
            dt.Columns.Add("Tags");
            foreach (var book in allBooks)
            {
                dt.Rows.Add(
                    book.Id, 
                    book.Title, 
                    book.TitleLong, 
                    book.Isbn, 
                    book.Publisher.Name, 
                    book.GetAuthorList(), 
                    book.GetCommaDelimitedTags()
                    );
            }
            this._view.DisplayedItems = dt;
        }

        public async Task DisplayMediaItems()
        {
            // fetch the data
            var allItems = await this._mediaItemRepo.GetAll();

            // create DataTable to display and assign to the view
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Title");
            dt.Columns.Add("Type");
            dt.Columns.Add("Number");
            dt.Columns.Add("Run Time");
            dt.Columns.Add("Release Year");
            dt.Columns.Add("Tags");
            foreach (var item in allItems)
            {
                dt.Rows.Add(
                    item.Id,
                    item.Title,
                    item.Type,
                    item.Number,
                    item.RunningTime,
                    item.ReleaseYear,
                    item.GetCommaDelimitedTags()
                    );
            }
            this._view.DisplayedItems = dt;
        }

        public async Task DisplayMediaItems(ItemType type)
        {
            // fetch the data
            var items = await this._mediaItemRepo.GetByType(type);

            // create DataTable to display and assign to the view
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Title");
            dt.Columns.Add("Type");
            dt.Columns.Add("Number");
            dt.Columns.Add("Run Time");
            dt.Columns.Add("Release Year");
            dt.Columns.Add("Tags");
            foreach (var item in items)
            {
                dt.Rows.Add(
                    item.Id,
                    item.Title,
                    item.Type,
                    item.Number,
                    item.RunningTime,
                    item.ReleaseYear,
                    item.GetCommaDelimitedTags()
                    );
            }
            this._view.DisplayedItems = dt;
        }

        #region View event handlers
        public void ItemSelectionChanged(object sender, EventArgs e)
        {
            // TODO: display item data in the appropriate area
            throw new NotImplementedException();
        }

        public async void CategorySelectionChanged(object sender, EventArgs e)
        {
            switch (this._view.CategoryDropDownSelectedIndex)
            {
                case 0:
                    await DisplayBooks();
                    break;
                case 1:
                    await DisplayMediaItems();
                    break;
                case 2:
                    await DisplayMediaItems(ItemType.Cd);
                    break;
                case 3:
                    await DisplayMediaItems(ItemType.Dvd);
                    break;
                case 4:
                    await DisplayMediaItems(ItemType.BluRay);
                    break;
                case 5:
                    await DisplayMediaItems(ItemType.Vhs);
                    break;
                case 6:
                    await DisplayMediaItems(ItemType.Vinyl);
                    break;
                case 7:
                    await DisplayMediaItems(ItemType.Other);
                    break;
                default:
                    await DisplayMediaItems();
                    break;
            }
        }
        #endregion
    }//class
}
