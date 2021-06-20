using JohnDoe.BookStore.Application.Shared.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace JohnDoe.BookStore.Application.Shared.Infra
{
    public static class ReadXmlFiles
    {

        public static string ReadXmlFilebyPath(string filePath)
        {
            try
            {
                string xmlFiles = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + filePath;

                using (FileStream fileStream = new FileStream(xmlFiles, FileMode.Open))
                {
                    TextReader reader = new StreamReader(fileStream);

                    return reader.ReadToEnd();
                    
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

       
    }
}
