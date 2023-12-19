using Microsoft.AspNetCore.Mvc;
using MyLibrary.Models;

namespace MyLibrary.Controllers{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookController : ControllerBase {
    
        public static readonly List<Book> Books = new() {
            new Book() {
                Id = 0,
                Name = "Power of Habits",
                Author = "Charles Duhigg",
                PublishYear = 2012,
                Edition = "10th"
            }
        };
        
        [HttpGet]
        public List<Book> GetAll() { 
            return Books;
        }

        [HttpGet]
        [Route("GetByID")] // You Can Do It This Way (Old School)
        public ActionResult<Book> GetById(int id) {
            
            var book = Books.Find(x => x.Id == id);

            if (book == null) return NotFound($"Book with id = {id} not found");

            return book;
        }

        [HttpGet("GetByAuthor")] // Or This Way (New School)
        public ActionResult<List<Book>> GetByAuthorName(string name) {
            List<Book> books = Books.FindAll(x => x.Author?.ToLower() == name.ToLower());

            if (books == null) return NotFound($"Books by Author {name} not found");

            return books;
        }

        [HttpPost]
        public List<Book> Post(Book NewBook) {
            Books.Add(NewBook);
            return Books;
        }

        [HttpDelete]
        public ActionResult<List<Book>> DeleteBook(int id){
            Book? book = Books.Find(x => x.Id == id);
          
            if (book == null) return NotFound($"Book with id = {id} not found");
            
            Books.Remove(book);
            return Books;
        }

        [HttpPut]
        public ActionResult<Book> UpdateBook(int id, BookUpdateDTO book){
            var selected_book = Books.Find(x => x.Id == id);
           
            if (selected_book == null) return NotFound($"Book with id = {id} not found");

           
            Map(selected_book, book);
            return selected_book;
        }

        private static void Map(Book original, BookUpdateDTO replacement){
            original.Name = replacement.Name ?? original.Name;
            original.Author = replacement.Author ?? original.Author;
            original.PublishYear = replacement.PublishYear == 0 ? original.PublishYear : replacement.PublishYear;
            original.Edition = replacement.Edition ?? original.Edition;
        }
    }

}

