# Changelog
All notable changes to this project will be documented in this file.

NOTE: all dates are in dd-mm-yyyy format.

## [1.3.0] - unreleased
### Fixed
- bug with filtering in manage tags for item dialog
### Added
- support for Flash Drives and Floppy Disks
- support for importing/updating book and media item worksheets
- various icons for dialogs and toolstrip
- support for author names with composite and apostrophied last names
### Changed
- replaced buttons below menustrip with toolstrip
- place main window category selection drop-down near toolbar
- enhancements to wishlist window

## [1.2.1] - 30-6-2022
### Fixed
- cancel button in add new book form disabled when adding another book in scan mode
- information from previously added book appears in pre-populated forms
- ok button enable logic in add new publisher or tag dialogs for add new item forms
- ISBN fields now disabled in search dialog during API requests

## [1.2.0] - 2-4-2022
### Remarks
First stable version. Previous versions were development versions.
### Fixed
- various performance fixes and improvements
- various user interface enhancements
- several memory leaks
- bug with new wishlist item title and notes not being cleared after saving
- bug with handling authors and publishers when scanning books
- bug with processing ISBNs with dashes from API
- bugs with populating authors, tags and publishers when pre-filling add new item forms
### Added
- import CSV functionality
- export as Excel functionality
- spinner when loading item data
- filtering for authors and publishers in add new book dialog
### Changed
- restructured database to store images in a separate tables
- better error handling when searching for books using API

## [1.1.1] - 1-3-2022
### Fixed
- bug with checking if ISBN already exists when adding new book
### Added
- filtering for tags when adding new item

## [1.1.0] - 24-2-2022
### Added
- wishlist feature

## [1.0.1] - 30-1-2022
### Fixed
- "insufficient parameters supplied to the command" error when attempting to remove tag from media item
- minor bugs in manage copies dialog
### Changed
- increased size of tags list box in main window filters
- increased sizes of tags, authors and publishers list boxes in add new item dialogs
### Removed
- sample data from database

## [1.0.0] - 23-1-2022
### Added
- manage copies feature
### Fixed
- bug with processing author names in comma format while adding new book

## [0.9.0] - 8-1-2022
### Fixed
- main window layout problem
### Changed
- minor colour changes to main window

## [0.8.0] - 31-12-2021
### Added
- initial release
