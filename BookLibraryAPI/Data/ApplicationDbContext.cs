using BookLibraryAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();
    }
}
