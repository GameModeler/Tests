using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using DataBase.Dynamic;
using DataBase.Sql;
using System;
using System.Collections.Generic;
using System.Dynamic;
using Tests.DataBase.Entities;

namespace Tests.DataBase.Dynamic
{
    [TestClass()]
    public class DynamicManagerTests
    {

        string user = "root";
        string pwd = string.Empty;

        public static IDictionary<string, object> example()
        {
            var dict_to_compare = new ExpandoObject() as IDictionary<string, object>;
            dict_to_compare["BookId"] = "0";
            dict_to_compare["Title"] = "Django tutorial";
            dict_to_compare["Year"] = "2017";
            dict_to_compare["Author"] = "Python";
            dict_to_compare["Is_active"] = "False";

            return dict_to_compare;
        }

        [TestMethod()]
        public void CreateObjectByDatabaseTest()
        {
            string database = "test10";

            string script = SqlManager.ConvertObjectInScript<Book>(Book.example_book(), false, database, true, true);
            var dict = new ExpandoObject() as IDictionary<string, object>;
            dict = DynamicManager.CreateObjectByDatabase(user, pwd, database, "book");

            var dict_to_compare = example();

            bool result = true;
            foreach(var t in dict_to_compare)
            {
                if ((string) t.Value != (string) dict[t.Key])
                {
                    result = false;
                }
            }

            // Deleted Database
            SqlManager.ExecuteStringSql("DROP DATABASE " + database, user, pwd);

            if (result == true)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateMySqlCommandArrayTest()
        {
            string database = "test12";

            SqlManager.ConvertObjectInScript<Book>(Book.example_book(), false, database, true, true);

            string myConnectionString = "server=127.0.0.1;Uid= " + user + ";Pwd= " + pwd + ";Database= " + database + ";";
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn = new MySqlConnection(myConnectionString);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            string query = "USE " + database + "; SELECT * FROM " + "book";
            string[] array = DynamicManager.CreateMySqlCommandArray(query, conn, true);

            string[] array_to_compare = new string[] { "0", "Django tutorial", "2017", "Python", "False" };


            bool result = true;
            for (int i = 0; i < array.Length; i++)
            {
                if ((string)array_to_compare[i] != (string)array[i])
                {
                    Console.WriteLine(array_to_compare[i] + " " + array[i] + " // type "+ array_to_compare[i].GetType() + " "+array[i].GetType());
                    result = false;
                }
            }

            // Deleted Database
            SqlManager.ExecuteStringSql("DROP DATABASE " + database, user, pwd);

            if (result == true)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateMySqlCommandDictTest()
        {
            string user = "root";
            string pwd = "";
            string database = "test11";

            SqlManager.ConvertObjectInScript<Book>(Book.example_book(), false, database, true, true);

            string myConnectionString = "server=127.0.0.1;Uid= " + user + ";Pwd= " + pwd + ";Database= " + database + ";";
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn = new MySqlConnection(myConnectionString);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            string query = "USE " + database + "; SELECT * FROM " + "book";
            Dictionary<string, string> dict = DynamicManager.CreateMySqlCommandDict(query, conn, true);

            var dict_to_compare = example();

            bool result = true;
            foreach (var t in dict_to_compare)
            {
                if ((string) t.Value != (string) dict[t.Key])
                {
                    result = false;
                }
            }

            // Deleted Database
            SqlManager.ExecuteStringSql("DROP DATABASE " + database, user, pwd);

            if (result == true)
            {
                return;
            }
            Assert.Fail();
        }
    }
}