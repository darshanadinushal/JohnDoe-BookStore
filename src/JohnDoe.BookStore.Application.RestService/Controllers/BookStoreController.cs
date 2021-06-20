using JohnDoe.BookStore.Application.Shared.Contract.Manager;
using JohnDoe.BookStore.Application.Shared.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JohnDoe.BookStore.Application.RestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly ILogger<BookStoreController> _logger;

        private readonly IBookStoreManager _bookStoreManager;

        public BookStoreController(ILogger<BookStoreController> logger , IBookStoreManager bookStoreManager)
        {
            _logger = logger;
            _bookStoreManager = bookStoreManager;
        }

        /// <summary>
        /// 1.	A method to return a list of available books:
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Book>> Get(CancellationToken cancellationToken)
        {
            try
            {
                return await _bookStoreManager.GetAvailableBooksAsync();
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// 2.	A search method to find a book by containing string. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{name}")]
        public async Task<Book> GetByname(string name , CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Provide proper book name");

                return await _bookStoreManager.GetAvailableBooksByNameAsync(name);
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 3.	A method to return information about a specific book
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{isbn}")]
        public async Task<BaseBookInfo> GetByISBN(string isbn, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(isbn))
                    throw new Exception("Provide proper book isbn number");

                return await _bookStoreManager.GetAvailableBooksByISBNAsync(isbn);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"BookOrderController" + ex.Message);
                throw;
            }
        }

    }
}
