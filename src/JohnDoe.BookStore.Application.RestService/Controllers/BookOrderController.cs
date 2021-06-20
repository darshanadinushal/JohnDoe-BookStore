using JohnDoe.BookStore.Application.Shared.Contract.Manager;
using JohnDoe.BookStore.Application.Shared.DomainModel;
using JohnDoe.BookStore.Application.Shared.DomainModel.common;
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
    public class BookOrderController : ControllerBase
    {
        private readonly ILogger<BookOrderController> _logger;

        private readonly IBookOrderManager _bookStoreManager;

        public BookOrderController(ILogger<BookOrderController> logger, IBookOrderManager bookOrderManager)
        {
            _logger = logger;
            _bookStoreManager = bookOrderManager;
        }

        [HttpPost]
        public async Task<MessageResponse> Post([FromBody] BookOrder bookOrder ,CancellationToken cancellationToken)
        {
            try
            {
                
                return await _bookStoreManager.SaveBookOrder(bookOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError($"BookOrderController" + ex.Message);
                throw;
            }
        }
    }
}
