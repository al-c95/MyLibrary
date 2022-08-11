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
using System.Threading.Tasks;
using MyLibrary.Models.BusinessLogic;
using MyLibrary.Models.Entities;
using MyLibrary.Views;

namespace MyLibrary.Presenters
{
    public class StatsPresenter
    {
        private IShowStats _view;

        private IBookService _bookService;
        private IMediaItemService _mediaItemService;
        private ITagService _tagService;
        private IPublisherService _publisherService;
        private IAuthorService _authorService;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="bookRepository"></param>
        /// <param name="mediaItemRepository"></param>
        /// <param name="tagRepository"></param>
        /// <param name="publisherRepository"></param>
        /// <param name="authorRepository"></param>
        public StatsPresenter(IShowStats view,
            IBookService bookService, IMediaItemService mediaItemService, 
            ITagService tagService, IPublisherService publisherService,
            IAuthorService authorRepository)
        {
            this._view = view;

            this._bookService = bookService;
            this._mediaItemService = mediaItemService;
            this._tagService = tagService;
            this._publisherService = publisherService;
            this._authorService = authorRepository;
        }

        public async Task ShowStats()
        {
            this._view.StatusLabelText = "Loading...";

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Books: " + (await _bookService.GetAll()).Count());
            builder.AppendLine("Publishers: " + (await _publisherService.GetAll()).Count());
            builder.AppendLine("Authors: " + (await _authorService.GetAll()).Count());
            builder.AppendLine("");
            builder.AppendLine("Media Items: " + (await _mediaItemService.GetAllAsync()).Count());
            builder.AppendLine("");
            builder.AppendLine("Tags: " + (await _tagService.GetAll()).Count());

            this._view.StatsBoxTest = builder.ToString();
            this._view.StatusLabelText = "Ready";
        }
    }//class
}
