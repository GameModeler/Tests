using DataBase.Database.Utils;
using System.ComponentModel.DataAnnotations;

namespace Tests.DataBase.Entities
{
    [Persistent]
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string Author { get; set; }

        public Book(string title, int year, string author)
        {
            Title = title;
            Year = year;
            Author = author;
        }

        public Book() { }
    }
    
}
