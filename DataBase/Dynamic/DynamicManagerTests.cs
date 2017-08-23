using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace Tests.DataBase.Dynamic
{
    [TestClass()]
    public class DynamicManagerTests
    {
        [TestMethod()]
        public void CreateObjectByDatabaseTest()
        {
            dynamic dynamicObj = new ExpandoObject();
            var dic = dynamicObj as IDictionary<string, object>;
            dic = DynamicManager.CreateObjectByDatabase("root", "", "test", "class");
        }

        [TestMethod()]
        public void CreateMySqlCommandArrayTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateMySqlCommandDictTest()
        {
            Assert.Fail();
        }
    }
}