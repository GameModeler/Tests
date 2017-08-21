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
    //[Table("E")]
    public class E : B
    {
        [Key]
        public int EId { get; set; }

        public string EName { get; set; }

        public virtual ICollection<B> Bs { get; set; }

        public virtual F f { get; set; }

        public E() { }
        public E(string name, string bName, string aName, int aNumber) : base(bName, aName, aNumber)
        {
            EName = name;
        }
    }
}
