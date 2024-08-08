namespace BookLibraryAPI.Models.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }

        //change to genre ID
        public string? Genre { get; set; }

        public string? Synopsis { get; set; }

        public required string Author { get; set; }

        public bool IsBorrowed { get; set; } = false;

    }
}
