using DataBase.Database.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DataBase.Entities.Annotation
{
    [Persistent("db_Cats")]
    public class Cat
    {
        [Key]
        public int CatId { get; set; }

        public string Color { get; set; }

        public int Year { get; set; }

        public string Name { get; set; }

        public Cat(string name, int year, string color)
        {
            Name = name;
            Year = year;
            Color = color;
        }

        public Cat() { }

    }
}
