using System;
using System.Windows;
using System.Windows.Controls;
using Map;
using Map.Graphics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Map.Code.Models;

namespace Tests.Map
{
    [TestClass]
    public class UtilitiesTests
    {
        /// <summary>
        /// Test if we find the requested parent by its type
        /// </summary>
        [TestMethod]
        public void FindParentTest()
        {
            var window = new Window();
            var dockpanel = new DockPanel
            {
                LastChildFill = true
            };
            var canvas = new Canvas();
            var gridLayer = new GridLayer();

            canvas.Children.Add(gridLayer);
            dockpanel.Children.Add(canvas);
            window.Content = dockpanel;

            Assert.AreEqual(null, Utilities.FindParent<StackPanel>(gridLayer));
            Assert.AreSame(dockpanel, Utilities.FindParent<DockPanel>(gridLayer));
        }

        [TestMethod]
        public void FindChildTest()
        {
            var window = new Window();
            var dockpanel = new DockPanel
            {
                LastChildFill = true
            };
            var canvas = new Canvas();
            var gridLayer = new GridLayer
            {
                Name = "GridLayer"
            };

            canvas.Children.Add(gridLayer);
            dockpanel.Children.Add(canvas);
            window.Content = dockpanel;

            Assert.AreSame(canvas, Utilities.FindChild<Canvas>(dockpanel));
            Assert.AreEqual(null, Utilities.FindChild<GridLayer>(dockpanel, "Grid"));
            Assert.AreSame(gridLayer, Utilities.FindChild<GridLayer>(dockpanel, "GridLayer"));

            dockpanel = null;

            Assert.AreEqual(null, Utilities.FindChild<GridLayer>(dockpanel));
        }

        [TestMethod]
        public void BrowseFilesTest()
        {
            var dialogTitle = "Dialog title";
            var fileTypeLabel = "Images";
            string[] fileTypes = {"jpg", "png", "gif"};

            var res = Utilities.BrowseFiles(dialogTitle, fileTypeLabel, fileTypes, true);

            if (res != null)
            {
                Assert.AreEqual(dialogTitle, res.Title);
                Assert.IsTrue(res.Multiselect);
                Assert.AreEqual("Images (*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif", res.Filter);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CloneExceptionTest()
        {
            var instanceB = new ClassB(1, "Class B");

            Utilities.Clone(instanceB);
        }

        [TestMethod]
        public void CloneTest()
        {
            var instanceA = new ClassA(1, "Class A");

            Assert.AreEqual(instanceA, Utilities.Clone(instanceA));
            Assert.AreNotSame(instanceA, Utilities.Clone(instanceA));

            instanceA = null;

            Assert.AreEqual(default(ClassA), Utilities.Clone(instanceA));
        }

        [TestMethod]
        public void ConcatenateArrayWithTransformationTest()
        {
            var stringArray = new[]
            {
                "Some text",
                "More text",
                "Even more text"
            };

            const string seperator = ";";
            const string prefix = "*/";
            const string suffix = "/*";

            Assert.AreEqual("*/Some text/*;*/More text/*;*/Even more text/*",
                Utilities.ConcatenateArrayWithTransformation(stringArray, seperator, prefix, suffix));
        }
    }
}