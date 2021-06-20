using JohnDoe.BookStore.Application.Shared.Contract.Manager;
using JohnDoe.BookStore.Application.Shared.Contract.Repository;
using JohnDoe.BookStore.Application.Shared.DomainModel;
using JohnDoe.BookStore.Application.Shared.DomainModel.common;
using JohnDoe.BookStore.Application.Shared.Infra.Validation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnDoe.BookStore.Application.Manager.Manager
{
    public class BookOrderManager: IBookOrderManager
    {
        private readonly ILogger<BookOrderManager> _logger;

        private readonly IBookOrderRepository _bookOrderRepository;

        private readonly IBookStoreRepository _bookStoreRepository;

        private readonly IBookStoreManager _bookStoreManager;

        public BookOrderManager(ILogger<BookOrderManager> logger , IBookOrderRepository bookOrderRepository ,
            IBookStoreRepository bookStoreRepository , IBookStoreManager bookStoreManager)
        {
            _logger = logger;
            _bookOrderRepository = bookOrderRepository;
            _bookStoreRepository = bookStoreRepository;
            _bookStoreManager = bookStoreManager;

        }

        public async Task<MessageResponse> SaveBookOrder(BookOrder bookOrder)
        {
            try
            {
                _logger.LogInformation("Start SaveBookOrder request:" + JsonConvert.SerializeObject(bookOrder));
                //Validate email Addreess 
                if (!EmailValidation.IsValidEmail(bookOrder.Email))
                    throw new Exception("provide email address is not valid");

                var bookstoreList = await _bookStoreRepository.GetBookStoreAsync();

                if (bookstoreList!= null && bookstoreList.Any())
                {
                    var bookStore = bookstoreList.Where(x => x.Identifier == bookOrder.StoreIdentifier).FirstOrDefault();
                    if (bookStore == null)
                        throw new Exception("provide book store Identifier is not valid");

                    var bookStoreInfo = await _bookStoreManager.GetAvailableBooksByISBNAsync(bookOrder.ISBNCode);

                    if (bookStoreInfo != null && bookStoreInfo.BookInfoListByStore !=null && bookStoreInfo.BookInfoListByStore.Any())
                    {
                        if( bookStoreInfo.BookInfoListByStore.Any(x=>x.StoreIdentifier== bookOrder.StoreIdentifier && x.Count>0)){
                            var result = await _bookOrderRepository.SaveBookOrder(bookOrder, bookStore);

                            _logger.LogInformation("End SaveBookOrder resopnse:" + true);
                            return new MessageResponse
                            {
                                isError = false,
                                Message = "order place success"
                            };
                        }
                        else
                        {
                            throw new Exception($"provide book ISBN code does not available book in {bookStore.Name}");
                        }
                    }
                    else
                    {
                        throw new Exception("provide book ISBN code does not have available book");
                    }

                   
                }
                else
                {
                    throw new Exception("there is no book store assing");
                }

                
            }
            catch (Exception ex)
            {
                _logger.LogError(" SaveBookOrder Error:" + ex.Message);
                return new MessageResponse
                {
                    isError = true,
                    Message = ex.Message

                };
            }
        }

    }
}
