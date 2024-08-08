using BookLibraryAPI.Data;
using BookLibraryAPI.Models.Dtos;
using BookLibraryAPI.Models.Entities;
using BookLibraryAPI.Models.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Controllers
{
    [Route ("api/[Controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public BooksController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //CRUD Operations
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var allBooks = await dbContext.Books
                .Select(book=>book.ToBookSummaryDto())
                .AsNoTracking()
                .ToListAsync();
            return Ok(allBooks);
        }

        [HttpGet]
        [Route("{id:guid}",Name = "GetBook")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await dbContext.Books.FindAsync(id);
            return book is null ? NotFound("Book could not be found in database") : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(CreateBookDto addBookDto)
        {
            var bookEntity = addBookDto.ToEntity();
            dbContext.Books.Add(bookEntity);
            await dbContext.SaveChangesAsync();
            return CreatedAtRoute("GetBook", new { id = bookEntity.Id
            },bookEntity.ToBookSummaryDto());
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateBook(Guid id, UpdateBookDto updateBookDto)
        {
            var bookToUpdate = await dbContext.Books.FindAsync(id);
            if (bookToUpdate is null)
            {
                return NotFound("Book could not be found in database");
            }
            dbContext.Entry(bookToUpdate)
                .CurrentValues
                .SetValues(updateBookDto.ToEntity(id));
            await dbContext.SaveChangesAsync();
            return Ok(bookToUpdate);
        }

        [HttpPut]
        [Route("borrow/{id:guid}")]
        public async Task<IActionResult> BorrowBook(Guid id)
        {
            var bookToUpdate = await dbContext.Books.FindAsync(id);
            if (bookToUpdate is null)
            {
                return NotFound("Book could not be found in database");
            }
            if (bookToUpdate.IsBorrowed)
            {
                return BadRequest("Book is already borrowed");
            }
            bookToUpdate.IsBorrowed = true;
            await dbContext.SaveChangesAsync();
            return Ok(bookToUpdate);
        }

        [HttpPut]
        [Route("return/{id:guid}")]
        public async Task<IActionResult> ReturnBook(Guid id)
        {
            var bookToUpdate = await dbContext.Books.FindAsync(id);
            if (bookToUpdate is null)
            {
                return NotFound("Book could not be found in database");
            }
            if (!bookToUpdate.IsBorrowed)
            {
                return BadRequest("Book is already returned");
            }
            bookToUpdate.IsBorrowed = false;
            await dbContext.SaveChangesAsync();
            return Ok(bookToUpdate);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var bookToDelete = await dbContext.Books
                .Where(book => book.Id == id)
                .ExecuteDeleteAsync();
            return NoContent();
        }
    }
}
