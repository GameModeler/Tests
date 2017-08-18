using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using DataBase.Database.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.RepositoryTests.MySQL
{
    [TestClass]
    public class InsertAndCreateDb
    {

        private static IRepository<Book> repository;
        private static IUniversalContext universalContext;

        private static Book book1;
        private static Book book2;
        private static Book book3;
        private static Book book4;

        private static List<Book> bookShelve;

        private static DataBaseInitilization databaseInit = DataBaseInitilization.Instance;

        public InsertAndCreateDb() { }

        /// <summary>
        /// Delete database before each test
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {

            repository = databaseInit.getNewRepository<Book>(ProviderType.MySQL);
            universalContext = repository.Context;

            book1 = new Book("The Way Of King", 2013, "Brandon Sanderson");
            book2 = new Book("Words Of Radiance", 2015, "Brandon Sanderson");
            book3 = new Book("The Lies Of Lock Lamora", 2009, "Scott Lynch");
            book4 = new Book("The Name Of The Wind", 2007, "Patrick Rothfuss");

            bookShelve = new List<Book>(); 

            bookShelve.Add(book2);
            bookShelve.Add(book3);
            bookShelve.Add(book4);

            bool isDbCreated = universalContext.DbContext.Database.Exists();

            if(isDbCreated)
            {
                universalContext.DbContext.Database.Delete();
            }
        }

        /// <summary>
        /// Delete db after test
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            universalContext.DbContext.Database.Delete();
        }

        /// <summary>
        /// Create the database and insert a single entity
        /// </summary>
        [TestMethod]
        public void InsertSingleTest()
        {

            // Insert book 1
            var insertResult = repository.Insert(book1);

            // Check if the database is created
            var dbExists = universalContext.DbContext.Database.Exists();
            Assert.IsTrue(dbExists);

            // Get book 1 from the database
            var book = repository.DbSet.SqlQuery("Select * from Books where Title='The Way Of King'").FirstOrDefault<Book>();

            // Assert the insert is effectued
            Assert.AreEqual(1, insertResult);
            Assert.AreEqual("The Way Of King", book.Title);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void InsertMultipleTest()
        {
            // Insert the book shelve
            var insertResult = repository.Insert(bookShelve);

            // Check if the database is created
            var dbExists = universalContext.DbContext.Database.Exists();
            Assert.IsTrue(dbExists);

            // Get number of books from the database
            var books = repository.DbSet.Count<Book>();

            Assert.AreEqual(3, insertResult);
            Assert.AreEqual(3, books);
        }
    }
}
