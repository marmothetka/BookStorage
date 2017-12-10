using BookService.Controllers;
using BookService.Infrastructure;
using BookService.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BookService.Integration.Tests
{
    [TestFixture]
    public class BookServiceControllerTests
    {
        private Book _book;
        private BooksController _bookService;

        [SetUp]
        public void SetUp()
        {
            _book = new Book()
            {
                Name = "TestNameBook",
                Authors = new List<Author>() { new Author() { LastName = "Ivanov", FirstName = "Ivan" } }
            };
            _bookService = new BooksController(new BookContext(new Microsoft.EntityFrameworkCore.DbContextOptions<BookContext>()));
        }

        [Test]
        public void PostBook_ShouldBeCorrect()
        { 
            Assert.DoesNotThrowAsync(() => _bookService.Post(_book));
        }

        [Test]
        public void PostBook_ShoudBeISBNException()
        {
            //todo: CopyTo
            Book book = _book;
            book.Isbn = null;
            //todo: Add ISBN Exception
            Assert.ThrowsAsync<ArgumentNullException>(() => _bookService.Post(book));
        }

        [Test]
        public async void GetBooks_ShoudBeCorrectAsync()
        {
            var result = await _bookService.Get();
            Assert.IsNotNull(result);
            Assert.Positive(((result as ViewResult)?.Model as IList<Book>)?.Count ?? 0);
        }

        [Test]
        public async void GetBookById_ShoudBeCorrectAsync()
        {
            //todo: get by dynamic id
            var result = await _bookService.Get(1);
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(((result as ViewResult)?.Model as Book)?.Name);
        }
    }
}
