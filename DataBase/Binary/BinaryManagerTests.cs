using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Binary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.DataBase.Entities;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Tests.DataBase.Binary
{
    [TestClass()]
    public class BinaryManagerTests
    {
        [TestMethod()]
        public void WriteToBinaryFileTest()
        {
            Book book = Book.example_book();
            BinaryManager.WriteToBinaryFile<Book>(Book.path(), "test.bin", book);
            string text = System.IO.File.ReadAllText(Book.path() + "test.bin");
            BinaryFormatter bf = new BinaryFormatter();
            string text_to_compare = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, book);
                text_to_compare = ms.ToString();
            }            
            Assert.AreEqual(text_to_compare, text);
        }

        [TestMethod()]
        public void ReadFromBinaryFileTest()
        {
            Book book_to_write = Book.example_book();
            BinaryManager.WriteToBinaryFile<Book>(Book.path(), "test.bin", book_to_write);
            Book bin_book = BinaryManager.ReadFromBinaryFile<Book>(Book.path(), "test.bin");
            Book book = Book.example_book();
            Assert.AreEqual(book, bin_book);
        }
    }
}