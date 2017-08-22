using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using Logger.Layout;

namespace Tests.LoggerTest
{
    /// <summary>
    /// Description résumée pour ModalBoxTest
    /// </summary>
    [TestClass]
    public class ModalBoxTest
    {
        public ModalBoxTest()
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
        public void TestModalBox()
        {
            ModalBox modalBox = new ModalBox("title", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            Assert.IsNotNull(modalBox);

            Assert.AreEqual("title", modalBox.Caption);
            Assert.AreEqual(MessageBoxButtons.OKCancel, modalBox.Buttons);
            Assert.AreEqual(MessageBoxIcon.Information, modalBox.Icon);
            Assert.IsTrue(modalBox.HasIcon);
        }

        [TestMethod]
        public void TestModalBox2()
        {
            ModalBox modalBox = new ModalBox("title", MessageBoxButtons.OKCancel);

            Assert.IsNotNull(modalBox);

            Assert.AreEqual("title", modalBox.Caption);
            Assert.AreEqual(MessageBoxButtons.OKCancel, modalBox.Buttons);
        }

        [TestMethod]
        public void TestModalBox3()
        {
            ModalBox modalBox = new ModalBox("title", MessageBoxIcon.Error);

            Assert.IsNotNull(modalBox);

            Assert.AreEqual("title", modalBox.Caption);
            Assert.AreEqual(MessageBoxIcon.Error, modalBox.Icon);
            Assert.IsTrue(modalBox.HasIcon);
        }

        [TestMethod]
        public void TestModalBox4()
        {
            ModalBox modalBox = new ModalBox("title");

            Assert.IsNotNull(modalBox);

            Assert.AreEqual("title", modalBox.Caption);
        }

        [TestMethod]
        public void TestSetAction()
        {
            ModalBox modalBox = new ModalBox("title");

            modalBox.SetAction(DialogResult.Cancel, ActionMethod);

            Assert.AreEqual(modalBox.ButtonActions.Count, 1);
        }

        private void ActionMethod()
        {
            Console.Write("Hello"); 
        }

    }
}
