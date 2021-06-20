using System;
using System.Collections.Generic;
using System.Text;

namespace JohnDoe.BookStore.Application.Shared.DomainModel
{
    public class Book
    {
        public string ISBNCode { get; set; }

        public string Author { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }
    }

    
}
