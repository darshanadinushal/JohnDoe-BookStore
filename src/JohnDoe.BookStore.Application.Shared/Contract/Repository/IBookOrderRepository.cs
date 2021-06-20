using JohnDoe.BookStore.Application.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JohnDoe.BookStore.Application.Shared.Contract.Repository
{
    public interface IBookOrderRepository
    {
        Task<bool> SaveBookOrder(BookOrder bookOrder, Shared.Model.BookStore bookStore);
    }
}
