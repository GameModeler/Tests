using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.DbSettings;
using DataBase.Database.DbSettings.DbClasses;
using System.Linq;
using Tests.DataBase.Entities.Mapping;

namespace Tests.DataBase.Tests.RepositoryTests.SQLite
{
    /// <summary>
    /// Description résumée pour Mappings
    /// </summary>
    [TestClass]
    public class Mappings
    {

        private static IList<Student> students;
        private static DatabaseMappingInit dataInit = new DatabaseMappingInit();
        private static SqLiteDatabase sqliteDb;

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

        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) {

            // Get students
            students = dataInit.Students;

            // Init database
            sqliteDb = dataInit.SqLiteDbTest;
        }

        #endregion

        [TestMethod]
        public void TestOneToOneMapping()
        {

            using (var context = DatabaseFactory.CreateContext(sqliteDb))
            {
                var repo = context.Entity<Student>();
                repo.Insert(students);

                IList<Student> students2 = context.Entity<Student>().DbSet.ToList();

                Student Marie = students2.Where<Student>(stu => stu.StudentName == "Marie").FirstOrDefault<Student>();
                Assert.AreEqual("14 rue des Alizés", Marie.Address.Address1);

                // Suppression de la base de données
                dataInit.clearTable(context);
            }
        }

        [TestMethod]
        public void TestOneToManyMapping()
        {
            using (var context = DatabaseFactory.CreateContext(sqliteDb))
            {
                var repo = context.Entity<Student>();
                repo.Insert(students);

                IList<Student> students2 = context.Entity<Student>().DbSet.ToList();

                Student Julie = students2.Where<Student>(stu => stu.StudentName == "Julie").FirstOrDefault<Student>();
                Assert.AreEqual("standard_2", Julie.Standard.Description);

                // Suppression de la base de données
                dataInit.clearTable(context);
            }
        }

        [TestMethod]
        public void TestManyToManyMapping()
        {
            using (var context = DatabaseFactory.CreateContext(sqliteDb))
            {
                var repo = context.Entity<Student>();
                repo.Insert(students);

                IList<Student> students2 = context.Entity<Student>().DbSet.ToList();

                Student Tom = students2.Where<Student>(stu => stu.StudentName == "Tom").FirstOrDefault<Student>();
                Assert.AreEqual(4, Tom.Courses.Count);

                IList<Course> courses = context.Entity<Course>().DbSet.ToList();
                Course Francais = courses.Where<Course>(cou => cou.CourseName == "Français").FirstOrDefault<Course>();
                Assert.AreEqual(3, Francais.Students.Count);

                // Suppression de la base de données            
                dataInit.clearTable(context);
            }
        }
    }
}
