using DataBase.Database.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DataBase.Entities.Inheritance
{
    //[Persistent("inherit_test")]
    //[Table("B")]
    public class B : A
    {
        [Key]
        public int BId { get; set; }

        public string BName { get; set; }

        public E e { get; set; }

        public B() { }
        public B(string name, string aName, int aNumber) : base(aName, aNumber)
        {
            BName = name;
        }
    }
}
