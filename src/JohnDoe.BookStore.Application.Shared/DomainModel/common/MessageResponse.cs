using System;
using System.Collections.Generic;
using System.Text;

namespace JohnDoe.BookStore.Application.Shared.DomainModel.common
{
    public class MessageResponse
    {
        public bool isError { get; set; }

        public string Message { get; set; }
    }
}
