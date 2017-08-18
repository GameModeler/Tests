using DataBase.Database.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.DataBase.Entities.Mapping
{
    [Persistent("base_test_mapping")]
    [Table("Stu")]
    public class Student
    {
        public Student() { }

        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public virtual StudentAddress Address { get; set; }

        public Student(string studentName, StudentAddress adress)
        {
            this.Address = adress;
            this.StudentName = studentName;
        }

        public virtual Standard Standard { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }

    [Persistent("base_test_mapping")]
    public class StudentAddress
    {
        // One To One
        [ForeignKey("Student")]
        public int StudentAddressId { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual Student Student { get; set; }
    }

    [Persistent("base_test_mapping")]
    public class Standard
    {
        public Standard()
        {
            Students = new List<Student>();
        }

        public Standard(string description)
        {
            Description = description;
            Students = new List<Student>();
        }

        public int StandardId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }

    [Persistent("base_test_mapping")]
    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
        }

        public Course(string courseName)
        {
            this.Students = new HashSet<Student>();
            CourseName = courseName;
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
