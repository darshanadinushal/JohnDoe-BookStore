using JohnDoe.BookStore.Application.Shared.Contract.Repository;
using JohnDoe.BookStore.Application.Shared.Infra;
using JohnDoe.BookStore.Application.Shared.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JohnDoe.BookStore.Application.Core.service.Repository
{
    public class BookStoreRepository :IBookStoreRepository
    {
        private readonly ILogger<BookStoreRepository> _logger;

        public BookStoreRepository(ILogger<BookStoreRepository> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Shared.Model.BookStore>> GetBookStoreAsync()
        {
            try
            {
                //Needs to replace this path from appsetting.json
                var bookStorePath = "\\BookStore\\reseller-store.xml";
                var resource = ReadXmlFiles.ReadXmlFilebyPath(bookStorePath);
                XmlSerializer serializer = new XmlSerializer(typeof(BookStoreCollection));
                using (TextReader reader = new StringReader(resource))
                {
                    BookStoreCollection bookStoreList = (BookStoreCollection)serializer.Deserialize(reader);
                    if (bookStoreList!=null && bookStoreList.BookStores!=null && bookStoreList.BookStores.Any())
                    {
                        return bookStoreList.BookStores;
                    } 
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<XmlBook>> GetBookListAsync(Shared.Model.BookStore bookStore)
        {
            try
            {
                var currentDateString = DateTime.Now.ToString("yyyy-MM-dd");
                //Needs to replace this path from appsetting.json
                var bookStoreWisePath = $"\\BookStore\\{bookStore.Name.Trim()}\\{bookStore.Name.Trim()}-{currentDateString}.xml";
                var resource = ReadXmlFiles.ReadXmlFilebyPath(bookStoreWisePath);
                XmlSerializer serializer = new XmlSerializer(typeof(BookCollection));
                using (TextReader reader = new StringReader(resource))
                {
                    BookCollection bookStoreList = (BookCollection)serializer.Deserialize(reader);
                    if (bookStoreList != null && bookStoreList.XmlBooks != null && bookStoreList.XmlBooks.Any())
                    {
                        return bookStoreList.XmlBooks;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
