--Return all books with a specific tag (eg "Prehistoric Life")
SELECT * FROM Books
INNER JOIN Book_Tag ON Book_Tag.bookId = Books.id
INNER JOIN Tags ON Tags.id = Book_Tag.tagId
WHERE Tags.name = "Prehistoric Life"; 