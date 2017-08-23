using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Yaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Yaml
{
    [TestClass()]
    public class YamlManagerTests
    {
        [TestMethod()]
        public void WriteToYamlFileTest()
        {
            Book book = Book.example_book();
            YamlManager.WriteToYamlFile<Book>(Book.path(), "test.yaml", book);
            String text = System.IO.File.ReadAllText(Book.path() + "test.yaml").Replace("\n", "").Replace(" ", "").Replace("\r", "");
            Assert.AreEqual("%YAML1.2---!Tests.DataBase.Entities.BookIs_active:FalseTitle:DjangotutorialAuthor:PythonYear:2017BookId:0...", text);
        }

        [TestMethod()]
        public void ReadFromYamlFileTest()
        {
            Book yaml_book = YamlManager.ReadFromYamlFile<Book>(Book.path(), "test.yaml");
            Book book = Book.example_book();
            Assert.AreEqual(book, yaml_book);
        }
    }
}