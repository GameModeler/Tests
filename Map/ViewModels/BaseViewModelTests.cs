using System.Windows;
using Map.Graphics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Map.Code.UserControls;
using Tests.Map.Code.ViewModels;

namespace Tests.Map.ViewModels
{
    [TestClass]
    public class BaseViewModelTests
    {
        public PieceViewModel PieceViewModel { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            PieceViewModel = new PieceViewModel()
            {
                GridLayer = new GridLayer
                {
                    Name = "GridLayer",
                    Rows = "1*;1*;1*;1*",
                    Columns = "1*;1*;1*;1*"
                }
            };

            var pieceUserControl = new PieceUserControl(PieceViewModel);
            PieceViewModel.GridLayer.Children.Add(pieceUserControl);

            PieceViewModel.Initialize();

            PieceViewModel.ValidPositions.AddRange(new[]
            {
                new Point(4, 4),
                new Point(2, 3)
            });

            PieceViewModel.ValidatePositions();
        }

        [TestMethod]
        public void ValidatePositionsTest()
        {
            Assert.IsFalse(PieceViewModel.GridLayer.Placeholders[0].IsActive);
            Assert.IsTrue(PieceViewModel.GridLayer.Placeholders[9].IsActive);
            Assert.IsTrue(PieceViewModel.GridLayer.Placeholders[15].IsActive);
        }

        [TestMethod]
        public void IsLegalMoveTest()
        {
            Assert.IsFalse(PieceViewModel.IsLegalMove(PieceViewModel.GridLayer.Placeholders[0].Position));
            Assert.IsTrue(PieceViewModel.IsLegalMove(PieceViewModel.GridLayer.Placeholders[9].Position));
            Assert.IsTrue(PieceViewModel.IsLegalMove(PieceViewModel.GridLayer.Placeholders[15].Position));
        }

        [TestMethod]
        public void GetLayerDataTest()
        {
            PieceViewModel.Piece.Position = new Point(3, 4);

            Assert.IsNull(PieceViewModel.GetLayerData(new Point(4, 4)));
        }
    }
}