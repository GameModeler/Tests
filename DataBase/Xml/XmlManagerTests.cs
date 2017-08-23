using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Xml
{
    [TestClass()]
    public class XmlManagerTests
    {
        [TestMethod()]
        public void WriteToXmlFileTest()
        {
            Book book = Book.example_book();
            XmlManager.WriteToXmlFile<Book>(Book.path(), "test.xml", book);
            String text = System.IO.File.ReadAllText(Book.path() + "test.xml").Replace("\n", "").Replace("\r", "");
            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<Book xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
                            "  <BookId>0</BookId>" +
                            "  <Title>Django tutorial</Title>" +
                            "  <Year>2017</Year>" +
                            "  <Author>Python</Author>" +
                            "  <Is_active>false</Is_active>" +
                            "</Book>", text);
        }

        [TestMethod()]
        public void ReadFromXmlFileTest()
        {
            Book xml_book = XmlManager.ReadFromXmlFile<Book>(Book.path(), "test.xml");
            Book book = Book.example_book();
            Assert.AreEqual(book, xml_book);
        }
    }
}