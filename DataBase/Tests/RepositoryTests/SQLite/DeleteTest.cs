using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.Utils;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using System.Linq;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.RepositoryTests.SQLite
{
    /// <summary>
    /// Description résumée pour DeleteTest
    /// </summary>
    [TestClass]
    public class DeleteTest
    {
        private static IRepository<Book> repository;
        private static IUniversalContext universalContext;

        private static Book book1;
        private static Book book2;
        private static Book book3;
        private static Book book4;

        private static List<Book> bookShelve;

        private static DataBaseInitilization databaseInit = DataBaseInitilization.Instance;

        public DeleteTest()
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
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        [TestInitialize()]
        public void MyTestInitialize() {

            repository = databaseInit.getNewRepository<Book>(ProviderType.SQLite);
            universalContext = repository.Context;

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

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            universalContext.DbContext.Database.ExecuteSqlCommand("DELETE FROM Books");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            universalContext.DbContext.Database.ExecuteSqlCommand("DELETE FROM Books");
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
