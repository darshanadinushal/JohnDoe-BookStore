using JohnDoe.BookStore.Application.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JohnDoe.BookStore.Application.Shared.Contract.Repository
{
    public interface IBookStoreRepository
    {
        Task<IEnumerable<Shared.Model.BookStore>> GetBookStoreAsync();

        Task<IEnumerable<XmlBook>> GetBookListAsync(Shared.Model.BookStore bookStore);
    }
}
