using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : Controller
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /*private static List<Book> BookList = new List<Book>(){
            new Book {
                Id = 1,
                Title = "Kitap1",
                GenreId = 1,
                PageCount = 200,
                PublishDate = new DateTime(2001,6,12)
                },
                new Book {
                Id = 2,
                Title = "Kitap2",
                GenreId = 2,
                PageCount = 400,
                PublishDate = new DateTime(2005,4,11)
                },
                new Book {
                Id = 3,
                Title = "Kitap3",
                GenreId = 2,
                PageCount = 705,
                PublishDate = new DateTime(2010,9,1)}
        };*/

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }
        // Route ile get isteği
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                result = query.Handle();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
                
        }

        /*
        // Query string ile get isteği
        [HttpGet]
        public Book Get([FromQuery] string id)
        {
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }*/
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            try
            {
                command.Model = newBook;
                command.Handle();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}