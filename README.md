# MarthaLibrary-Lenkie
This is a library management system with options to reserve, 
checkout  and check in books.


## Please note, for swagger documentation url  is http://localhost:5200/index.html
- versioning 1.0

## 1.  Authenticate with this default user details to generate a token 
{
  "username": "superadmin",
  "password": "123456"
}

- please note that you can also register a new client user

## 2. Add a book the library shelve

http://localhost:5200/api/v1/LibraryBook/Add

Body paramater example with enum status:1 meaning Available
{
  "isbn": "SBN-AAA-333",
  "author": "Kinsgley Awe",
  "deweyIndex": "1",
  "title": "The thousand",
  "year": 2011,
  "status": 1,
  "cost": 8000,
  "numberOfCopies": 1,
}

## 3. view all list of books added
http://localhost:5200/api/v1/LibraryBook/GetAll

## 4. Reserve A book

http://localhost:5200/api/v1/BookCheck/reservebook

Body paramater example with enum status:4 meaning Reserve
{
  "libraryBookId": 2,
  "cardId": 1,
  "status": 4,
  "since": "2023-04-22T18:54:10.849Z",
  "until": "2023-04-27T18:54:10.850Z",
}

- Please note that on reserve, the apps check if book is reserve on borrow by another customer
- and notifies accordingly and also validate reserve for 24hr.

## 5. CheckOut the book from the library shelve
http://localhost:5200/api/v1/BookCheck/checkout/1

- Body paramater example 
{
  "libraryBookId": 1,
  "status": 1,
  "checkOut": "2023-04-22T19:36:20.878Z"
}

## 6. CheckIn the book back to the library shelve
http://localhost:5200/api/v1/BookCheck/checkin/1
- Body paramater example 
{
  "libraryBookId": 1,
  "status": 1,
  "checkIn": "2023-04-22T19:36:20.878Z"
}