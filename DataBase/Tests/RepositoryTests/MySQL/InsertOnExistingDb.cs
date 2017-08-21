using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.Repositories.Interfaces;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Utils;
using System.Linq;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.RepositoryTests.MySQL
{
    /// <summary>
    /// Description résumée pour InsertOnExistingDb
    /// </summary>
    [TestClass]
    public class InsertOnExistingDb
    {

        private static IRepository<Book> repository;
        private static IUniversalContext universalContext;

        private static Book book1;
        private static Book book2;
        private static Book book3;
        private static Book book4;

        private static List<Book> bookShelve;

        private static DataBaseInitilization databaseInit = DataBaseInitilization.Instance;

        public InsertOnExistingDb()
        {

        }

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
        
        [TestInitialize]
        public void MyTestInitialize() {

            repository = databaseInit.getNewRepository<Book>(ProviderType.MySQL);
            universalContext = repository.Context;

            var isDbExist = universalContext.DbContext.Database.Exists();

            if(isDbExist)
            {
                universalContext.DbContext.Database.Delete();
            }

            universalContext.DbContext.Database.CreateIfNotExists();

            book1 = new Book("The Way Of King", 2013, "Brandon Sanderson");
            book2 = new Book("Words Of Radiance", 2015, "Brandon Sanderson");
            book3 = new Book("The Lies Of Lock Lamora", 2009, "Scott Lynch");
            book4 = new Book("The Name Of The Wind", 2007, "Patrick Rothfuss");

            bookShelve = new List<Book>();

            bookShelve.Add(book2);
            bookShelve.Add(book3);
            bookShelve.Add(book4);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            // Delete test database after tests
            universalContext.DbContext.Database.Delete();
        }

        #endregion

        [TestMethod]
        public void InsertSingleTest()
        {
            // Insert book 1
            var insertResult = repository.Insert(book1);

            // Get book 1 from the database
            var book = repository.DbSet.SqlQuery("Select * from Books where Title='The Way Of King'").FirstOrDefault<Book>();

            // Assert the insert is effectued
            Assert.AreEqual(1, insertResult);
            Assert.AreEqual("The Way Of King", book.Title);
        }

        /// <summary>
        /// Insert several items in an already existing table
        /// </summary>
        [TestMethod]
        public void InsertMultipleTest()
        {
            // Add book 1 into the db
            repository.Insert(book1);

            // Insert the book shelve
            var insertResult = repository.Insert(bookShelve);

            // Check if the database is created
            var dbExists = universalContext.DbContext.Database.Exists();
            Assert.IsTrue(dbExists);

            // Get number of books from the database
            var books = repository.DbSet.Count<Book>();

            //var books = universalContext.En

            Assert.AreEqual(3, insertResult);
            Assert.AreEqual(4, books);
        }
    }
}
