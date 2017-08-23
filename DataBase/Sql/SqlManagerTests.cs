using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Sql
{
    [TestClass()]
    public class SqlManagerTests
    {
        [TestMethod()]
        public void ConvertObjectInScriptTest()
        {
            Book book = Book.example_book();
            string script = SqlManager.ConvertObjectInScript<Book>(book, false, "test2", true);
            Console.WriteLine(script.Replace("\n", ""));
            string script_to_compare = "CREATE DATABASE test2;USE test2;CREATE TABLE Book (BookId INT(11),Title VARCHAR(255),Year INT(11),Author VARCHAR(255),Is_active TINYINT(1));INSERT INTO Book VALUES(0,'Django tutorial',2017,'Python',False);";
            Assert.AreEqual(script_to_compare, script.Replace("\n", "").Replace("\r", ""));
        }

        [TestMethod()]
        public void ExecuteStringSqlTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteToSqlFileTest()
        {
            Assert.Fail();
        }
    }
}