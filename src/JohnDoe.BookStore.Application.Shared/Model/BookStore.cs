using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JohnDoe.BookStore.Application.Shared.Model
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("BookStoreCollection")]
    public class BookStoreCollection
    {
        [XmlElement("BookStore")]
        public List<BookStore> BookStores { get; set; }
    }

    [Serializable()]
    public class BookStore
    {

        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Identifier")]
        public string Identifier { get; set; }
    }
}
