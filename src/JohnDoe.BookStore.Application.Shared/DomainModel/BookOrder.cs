using System;
using System.Collections.Generic;
using System.Text;

namespace JohnDoe.BookStore.Application.Shared.DomainModel
{
    public class BookOrder
    {
        public string ISBNCode { get; set; }

        public string StoreIdentifier { get; set; }

        public string Email { get; set; }
    }
}
