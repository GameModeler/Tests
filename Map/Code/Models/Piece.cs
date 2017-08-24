using System.Windows;
using System.Windows.Media;
using Map.Interfaces;
using Map.Models;

namespace Tests.Map.Code.Models
{
    public class Piece : BaseModel, IModel, IMovable
    {
        #region Attributes

        private int _width;

        private int _height;

        private Brush _color;

        private Brush _borderColor;

        private Point _position;

        #endregion

        #region Properties

        public int Id { get; set; }

        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                NotifyPropertyChanged();
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                NotifyPropertyChanged();
            }
        }

        public Brush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                NotifyPropertyChanged();
            }
        }

        public Brush BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                NotifyPropertyChanged();
            }
        }

        public Point Position
        {
            get { return _position; }
            set
            {
                _position = value;
                X = value.X;
                Y = value.Y;
                NotifyPropertyChanged();
            }
        }

        public double X { get; set; }

        public double Y { get; set; }

        #endregion

        #region Constructors

        public Piece()
        {
            Position = default(Point);
        }

        public Piece(int width, int height, Brush color, Brush borderColor)
            : this()
        {
            Width = width;
            Height = height;
            Color = color;
            BorderColor = borderColor;
        }

        #endregion
    }
}
