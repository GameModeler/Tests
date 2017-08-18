using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database;
using DataBase.Database.DbSettings.DbClasses;
using DataBase.Database.DbSettings;
using DataBase.Database.Repositories;
using System.Data.Entity;
using DataBase.Database.DbContexts.Interfaces;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.ContextTests
{
    [TestClass]
    public class UniversalContextTest
    {
        private DbManager dbManager = DbManager.Instance;

        private IUniversalContext context;
        private MySqlDatabase dbTest;

        private Book book1;
        private Book book2;
        private Book book3;
        private Book book4;

        public UniversalContextTest()
        {
           dbTest = DatabaseFactory
                .MySqlDb
                .Set
                .DatabaseName("test_db")
                .Server("test_server")
                .UserId("root")
                .ToMySqlDatabase;


            context = dbManager.CreateContext(dbTest);

            book1 = new Book("The Way Of King", 2013, "Brandon Sanderson");
            book2 = new Book("Words Of Radiance", 2015, "Brandon Sanderson");
            book3 = new Book("The Lies Of Lock Lamora", 2009, "Scott Lynch");
            book4 = new Book("The Name Of The Wind", 2007, "Patrick Rothfuss");
        }

        //[TestMethod]
        //public void SetTest()
        //{
        //    var dbSetTest = context.Set<Book>();
        //    Assert.IsInstanceOfType(dbSetTest, typeof(DbSet<Book>));
        //}

        //[TestMethod]
        //public void EntityTest()
        //{
        //    var repoTest = context.Entity<Book>();
        //    Assert.IsInstanceOfType(repoTest, typeof(Repository<Book>));
        //}
    }
}
