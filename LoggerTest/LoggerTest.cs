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
    /// Description résumée pour LoggerTest
    /// </summary>
    [TestClass]
    public class LoggerTest
    {
        private LoggerManager loggerManager;
        private const String LOGGER_NAME = "TEST_LOGGER";
        private ILogger loggerTest;

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

        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        [TestInitialize()]
        public void MyTestInitialize() {

            loggerManager = new LoggerManager();
            loggerTest = new LoggerManager().CreateLogger(LOGGER_NAME);
        }

        #endregion

        /// <summary>
        /// Constructor test
        /// </summary>
        [TestMethod]
        public void TestLogger()
        {
            // Default constructor
            ILogger logger = new Logger.Loggers.Logger("Logger1", Level.DEBUG, loggerManager);

            Assert.IsNotNull(logger);

            Assert.AreEqual("Logger1", logger.Name);
            Assert.AreEqual(Level.DEBUG, logger.Level);
        }

        /// <summary>
        /// Add Appender
        /// </summary>
        [TestMethod]
        public void AddAppenderTest()
        {
            AppenderType appenderType = AppenderType.CONSOLE;
            var appender = loggerTest.AddAppender(appenderType);

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
            var appender = loggerTest.AddAppender(appenderType, APPENDER_NAME);

            Assert.IsInstanceOfType(appender, typeof(IAppender));
            Assert.AreEqual(appender.AppenderName, APPENDER_NAME);

        }

        /// <summary>
        /// Reset logger
        /// </summary>
        [TestMethod]
        public void ResetTest()
        {
            loggerTest.Reset();

            Assert.AreEqual(Level.INFO, loggerTest.Level);
            Assert.AreEqual(loggerTest.AppenderManager.AppenderList.Count, 0);
        }

        /// <summary>
        /// Remove Appender
        /// </summary>
        [TestMethod]
        public void RemoveAppenderTest()
        {
            var consoleAppender = loggerTest.AddAppender(AppenderType.CONSOLE);
            var toastAppender = loggerTest.AddAppender(AppenderType.TOAST);
            var dataBaseAppender = loggerTest.AddAppender(AppenderType.DATABASE);

            Assert.AreEqual(loggerTest.AppenderManager.AppenderList.Count, 3);
            loggerTest.RemoveAppender(consoleAppender);

            Assert.AreEqual(loggerTest.AppenderManager.AppenderList.Count, 2);

        }
    }
}
