using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.DbSettings;
using DataBase.Database.DbSettings.DbClasses;
using Tests.DataBase.Entities.Inheritance;

namespace Tests.DataBase.Tests.RepositoryTests.GlobalContext
{
    /// <summary>
    /// Description résumée pour Inheritance
    /// </summary>
    [TestClass]
    public class Inheritance
    {
        // Initialization
        private DatabaseInheritanceInit dataInit = new DatabaseInheritanceInit();
        private static SqLiteDatabase sqliteDb;
        private static MySqlDatabase mysqlDb;

        #region Attributs de tests supplémentaires


        [TestInitialize()]
        public void MyTestInitialize() {

            // Init database
            mysqlDb = dataInit.MySQLDbTest;
            sqliteDb = dataInit.SqLiteDbTest;
        }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        [TestCleanup()]
        public void MyTestCleanup() {
        }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            using(var globalContext = DatabaseFactory.CreateGlobalContext())
            {

                var mySqlContext = DatabaseFactory.CreateContext(mysqlDb);
                //var sqliteContext = DatabaseFactory.CreateContext(sqliteDb);

                globalContext.Add(mySqlContext);

                var result = globalContext.Entity<B>().Insert(dataInit.AllBs);

                // Suppression de la base de données
                mySqlContext.DbContext.Database.Delete();
            }
        }
    }
}
