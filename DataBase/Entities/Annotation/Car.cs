using DataBase.Database.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DataBase.Entities.Annotation
{
    [Persistent("db_Cars")]
    public class Car
    {

        [Key]
        public int CarId { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Manufacturer { get; set; }
    }
}
