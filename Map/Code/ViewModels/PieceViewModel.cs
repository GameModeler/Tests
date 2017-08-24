using System.Windows.Media;
using Map.ViewModels;
using Tests.Map.Code.Models;

namespace Tests.Map.Code.ViewModels
{
    public class PieceViewModel : BaseViewModel
    {
        #region Properties

        public Piece Piece { get; set; }

        #endregion

        public PieceViewModel()
        {
            Piece = new Piece(70, 70, Brushes.Aqua, Brushes.Aquamarine);
        }
    }
}
