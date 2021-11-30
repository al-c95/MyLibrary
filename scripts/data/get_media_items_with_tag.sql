--Return all media items with a specific tag
SELECT * FROM Media
INNER JOIN Media_Tag ON Media_Tag.bookId = Books.id
INNER JOIN Tags ON Tags.id = Media_Tag.tagId
WHERE Tags.name = "tag"; 