using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using System.Linq;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.RepositoryTests.GlobalContext.SQLite_MySQL
{
    /// <summary>
    /// Description résumée pour InsertTest
    /// </summary>
    [TestClass]
    public class InsertTest
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
        public static void MyClassCleanup()
        {
            mySqlContext.DbContext.Database.Delete();
            sqliteContext.DbContext.Database.ExecuteSqlCommand("DELETE FROM Books");
        }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        [TestInitialize()]
        public void MyTestInitialize()
        {

            repository = dataInit.getNewGlobalRepository_Sqlite_MySql<Book>();

            mySqlContext = dataInit.MySqlContext;
            sqliteContext = dataInit.SqliteContext;

            bookShelve = dataInit.BookShelve;
        }

        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        [TestCleanup()]
        public void MyTestCleanup()
        {

            mySqlContext.DbContext.Database.Delete();
            sqliteContext.DbContext.Database.ExecuteSqlCommand("DELETE FROM Books");

        }

        #endregion

        [TestMethod]
        public void InsertSingleTest()
        {
            // Insert book 1
            var insertResult = repository.Insert(dataInit.Book1);

            // Get book 1 from the database
            var bookmysql = repository.Repositories[mySqlContext].DbSet.SqlQuery("Select * from Books where Title='The Way Of King'").FirstOrDefault<Book>();
            var booksqlite = repository.Repositories[sqliteContext].DbSet.SqlQuery("Select * from Books where Title='The Way Of King'").FirstOrDefault<Book>();

            // Assert the insert is effectued
            Assert.AreEqual(1, insertResult[mySqlContext]);
            Assert.AreEqual(1, insertResult[sqliteContext]);

            Assert.AreEqual("The Way Of King", bookmysql.Title);
            Assert.AreEqual("The Way Of King", booksqlite.Title);
        }

        /// <summary>
        /// Insert several items in an already existing table
        /// </summary>
        [TestMethod]
        public void InsertMultipleTest()
        {

            // Insert the book shelve
            var insertResult = repository.Insert(bookShelve);

            // Get number of books from the database

            var bookmysql = repository.Repositories[mySqlContext].DbSet.Count<Book>();
            var booksqlite = repository.Repositories[sqliteContext].DbSet.Count<Book>();

            Assert.AreEqual(4, insertResult[mySqlContext]);
            Assert.AreEqual(4, insertResult[sqliteContext]);

            Assert.AreEqual(4, bookmysql);
            Assert.AreEqual(4, booksqlite);
        }
    }
}
