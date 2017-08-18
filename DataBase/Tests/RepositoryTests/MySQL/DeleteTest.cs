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
    /// Description résumée pour DeleteTest
    /// </summary>
    [TestClass]
    public class DeleteTest
    {
        private static DataBaseInitilization dataInit = DataBaseInitilization.Instance;

        private static IUniversalContext universalcontext;
        private static IRepository<Book> repository;

        private static Book book1;
        private static Book book2;
        private static Book book3;
        private static Book book4;

        private static List<Book> bookShelve;


        public DeleteTest() {}

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
 
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        [TestInitialize()]
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

            // Insert data 
            repository.Insert(bookShelve);

        }
        
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        [TestCleanup()]
        public void TestCleanup() {
            universalcontext.DbContext.Database.Delete();
        }

        #endregion

        /// <summary>
        /// Delete single entity
        /// </summary>
        [TestMethod]
        public void DeleteSingleTest()
        {
            // Delete Book 1
            int result = repository.Delete(book1);

            Assert.AreEqual(1, result);

            Book deletedBook = repository.DbSet.Where(b => b.BookId == 1).FirstOrDefault();

            Assert.IsNull(deletedBook);

        }

        /// <summary>
        /// Delete multiple entities
        /// </summary>
        [TestMethod]
        public void DeleteMultipleTest()
        {
            List<Book> booksToDelete = new List<Book>();

            booksToDelete.Add(book2);
            booksToDelete.Add(book4);

            // Delete Book 1
            int result = repository.Delete(booksToDelete);

            Assert.AreEqual(2, result);

            Book deletedBook1 = repository.DbSet.Where(b => b.BookId == 2).FirstOrDefault();
            Book deletedBook2 = repository.DbSet.Where(b => b.BookId == 4).FirstOrDefault();

            Assert.IsNull(deletedBook1);
            Assert.IsNull(deletedBook2);
        }
    }
}
