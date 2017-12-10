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

        public BooksController(BookContext context)
        {
            _bookContext = context ?? throw new ArgumentNullException(nameof(context));

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
        public async Task<IActionResult> Post([FromBody]Book book)
        {
            if (book.Id == 0)
                await _bookContext.Books.AddAsync(book);
            else
                _bookContext.Books.Update(book);

            await _bookContext.SaveChangesAsync();
            return Ok();
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
            await _bookContext.SaveChangesAsync();
            return Ok();
        }
    }
}
