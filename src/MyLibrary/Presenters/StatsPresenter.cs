﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.BusinessLogic.Repositories;
using MyLibrary.Views;

namespace MyLibrary.Presenters
{
    public class StatsPresenter
    {
        private IShowStats _view;

        private BookRepository _bookRepo;
        private MediaItemRepository _mediaItemRepo;
        private TagRepository _tagRepo;
        private PublisherRepository _publisherRepo;
        private AuthorRepository _authorRepo;

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
            BookRepository bookRepository, MediaItemRepository mediaItemRepository, 
            TagRepository tagRepository, PublisherRepository publisherRepository,
            AuthorRepository authorRepository)
        {
            this._view = view;

            this._bookRepo = bookRepository;
            this._mediaItemRepo = mediaItemRepository;
            this._tagRepo = tagRepository;
            this._publisherRepo = publisherRepository;
            this._authorRepo = authorRepository;
        }

        public async Task ShowStats()
        {
            this._view.StatusLabelText = "Loading...";

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Books: " + (await _bookRepo.GetAll()).Count());
            builder.AppendLine("Publishers: " + (await _publisherRepo.GetAll()).Count());
            builder.AppendLine("Authors: " + (await _authorRepo.GetAll()).Count());
            builder.AppendLine("");
            builder.AppendLine("Media Items: " + (await _mediaItemRepo.GetAll()).Count());
            builder.AppendLine("");
            builder.AppendLine("Tags: " + (await _tagRepo.GetAll()).Count());

            this._view.StatsBoxTest = builder.ToString();
            this._view.StatusLabelText = "Ready";
        }
    }//class
}
