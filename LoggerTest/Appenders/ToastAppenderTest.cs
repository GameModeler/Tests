using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger.Appenders;

namespace Tests.LoggerTest.Appenders
{
    /// <summary>
    /// Description résumée pour ToastAppender
    /// </summary>
    [TestClass]
    public class ToastAppenderTest
    {
        public ToastAppenderTest()
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
        public void TestToastAppender()
        {
            ToastAppender toastAppender = new ToastAppender("toast_appender");

            Assert.IsNotNull(toastAppender);
            Assert.AreEqual("toast_appender", toastAppender.AppenderName);
            Assert.IsNotNull(toastAppender.ToastLayout);

            ToastAppender toastAppender2 = new ToastAppender(null);

            Assert.IsNotNull(toastAppender2);
            Assert.AreEqual("GM_TOAST_APPENDER", toastAppender2.AppenderName);
            Assert.IsNotNull(toastAppender2.ToastLayout);



        }
    }
}
