using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using System.Linq;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.RepositoryTests.GlobalContext.MySQL_SQLite
{
    /// <summary>
    /// Description résumée pour DeleteTest
    /// </summary>
    [TestClass]
    public class DeleteTest
    {
        private DataBaseInit dataInit = new DataBaseInit();

        private static IUniversalContext mySqlContext;
        private static IUniversalContext sqliteContext;

        private IGlobalRepository<Book> repository;

        private static List<Book> bookShelve;

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        [ClassCleanup()]
        public static void MyClassCleanup() {
            mySqlContext.DbContext.Database.Delete();
            sqliteContext.DbContext.Database.ExecuteSqlCommand("DELETE FROM Books");
        }
        
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        [TestInitialize()]
        public void MyTestInitialize() {

            repository = dataInit.getNewGlobalRepository_MySql_Sqlite<Book>();

            mySqlContext = dataInit.MySqlContext;
            sqliteContext = dataInit.SqliteContext;

            bookShelve = dataInit.BookShelve;

            repository.Insert(bookShelve);
        }
        
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        [TestCleanup()]
        public void MyTestCleanup() {
            mySqlContext.DbContext.Database.Delete();
            sqliteContext.DbContext.Database.ExecuteSqlCommand("DELETE FROM Books");
        }
        
        #endregion

        /// <summary>
        /// Delete single entity
        /// </summary>
        [TestMethod]
        public void DeleteSingleTest()
        {
            // Delete Book 1
            var result = repository.Delete(dataInit.Book1);

            Assert.AreEqual(1, result[mySqlContext]);
            Assert.AreEqual(1, result[sqliteContext]);

            Book deletedBookMysql = repository.Repositories[mySqlContext].DbSet.Where(b => b.BookId == 1).FirstOrDefault();
            Book deletedBookSqlite = repository.Repositories[sqliteContext].DbSet.Where(b => b.BookId == 1).FirstOrDefault();

            Assert.IsNull(deletedBookMysql);
            Assert.IsNull(deletedBookSqlite);

        }

        /// <summary>
        /// Delete multiple entities
        /// </summary>
        [TestMethod]
        public void DeleteMultipleTest()
        {
            List<Book> booksToDelete = new List<Book>();

            booksToDelete.Add(dataInit.Book2);
            booksToDelete.Add(dataInit.Book4);

            // Delete Book 1
            var result = repository.Delete(booksToDelete);

            Assert.AreEqual(2, result[mySqlContext]);
            Assert.AreEqual(2, result[sqliteContext]);

            Book deletedBookMysql1 = repository.Repositories[mySqlContext].DbSet.Where(b => b.BookId == 2).FirstOrDefault();
            Book deletedBookSqlite1 = repository.Repositories[sqliteContext].DbSet.Where(b => b.BookId == 2).FirstOrDefault();

            Book deletedBookMysql2 = repository.Repositories[mySqlContext].DbSet.Where(b => b.BookId == 4).FirstOrDefault();
            Book deletedBookSqlite2 = repository.Repositories[sqliteContext].DbSet.Where(b => b.BookId == 4).FirstOrDefault();

            Assert.IsNull(deletedBookMysql1);
            Assert.IsNull(deletedBookSqlite1);

            Assert.IsNull(deletedBookMysql2);
            Assert.IsNull(deletedBookSqlite2);
        }
    }
}
