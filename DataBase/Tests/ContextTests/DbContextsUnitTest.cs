using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database;
using DataBase.Database.DbSettings.DbClasses;
using DataBase.Database.DbSettings;
using DataBase.Database.Repositories;
using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.Repositories.Interfaces;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Tests.ContextTests
{
    [TestClass]
    public class DbContextsUnitTest
    {
        private DbManager dbManager = DbManager.Instance;

        private IUniversalContext context1;
        private IUniversalContext context2;

        private IGlobalContext globalContext;
        private MySqlDatabase dbTest1;
        private MySqlDatabase dbTest2;

        public DbContextsUnitTest()
        {
            dbTest1 = DatabaseFactory
                .MySqlDb
                .Set
                .DatabaseName("test_db")
                .Server("test_server")
                .UserId("root")
                .ToMySqlDatabase;

            dbTest2 = DatabaseFactory
                 .MySqlDb
                 .Set
                 .DatabaseName("test_db2")
                 .Server("test_server")
                 .UserId("root")
                 .ToMySqlDatabase;

            context1 = dbManager.CreateContext(dbTest1);
            context2 = dbManager.CreateContext(dbTest2);

            globalContext = dbManager.CreateGlobalContext();
        }

        [TestMethod]
        public void AddTest()
        {

            globalContext.Add(context1);

            var globalContextList = globalContext.Contexts;
            Assert.AreEqual(1, globalContextList.Count);

            globalContext.Add(context2);
            globalContextList = globalContext.Contexts;
            Assert.AreEqual(2, globalContextList.Count);

        }

        [TestMethod]
        public void EntityTest()
        {
            var repoTest = globalContext.Entity<Book>();
            Assert.IsInstanceOfType((object)repoTest, typeof(IGlobalRepository<Book>));
        }
    }
}
