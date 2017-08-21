using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.Utils;
using DataBase.Database.Repositories.Interfaces;
using DataBase.Database.DbContexts.Interfaces;
using System.Linq;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.RepositoryTests.SQLite
{
    /// <summary>
    /// Description résumée pour InsertAndCreateDb
    /// </summary>
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

        public InsertAndCreateDb()
        { }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active, ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires

        /// <summary>
        /// Delete database before each test
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            repository = databaseInit.getNewRepository<Book>(ProviderType.SQLite);
            universalContext = repository.Context;

            book1 = new Book("The Way Of King", 2013, "Brandon Sanderson");
            book2 = new Book("Words Of Radiance", 2015, "Brandon Sanderson");
            book3 = new Book("The Lies Of Lock Lamora", 2009, "Scott Lynch");
            book4 = new Book("The Name Of The Wind", 2007, "Patrick Rothfuss");

            bookShelve = new List<Book>();

            bookShelve.Add(book2);
            bookShelve.Add(book3);
            bookShelve.Add(book4);
        }

        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            universalContext.DbContext.Database.ExecuteSqlCommand("DELETE FROM Books");
        }

        /// <summary>
        /// Delete db after test
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            universalContext.DbContext.Database.ExecuteSqlCommand("DELETE FROM Books");
        }

        #endregion

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
        /// Create the database ans insert multible entities
        /// </summary>
       
    }
}
