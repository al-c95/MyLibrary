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
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;
using MyLibrary.DataAccessLayer.ServiceProviders;

namespace MyLibrary
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
            using (var uow = this._uowProvider.Get())
            {
                IBookRepository repo = this._repoProvider.Get(uow);
                return await repo.ReadAllAsync();
            }
        }

        /// <summary>
        /// Gets a single book object from the database, by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Book> GetByIdAsync(int id)
        {
            using (var uow = this._uowProvider.Get())
            {
                IBookRepository repo = this._repoProvider.Get(uow);
                return await repo.GetByIdAsync(id);
            }
        }

        public async Task<int> GetIdByTitleAsync(string title)
        {
            using (var uow = this._uowProvider.Get())
            {
                IBookRepository repo = this._repoProvider.Get(uow);
                return await repo.GetIdByTitleAsync(title);
            }
        }

        public async Task<Boolean> ExistsWithIdAsync(int id)
        {
            var allBooks = await GetAllAsync();
            return allBooks.Any(b => b.Id == id);
        }

        public async Task<Boolean> ExistsWithTitleAsync(string title)
        {
            using (var uow = this._uowProvider.Get())
            {
                IBookRepository repo = this._repoProvider.Get(uow);
                return await repo.ExistsWithTitleAsync(title);
            }
        }

        public async Task<Boolean> ExistsWithLongTitleAsync(string longTitle)
        {
            using (var uow = this._uowProvider.Get())
            {
                IBookRepository repo = this._repoProvider.Get(uow);
                return await repo.ExistsWithLongTitleAsync(longTitle);
            }
        }

        /// <summary>
        /// Checks if a book exists with ISBN10 or ISBN13 as given.
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public async Task<Boolean> ExistsWithIsbnAsync(string isbn)
        {
            using (var uow = this._uowProvider.Get())
            {
                IBookRepository repo = this._repoProvider.Get(uow);
                return await repo.ExistsWithIsbnAsync(isbn) || await repo.ExistsWithIsbn13Async(isbn);
            }
        }

        public async Task<bool> ExistsWithIsbn13Async(string isbn)
        {
            using (var uow = this._uowProvider.Get())
            {
                IBookRepository repo = this._repoProvider.Get(uow);
                return await repo.ExistsWithIsbn13Async(isbn);
            }
        }

        public async Task AddAsync(Book book)
        {

                // begin transaction
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository bookRepo = this._repoProvider.Get(uow);
                IPublisherRepository publisherRepo = this._publisherRepoProvider.Get(uow);
                ITagRepository tagRepo = this._tagRepoProvider.Get(uow);
                IAuthorRepository authorRepo = this._authorRepoProvider.Get(uow);
                uow.Begin();

                // handle publisher
                int publisherId = 0;
                if (await publisherRepo.ExistsWithNameAsync(book.Publisher.Name))
                {
                    // publisher exists
                    // get the id
                    publisherId = await publisherRepo.GetIdByNameAsync(book.Publisher.Name);
                }
                else
                {
                    // publisher does not exist
                    // insert publisher
                    await publisherRepo.CreateAsync(book.Publisher);
                    // get the id
                    publisherId = await publisherRepo.GetIdByNameAsync(book.Publisher.Name);
                }
                book.Publisher.Id = publisherId;

                // insert Books table record
                await bookRepo.CreateAsync(book);

                // handle tags
                // get all tag Ids
                List<int> tagIds = new List<int>();
                foreach (var tag in book.Tags)
                {
                    if (await tagRepo.ExistsWithNameAsync(tag.Name))
                    {
                        // tag exists
                        // get the id
                        int tagId = await tagRepo.GetIdByNameAsync(tag.Name);
                        tagIds.Add(tagId);
                    }
                    else
                    {
                        // tag does not exist
                        // insert tag
                        await tagRepo.CreateAsync(tag);
                        // get the id
                        int tagId = await tagRepo.GetIdByNameAsync(tag.Name);
                        tagIds.Add(tagId);
                    }
                }
                // insert records in Book_Tag link table
                int bookId = await bookRepo.GetIdByTitleAsync(book.Title);
                foreach (int tagId in tagIds)
                {
                    await tagRepo.LinkBookAsync(bookId, tagId);
                }

                // handle authors
                // get all author Ids
                List<int> authorIds = new List<int>();
                foreach (var author in book.Authors)
                {
                    if (await authorRepo.AuthorExistsAsync(author.FirstName, author.LastName))
                    {
                        // author exists
                        // get the Id
                        int authorId = await authorRepo.GetIdByNameAsync(author.FirstName, author.LastName);
                        authorIds.Add(authorId);
                    }
                    else
                    {
                        // author does not exist
                        // insert author
                        await authorRepo.CreateAsync(author);
                        // get the Id
                        int authorId = await authorRepo.GetIdByNameAsync(author.FirstName, author.LastName);
                        authorIds.Add(authorId);
                    }
                }
                // insert records in Book_Author table
                foreach (int authorId in authorIds)
                {
                    await authorRepo.LinkBookAsync(bookId, authorId);
                }
                
                // commit transaction
                uow.Commit();
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
                // begin transaction
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository repo = this._repoProvider.Get(uow);
                uow.Begin();

                // do the work
                repo.UpdateAsync(book, includeImage);

                // commit transaction
                uow.Commit();
            });
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() =>
            {
                // begin transaction
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository repo = this._repoProvider.Get(uow);
                uow.Begin();

                // do the work
                repo.DeleteByIdAsync(id);

                // commit transaction
                uow.Commit();
            });
        }

        public async Task UpdateTagsAsync(ItemTagsDto dto)
        {

                // begin transaction
                IUnitOfWork uow = this._uowProvider.Get();
                IBookRepository bookRepo = this._repoProvider.Get(uow);
                ITagRepository tagRepo = this._tagRepoProvider.Get(uow);
                uow.Begin();

                // add tags
                foreach (var tag in dto.TagsToAdd)
                {
                    if (await tagRepo.ExistsWithNameAsync(tag))
                    {
                        // tag exists
                        // get the Id
                        int tagId = await tagRepo.GetIdByNameAsync(tag);
                        // insert record into link table
                        await tagRepo.LinkBookAsync(dto.Id, tagId);
                    }
                    else
                    {
                        // tag does not exist
                        // insert it
                        await tagRepo.CreateAsync(new Tag { Name = tag });
                        // get the id
                        int tagId = await tagRepo.GetIdByNameAsync(tag);
                        // insert record into link table
                        await tagRepo.LinkBookAsync(dto.Id, tagId);
                    }
                }

                // remove tags
                foreach (var tag in dto.TagsToRemove)
                {
                    if (await tagRepo.ExistsWithNameAsync(tag))
                    {
                        // tag exists
                        // get the id
                        int tagId = await tagRepo.GetIdByNameAsync(tag);
                        // delete record from link table
                        await tagRepo.UnlinkBookAsync(dto.Id, tagId);
                    }
                }

                // commit transaction
                uow.Commit();
        }
    }//BookService
}