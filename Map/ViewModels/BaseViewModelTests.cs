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
        }

        [TestMethod]
        public void InitializeTest()
        {
            
        }

        [TestMethod]
        public void ValidatePositionsTest()
        {
            BaseViewModel.ValidPositions
        }

        [TestMethod]
        public void IsLegalMoveTest()
        {

        }
    }
}