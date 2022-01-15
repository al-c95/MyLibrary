using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Models.Entities;
using MyLibrary.DataAccessLayer;
using MyLibrary.DataAccessLayer.Repositories;

namespace MyLibrary.Models.BusinessLogic
{
    public class BookService : IBookService
    {
        public BookService() { }

        public async virtual Task<IEnumerable<Book>> GetAll()
        {
            IEnumerable<Book> allBooks = null;
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                BookRepository repo = new BookRepository(uow);
                allBooks = repo.ReadAll();
                uow.Dispose();
            });

            return allBooks;
        }

        public async Task<Book> GetById(int id)
        {
            var allBooks = await GetAll();
            return allBooks.FirstOrDefault(b => b.Id == id);
        }

        public async Task<int> GetIdByTitle(string title)
        {
            var allBooks = await GetAll();
            return allBooks.FirstOrDefault(b => b.Title.Equals(title)).Id;
        }

        public async Task<Boolean> ExistsWithId(int id)
        {
            var allBooks = await GetAll();
            return allBooks.Any(b => b.Id == id);
        }

        public async Task<Boolean> ExistsWithTitle(string title)
        {
            var allBooks = await GetAll();
            return allBooks.Any(b => b.Title.Equals(title));
        }

        public async Task<Boolean> ExistsWithLongTitle(string longTitle)
        {
            var allBooks = await GetAll();
            return allBooks.Any(b => b.TitleLong.Equals(longTitle));
        }

        public async Task<Boolean> ExistsWithIsbn(string isbn)
        {
            var allBooks = await GetAll();

            foreach (var book in allBooks)
            {
                if (book.Isbn != null)
                {
                    if (book.Isbn.Equals(isbn))
                        return true;
                }

                if (book.Isbn13 != null)
                {
                    if (book.Isbn13.Equals(isbn))
                        return true;
                }
            }//foreach

            return false;
        }

        public async Task Add(Book book)
        {
            await Task.Run(() =>
            {
                // begin transaction
                UnitOfWork uow = new UnitOfWork();
                BookRepository bookRepo = new BookRepository(uow);
                PublisherRepository publisherRepo = new PublisherRepository(uow);
                TagRepository tagRepo = new TagRepository(uow);
                AuthorRepository authorRepo = new AuthorRepository(uow);
                uow.Begin();

                // handle publisher
                int publisherId = 0;
                if (publisherRepo.ExistsWithName(book.Publisher.Name))
                {
                    // publisher exists
                    // get the id
                    publisherId = publisherRepo.GetIdByName(book.Publisher.Name);
                }
                else
                {
                    // publisher does not exist
                    // insert publisher
                    publisherRepo.Create(book.Publisher);
                    // get the id
                    publisherId = publisherRepo.GetIdByName(book.Publisher.Name);
                }
                book.Publisher.Id = publisherId;

                // insert Books table record
                bookRepo.Create(book);

                // handle tags
                // get all tag Ids
                List<int> tagIds = new List<int>();
                foreach (var tag in book.Tags)
                {
                    if (tagRepo.ExistsWithName(tag.Name))
                    {
                        // tag exists
                        // get the id
                        int tagId = tagRepo.GetIdByName(tag.Name);
                        tagIds.Add(tagId);
                    }
                    else
                    {
                        // tag does not exist
                        // insert tag
                        tagRepo.Create(tag);
                        // get the id
                        int tagId = tagRepo.GetIdByName(tag.Name);
                        tagIds.Add(tagId);
                    }
                }
                // insert records in Book_Tag link table
                //int bookId = GetIdByTitle(book.Title).Result;
                int bookId = bookRepo.GetIdByTitle(book.Title);
                foreach (int tagId in tagIds)
                {
                    tagRepo.LinkBook(bookId, tagId);
                }

                // handle authors
                // get all author Ids
                List<int> authorIds = new List<int>();
                foreach (var author in book.Authors)
                {
                    if (authorRepo.AuthorExists(author.FirstName, author.LastName))
                    {
                        // author exists
                        // get the Id
                        int authorId = authorRepo.GetIdByName(author.FirstName, author.LastName);
                        authorIds.Add(authorId);
                    }
                    else
                    {
                        // author does not exist
                        // insert author
                        authorRepo.Create(author);
                        // get the Id
                        int authorId = authorRepo.GetIdByName(author.FirstName, author.LastName);
                        authorIds.Add(authorId);
                    }
                }
                // insert records in Book_Author table
                foreach (int authorId in authorIds)
                {
                    authorRepo.LinkBook(bookId, authorId);
                }
                
                // commit transaction
                uow.Commit();
            });
        }

        public async virtual Task Update(Book book)
        {
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                BookRepository repo = new BookRepository(uow);
                repo.Update(book);
                uow.Dispose();
            });
        }

        public async virtual Task DeleteById(int id)
        {
            await Task.Run(() =>
            {
                UnitOfWork uow = new UnitOfWork();
                BookRepository repo = new BookRepository(uow);
                repo.DeleteById(id);
                uow.Dispose();
            });
        }

        public async virtual Task UpdateTags(ItemTagsDto dto)
        {
            await Task.Run(() =>
            {
                // begin transaction
                UnitOfWork uow = new UnitOfWork();
                BookRepository bookRepo = new BookRepository(uow);
                TagRepository tagRepo = new TagRepository(uow);
                uow.Begin();

                // add tags
                foreach (var tag in dto.TagsToAdd)
                {
                    if (tagRepo.ExistsWithName(tag))
                    {
                        // tag exists
                        // get the Id
                        int tagId = tagRepo.GetIdByName(tag);
                        // insert record into link table
                        tagRepo.LinkBook(dto.Id, tagId);
                    }
                    else
                    {
                        // tag does not exist
                        // insert it
                        tagRepo.Create(new Tag { Name = tag });
                        // get the id
                        int tagId = tagRepo.GetIdByName(tag);
                        // insert record into link table
                        tagRepo.LinkBook(dto.Id, tagId);
                    }
                }

                // remove tags
                foreach (var tag in dto.TagsToRemove)
                {
                    if (tagRepo.ExistsWithName(tag))
                    {
                        // tag exists
                        // get the id
                        int tagId = tagRepo.GetIdByName(tag);
                        // delete record from link table
                        tagRepo.UnlinkBook(dto.Id, tagId);
                    }
                }

                // commit transaction
                uow.Commit();
            });
        }
    }//BookService
}
