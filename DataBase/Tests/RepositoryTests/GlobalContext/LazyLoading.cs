using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.DbSettings.DbClasses;
using DataBase.Database.DbSettings;
using System.Linq;
using Tests.DataBase.Entities.Mapping;

namespace Tests.DataBase.Tests.RepositoryTests.GlobalContext
{
    /// <summary>
    /// Description résumée pour LazyLoading
    /// </summary>
    [TestClass]
    public class LazyLoading
    {
        private static IList<Student> students;
        private static DatabaseMappingInit dataInit = new DatabaseMappingInit();
        private static SqLiteDatabase sqliteDb;
        private static MySqlDatabase mysqlDb;

        #region Attributs de tests supplémentaires

        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        [TestInitialize()]
        public void MyTestInitialize()
        {

            // Get students
            students = dataInit.Students;

            // Init database
            mysqlDb = dataInit.MySQLDbTest;
            sqliteDb = dataInit.SqLiteDbTest;
        }

        #endregion

        [TestMethod]
        public void LazyLoadingTest()
        {

            using(var globalContext = DatabaseFactory.CreateGlobalContext())
            {
                var mysqlContext = DatabaseFactory.CreateContext(sqliteDb);
                var sqliteContext = DatabaseFactory.CreateContext(mysqlDb);

                // Désactivation du lazy loading
                mysqlContext.EnableLazyLoading = false;
                sqliteContext.EnableLazyLoading = false;

                globalContext.Add(mysqlContext).Add(sqliteContext);

                // Insertion des students
                var repo = globalContext.Entity<Student>();
                repo.Insert(students);

                List<Student> stuMySql = repo.Repositories[mysqlContext].DbSet.ToList();
                List<Student> stuSqlite = repo.Repositories[sqliteContext].DbSet.ToList(); 
         
            }
        }
    }
}
