using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger.Loggers;
using Logger.Utils;

namespace Tests.LoggerTest.Utils
{
    /// <summary>
    /// Description résumée pour LoggerUtilsTest
    /// </summary>
    [TestClass]
    public class LoggerUtilsTest
    {
        private LoggerManager manager;

        public LoggerUtilsTest()
        {
            manager = new LoggerManager();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active, ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestIsALoggerName()
        {
            manager.CreateLogger("Logger1");
            manager.CreateLogger("Logger2");
            manager.CreateLogger("Logger3");

            var response = manager.Loggers.IsALoggerName("Logger1");

            Assert.IsTrue(response);

            var response2 = manager.Loggers.IsALoggerName("Logger4");

            Assert.IsFalse(response2);

        }

        [TestMethod]
        public void TestIsAnAppenderName()
        {
           var logger = manager.CreateLogger("Logger1");

            logger.AddAppender(AppenderType.CONSOLE, "console_appender1");
            logger.AddAppender(AppenderType.CONSOLE, "console_appender2");
            logger.AddAppender(AppenderType.DATABASE, "db_appender");

            var res = logger.AppenderManager.AppenderList.IsAnAppenderName("console_appender1");

            Assert.IsTrue(res);

            var res2 = logger.AppenderManager.AppenderList.IsAnAppenderName("console_appender3");

            Assert.IsFalse(res2);

        }
    }
}
