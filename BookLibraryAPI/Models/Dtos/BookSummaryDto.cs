using BookLibraryAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPI.Models.Dtos
{
    public record class BookSummaryDto(
        Guid Id, 
        [Required]string Title,
        string? Genre, 
        string? Synopsis, 
        [Required] string Author,
        bool IsBorrowed
    );
}
