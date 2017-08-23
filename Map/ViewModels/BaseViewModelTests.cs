using System.Windows;
using Map.Graphics;
using Map.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Map.ViewModels
{
    [TestClass]
    public class BaseViewModelTests
    {
        public BaseViewModel BaseViewModel { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            BaseViewModel = new BaseViewModel
            {
                GridLayer = new GridLayer
                {
                    Name = "GridLayer",
                    Rows = "1*;1*;1*;1*",
                    Columns = "1*;1*;1*;1*"
                }
            };

            BaseViewModel.Initialize();

            BaseViewModel.ValidPositions.AddRange(new[]
            {
                new Point(4, 4),
                new Point(2, 3)
            });

            BaseViewModel.ValidatePositions();
        }

        [TestMethod]
        public void ValidatePositionsTest()
        {
            Assert.IsFalse(BaseViewModel.GridLayer.Placeholders[0].IsActive);
            Assert.IsTrue(BaseViewModel.GridLayer.Placeholders[9].IsActive);
            Assert.IsTrue(BaseViewModel.GridLayer.Placeholders[15].IsActive);
        }

        [TestMethod]
        public void IsLegalMoveTest()
        {
            Assert.IsFalse(BaseViewModel.IsLegalMove(BaseViewModel.GridLayer.Placeholders[0].Position));
            Assert.IsTrue(BaseViewModel.IsLegalMove(BaseViewModel.GridLayer.Placeholders[9].Position));
            Assert.IsTrue(BaseViewModel.IsLegalMove(BaseViewModel.GridLayer.Placeholders[15].Position));
        }
    }
}