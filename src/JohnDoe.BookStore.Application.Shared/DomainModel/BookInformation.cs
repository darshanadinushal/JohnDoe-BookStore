using System;
using System.Collections.Generic;
using System.Text;

namespace JohnDoe.BookStore.Application.Shared.DomainModel
{
    public class BookInformation
    {
        public string ISBNCode { get; set; }

        public string Author { get; set; }

        public string Name { get; set; }

        public string StoreName { get; set; }

        public string StoreIdentifier { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }
    }
}
