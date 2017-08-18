using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Database.DbSettings;
using DataBase.Database.DbSettings.DbClasses;
using System.Linq;
using Tests.DataBase.Entities.Mapping;

namespace Tests.DataBase.Tests.RepositoryTests.MySQL
{
    /// <summary>
    /// Description résumée pour Mappings
    /// </summary>
    [TestClass]
    public class Mappings
    {

        private static IList<Student> students;
        private static DatabaseMappingInit dataInit = new DatabaseMappingInit();
        private static MySqlDatabase mysqlDb;

        #region Attributs de tests supplémentaires
   
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        [TestInitialize()]
        public void MyTestInitialize() {

            // Get students
            students = dataInit.Students;

            // Init database
            mysqlDb = dataInit.MySQLDbTest;
        }

        #endregion

        [TestMethod]
        public void TestOneToOneMapping()
        {

            using ( var context = DatabaseFactory.CreateContext(mysqlDb))
            {
                var repo = context.Entity<Student>();
                repo.Insert(students);

                IList<Student> students2 = context.Entity<Student>().DbSet.ToList();

                Student Marie = students2.Where<Student>(stu => stu.StudentName == "Marie").FirstOrDefault<Student>();
                Assert.AreEqual("14 rue des Alizés", Marie.Address.Address1);

                // Suppression de la base de données
                context.DbContext.Database.Delete();
            }
        }

        [TestMethod]
        public void TestOneToManyMapping()
        {
            using (var context = DatabaseFactory.CreateContext(mysqlDb))
            {
                var repo = context.Entity<Student>();
                repo.Insert(students);

                IList<Student> students2 = context.Entity<Student>().DbSet.ToList();

                Student Julie = students2.Where<Student>(stu => stu.StudentName == "Julie").FirstOrDefault<Student>();
                Assert.AreEqual("standard_2", Julie.Standard.Description);

                // Suppression de la base de données
                context.DbContext.Database.Delete();
            }
        }

        [TestMethod]
        public void TestManyToManyMapping()
        {
            using (var context = DatabaseFactory.CreateContext(mysqlDb))
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
                context.DbContext.Database.Delete();
            }
        }

    }
}
