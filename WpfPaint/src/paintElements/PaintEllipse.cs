using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfPaint
{
    class PaintEllipse : IPaintObject
    {
        private Point startPoint;
        private Point endPoint;

        private Color color = Colors.Black;
        private int lineSize = 1;
        private PaintCanvas paintCanvas;
        private Ellipse ellipse;

        public PaintEllipse(PaintCanvas paintCanvas)
        {
            this.PaintCanvas = paintCanvas;
        }

        public PaintEllipse(PaintCanvas paintCanvas, Color color, int size)
        {
            this.PaintCanvas = paintCanvas;
            this.Color = color;
            this.LineSize = size;
        }

        public Color Color { get => color; set => color = value; }
        public int LineSize { get => lineSize; set => lineSize = value; }
        public PaintCanvas PaintCanvas { get => paintCanvas; set => paintCanvas = value; }

        public void Draw()
        {
            int x = (int)Math.Max(0, Math.Min(startPoint.X, endPoint.X));
            int y = (int)Math.Max(0, Math.Min(startPoint.Y, endPoint.Y));
            int width = (int)Math.Abs(startPoint.X - endPoint.X);
            int height = (int)Math.Abs(startPoint.Y - endPoint.Y);
            Canvas.SetLeft(this.ellipse, x);
            Canvas.SetTop(this.ellipse, y);
            this.ellipse.Height = height;
            this.ellipse.Width = width;
            this.ellipse.Stroke = new SolidColorBrush(this.Color);
            this.ellipse.StrokeThickness = this.lineSize;

        }

        public void EndMove(Point point)
        {
            this.endPoint = point;
            this.Draw();
        }

        public void Move(Point point)
        {
            this.endPoint = point;
            this.Draw();
        }

        public bool BackAndNeedDelete()
        {
            return true;
        }

        public void StartMove(Point point)
        {
            if (this.ellipse == null) {
                this.ellipse = new Ellipse();
                this.paintCanvas.Children.Add(ellipse);
            }
            this.startPoint = point;
            this.endPoint = this.startPoint;
            Draw();
        }

        public void Clear()
        {
            if (this.ellipse != null)
            {
                this.paintCanvas.Children.Remove(this.ellipse);
            }
        }
    }
}
