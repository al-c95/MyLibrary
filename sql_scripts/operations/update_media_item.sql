UPDATE Media
SET image = NULL, notes = "test notes" --FIXME: cannot have notes text "notes" - it will not update the field.
WHERE id = 1;