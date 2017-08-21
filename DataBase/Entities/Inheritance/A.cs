using DataBase.Database.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.DataBase.Entities.Inheritance
{
    //[Persistent("inherit_test")]
    //[Table("A")]
    public abstract class A
    {

        public int AId {get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        //public int D_Id { get; set; }

        public A() {}
        public A(string name, int number)
        {
            Name = name;
            Number = number;
        }
    }
}
