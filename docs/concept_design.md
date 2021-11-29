# Concept Design

## Introduction
MyLibrary is a Windows application implemented in WinForms that is designed to store information about books and other items in a local database. It shows a list of all items in the database and allows adding, editing, updating and deleting the information. New items can be added manually or with the help of an online service to retrieve book information. Searching and filtering can be performed on the results.

## Requirements
### R1
The system must support storing books and various media items, such as DVDs, CDs, BluRays and VHSs.

### R2
The application must support searching and filtering for stored items based on various criteria.

### R3
The application must allow creating, viewing, updating and deleting items.

### R4
The application must allow interfacing with a web service to allow optional downloading of item information.

### R5
The application must allow storing an arbitrary number of items of the same title.

### R6
The application must provide a tagging functionality. That is, items stored in the database can be associated to one or more tags, which can then be used to assist in searching and filtering for items.

### R7
The application must allow the data to be stored locally.

### R8
The application must allow images and notes (arbitrary text) to be stored along with each item's metadata (such as ISBN, publisher, or running time, amongst others).

### R9
The application must allow the user to look up online information on a selected item.

### R10
The application must allow viewing of database statistics.

### R11
The application must allow importing and exporting item data.

### R12
The application must allow saving filters.

### R13
The application must have a "wishlist" feature. This entails a separate list and view of items, not displayed in the main list.

### R14
The application must eventually provide a loaning feature.

## Architecture
The application is built on .NET WinForms, targeting .NET Framework 4.7.2. 

The database system used is Sqlite, chosen for its simplicity, and the fact that no server is required, enabling data to be stored locally.