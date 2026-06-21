using Assignment.Models.Entities;
using Assignment.Models.Responses;

namespace Assignment.Helpers
{
    public static class BookResponseMapper
    {
        public static BookResponseV1 ToV1(Book book)
        {
            return new BookResponseV1
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                IsAvailable = book.AvailableCopies > 0
            };
        }

        public static BookResponseV2 ToV2(Book book)
        {
            return new BookResponseV2
            {
                BookId = book.BookId,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher,
                PublishedYear = book.PublishedYear,
                Genre = book.Genre,
                AvailableCopies = book.AvailableCopies
            };
        }
    }
}