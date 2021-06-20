using JohnDoe.BookStore.Application.Shared.Contract.Repository;
using JohnDoe.BookStore.Application.Shared.DomainModel;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JohnDoe.BookStore.Application.Core.service.Repository
{
    public class BookOrderRepository : IBookOrderRepository
    {
        private readonly ILogger<BookOrderRepository> _logger;

        public BookOrderRepository(ILogger<BookOrderRepository> logger)
        {
            _logger = logger;
        }


        public async Task<bool> SaveBookOrder(BookOrder bookOrder , Shared.Model.BookStore bookStore)
        {
            try
            {
                var currentDateString = DateTime.Now.ToString("yyyy-MM-dd");//+"-"+ Guid.NewGuid().ToString();
                var bookorderBasePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+$"\\BookStore\\Orders\\{bookStore.Name.Trim()}\\{currentDateString}";

                if (!Directory.Exists(bookorderBasePath))
                {
                    Directory.CreateDirectory(bookorderBasePath);
                }

                var content = JsonConvert.SerializeObject(bookOrder);
                var fileName = bookorderBasePath+"\\"+bookOrder.ISBNCode + "-" + Guid.NewGuid().ToString()+ ".txt";
                await File.WriteAllTextAsync(fileName, content);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
