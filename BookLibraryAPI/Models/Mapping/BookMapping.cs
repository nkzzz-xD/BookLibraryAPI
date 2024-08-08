using BookLibraryAPI.Models.Dtos;
using BookLibraryAPI.Models.Entities;

namespace BookLibraryAPI.Models.Mapping
{
    public static class BookMapping
    {
        public static Book ToEntity(this CreateBookDto bookDto)
        {
            return new Book()
            {
                Title = bookDto.Title,
                Genre = bookDto.Genre,
                Synopsis = bookDto.Synopsis,
                Author = bookDto.Author
            };
        }

        public static Book ToEntity(this UpdateBookDto bookDto, Guid id)
        {
            return new Book()
            {
                Id = id,
                Title = bookDto.Title,
                Genre = bookDto.Genre,
                Synopsis = bookDto.Synopsis,
                Author = bookDto.Author
            };
        }

        public static BookSummaryDto ToBookSummaryDto(this Book book)
        {
            return new(
                book.Id,
                book.Title,
                book.Genre,
                book.Synopsis,
                book.Author,
                book.IsBorrowed
            );
        }
    }
}
