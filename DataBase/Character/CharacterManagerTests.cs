using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Character
{
    [TestClass()]
    public class CharacterManagerTests
    {
        [TestMethod()]
        public void WriteToCharacterFileTest()
        {
            Book book = Book.example_book();
            CharacterManager.WriteToCharacterFile<Book>(Book.path(), "test.txt", book);
            string text = System.IO.File.ReadAllText(Book.path() + "test.txt");
            Assert.AreEqual("Tests.DataBase.Entities.Book>BookId:0|Title:Django tutorial|Year:2017|Author:Python|Is_active:False\n", text);
        }

        [TestMethod()]
        public void ReadFromCharacterFileTest()
        {
            List<Book> char_book = CharacterManager.ReadFromCharacterFile<Book>(Book.path(), "test.txt");
            List<Book> books = new List<Book>();
            Book book = new Book();
            books.Add(Book.example_book());
            foreach(Book b in char_book) {
                if (b != null)
                    book = b;
            }
            Assert.AreEqual(books.First(), book);
        }
    }
}