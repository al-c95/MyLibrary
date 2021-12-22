using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using MyLibrary.Models.Entities;

namespace MyLibrary.DataAccessLayer
{
    /// <summary>
    /// Abstraction over database for book item operations.
    /// </summary>
    public class BookDataAccessor : ItemDataAccessor<Book>
    {
        public override async Task Create(Book toAdd)
        {
            using (var conn = GetConnection())
            {
                await conn.OpenAsync();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // insert publisher information
                        int publisherId = 0;
                        Publisher publisher = toAdd.Publisher;
                        bool publisherExists = await conn.ExecuteScalarAsync<bool>("SELECT COUNT(1) FROM Publishers WHERE name=@name", new
                        {
                            publisher.Name
                        });
                        if (publisherExists)
                        {
                            // publisher exists
                            // get the id
                            publisherId = await conn.QuerySingleAsync<int>("SELECT id FROM Publishers WHERE name=@name", new
                            {
                                publisher.Name
                            });
                        }
                        else
                        {
                            // publisher does not exist
                            // insert publisher
                            await conn.ExecuteAsync("INSERT INTO Publishers(name) VALUES(@name);", new
                            {
                                name = publisher.Name
                            });
                            // get the id
                            publisherId = await conn.QuerySingleAsync<int>("SELECT id FROM Publishers WHERE name=@name", new
                            {
                                publisher.Name
                            });
                        }

                        // insert Book table record
                        const string INSERT_BOOK_SQL = "INSERT INTO Books(title,titleLong,isbn,isbn13,deweyDecimal,publisherId,format,language,datePublished,edition,pages,dimensions,overview,image,msrp,excerpt,synopsys,notes) " +
                            "VALUES(@title,@titleLong,@isbn,@isbn13,@deweyDecimal,@publisherId,@format,@language,@datePublished,@edition,@pages,@dimensions,@overview,@image,@msrp,@excerpt,@synopsys,@notes);";
                        await conn.ExecuteAsync(INSERT_BOOK_SQL, new
                        {
                            title = toAdd.Title,
                            titleLong = toAdd.TitleLong,
                            isbn = toAdd.Isbn,
                            isbn13 = toAdd.Isbn13,
                            deweyDecimal = toAdd.DeweyDecimal,
                            publisherId = publisherId,
                            format = toAdd.Format,
                            language = toAdd.Language,
                            datePublished = toAdd.DatePublished,
                            edition = toAdd.Edition,
                            pages = toAdd.Pages,
                            dimensions = toAdd.Dimensions,
                            overview = toAdd.Overview,
                            image = toAdd.Image,
                            msrp = toAdd.Msrp,
                            excerpt = toAdd.Excerpt,
                            synopsys = toAdd.Synopsys,
                            notes = toAdd.Notes
                        });

                        // get all tag ids
                        List<int> tagIds = new List<int>();
                        foreach (var tag in toAdd.Tags)
                        {
                            bool exists = await conn.ExecuteScalarAsync<bool>("SELECT COUNT(1) FROM Tags WHERE name=@name", new
                            {
                                tag.Name
                            });
                            if (exists)
                            {
                                // tag exists
                                // get the id
                                int tagId = await conn.QuerySingleAsync<int>("SELECT id FROM Tags WHERE name=@name", new
                                {
                                    tag.Name
                                });
                                tagIds.Add(tagId);
                            }
                            else
                            {
                                // tag does not exist
                                // insert tag
                                await conn.ExecuteAsync("INSERT INTO Tags(name) VALUES(@name);", new
                                {
                                    tag.Name
                                });
                                // get the id
                                int tagId = await conn.QuerySingleAsync<int>("SELECT id FROM Tags WHERE name=@name", new
                                {
                                    tag.Name
                                });
                                tagIds.Add(tagId);
                            }
                        }//foreach

                        // insert records into Book_Tag table
                        int itemId = await conn.QuerySingleAsync<int>("SELECT id FROM Books WHERE title=@title", new
                        {
                            toAdd.Title
                        });
                        foreach (int tagId in tagIds)
                        {
                            const string INSERT_MEDIA_TAG_SQL = "INSERT INTO Book_Tag(bookId,tagId) " +
                                "VALUES(@bookId,@tagId);";
                            await conn.ExecuteAsync(INSERT_MEDIA_TAG_SQL, new
                            {
                                bookId = itemId,
                                tagId = tagId
                            });
                        }

                        // get all author ids
                        List<int> authorIds = new List<int>();
                        foreach (var author in toAdd.Authors)
                        {
                            bool exists = await conn.ExecuteScalarAsync<bool>("SELECT COUNT(1) FROM Authors WHERE firstName=@firstName AND lastName=@lastName", new
                            {
                                firstName = author.FirstName,
                                lastName = author.LastName
                            });
                            if (exists)
                            {
                                // author exists
                                // get the id
                                int authorId = await conn.QuerySingleAsync<int>("SELECT id FROM Authors WHERE firstName=@firstName AND lastName=@lastName", new
                                {
                                    firstName = author.FirstName,
                                    lastName = author.LastName
                                });
                                authorIds.Add(authorId);
                            }
                            else
                            {
                                // author does not exist
                                // insert author
                                await conn.ExecuteAsync("INSERT INTO Authors(firstName,lastName) VALUES(@firstName,@lastName);", new
                                {
                                    firstName = author.FirstName,
                                    lastName = author.LastName
                                });
                                // get the id
                                int tagId = await conn.QuerySingleAsync<int>("SELECT id FROM Authors WHERE firstName=@firstName AND lastName=@lastName", new
                                {
                                    firstName = author.FirstName,
                                    lastName = author.LastName
                                });
                                authorIds.Add(tagId);
                            }
                        }//foreach

                        // insert records(s) in Book_Author table
                        int bookId = await conn.QuerySingleAsync<int>("SELECT id FROM Books WHERE title=@title AND titleLong=@titleLong", new
                        {
                            title = toAdd.Title,
                            titleLong = toAdd.TitleLong
                        });
                        foreach (int authorId in authorIds)
                        {
                            const string INSERT_BOOK_AUTHOR_SQL = "INSERT INTO Book_Author(bookId,authorId) VALUES(@bookId,@authorId)";
                            await conn.ExecuteAsync(INSERT_BOOK_AUTHOR_SQL, new
                            {
                                bookId = bookId,
                                authorId = authorId
                            });
                        }

                        // if the transaction succeeded, commit it
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // transaction failed
                        // roll it back
                        transaction.Rollback();
                        // pass up the exception
                        throw;
                    }
                }//transaction

                conn.Close();
            }//conn
        }

        /// <summary>
        /// Retrieve all Books from the database.
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<Book>> ReadAll()
        {
            using (var conn = GetConnection())
            {
                var authorsSql = "SELECT A.*, B.id AS BookId FROM Books as B " +
                         "INNER JOIN Book_Author AS B2A ON B.id = B2A.bookId " +
                         "INNER JOIN Authors A ON B2A.authorId = A.id;";
                var allAuthorsWithBookId = await conn.QueryAsync<dynamic>(authorsSql);

                var tagsSql = "SELECT T.*, B.id AS BookId FROM Books as B " +
                              "INNER JOIN Book_Tag AS B2T ON B.id = B2T.bookId " +
                              "INNER JOIN Tags T ON B2T.tagId = T.id;";
                var allTagsWithBookId = await conn.QueryAsync<dynamic>(tagsSql);

                var sql = "SELECT * FROM Books as B " +
                          "INNER JOIN Publishers AS P ON B.publisherId = P.id;";
                var allBooks = await conn.QueryAsync<Book, Publisher, Book>(sql, (book, publisher) =>
                {
                    book.Publisher = publisher;
                    book.Authors = allAuthorsWithBookId.Where(row => row.BookId == (int)book.Id).Select(row => new Author { Id = (int)row.id, FirstName = row.firstName, LastName = row.lastName }).AsList();
                    book.Tags = allTagsWithBookId.Where(row => (int)row.BookId == book.Id).Select(row => new Tag { Id = (int)row.id, Name = row.name }).AsList();
                    return book;
                });

                return allBooks;
            }
        }

        /// <summary>
        /// Update image and/or notes fields of book record in database.
        /// </summary>
        /// <param name="toUpdate"></param>
        public override async Task Update(Book toUpdate)
        {
            const string SQL = "UPDATE Books " +
                "SET image = @image, notes = @notes " +
                "WHERE id = @id;";

            using (var conn = GetConnection())
            {
                await conn.ExecuteAsync(SQL, new
                {
                    toUpdate.Id,

                    toUpdate.Image,
                    toUpdate.Notes
                });
            }
        }

        public override async Task UpdateTags(ItemTagsDto itemTagsDto)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // add tags
                        foreach (var tag in itemTagsDto.TagsToAdd)
                        {
                            bool exists = await conn.ExecuteScalarAsync<bool>("SELECT COUNT(1) FROM Tags WHERE name=@name", new
                            {
                                name = tag
                            });
                            if (exists)
                            {
                                // tag exists
                                // get the id
                                int tagId = await conn.QuerySingleAsync<int>("SELECT id FROM Tags WHERE name=@name", new
                                {
                                    name = tag
                                });
                                // insert record into link table
                                await conn.ExecuteAsync("INSERT INTO Book_Tag (bookId,tagId) VALUES(@bookId,@tagId);", new
                                {
                                    bookId = itemTagsDto.Id,
                                    tagId = tagId
                                });
                            }
                            else
                            {
                                // tag does not exist
                                // insert it
                                await conn.ExecuteAsync("INSERT INTO Tags (name) VALUES(@name);", new
                                {
                                    name = tag
                                });
                                // get the id
                                int tagId = await conn.QuerySingleAsync<int>("SELECT id FROM Tags WHERE name=@name", new
                                {
                                    name = tag
                                });
                                // insert record into link table
                                await conn.ExecuteAsync("INSERT INTO Book_Tag (bookId,tagId) VALUES(@bookId,@tagId);", new
                                {
                                    bookId = itemTagsDto.Id,
                                    tagId = tagId
                                });
                            }
                        }

                        // remove tags
                        foreach (var tag in itemTagsDto.TagsToRemove)
                        {
                            bool exists = await conn.ExecuteScalarAsync<bool>("SELECT COUNT(1) FROM Tags WHERE name=@name", new
                            {
                                name = tag
                            });
                            if (exists)
                            {
                                // tag exists
                                // get the id
                                int tagId = await conn.QuerySingleAsync<int>("SELECT id FROM Tags WHERE name=@name", new
                                {
                                    name = tag
                                });
                                // delete record from link table
                                await conn.ExecuteAsync("DELETE FROM Book_Tag WHERE bookId=@bookId AND tagId=@tagId;", new
                                {
                                    bookId = itemTagsDto.Id,
                                    tagId = tagId
                                });
                            }
                        }

                        // if the transaction succeeded, commit it
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // transaction failed
                        // roll it back
                        transaction.Rollback();
                        // pass up the exception
                        throw;
                    }
                }//transaction
            }//conn
        }

        /// <summary>
        /// Delete a book record by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task DeleteById(int id)
        {
            const string SQL = "DELETE FROM Books WHERE id = @id;";

            using (var conn = GetConnection())
            {
                await conn.ExecuteAsync(SQL, new { id });
            }
        }//DeleteById
    }//class
}