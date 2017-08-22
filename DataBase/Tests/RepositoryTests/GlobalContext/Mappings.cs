using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataBase.Database.DbSettings.DbClasses;
using System.Linq;
using DataBase.Database.DbSettings;
using Tests.DataBase.Entities.Mapping;

namespace Tests.DataBase.Tests.RepositoryTests.GlobalContext
{
    [TestClass]
    public class Mappings
    {
        private static IList<Student> students;
        private static DatabaseMappingInit dataInit = new DatabaseMappingInit();
        private static SqLiteDatabase sqliteDb;
        private static MySqlDatabase mysqlDb;

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

        [TestMethod]
        public void TestOneToOneMapping()
        {

            using (var context = DatabaseFactory.CreateGlobalContext())
            {
                var sqliteContext = DatabaseFactory.CreateContext(sqliteDb);
                var mysqlContext = DatabaseFactory.CreateContext(mysqlDb);

                context.Add(sqliteContext).Add(mysqlContext);

                context.Entity<Student>().Insert(students);

                IList<Student> studentsMysql = mysqlContext.Entity<Student>().DbSet.ToList();
                IList<Student> studentsSqlite = sqliteContext.Entity<Student>().DbSet.ToList();

                Student Marie = studentsMysql.Where<Student>(stu => stu.StudentName == "Marie").FirstOrDefault<Student>();
                Student Marie2 = studentsSqlite.Where<Student>(stu => stu.StudentName == "Marie").FirstOrDefault<Student>();

                Assert.AreEqual("14 rue des Alizés", Marie.Address.Address1);
                Assert.AreEqual("14 rue des Alizés", Marie2.Address.Address1);

                // Suppression de la base de données
                mysqlContext.DbContext.Database.Delete();
                dataInit.clearTable(sqliteContext);
            }
        }

        [TestMethod]
        public void TestOneToManyMapping()
        {
            using (var context = DatabaseFactory.CreateGlobalContext())
            {
                var sqliteContext = DatabaseFactory.CreateContext(sqliteDb);
                var mysqlContext = DatabaseFactory.CreateContext(mysqlDb);

                context.Add(sqliteContext).Add(mysqlContext);

                context.Entity<Student>().Insert(students);

                IList<Student> studentsSqlite = sqliteContext.Entity<Student>().DbSet.ToList();
                IList<Student> studentsMysql = mysqlContext.Entity<Student>().DbSet.ToList();

                Student Julie = studentsMysql.Where<Student>(stu => stu.StudentName == "Julie").FirstOrDefault<Student>();
                Student Julie2 = studentsSqlite.Where<Student>(stu => stu.StudentName == "Julie").FirstOrDefault<Student>();


                Assert.AreEqual("standard_2", Julie.Standard.Description);
                Assert.AreEqual("standard_2", Julie2.Standard.Description);

                // Suppression de la base de données
                mysqlContext.DbContext.Database.Delete();
                dataInit.clearTable(sqliteContext);
            }
        }

        [TestMethod]
        public void TestManyToManyMapping()
        {
            using (var context = DatabaseFactory.CreateGlobalContext())
            {

                var sqliteContext = DatabaseFactory.CreateContext(sqliteDb);
                var mysqlContext = DatabaseFactory.CreateContext(mysqlDb);

                context.Add(sqliteContext).Add(mysqlContext);

                context.Entity<Student>().Insert(students);

                IList<Student> studentsSqlite = sqliteContext.Entity<Student>().DbSet.ToList();
                IList<Student> studentsMysql = mysqlContext.Entity<Student>().DbSet.ToList();

                Student Tom = students.Where<Student>(stu => stu.StudentName == "Tom").FirstOrDefault<Student>();
                Student Tom2 = students.Where<Student>(stu => stu.StudentName == "Tom").FirstOrDefault<Student>();

                Assert.AreEqual(4, Tom.Courses.Count);
                Assert.AreEqual(4, Tom2.Courses.Count);

                IList<Course> coursesSqlite = sqliteContext.Entity<Course>().DbSet.ToList();
                IList<Course> coursesMySql = mysqlContext.Entity<Course>().DbSet.ToList();

                Course Francais = coursesSqlite.Where<Course>(cou => cou.CourseName == "Français").FirstOrDefault<Course>();
                Course Francais2 = coursesMySql.Where<Course>(cou => cou.CourseName == "Français").FirstOrDefault<Course>();

                Assert.AreEqual(3, Francais.Students.Count);
                Assert.AreEqual(3, Francais2.Students.Count);

                // Suppression de la base de données
                mysqlContext.DbContext.Database.Delete();
                dataInit.clearTable(sqliteContext);
            }
        }
    }
}
