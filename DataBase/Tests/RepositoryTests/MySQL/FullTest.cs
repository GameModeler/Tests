using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using System.Linq;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.RepositoryTests.MySQL
{
    /// <summary>
    /// Description résumée pour FullTest
    /// </summary>
    [TestClass]
    public class FullTest
    {

        private DataBaseInit dataInit = new DataBaseInit();

        private static IUniversalContext mySqlContext;
        private static IRepository<Book> repository;


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
        }

        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        [TestInitialize()]
        public void MyTestInitialize() {

            repository = dataInit.getRepositoryMySql<Book>();

            mySqlContext = dataInit.MySqlContext;
            
            bookShelve = dataInit.BookShelve;
          
        }

        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        [TestCleanup()]
        public void MyTestCleanup() {
            mySqlContext.DbContext.Database.Delete();
        }

        #endregion

        [TestMethod]
        public void MySQLFullTest()
        {
            // Insert multiple lies
            var insertMultiResult = repository.Insert(bookShelve);

            Assert.AreEqual(4, insertMultiResult);


            // Update 1 line
            var book1 = dataInit.Book1;
            book1.Title = "Mistborn";
            book1.Year = 2017;

            var updateSingleResult = repository.Update(book1);

            Assert.AreEqual(1, updateSingleResult);

            // Query from DbSet
            const string MISTBORN = "Mistborn";

            Book updatedBookMySql = repository.DbSet.Where(b => b.Title == MISTBORN)
                                                    .FirstOrDefault();

            Assert.AreEqual(MISTBORN, updatedBookMySql.Title);

            Book bookmysql = mySqlContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=1").FirstOrDefault<Book>();

            Assert.AreEqual(MISTBORN, bookmysql.Title);

            // Get All

            var getAllResult = repository.Get();

            Assert.AreEqual(4, getAllResult.ToList().Count);

            List<Book> dbSetSelectedBookMysql = repository.DbSet.ToList<Book>();

            Assert.AreEqual(4, dbSetSelectedBookMysql.Count);
   

            // Insert 1 line
            const string AMERICAN_GODS = "American Gods";

            Book book5 = new Book("American Gods", 1998, "Neil Gaiman");

            var insertSingleResult = repository.Insert(book5);

            // Get book 1 from the database
            var bookmysqlInserted = repository.DbSet
                                              .SqlQuery("Select * from Books where Title=@p0", AMERICAN_GODS)
                                              .FirstOrDefault<Book>();


            // Assert the insert is effectued
            Assert.AreEqual(1, insertSingleResult);
            Assert.AreEqual(AMERICAN_GODS, bookmysqlInserted.Title);
 
            // Delete 1 line

            var deleteOneResult = repository.Delete(dataInit.Book1);

            Assert.AreEqual(1, deleteOneResult);

            Book deletedBookMysql = repository.DbSet.Where(b => b.BookId == 1).FirstOrDefault();

            Assert.IsNull(deletedBookMysql);

            // Get 1 line

            var getOneResult = repository.Get(2);

            const string WORDS_OF_RADIANCE = "Words Of Radiance";
            const string THE_NAME_OF_THE_WIND = "The Name Of The Wind";

            Assert.AreEqual(2, getOneResult.BookId);
            Assert.AreEqual(WORDS_OF_RADIANCE, getOneResult.Title);

            var selectedBook4 = repository.Get(dataInit.Book4.BookId);

            Assert.AreEqual(4, selectedBook4.BookId);
            Assert.AreEqual(THE_NAME_OF_THE_WIND, selectedBook4.Title);
    

            // Update multiple lines

            dataInit.Book2.Author = "Robert Charles Wilson";
            dataInit.Book2.Title = "Spin";

            dataInit.Book3.Author = "Dan Simmons";
            dataInit.Book3.Title = "Hyperion";

            List<Book> newBookShelve = new List<Book>();
            newBookShelve.Add(dataInit.Book2);
            newBookShelve.Add(dataInit.Book3);

            var updateResult = repository.Update(newBookShelve);

            Assert.AreEqual(2, updateResult);

            // Query from DbSet
            Book updatedMultiBookMysql = repository.DbSet.Where(b => b.Title == "Spin")
                                                   .FirstOrDefault();

            Assert.AreEqual("Spin", updatedMultiBookMysql.Title);

            Book spinBookMysql = mySqlContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=2").FirstOrDefault<Book>();
   
            Book hyperionBookMysql = mySqlContext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=3").FirstOrDefault<Book>();

            Assert.AreEqual("Spin", spinBookMysql.Title);
            Assert.AreEqual("Dan Simmons", hyperionBookMysql.Author);

            // Delete multiple lines
            List<Book> booksToDelete = new List<Book>();

            booksToDelete.Add(dataInit.Book2);
            booksToDelete.Add(dataInit.Book4);

            // Delete Book 1
            var result = repository.Delete(booksToDelete);

            Assert.AreEqual(2, result);

            Book deletedBookMysql1 = repository.DbSet.Where(b => b.BookId == 2).FirstOrDefault();
            Book deletedBookMysql2 = repository.DbSet.Where(b => b.BookId == 4).FirstOrDefault();
        
            Assert.IsNull(deletedBookMysql1);
            Assert.IsNull(deletedBookMysql2);
            
        }
    }
}
