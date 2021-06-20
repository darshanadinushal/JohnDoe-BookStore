using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JohnDoe.BookStore.Application.Shared.Model
{
    [Serializable()]
    [XmlRoot("bookCollection")]
    public class BookCollection
    {
        [XmlElement("book")]
        public List<XmlBook> XmlBooks { get; set; }
    }

    public class XmlBook
    {
        [XmlElement("author")]
        public string Author { get; set; }


        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("isbnCode")]
        public string ISBNCode { get; set; }
    }
}
