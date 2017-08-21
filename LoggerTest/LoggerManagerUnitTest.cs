using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger.Loggers;
using Logger.Utils;
using Logger.Interfaces;

namespace Tests.LoggerTest
{
    [TestClass]
    public class LoggerManagerUnitTest
    {

        private LoggerManager manager;

        public LoggerManagerUnitTest()
        {
            manager = new LoggerManager();
        }

        /// <summary>
        /// Create a logger
        /// </summary>
        [TestMethod]
        public void CreateLoggerTest()
        {
            String name = "TEST_LOGGER";
            Level level = Level.INFO;         

            var logger = manager.CreateLogger(name, level);

            Assert.IsInstanceOfType(logger, typeof(ILogger));
            Assert.AreEqual(name, logger.Name);
            Assert.AreEqual(level, logger.Level);
        }

        /// <summary>
        /// Create a logger
        /// </summary>
        [TestMethod]
        public void CreateLoggerTest_EmptyParams()
        {

            var logger = manager.CreateLogger();

            Assert.IsInstanceOfType(logger, typeof(ILogger));
            Assert.AreEqual(logger.Name, "GM_LOGGER");
            Assert.AreEqual(logger.Level, Level.INFO);
        }

        /// <summary>
        /// Duplicate a logger
        /// </summary>
        [TestMethod]
        public void DuplicateLoggerByNameTest()
        {
            const String LOGGER_NAME = "DUPLICATE_LOGGER";
;           var logger = manager.CreateLogger(LOGGER_NAME);

            var copyLogger = manager.DuplicateLogger(logger.Name);

            Assert.IsInstanceOfType(copyLogger, typeof(ILogger));
            Assert.AreEqual("DUPLICATE_LOGGER_1", "DUPLICATE_LOGGER_1");

            var copyLogger2 = manager.DuplicateLogger(logger.Name);

            Assert.IsInstanceOfType(copyLogger, typeof(ILogger));
            Assert.AreEqual("DUPLICATE_LOGGER_2", "DUPLICATE_LOGGER_2");
        }

        /// <summary>
        /// Delete logger
        /// </summary>
        [TestMethod]
        public void DeleteTest()
        {
            var logger = manager.CreateLogger("DELETE_LOGGER");
            bool result = manager.Delete(logger.Name);

            Assert.IsTrue(result);

            var logger_2 = manager.CreateLogger("DELETE_LOGGER_2");
            bool result2 = manager.Delete(logger_2.Name);

            Assert.IsTrue(result2);
        }
    }
}
