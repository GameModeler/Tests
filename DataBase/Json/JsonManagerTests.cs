using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Json
{
    [TestClass()]
    public class JsonManagerTests
    {
        [TestMethod()]
        public void WriteToJsonFileTest()
        {
            Book book = Book.example_book();
            JsonManager.WriteToJsonFile<Book>(Book.path(), "test.json", book);
            string text = System.IO.File.ReadAllText(Book.path() + "test.json");
            Assert.AreEqual("{\"BookId\":0,\"Title\":\"Django tutorial\",\"Year\":2017,\"Author\":\"Python\",\"Is_active\":false}", text);
        }

        [TestMethod()]
        public void ReadFromJsonFileTest()
        {
            Book json_book = JsonManager.ReadFromJsonFile<Book>(Book.path(), "test.json");
            Book book = Book.example_book();
            Assert.AreEqual(book, json_book);
        }
    }
}