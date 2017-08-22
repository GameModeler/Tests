using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger.Interfaces;
using Logger.Loggers;
using Logger.Appenders;
using Logger.Utils;

namespace Tests.LoggerTest
{
    /// <summary>
    /// Description résumée pour AppenderMaagerTest
    /// </summary>
    [TestClass]
    public class AppenderManagerTest
    {
        private LoggerManager manager;

        public AppenderManagerTest()
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
        public void TestAppenderManager()
        {
            ILogger logger = manager.CreateLogger("testLogger");

            AppenderManager appManager = new AppenderManager(logger);

            Assert.IsNotNull(appManager);

            Assert.IsNotNull(appManager.AppenderList.Count);
        }

        [TestMethod]
        public void TestAddAppender()
        {
            ILogger logger = manager.CreateLogger("testLogger");

            AppenderManager appManager = new AppenderManager(logger);

            appManager.AddAppender(AppenderType.CONSOLE);
            appManager.AddAppender(AppenderType.TOAST);

            Assert.AreEqual(2, appManager.AppenderList.Count);
        }


        [TestMethod]
        public void TestAddAppender2()
        {
            ILogger logger = manager.CreateLogger("testLogger");

            AppenderManager appManager = new AppenderManager(logger);

            IAppender appender = new ConsoleAppender("console_appender");

            appManager.AddAppender(appender);

            Assert.AreEqual(1, appManager.AppenderList.Count);
        }

        [TestMethod]
        public void TestAddAppender3()
        {
            ILogger logger = manager.CreateLogger("testLogger");

            AppenderManager appManager = new AppenderManager(logger);

            IAppender appender = new ConsoleAppender("console_appender");

            var appenderreturn = appManager.AddAppender(appender);

            Assert.AreEqual(appender, appenderreturn);

            var appresult2 = appManager.AddAppender(AppenderType.CONSOLE, "console_appender");

            Assert.IsNull(appresult2);
        }


        [TestMethod]
        public void TestDetach()
        {
            ILogger logger = manager.CreateLogger("testLogger");

            AppenderManager appManager = new AppenderManager(logger);

            IAppender appender = new ConsoleAppender("console_appender");

            var appenderreturn = appManager.AddAppender(appender);

            Assert.AreEqual(1, appManager.AppenderList.Count);

            appManager.Detach(appender);

            Assert.AreEqual(0, appManager.AppenderList.Count);
        }

        [TestMethod]
        public void TestDetach2()
        {
            ILogger logger = manager.CreateLogger("testLogger");

            AppenderManager appManager = new AppenderManager(logger);

            IAppender appender = new ConsoleAppender("console_appender");

            var appenderreturn = appManager.AddAppender(appender);

            Assert.AreEqual(1, appManager.AppenderList.Count);

            appManager.Detach("console_appender");

            Assert.AreEqual(0, appManager.AppenderList.Count);
        }


        [TestMethod]
        public void TestCreateAppender()
        {
            ILogger logger = manager.CreateLogger("testLogger");

            AppenderManager appManager = new AppenderManager(logger);

            var appender = appManager.CreateAppender(AppenderType.FILE, "file_appender");

            Assert.IsNotNull(appender);

            Assert.IsInstanceOfType(appender, typeof(FileAppender));
        }
    }
}
