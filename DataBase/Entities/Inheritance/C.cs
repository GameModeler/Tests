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
    //[Table("C")]
    public class C : A
    {
        [Key]
        public int CId { get; set; }

        public string CName { get; set; }

        public virtual ICollection<D> Ds { get; set; }

        public C() { }

        public C(string name, string aName, int aNumber) : base(aName, aNumber)
        {
            CName = name;
        }
    }
}
