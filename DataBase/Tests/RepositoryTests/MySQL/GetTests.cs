using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using DataBase.Database.Utils;
using System.Linq;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.RepositoryTests.MySQL
{
    /// <summary>
    /// Description résumée pour GetTests
    /// </summary>
    [TestClass]
    public class GetTests
    {

        private static DataBaseInitilization dataInit = DataBaseInitilization.Instance;

        private static IUniversalContext universalcontext;
        private static IRepository<Book> repository;

        private static Book book1;
        private static Book book2;
        private static Book book3;
        private static Book book4;

        private static List<Book> bookShelve;

        public GetTests() { }

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
        public void TestInitialize() {

            repository = dataInit.getNewRepository<Book>(ProviderType.MySQL);
            universalcontext = repository.Context;

            bool isDbExist = universalcontext.DbContext.Database.Exists();

            if (isDbExist)
            {
                universalcontext.DbContext.Database.Delete();
            }

            book1 = new Book("The Way Of King", 2013, "Brandon Sanderson");
            book2 = new Book("Words Of Radiance", 2015, "Brandon Sanderson");
            book3 = new Book("The Lies Of Lock Lamora", 2009, "Scott Lynch");
            book4 = new Book("The Name Of The Wind", 2007, "Patrick Rothfuss");

            bookShelve = new List<Book>();

            bookShelve.Add(book1);
            bookShelve.Add(book2);
            bookShelve.Add(book3);
            bookShelve.Add(book4);

            repository.Insert(bookShelve);

        }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        [TestCleanup()]
        public void MyClassCleanup() {
            universalcontext.DbContext.Database.Delete();
        }

        #endregion

        /// <summary>
        /// Test the selection a single entity
        /// </summary>
        [TestMethod]
        public void GetSingleTest()
        {
            Book selectedBook = repository.Get(book1.BookId);

            Assert.AreEqual(1, selectedBook.BookId);
            Assert.AreEqual("The Way Of King", selectedBook.Title);

            Book selectedBook4 = repository.Get(book4.BookId);

            Assert.AreEqual(4, selectedBook4.BookId);
            Assert.AreEqual("The Name Of The Wind", selectedBook4.Title);
        }

        /// <summary>
        /// Test the selection a multiple entities
        /// </summary>
        [TestMethod]
        public void GetMultipleTest()
        {
            List<Book> selectedBook = repository.Get().ToList();
            Assert.AreEqual(4, selectedBook.Count);

            List<Book> dbSetSelectedBook = repository.DbSet.ToList<Book>();
            Assert.AreEqual(4, dbSetSelectedBook.Count);
        }
    }
}
