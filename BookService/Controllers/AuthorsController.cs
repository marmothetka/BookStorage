using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookService.Infrastructure;
using BookService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers
{
    [Produces("application/json")]
    [Route("api/Authors")]
    public class AuthorsController : Controller
    {
        private readonly BookContext _bookContext;

        public AuthorsController(BookContext bookContext)
        {
            _bookContext = bookContext;
        }
        
        // POST: api/Authors
        [HttpPost]
        public async Task PostAsync([FromBody] IList<Author> authors)
        {
            foreach (var author in authors)
            {
                if (author.Id == 0 && _bookContext.Author.FirstOrDefault(x => x.FullName == author.FullName) == null)
                    await _bookContext.Author.AddAsync(author);
            }
            await _bookContext.SaveChangesAsync();
        }
        
        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
