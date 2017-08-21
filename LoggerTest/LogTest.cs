using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger.Loggers;
using Logger.Interfaces;
using Logger.Utils;

namespace Tests.LoggerTest
{
    /// <summary>
    /// Description résumée pour LogTest
    /// </summary>
    [TestClass]
    public class LogTest
    {
        private LoggerManager loggerManager;

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
        [TestInitialize()]
        public void MyTestInitialize() {

            // 1. Get a LoggerManager
            loggerManager = new LoggerManager();
        }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        /// Test Log contructor
        /// </summary>
        [TestMethod]
        public void TestLog()
        {
            // Test Consol logger appender
            ILogger logger = loggerManager.CreateLogger();

            Log log = new Log(logger, "Log1", Level.INFO, new Exception());

            Assert.IsNotNull(log);
            Assert.AreEqual("GM_LOGGER", log.LoggerName);          

        }
    }
}
