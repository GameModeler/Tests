using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Csv
{
    [TestClass()]
    public class CsvManagerTests
    {
        [TestMethod()]
        public void WriteToCsvFileTest()
        {
            Book book = Book.example_book();
            CsvManager.WriteToCsvFile<Book>(Book.path(), "test.csv", book);
            string text = System.IO.File.ReadAllText(Book.path() + "test.csv").Replace("\n", "").Replace(" ", "").Replace("\r", "");
            Assert.AreEqual("0,Djangotutorial,2017,Python,False", text);
        }

        [TestMethod()]
        public void ReadFromCsvFileTest()
        {
            Book csv_book = CsvManager.ReadFromCsvFile<Book>(Book.path(), "test.csv");
            Book book = Book.example_book();
            Assert.AreEqual(book, csv_book);
        }
    }
}