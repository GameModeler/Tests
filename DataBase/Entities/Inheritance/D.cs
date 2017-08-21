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
    //[Table("D")]
    public class D : B
    {
        [Key]
        public int DId { get; set; }
        public string DName { get; set; }

        public virtual ICollection<C> Cs { get; set; }
        public virtual ICollection<A> As { get; set; }

        public D() { }
        public D(string name, string bName, string aName, int aNumber) : base(bName, aName, aNumber)
        {
            DName = name;
        }
    }
}
