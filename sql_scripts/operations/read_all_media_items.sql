SELECT M.id, title, type, number, image, runningTime, releaseYear, notes, T.id, name
FROM Media M
INNER JOIN Media_Tag MT ON MT.mediaId = M.id
INNER JOIN Tags T ON T.id = MT.tagId;