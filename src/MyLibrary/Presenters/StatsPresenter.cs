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

using System.Text;
using System.Threading.Tasks;
using MyLibrary.DataAccessLayer;
using MyLibrary.Views;

namespace MyLibrary.Presenters
{
    public class StatsPresenter
    {
        private IShowStats _view;

        private IStatsService _statsService;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="bookRepository"></param>
        /// <param name="mediaItemRepository"></param>
        /// <param name="tagRepository"></param>
        /// <param name="publisherRepository"></param>
        /// <param name="authorRepository"></param>
        public StatsPresenter(IShowStats view, IStatsService statsService)
        {
            this._view = view;

            this._statsService = statsService;
        }

        public async Task LoadDataAsync()
        {
            this._view.StatusLabelText = "Loading...";

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Books: " + (await _statsService.GetBooksCountAsync()));
            builder.AppendLine("Publishers: " + (await _statsService.GetPublishersCountAsync()));
            builder.AppendLine("Authors: " + (await _statsService.GetAuthorsCountAsync()));
            builder.AppendLine("");
            builder.AppendLine("Media Items: " + (await _statsService.GetMediaItemsCountAsync()));
            builder.AppendLine("");
            builder.AppendLine("Tags: " + (await _statsService.GetTagsCountAsync()));

            this._view.StatsBoxTest = builder.ToString();
            this._view.StatusLabelText = "Ready";
        }
    }//class
}