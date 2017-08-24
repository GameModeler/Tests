using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;
using Map.UserControls;
using Tests.Map.Code.ViewModels;

namespace Tests.Map.Code.UserControls
{
    public class PieceUserControl : BaseUserControl
    {
        public Ellipse Ellipse { get; set; }

        public PieceUserControl(PieceViewModel pieceViewModel)
        {
            DataContext = pieceViewModel.Piece;

            var widthBinding = new Binding
            {
                Path = new PropertyPath("DataContext.Width"),
                Mode = BindingMode.TwoWay,
                RelativeSource = new RelativeSource()
            };

            var heightBinding = new Binding
            {
                Path = new PropertyPath("DataContext.Height"),
                Mode = BindingMode.TwoWay
            };

            var colorBinding = new Binding
            {
                Path = new PropertyPath("DataContext.Color"),
                Mode = BindingMode.TwoWay
            };

            var borderColorBinding = new Binding
            {
                Path = new PropertyPath("DataContext.BorderColor"),
                Mode = BindingMode.TwoWay
            };

            var positionBinding = new Binding
            {
                Path = new PropertyPath("DataContext.Position"),
                Mode = BindingMode.TwoWay,
                RelativeSource = new RelativeSource()
            };

            Ellipse = new Ellipse
            {
                StrokeThickness = 5
            };

            BindingOperations.SetBinding(Ellipse, WidthProperty, widthBinding);
            BindingOperations.SetBinding(Ellipse, HeightProperty, heightBinding);
            BindingOperations.SetBinding(Ellipse, BackgroundProperty, colorBinding);
            BindingOperations.SetBinding(Ellipse, BorderBrushProperty, borderColorBinding);
            BindingOperations.SetBinding(this, PositionProperty, positionBinding);
        }
    }
}
