using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Sql;
using DataBase.Dynamic;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Dynamic;
using Tests.DataBase.Dynamic;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Sql
{
    [TestClass()]
    public class SqlManagerTests
    {
        string user = "root";
        string pwd = string.Empty;

        [TestMethod()]
        public void ConvertObjectInScriptTest()
        {
            string database = "test7";

            Book book = Book.example_book();
            string script = SqlManager.ConvertObjectInScript<Book>(book, false, database, true, true, user, pwd);
            string script_to_compare = "CREATE DATABASE " + database + ";USE " + database + ";CREATE TABLE Book (BookId INT(11),Title VARCHAR(255),Year INT(11),Author VARCHAR(255),Is_active TINYINT(1));INSERT INTO Book VALUES(0,'Django tutorial',2017,'Python',False);";
            SqlManager.ExecuteStringSql("DROP DATABASE " + database, user, pwd);
            Assert.AreEqual(script_to_compare, script.Replace("\n", "").Replace("\r", ""));
        }

        [TestMethod()]
        public void ExecuteStringSqlTest()
        {
            string database = "test8";
            SqlManager.ExecuteStringSql("CREATE DATABASE " + database, user, pwd);

            string myConnectionString = "server=127.0.0.1;Uid= " + user + ";Pwd= " + pwd + ";";
            try
            {
                MySqlConnection conn = new MySqlConnection(myConnectionString);
                conn.Open();
                MySqlCommand myCommand = new MySqlCommand("DROP DATABASE " + database, conn);
                MySqlDataReader rdr = myCommand.ExecuteReader();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail();
            }
            return;
        }

        [TestMethod()]
        public void WriteToSqlFileTest()
        {
            Assert.Fail();
        }
    }
}