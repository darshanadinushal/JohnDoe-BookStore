using JohnDoe.BookStore.Application.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JohnDoe.BookStore.Application.Shared.Contract.Manager
{
    public interface IBookStoreManager
    {
        Task<IEnumerable<Book>> GetAvailableBooksAsync();

        Task<Book> GetAvailableBooksByNameAsync(string name);

        Task<BaseBookInfo> GetAvailableBooksByISBNAsync(string isbn);
    }
}
