using DataBase.Database;
using DataBase.Database.DbContexts.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.DataBase.Entities.Annotation;

namespace Tests.DataBase.Tests.Annotation
{
    /// <summary>
    /// Description résumée pour Persistance
    /// </summary>
    [TestClass]
    public class Persistance
    {
        private DataAnnotationInit dataInit = new DataAnnotationInit();

        private DbManager dbManager = DbManager.Instance;

        private static IUniversalContext mySqlContextCars;
        private static IUniversalContext sqliteContextCars;

        private static IUniversalContext mySqlContextCats;
        private static IUniversalContext sqliteContextCats;

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
        public static void MyClassCleanup()
        {
            mySqlContextCars.DbContext.Database.Delete();
            mySqlContextCats.DbContext.Database.Delete();
            sqliteContextCars.DbContext.Database.ExecuteSqlCommand("DELETE FROM Cars");
            sqliteContextCats.DbContext.Database.ExecuteSqlCommand("DELETE FROM Cats");
        }

        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        [TestInitialize()]
        public void MyTestInitialize() {

            mySqlContextCars = dataInit.MySqlContextCars;
            sqliteContextCars = dataInit.SqliteContextCars;

            mySqlContextCats = dataInit.MySqlContextCats;
            sqliteContextCats = dataInit.SqliteContextCats;

        }

        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        [TestCleanup()]
        public void MyTestCleanup() {

            mySqlContextCars.DbContext.Database.Delete();
            mySqlContextCats.DbContext.Database.Delete();
            sqliteContextCars.DbContext.Database.ExecuteSqlCommand("DELETE FROM Cars");
            sqliteContextCats.DbContext.Database.ExecuteSqlCommand("DELETE FROM Cats");
        }

        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            using(var context1 = dbManager.CreateGlobalContext())
            {

                var repo1 = context1.Add(mySqlContextCats).Add(sqliteContextCats).Entity<Cat>();
               
                repo1.Insert(dataInit.CatsHouse);

                // MySql
                var mysqltable1 = mySqlContextCats.DbContext.Database.ExecuteSqlCommand("SELECT 1 FROM cats LIMIT 1;");
                Assert.AreEqual(-1, mysqltable1);

                var mysqlTable3 = mySqlContextCats.DbContext.Database.ExecuteSqlCommand("SELECT 1 FROM books LIMIT 1;");
                Assert.AreEqual(-1, mysqlTable3);

                try
                {
                    var mysqltable2 = mySqlContextCats.DbContext.Database.ExecuteSqlCommand("SELECT 1 FROM cars LIMIT 1;");
                    Assert.Fail("MySqlException should be thrown");
                }
                catch (Exception ex)
                {
                    Assert.AreEqual("La table 'db_cats.cars' n'existe pas", ex.Message);
                }
            }

            using (var context2 = dbManager.CreateGlobalContext())
            {
                var repo2 = context2.Add(mySqlContextCars).Add(sqliteContextCars).Entity<Car>();
                
                repo2.Insert(dataInit.Parking);

                var mysqltable1 = mySqlContextCars.DbContext.Database.ExecuteSqlCommand("SELECT 1 FROM cars LIMIT 1;");
                Assert.AreEqual(-1, mysqltable1);

                var mysqlTable3 = mySqlContextCars.DbContext.Database.ExecuteSqlCommand("SELECT 1 FROM books LIMIT 1;");
                Assert.AreEqual(-1, mysqlTable3);

                try
                {
                    var mysqltable2 = mySqlContextCars.DbContext.Database.ExecuteSqlCommand("SELECT 1 FROM cats LIMIT 1;");
                    Assert.Fail("MySqlException should be thrown");
                }
                catch (Exception ex)
                {
                    Assert.AreEqual("La table 'db_cars.cats' n'existe pas", ex.Message);
                }
            }          
        }
    }
}
