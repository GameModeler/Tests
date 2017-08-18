using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using DataBase.Database.Utils;
using System.Collections.Generic;
using System.Linq;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.RepositoryTests.MySQL
{
    /// <summary>
    /// Description résumée pour Update
    /// </summary>
    [TestClass]
    public class UpdateTest
    {
        private static DataBaseInitilization dataInit = DataBaseInitilization.Instance;

        private static IUniversalContext universalcontext;
        private static IRepository<Book> repository;

        private static Book book1;
        private static Book book2;
        private static Book book3;
        private static Book book4;

        private static List<Book> bookShelve;

        public UpdateTest()
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
        public void TestInitialize() {

            repository = dataInit.getNewRepository<Book>(ProviderType.MySQL);
            universalcontext = repository.Context;

            bool isDbExist = universalcontext.DbContext.Database.Exists();

            if(isDbExist)
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
       

        [TestCleanup()]
        public void TestCleanup() { 
            // Delete test database after tests
            universalcontext.DbContext.Database.Delete();
        }


        #endregion

        [TestMethod]
        public void UpdateSingleTest()
        {
            book1.Author = "Stephan King";
            book1.Title = "The Dark Tower";

            int updateResult = repository.Update(book1);

            Assert.AreEqual(1, updateResult);

            // Query from DbSet
            Book updatedBookTitle = repository.DbSet.Where(b => b.Title == "The Dark Tower")
                                                    .FirstOrDefault();

            Assert.AreEqual("The Dark Tower", updatedBookTitle.Title);

           Book book = universalcontext.DbContext.Database.SqlQuery<Book>(
                       "SELECT * FROM Books WHERE BookId=1").FirstOrDefault<Book>();

            Assert.AreEqual("The Dark Tower", book.Title);
        }

        [TestMethod]
        public void UpdateMultipleTest()
        {

            book2.Author = "Robert Charles Wilson";
            book2.Title = "Spin";

            book3.Author = "Dan Simmons";
            book3.Title = "Hyperion";

            List<Book> newBookShelve = new List<Book>();
            newBookShelve.Add(book2);
            newBookShelve.Add(book3);

            int updateResult = repository.Update(newBookShelve);

            Assert.AreEqual(2, updateResult);

            // Query from DbSet
            Book updatedBookTitle = repository.DbSet.Where(b => b.Title == "Spin")
                                                    .FirstOrDefault();

            Assert.AreEqual("Spin", updatedBookTitle.Title);

            Book spinBook = universalcontext.DbContext.Database.SqlQuery<Book>(
                        "SELECT * FROM Books WHERE BookId=2").FirstOrDefault<Book>();

            Book hyperionBook = universalcontext.DbContext.Database.SqlQuery<Book>(
            "SELECT * FROM Books WHERE BookId=3").FirstOrDefault<Book>();

            Assert.AreEqual("Spin", spinBook.Title);
            Assert.AreEqual("Dan Simmons", hyperionBook.Author);
        }
    }
}
