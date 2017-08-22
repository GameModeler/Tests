using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using System.Linq;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.RepositoryTests.GlobalContext.MySQL_SQLite
{
    /// <summary>
    /// Description résumée pour Update
    /// </summary>
    [TestClass]
    public class Update
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

        [TestMethod]
        public void UpdateSingleTest()
        {
            dataInit.Book1.Author = "Stephan King";
            dataInit.Book1.Title = "The Dark Tower";

            var updateResult = repository.Update(dataInit.Book1);

            Assert.AreEqual(1, updateResult[mySqlContext]);
            Assert.AreEqual(1, updateResult[sqliteContext]);

            // Query from DbSet
            Book updatedBookMySql = repository.Repositories[mySqlContext].DbSet.Where(b => b.Title == "The Dark Tower")
                                                    .FirstOrDefault();

            Book updatedBookSqlite = repository.Repositories[sqliteContext].DbSet.Where(b => b.Title == "The Dark Tower")
                                        .FirstOrDefault();

            Assert.AreEqual("The Dark Tower", updatedBookMySql.Title);
            Assert.AreEqual("The Dark Tower", updatedBookSqlite.Title);

            Book bookmysql = mySqlContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=1").FirstOrDefault<Book>();

            Book booksqlite = sqliteContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=1").FirstOrDefault<Book>();


            Assert.AreEqual("The Dark Tower", bookmysql.Title);
            Assert.AreEqual("The Dark Tower", booksqlite.Title);
        }

        [TestMethod]
        public void UpdateMultipleTest()
        {

            dataInit.Book2.Author = "Robert Charles Wilson";
            dataInit.Book2.Title = "Spin";

            dataInit.Book3.Author = "Dan Simmons";
            dataInit.Book3.Title = "Hyperion";

            List<Book> newBookShelve = new List<Book>();
            newBookShelve.Add(dataInit.Book2);
            newBookShelve.Add(dataInit.Book3);

            var updateResult = repository.Update(newBookShelve);

            Assert.AreEqual(2, updateResult[mySqlContext]);
            Assert.AreEqual(2, updateResult[sqliteContext]);

            // Query from DbSet
            Book updatedBookMysql = repository.Repositories[mySqlContext].DbSet.Where(b => b.Title == "Spin")
                                                    .FirstOrDefault();

            Book updatedBookSqlite = repository.Repositories[sqliteContext].DbSet.Where(b => b.Title == "Spin")
                                                   .FirstOrDefault();

            Assert.AreEqual("Spin", updatedBookMysql.Title);
            Assert.AreEqual("Spin", updatedBookSqlite.Title);

            Book spinBookMysql = mySqlContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=2").FirstOrDefault<Book>();

            Book spinBookSqlite = sqliteContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=2").FirstOrDefault<Book>();

            Book hyperionBookMysql = sqliteContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=3").FirstOrDefault<Book>();

            Book hyperionBookSqlite = sqliteContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=3").FirstOrDefault<Book>();

            Assert.AreEqual("Spin", spinBookMysql.Title);
            Assert.AreEqual("Spin", spinBookSqlite.Title);

            Assert.AreEqual("Dan Simmons", hyperionBookMysql.Author);
            Assert.AreEqual("Dan Simmons", hyperionBookSqlite.Author);
        }
    }
}
