using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger.Appenders;
using Logger.Utils;

namespace Tests.LoggerTest.Appenders
{
    /// <summary>
    /// Description résumée pour ConsoleAppender
    /// </summary>
    [TestClass]
    public class ConsoleAppenderTest
    {
        public ConsoleAppenderTest()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
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
        public void TestConsoleAppender()
        {
            ConsoleAppender consoleAppender = new ConsoleAppender("console_appender");

            Assert.IsNotNull(consoleAppender);
            Assert.AreEqual("console_appender", consoleAppender.AppenderName);
            Assert.AreEqual(LogPatternConstants.DEFAULT_PATTERN, consoleAppender.Layout);

            ConsoleAppender consoleAppender2 = new ConsoleAppender("");

            Assert.IsNotNull(consoleAppender2);
            Assert.AreEqual("GM_CONSOLE_APPENDER", consoleAppender2.AppenderName);
            Assert.AreEqual(LogPatternConstants.DEFAULT_PATTERN, consoleAppender2.Layout);
        }
    }
}
