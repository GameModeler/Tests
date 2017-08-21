using DataBase.Database.DbContexts.Interfaces;
using DataBase.Database.DbSettings;
using DataBase.Database.DbSettings.DbClasses;
using System.Collections.Generic;
using Tests.DataBase.Entities.Mapping;

namespace Tests.DataBase
{
    public class DatabaseMappingInit
    {

        const string SQLITE_DB_PATH = @"C:\Users\Anne\SQLDatabase\sqlite_mapping_test.db";

        private MySqlDatabase mySQLDbTest;
        private SqLiteDatabase sqLiteDbTest;

        public Student Stu1 { get; set; }
        public Student Stu2 { get; set; }
        public Student Stu3 { get; set; }


        public MySqlDatabase MySQLDbTest
        {
            get { return mySQLDbTest; }
        }

        public SqLiteDatabase SqLiteDbTest
        {
            get { return sqLiteDbTest; }
        }

        public DatabaseMappingInit()
        {

            StudentAddress stu_ad1 = new StudentAddress();
            stu_ad1.Address1 = "14 rue des Alizés";
            stu_ad1.Country = "France";
            stu_ad1.City = "Cesson";
            stu_ad1.Zipcode = 35510;

            StudentAddress stu_ad2 = new StudentAddress();
            stu_ad2.Address1 = "12 rue du Soleil";
            stu_ad2.Country = "France";
            stu_ad2.City = "Laval";
            stu_ad2.Zipcode = 53000;

            StudentAddress stu_ad3 = new StudentAddress();
            stu_ad3.Address1 = "12 abc";
            stu_ad3.Country = "Canada";
            stu_ad3.City = "Gaspe";
            stu_ad3.Zipcode = 12345;

            Standard st1 = new Standard("standard_1");
            Standard st2 = new Standard("standard_2");

            Course francais = new Course("Français");
            Course maths = new Course("Maths");
            Course histoire = new Course("Histoire");
            Course svt = new Course("SVT");

            IList<Course> course_Marie = new List<Course>();
            course_Marie.Add(francais);
            course_Marie.Add(maths);

            IList<Course> course_Julie = new List<Course>();
            course_Julie.Add(francais);
            course_Julie.Add(histoire);

            IList<Course> course_Tom = new List<Course>();
            course_Tom.Add(francais);
            course_Tom.Add(svt);
            course_Tom.Add(maths);
            course_Tom.Add(histoire);

            Stu1 = new Student("Marie", stu_ad1);
            Stu2 = new Student("Tom", stu_ad2);
            Stu3 = new Student("Julie", stu_ad3);

            Stu1.Standard = st1;
            Stu2.Standard = st1;
            Stu3.Standard = st2;

            Stu1.Courses = course_Marie;
            Stu2.Courses = course_Tom;
            Stu3.Courses = course_Julie;


            mySQLDbTest = DatabaseFactory.MySqlDb
                                            .Set
                                            .DatabaseName("base_test_mapping")
                                            .Server("localhost")
                                            .UserId("root")
                                            .ToMySqlDatabase;


            sqLiteDbTest = DatabaseFactory.SqLiteDb.Set.DatabaseName("base_test_mapping")
                                                   .DataSource(SQLITE_DB_PATH)
                                                   .ToSqLiteDatabase;
        }

        /// <summary>
        /// Get a new List of Students
        /// </summary>
        public IList<Student> Students
        {
            get
            {
                IList<Student> students = new List<Student>();

                students.Add(Stu1);
                students.Add(Stu2);
                students.Add(Stu3);
        
                return students;
            }
        }

        /// <summary>
        /// Clear tables
        /// </summary>
        /// <param name="context"></param>
        public void clearTable(IUniversalContext context)
        {
            context.DbContext.Database.ExecuteSqlCommand("DELETE FROM Books");
            context.DbContext.Database.ExecuteSqlCommand("DELETE FROM Courses");
            context.DbContext.Database.ExecuteSqlCommand("DELETE FROM Stu");
            context.DbContext.Database.ExecuteSqlCommand("DELETE FROM StudentAddresses");
            context.DbContext.Database.ExecuteSqlCommand("DELETE FROM CourseStudents");
            context.DbContext.Database.ExecuteSqlCommand("DELETE FROM Standards");
        }
    }
}
