using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger.Layout;

namespace Tests.LoggerTest
{
    /// <summary>
    /// Description résumée pour ToastLayoutTest
    /// </summary>
    [TestClass]
    public class ToastLayoutTest
    {
        public ToastLayoutTest()
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
        public void TestToastLayout()
        {
            ToastLayout toastLayout = new ToastLayout("line1", "line2", "line3");

            Assert.IsNotNull(toastLayout);

            Assert.AreEqual(3, toastLayout.Elements.Count);
        }

        [TestMethod]
        public void TestToastLayout2()
        {
            ToastLayout toastLayout = new ToastLayout("line1", "line2");

            Assert.IsNotNull(toastLayout);

            Assert.AreEqual(2, toastLayout.Elements.Count);
        }

        [TestMethod]
        public void TestToastLayout3()
        {
            ToastLayout toastLayout = new ToastLayout("caption");

            Assert.IsNotNull(toastLayout);

            Assert.AreEqual(1, toastLayout.Elements.Count);
            Assert.AreEqual("caption", toastLayout.Elements[0]);
        }
    }
}
