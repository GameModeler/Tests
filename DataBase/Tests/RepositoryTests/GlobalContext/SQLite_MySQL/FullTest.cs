using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using System.Linq;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.RepositoryTests.GlobalContext.SQLite_MySQL
{
    /// <summary>
    /// Description résumée pour FullTest
    /// </summary>
    [TestClass]
    public class FullTest
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

        [ClassCleanup()]
        public static void MyClassCleanup() {
            mySqlContext.DbContext.Database.Delete();
            sqliteContext.DbContext.Database.ExecuteSqlCommand("DELETE FROM Books");
        }
    
        [TestInitialize()]
        public void MyTestInitialize() {

            repository = dataInit.getNewGlobalRepository_Sqlite_MySql<Book>();

            mySqlContext = dataInit.MySqlContext;
            sqliteContext = dataInit.SqliteContext;

            bookShelve = dataInit.BookShelve;
        }

        [TestCleanup()]
        public void MyTestCleanup() {
            mySqlContext.DbContext.Database.Delete();
            sqliteContext.DbContext.Database.ExecuteSqlCommand("DELETE FROM Books");
        }

        #endregion

        [TestMethod]
        public void FullCrudTest()
        {
            // Insert multiple lies
            var insertMultiResult = repository.Insert(bookShelve);

            Assert.AreEqual(4, insertMultiResult[mySqlContext]);
            Assert.AreEqual(4, insertMultiResult[sqliteContext]);


            // Update 1 line
            var book1 = dataInit.Book1;
            book1.Title = "Mistborn";
            book1.Year = 2017;

            var updateSingleResult = repository.Update(book1);

            Assert.AreEqual(1, updateSingleResult[mySqlContext]);
            Assert.AreEqual(1, updateSingleResult[sqliteContext]);

            // Query from DbSet
            const string MISTBORN = "Mistborn";

            Book updatedBookMySql = repository.Repositories[mySqlContext].DbSet.Where(b => b.Title == MISTBORN)
                                                    .FirstOrDefault();

            Book updatedBookSqlite = repository.Repositories[sqliteContext].DbSet.Where(b => b.Title == MISTBORN)
                                        .FirstOrDefault();

            Assert.AreEqual(MISTBORN, updatedBookMySql.Title);
            Assert.AreEqual(MISTBORN, updatedBookSqlite.Title);

            Book bookmysql = mySqlContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=1").FirstOrDefault<Book>();

            Book booksqlite = sqliteContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=1").FirstOrDefault<Book>();


            Assert.AreEqual(MISTBORN, bookmysql.Title);
            Assert.AreEqual(MISTBORN, booksqlite.Title);

            // Get All

            var getAllResult = repository.Get();

            Assert.AreEqual(4, getAllResult[mySqlContext].ToList().Count);
            Assert.AreEqual(4, getAllResult[sqliteContext].ToList().Count);

            List<Book> dbSetSelectedBookMysql = repository.Repositories[mySqlContext].DbSet.ToList<Book>();
            List<Book> dbSetSelectedBookSqlite = repository.Repositories[sqliteContext].DbSet.ToList<Book>();

            Assert.AreEqual(4, dbSetSelectedBookMysql.Count);
            Assert.AreEqual(4, dbSetSelectedBookSqlite.Count);

            // Insert 1 line
            const string AMERICAN_GODS = "American Gods";

            Book book5 = new Book("American Gods", 1998, "Neil Gaiman");

            var insertSingleResult = repository.Insert(book5);

            // Get book 1 from the database
            var bookmysqlInserted = repository.Repositories[mySqlContext].DbSet
                                              .SqlQuery("Select * from Books where Title=@p0", AMERICAN_GODS)
                                              .FirstOrDefault<Book>();

            var booksqliteInserted = repository.Repositories[sqliteContext].DbSet
                                               .SqlQuery("Select * from Books where Title=@p0", AMERICAN_GODS)
                                               .FirstOrDefault<Book>();

            // Assert the insert is effectued
            Assert.AreEqual(1, insertSingleResult[mySqlContext]);
            Assert.AreEqual(1, insertSingleResult[sqliteContext]);

            Assert.AreEqual(AMERICAN_GODS, bookmysqlInserted.Title);
            Assert.AreEqual(AMERICAN_GODS, booksqliteInserted.Title);


            // Delete 1 line
            var deleteOneResult = repository.Delete(dataInit.Book1);

            Assert.AreEqual(1, deleteOneResult[mySqlContext]);
            Assert.AreEqual(1, deleteOneResult[sqliteContext]);

            Book deletedBookMysql = repository.Repositories[mySqlContext].DbSet.Where(b => b.BookId == 1).FirstOrDefault();
            Book deletedBookSqlite = repository.Repositories[sqliteContext].DbSet.Where(b => b.BookId == 1).FirstOrDefault();

            Assert.IsNull(deletedBookMysql);
            Assert.IsNull(deletedBookSqlite);

            // Get 1 line

            var getOneResult = repository.Get(2);

            const string WORDS_OF_RADIANCE = "Words Of Radiance";
            const string THE_NAME_OF_THE_WIND = "The Name Of The Wind";

            Assert.AreEqual(2, getOneResult[mySqlContext].BookId);
            Assert.AreEqual(2, getOneResult[sqliteContext].BookId);

            Assert.AreEqual(WORDS_OF_RADIANCE, getOneResult[mySqlContext].Title);
            Assert.AreEqual(WORDS_OF_RADIANCE, getOneResult[sqliteContext].Title);

            var selectedBook4 = repository.Get(dataInit.Book4.BookId);

            Assert.AreEqual(4, selectedBook4[mySqlContext].BookId);
            Assert.AreEqual(4, selectedBook4[sqliteContext].BookId);

            Assert.AreEqual(THE_NAME_OF_THE_WIND, selectedBook4[mySqlContext].Title);
            Assert.AreEqual(THE_NAME_OF_THE_WIND, selectedBook4[sqliteContext].Title);

            // Update multiple lines

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
            Book updatedMultiBookMysql = repository.Repositories[mySqlContext]
                                                   .DbSet.Where(b => b.Title == "Spin")
                                                   .FirstOrDefault();

            Book updatedMultiBookSqlite = repository.Repositories[sqliteContext]
                                                    .DbSet.Where(b => b.Title == "Spin")
                                                    .FirstOrDefault();

            Assert.AreEqual("Spin", updatedMultiBookMysql.Title);
            Assert.AreEqual("Spin", updatedMultiBookSqlite.Title);

            Book spinBookMysql = mySqlContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=2").FirstOrDefault<Book>();

            Book spinBookSqlite = sqliteContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=2").FirstOrDefault<Book>();

            Book hyperionBookMysql = mySqlContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=3").FirstOrDefault<Book>();

            Book hyperionBookSqlite = sqliteContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=3").FirstOrDefault<Book>();

            Assert.AreEqual("Spin", spinBookMysql.Title);
            Assert.AreEqual("Spin", spinBookSqlite.Title);

            Assert.AreEqual("Dan Simmons", hyperionBookMysql.Author);
            Assert.AreEqual("Dan Simmons", hyperionBookSqlite.Author);


            // Delete multiple lines
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
