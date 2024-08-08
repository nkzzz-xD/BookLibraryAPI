using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPI.Models.Dtos
{
    public record class CreateBookDto(
        [Required] string Title,
        //change to genre ID
        string? Genre,
        string? Synopsis,
        //TODO Remove this
        string Author
    );
}
