using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SQLite;
using System.IO;

namespace DBTool
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = @"Data Source=..\..\..\library.db; foreign keys=True;";
            SQLiteConnection conn = new SQLiteConnection(connString);
            conn.Open();

            // read image
            Console.WriteLine("Reading image...");
            byte[] imageBytes = File.ReadAllBytes(@"..\..\..\test_image.png");

            // insert tags
            Console.WriteLine("Inserting Tags...");
            for (int i = 1; i <= 100; i++)
            {
                ExecuteQuery("INSERT INTO Tags(name) VALUES('tag" + i + "');", conn);
            }
            
            // insert authors
            Console.WriteLine("Inserting Authors...");
            ExecuteQuery("INSERT INTO Authors(firstName, lastName) VALUES('John', 'Smith');", conn);
            ExecuteQuery("INSERT INTO Authors(firstName, lastName) VALUES('Jane', 'Doe');", conn);

            // insert publishers
            Console.WriteLine("Inserting Publishers...");
            ExecuteQuery("INSERT INTO Publishers(name) VALUES('publisher1');", conn);
            ExecuteQuery("INSERT INTO Publishers(name) VALUES('publisher2');", conn);

            // insert images
            Console.WriteLine("Inserting images...");
            for (int i = 1; i <= 10; i++)
            {
                conn.Execute("INSERT INTO Images(image) VALUES(@image);", new
                {
                    image = imageBytes
                });
            }

            // insert books
            Console.WriteLine("Inserting Books...");
            for (int i = 1; i <= 1000; i++)
            {
                StringBuilder SQL = new StringBuilder("INSERT INTO Books(title, titleLong, publisherId, language, datePublished, placeOfPublication, edition, pages, notes) VALUES(@title, @titleLong, @publisherId, @language, @datePublished, @placeOfPublication, @edition, @pages, @notes);");
                string title = "book" + i.ToString();
                Console.WriteLine("Inserting " + title);
                int bookId = 0;
                if (i % 2 == 0)
                {
                    conn.Execute(SQL.ToString(), new
                    {
                        title=title,
                        titleLong=title,
                        publisherId=1,
                        language="English",
                        datePublished="2022",
                        placeOfPublication="AU",
                        edition="1st",
                        pages=100,
                        notes= "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                    });

                    bookId = conn.QuerySingle<int>("SELECT id FROM Books where title=@title", new
                    {
                        title=title
                    });
                    conn.Execute("INSERT INTO Book_Tag (bookId,tagId) VALUES(@bookId,@tagId);", new 
                    {
                        bookId=bookId,
                        tagId=1
                    });
                    conn.Execute("INSERT INTO Book_Author (bookId,authorId) VALUES(@bookId,@authorId);", new
                    {
                        bookId = bookId,
                        authorId = 1
                    });
                }
                else
                {
                    conn.Execute(SQL.ToString(), new
                    {
                        title = title,
                        titleLong = title,
                        publisherId = 2,
                        language = "English",
                        datePublished = "2022",
                        placeOfPublication = "AU",
                        edition = "1st",
                        pages = 100,
                        notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                    });

                    bookId = conn.QuerySingle<int>("SELECT id FROM Books where title=@title", new
                    {
                        title = title
                    });
                    conn.Execute("INSERT INTO Book_Tag (bookId,tagId) VALUES(@bookId,@tagId);", new
                    {
                        bookId = bookId,
                        tagId = 2
                    });
                    conn.Execute("INSERT INTO Book_Author (bookId,authorId) VALUES(@bookId,@authorId);", new
                    {
                        bookId = bookId,
                        authorId = 2
                    });
                }
            }
            // give a couple of books an image
            for (int i = 1; i <= 5; i++)
            {
                conn.Execute("UPDATE Books SET imageId=@imageId WHERE id=@id;", new
                {
                    id = i,
                    imageId = i
                }); ;
            }

            // insert media items
            Console.WriteLine("Inserting Media items...");
            for (int i = 1; i <= 1000; i++)
            {
                string title = "media" + i.ToString();
                Console.WriteLine("Inserting " + title);

                string SQL = "INSERT INTO Media (title,type,number,runningTime,releaseYear,notes) " +
                "VALUES(@title,@type,@number,@runningTime,@releaseYear,@notes);";
                conn.Execute(SQL, new
                {
                    title=title,
                    type=ItemType.Dvd,
                    number=0123456789,
                    runningTime=60,
                    releaseYear=2022,
                    notes= "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                });
            }
            // give a couple of media items an image
            for (int i = 5; i <= 10; i++)
            {
                conn.Execute("UPDATE Media SET imageId=@imageId WHERE id=@id;", new
                {
                    id = i,
                    imageId = i
                }); ;
            }

            conn.Close();
            conn.Dispose();

            Console.WriteLine("Finished...");
            Console.ReadKey();
        }

        static void ExecuteQuery(string SQL, SQLiteConnection conn)
        {
            Console.WriteLine("Executing Query: " + SQL);

            conn.Execute(SQL);
        }

        enum ItemType
        {
            Book,
            Cd,
            Dvd,
            BluRay,
            Vhs,
            Vinyl,
            Other
        }
    }
}
