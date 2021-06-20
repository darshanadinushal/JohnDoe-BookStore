using JohnDoe.BookStore.Application.Shared.DomainModel;
using JohnDoe.BookStore.Application.Shared.DomainModel.common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JohnDoe.BookStore.Application.Shared.Contract.Manager
{
    public interface IBookOrderManager
    {
        Task<MessageResponse> SaveBookOrder(BookOrder bookOrder);
    }
}
