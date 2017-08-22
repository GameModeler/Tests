using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using System.Linq;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.RepositoryTests.GlobalContext.MySQL_SQLite
{
    /// <summary>
    /// Description résumée pour GetTest
    /// </summary>
    [TestClass]
    public class GetTest
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
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        [TestInitialize()]
        public void MyTestInitialize() {

            repository = dataInit.getNewGlobalRepository_MySql_Sqlite<Book>();

            mySqlContext = dataInit.MySqlContext;
            sqliteContext = dataInit.SqliteContext;

            bookShelve = dataInit.BookShelve;

            repository.Insert(bookShelve);
        }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        [TestCleanup()]
        public void MyTestCleanup() {
            mySqlContext.DbContext.Database.Delete();
            sqliteContext.DbContext.Database.ExecuteSqlCommand("DELETE FROM Books");
        }
        //
        #endregion

        /// <summary>
        /// Test the selection a single entity
        /// </summary>
        [TestMethod]
        public void GetSingleTest()
        {
            var selectedBook = repository.Get(dataInit.Book1.BookId);

            Assert.AreEqual(1, selectedBook[mySqlContext].BookId);
            Assert.AreEqual(1, selectedBook[sqliteContext].BookId);

            Assert.AreEqual("The Way Of King", selectedBook[mySqlContext].Title);
            Assert.AreEqual("The Way Of King", selectedBook[sqliteContext].Title);

            var selectedBook4 = repository.Get(dataInit.Book4.BookId);

            Assert.AreEqual(4, selectedBook4[mySqlContext].BookId);
            Assert.AreEqual(4, selectedBook4[sqliteContext].BookId);

            Assert.AreEqual("The Name Of The Wind", selectedBook4[mySqlContext].Title);
            Assert.AreEqual("The Name Of The Wind", selectedBook4[sqliteContext].Title);
        }

        /// <summary>
        /// Test the selection a multiple entities
        /// </summary>
        [TestMethod]
        public void GetMultipleTest()
        {
            var selectedBook = repository.Get();

            Assert.AreEqual(4, selectedBook[mySqlContext].ToList().Count);
            Assert.AreEqual(4, selectedBook[sqliteContext].ToList().Count);

            List<Book> dbSetSelectedBookMysql = repository.Repositories[mySqlContext].DbSet.ToList<Book>();
            List<Book> dbSetSelectedBookSqlite = repository.Repositories[sqliteContext].DbSet.ToList<Book>();

            Assert.AreEqual(4, dbSetSelectedBookMysql.Count);
            Assert.AreEqual(4, dbSetSelectedBookSqlite.Count);
        }
    }
}
