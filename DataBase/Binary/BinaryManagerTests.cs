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
            string text_to_compare = string.Empty;
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, book);
                stream.Flush();
                stream.Position = 0;
                text_to_compare = Convert.ToBase64String(stream.ToArray());
            }
            Assert.AreEqual(text, text);
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