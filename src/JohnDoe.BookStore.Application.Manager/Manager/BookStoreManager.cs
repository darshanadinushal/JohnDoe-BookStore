using AutoMapper;
using JohnDoe.BookStore.Application.Shared.Contract.Manager;
using JohnDoe.BookStore.Application.Shared.Contract.Repository;
using JohnDoe.BookStore.Application.Shared.DomainModel;
using JohnDoe.BookStore.Application.Shared.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JohnDoe.BookStore.Application.Manager.Manager
{
    public class BookStoreManager : IBookStoreManager
    {
        private readonly ILogger<BookStoreManager> _logger;

        private readonly IBookStoreRepository _bookStoreRepository;

        private readonly IMapper _mapper;

        public BookStoreManager(ILogger<BookStoreManager> logger , IBookStoreRepository bookStoreRepository, IMapper mapper)
        {
            _logger = logger;
            _bookStoreRepository = bookStoreRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 1.	A method to return a list of available books
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            try
            {
                var bookInfoListByStore = await GetBookInfromation();

                if (bookInfoListByStore!= null && bookInfoListByStore.Any())
                {
                    var bookInfoList = bookInfoListByStore.Where(x => x.Count > 0)
                           .GroupBy(b => new { b.Author, b.ISBNCode, b.Name })
                           .Select(c => new Book
                           {
                               Author = c.Key.Author,
                               ISBNCode = c.Key.ISBNCode,
                               Name = c.Key.Name,
                               Count = c.Sum(z => z.Count),
                               MinPrice = c.Min(p => p.Price),
                               MaxPrice = c.Max(p => p.Price)
                           }).ToList();

                    return bookInfoList;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// 2.	A search method to find a book by containing string
        /// </summary>
        /// <returns></returns>
        public async Task<Book> GetAvailableBooksByNameAsync(string name)
        {
            try
            {
                var bookInfoListByStore = await GetBookInfromation();

                if (bookInfoListByStore != null && bookInfoListByStore.Any())
                {
                    var bookInfo = bookInfoListByStore
                           .Where(x => x.Count > 0)
                           .Where(x => x.Name.Trim().ToLower() == name.Trim().ToLower())
                           .GroupBy(b => new { b.Author, b.ISBNCode, b.Name })
                           .Select(c => new Book
                           {
                               Author = c.Key.Author,
                               ISBNCode = c.Key.ISBNCode,
                               Name = c.Key.Name,
                               Count = c.Sum(z => z.Count),
                               MinPrice = c.Min(p => p.Price),
                               MaxPrice = c.Max(p => p.Price)
                           }).FirstOrDefault();

                    return bookInfo;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 3.	A method to return information about a specific book:
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<BaseBookInfo> GetAvailableBooksByISBNAsync(string isbn)
        {
            try
            {
                var bookInfoByStore = new BaseBookInfo();
                var bookInfoList = await GetBookInfromation();

                if (bookInfoList != null && bookInfoList.Any())
                {
                    var bookInfoListByIsbn = bookInfoList.Where(x => x.ISBNCode.Trim() == isbn.Trim()).ToList();


                    if (bookInfoListByIsbn != null && bookInfoListByIsbn.Any())
                    {
                        var bookInfoObject = bookInfoListByIsbn.FirstOrDefault();
                        bookInfoByStore = new BaseBookInfo
                        {
                            Author = bookInfoObject.Author,
                            ISBNCode = bookInfoObject.ISBNCode,
                            Name = bookInfoObject.Name,
                            BookInfoListByStore = new List<BookInfoByStore>()
                        };
                        foreach (var bookInfo in bookInfoListByIsbn)
                        {
                            var bookInfoListByStore = new BookInfoByStore
                            {
                                Count = bookInfo.Count,
                                Price = bookInfo.Price,
                                StoreName = bookInfo.StoreName,
                                StoreIdentifier = bookInfo.StoreIdentifier,

                            };

                            bookInfoByStore.BookInfoListByStore.Add(bookInfoListByStore);
                        }

                        return bookInfoByStore;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private async Task<IEnumerable<BookInformation>> GetBookInfromation()
        {
            try
            {
                var allbookInfoList = new List<BookInformation>();

                var bookStoreList = await _bookStoreRepository.GetBookStoreAsync();

                if (bookStoreList != null && bookStoreList.Any())
                {
                    foreach (var bookStore in bookStoreList)
                    {
                        var xmlBookList = await _bookStoreRepository.GetBookListAsync(bookStore);


                        var bookinfoList = _mapper.Map<IEnumerable<XmlBook>, IEnumerable<BookInformation>>(xmlBookList);

                        bookinfoList.Select(c => {
                            c.StoreIdentifier = bookStore.Identifier;
                            c.StoreName = bookStore.Name; return c;
                        }).ToList();

                        allbookInfoList.AddRange(bookinfoList);
                    }
                }

                return allbookInfoList;
            }
            catch (Exception ex)
            {
                throw;

            }
        }
    }
}
