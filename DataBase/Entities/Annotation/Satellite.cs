using DataBase.Database.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.DataBase.Entities.Annotation
{
    [Persistent("db_Satellite")]
    [Table("Star")]
    public class Satellite
    {

        [Key]
        public string Name { get; set; }

        public int Distance { get; set; }

        public int Rayon { get; set; }

        public Satellite(string name, int distance, int rayon)
        {
            Name = name;
            Distance = distance;
            Rayon = rayon;
        }

        public Satellite() { }
    }
}
