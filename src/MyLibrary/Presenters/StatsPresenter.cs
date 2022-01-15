using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyLibrary.BusinessLogic.Repositories; // TODO: remove

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
        private TagRepository _tagRepo;
        private PublisherRepository _publisherRepo;
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
            TagRepository tagRepository, PublisherRepository publisherRepository,
            IAuthorService authorRepository)
        {
            this._view = view;

            this._bookService = bookService;
            this._mediaItemService = mediaItemService;
            this._tagRepo = tagRepository;
            this._publisherRepo = publisherRepository;
            this._authorService = authorRepository;
        }

        public async Task ShowStats()
        {
            this._view.StatusLabelText = "Loading...";

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Books: " + (await _bookService.GetAll()).Count());
            builder.AppendLine("Publishers: " + (await _publisherRepo.GetAll()).Count());
            builder.AppendLine("Authors: " + (await _authorService.GetAll()).Count());
            builder.AppendLine("");
            builder.AppendLine("Media Items: " + (await _mediaItemService.GetAll()).Count());
            builder.AppendLine("");
            builder.AppendLine("Tags: " + (await _tagRepo.GetAll()).Count());

            this._view.StatsBoxTest = builder.ToString();
            this._view.StatusLabelText = "Ready";
        }
    }//class
}
