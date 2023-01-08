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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary.Models.BusinessLogic
{
    public class BookService : IBookService
    {
        protected IUnitOfWorkProvider _uowProvider;
        protected IBookRepositoryProvider _repoProvider;
        protected IPublisherRepositoryProvider _publisherRepoProvider;
        protected IAuthorRepositoryProvider _authorRepoProvider;
        protected ITagRepositoryServiceProvider _tagRepoProvider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BookService() 
        {
            this._uowProvider = new UnitOfWorkProvider();
            this._repoProvider = new BookRepositoryProvider();
            this._publisherRepoProvider = new PublisherRepositoryProvider();
            this._authorRepoProvider = new AuthorRepositoryProvider();
            this._tagRepoProvider = new TagRepositoryServiceProvider();
        }

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        public BookService(IUnitOfWorkProvider uowProvider,
            IBookRepositoryProvider repoProvider, IPublisherRepositoryProvider publisherRepoProvider, 
            IAuthorRepositoryProvider authorRepoProvider, ITagRepositoryServiceProvider tagRepoProvider)
        {
            this._uowProvider = uowProvider;
            this._repoProvider = repoProvider;
            this._publisherRepoProvider = publisherRepoProvider;
            this._authorRepoProvider = authorRepoProvider;
            this._tagRepoProvider = tagRepoProvider;
        }

        /// <summary>
        /// Gets all books in the database. Does not include images.
        /// </summary>
        /// <returns></returns>
        public async virtual Task<IEnumerable<Book>> GetAllAsync()
        {
            IEnumerable<Book> allBooks = null;
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository repo = this._repoProvider.Get(uow);
                allBooks = repo.ReadAll();
                uow.Dispose();
            });

            return allBooks;
        }

        /// <summary>
        /// Gets a single book object from the database, by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Book> GetByIdAsync(int id)
        {
            Book book = null;
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository repo = this._repoProvider.Get(uow);
                book = repo.GetById(id);
                uow.Dispose();
            });

            return book;
        }

        public async Task<int> GetIdByTitleAsync(string title)
        {
            int id = 0;
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository repo = this._repoProvider.Get(uow);
                id = repo.GetIdByTitle(title);
                uow.Dispose();
            });

            return id;
        }

        public async Task<Boolean> ExistsWithIdAsync(int id)
        {
            var allBooks = await GetAllAsync();
            return allBooks.Any(b => b.Id == id);
        }

        public async Task<Boolean> ExistsWithTitleAsync(string title)
        {
            bool exists = false;
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository repo = this._repoProvider.Get(uow);

                exists = (repo.GetTitles().Any(t => t.Equals(title)));
                uow.Dispose();
            });

            return exists;
        }

        public async Task<Boolean> ExistsWithLongTitleAsync(string longTitle)
        {
            bool exists = false;
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository repo = this._repoProvider.Get(uow);

                exists = (repo.GetLongTitles().Any(t => t.Equals(longTitle)));
                uow.Dispose();
            });

            return exists;
        }

        /// <summary>
        /// Checks if a book exists with ISBN10 or ISBN13 as given.
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public async Task<Boolean> ExistsWithIsbnAsync(string isbn)
        {
            bool exists = false;
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository repo = this._repoProvider.Get(uow);
                exists = (repo.GetIsbns().Any(i => i.Equals(isbn)) || (repo.GetIsbn13s().Any(i => i.Equals(isbn))));
                uow.Dispose();
            });

            return exists;
        }

        public async Task AddAsync(Book book)
        {
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository bookRepo = this._repoProvider.Get(uow);
                IPublisherRepository publisherRepo = this._publisherRepoProvider.Get(uow);
                ITagRepository tagRepo = this._tagRepoProvider.Get(uow);
                IAuthorRepository authorRepo = this._authorRepoProvider.Get(uow);
                uow.Begin();

                // handle publisher
                int publisherId = 0;
                if (publisherRepo.ExistsWithName(book.Publisher.Name))
                {
                    publisherId = publisherRepo.GetIdByName(book.Publisher.Name);
                }
                else
                {
                    publisherRepo.Create(book.Publisher);
                    publisherId = publisherRepo.GetIdByName(book.Publisher.Name);
                }
                book.Publisher.Id = publisherId;

                bookRepo.Create(book);

                // handle tags
                List<int> tagIds = new List<int>();
                foreach (var tag in book.Tags)
                {
                    if (tagRepo.ExistsWithName(tag.Name))
                    {
                        int tagId = tagRepo.GetIdByName(tag.Name);
                        tagIds.Add(tagId);
                    }
                    else
                    {
                        tagRepo.Create(tag);
                        int tagId = tagRepo.GetIdByName(tag.Name);
                        tagIds.Add(tagId);
                    }
                }
                int bookId = bookRepo.GetIdByTitle(book.Title);
                foreach (int tagId in tagIds)
                {
                    tagRepo.LinkBook(bookId, tagId);
                }

                // handle authors
                List<int> authorIds = new List<int>();
                foreach (var author in book.Authors)
                {
                    if (authorRepo.AuthorExists(author.FirstName, author.LastName))
                    {
                        int authorId = authorRepo.GetIdByName(author.FirstName, author.LastName);
                        authorIds.Add(authorId);
                    }
                    else
                    {
                        authorRepo.Create(author);
                        int authorId = authorRepo.GetIdByName(author.FirstName, author.LastName);
                        authorIds.Add(authorId);
                    }
                }
                foreach (int authorId in authorIds)
                {
                    authorRepo.LinkBook(bookId, authorId);
                }

                uow.Commit();
            });
        }

        public async Task<bool> AddIfNotExistsAsync(Book book)
        {
            if (await ExistsWithTitleAsync(book.Title))
            {
                return false;
            }
            else
            {
                await AddAsync(book);

                return true;
            }
        }

        public async Task UpdateAsync(Book book, bool includeImage)
        {
            await Task.Run(() =>
            {

                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository repo = this._repoProvider.Get(uow);
                uow.Begin();

                repo.Update(book, includeImage);

                uow.Commit();
            });
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository repo = this._repoProvider.Get(uow);
                uow.Begin();

                repo.DeleteById(id);

                uow.Commit();
            });
        }

        public async Task UpdateTagsAsync(ItemTagsDto dto)
        {
            await Task.Run(() =>
            {
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository bookRepo = this._repoProvider.Get(uow);
                ITagRepository tagRepo = this._tagRepoProvider.Get(uow);
                uow.Begin();

                foreach (var tag in dto.TagsToAdd)
                {
                    if (tagRepo.ExistsWithName(tag))
                    {
                        int tagId = tagRepo.GetIdByName(tag);
                        tagRepo.LinkBook(dto.Id, tagId);
                    }
                    else
                    {
                        tagRepo.Create(new Tag { Name = tag });
                        int tagId = tagRepo.GetIdByName(tag);
                        tagRepo.LinkBook(dto.Id, tagId);
                    }
                }

                foreach (var tag in dto.TagsToRemove)
                {
                    if (tagRepo.ExistsWithName(tag))
                    {
                        int tagId = tagRepo.GetIdByName(tag);
                        tagRepo.UnlinkBook(dto.Id, tagId);
                    }
                }

                uow.Commit();
            });
        }
    }//BookService
}