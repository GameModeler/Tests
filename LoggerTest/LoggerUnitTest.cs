using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger.Utils;
using Logger.Interfaces;
using Logger.Loggers;

namespace Tests.LoggerTest
{
    /// <summary>
    /// Description résumée pour LoggerUnitTest
    /// </summary>
    [TestClass]
    public class LoggerUnitTest
    {
        private const String LOGGER_NAME = "TEST_LOGGER";

        public LoggerUnitTest()
        {
            loggerTest = (Logger.Loggers.Logger) new LoggerManager().CreateLogger(LOGGER_NAME);
        }

        private Logger.Loggers.Logger loggerTest;


        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active, ainsi que ses fonctionnalités.
        ///</summary>
        public Logger.Loggers.Logger LoggerTest
        {
            get
            {
                return loggerTest;
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
        

        /// <summary>
        /// Add Appender
        /// </summary>
        [TestMethod]
        public void AddAppenderTest()
        {
            AppenderType appenderType = AppenderType.CONSOLE;
            var appender = LoggerTest.AddAppender(appenderType);

            Assert.IsInstanceOfType(appender, typeof(IAppender));
            Assert.AreEqual(appender.AppenderName, "GM_CONSOLE_APPENDER");

        }

        /// <summary>
        /// Add Appender with name
        /// </summary>
        [TestMethod]
        public void AddAppenderWithNameTest()
        {
            AppenderType appenderType = AppenderType.CONSOLE;
            const String APPENDER_NAME = "TEST_APPENDER_NAME";
            var appender = LoggerTest.AddAppender(appenderType, APPENDER_NAME);

            Assert.IsInstanceOfType(appender, typeof(IAppender));
            Assert.AreEqual(appender.AppenderName, APPENDER_NAME);

        }

        /// <summary>
        /// Reset logger
        /// </summary>
        [TestMethod]
        public void ResetTest()
        {
            LoggerTest.Reset();

            Assert.AreEqual(LoggerTest.Level, Level.TRACE);
            Assert.AreEqual(LoggerTest.AppenderManager.AppenderList.Count, 0);
        }

        /// <summary>
        /// Remove Appender
        /// </summary>
        [TestMethod]
        public void RemoveAppenderTest()
        {
            var consoleAppender = LoggerTest.AddAppender(AppenderType.CONSOLE);
            var toastAppender = LoggerTest.AddAppender(AppenderType.TOAST);
            var dataBaseAppender = LoggerTest.AddAppender(AppenderType.DATABASE);

            Assert.AreEqual(LoggerTest.AppenderManager.AppenderList.Count, 3);
            LoggerTest.RemoveAppender(consoleAppender);

            Assert.AreEqual(LoggerTest.AppenderManager.AppenderList.Count, 2);

        }

        //[TestMethod]
        //public void CallAppenderTest()
        //{
        //    var consoleAppender = LoggerTest.AddAppender(AppenderType.CONSOLE);
        //    var dbAppender = LoggerTest.AddAppender(AppenderType.DATABASE);

        //    Log theLog = new Log();

        //    LoggerTest.CallAppenders(theLog);

        //}

        //[TestMethod]
        //public void LogTest()
        //{

        //    var consoleAppender = LoggerTest.AddAppender(AppenderType.CONSOLE);
        //    var dbAppender = LoggerTest.AddAppender(AppenderType.DATABASE);
        //    LoggerTest.Log()

        //}
    }
}
