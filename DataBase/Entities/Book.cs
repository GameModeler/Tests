using DataBase.Database.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tests.DataBase.Entities
{
    [Persistent]
    [Serializable]
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string Author { get; set; }

        public bool Is_active { get; set; }

        public Book(string title, int year, string author)
        {
            Title = title;
            Year = year;
            Author = author;
            Is_active = true;
        }

        public Book(string title, int year, string author, bool is_active)
        {
            Title = title;
            Year = year;
            Author = author;
            Is_active = is_active;
        }

        public Book() { }

        public override bool Equals(object obj)
        {
            if (obj is Book)
            {
                var that = obj as Book;
                return this.BookId == that.BookId && this.Title == that.Title && this.Year == that.Year && this.Author == that.Author && this.Is_active == that.Is_active;
            }

            return false;
        }

        public static Book example_book()
        {
            return new Book("Django tutorial", 2017, "Python", false);
        }

        public static string path()
        {
            return @"A:\";
        }

    }
    
}
