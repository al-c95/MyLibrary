--Return all books with a specific tag
SELECT * FROM Books
INNER JOIN Book_Tag ON Book_Tag.bookId = Books.id
INNER JOIN Tags ON Tags.id = Book_Tag.tagId
WHERE Tags.name = "tag"; 