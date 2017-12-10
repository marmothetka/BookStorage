using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookService.Infrastructure;
using BookService.Models;
using BookService.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly BookContext _bookContext;
        private readonly AuthorsController _authorsController;

        //todo IBookContext and add mock-s
        public BooksController(BookContext context)
        {
            _bookContext = context ?? throw new ArgumentNullException(nameof(context));
            _authorsController = new AuthorsController(_bookContext);
            ((DbContext)context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // GET api/values
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Book>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var totalItems = await _bookContext.Books
                .LongCountAsync();

            var itemsOnPage = await _bookContext.Books.Include(x => x.Authors)
                .OrderBy(x => x.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            var model = new PaginatedItemsViewModel<Book>(
                pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var item = await _bookContext.Books
                .SingleOrDefaultAsync(x => x.Id == id);

            if (item != null)
                return Ok(item);

            return NotFound();
        } 

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]Book book)
        {
            if (book.Id == 0)
            {
                await _authorsController.PostAsync(book.Authors);
                await _bookContext.Books.AddAsync(book);
            }
            else
            {
                await _authorsController.PostAsync(book.Authors);
                _bookContext.Books.Update(book);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentNullException(nameof(id));

            var item = await _bookContext.Books.SingleOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return BadRequest();

            _bookContext.Books.Remove(item);
            //todo validate authors and if author without book - remove author
            await _bookContext.SaveChangesAsync();
           
            return Ok();
        }
    }
}
