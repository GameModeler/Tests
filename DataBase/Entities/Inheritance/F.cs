using DataBase.Database.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.DataBase.Entities.Inheritance
{
    //[Persistent("inherit_test")]
    //[Table("F")]
    public class F
    {

        [Key, ForeignKey("e")]
        public int FId { get; set; }

        public string FName { get; set; }

        public virtual E e { get; set; }

        public F() {}

        public F(string name)
        {
            FName = name;
        }
    }
}
