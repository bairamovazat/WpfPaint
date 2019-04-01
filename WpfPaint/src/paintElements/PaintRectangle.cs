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
    class PaintRectangle : IPaintObject
    {
        private Point startPoint;
        private Point endPoint;

        private Color color = Colors.Black;
        private int lineSize = 1;
        private PaintCanvas paintCanvas;
        private Polyline polyline;

        public PaintRectangle(PaintCanvas paintCanvas)
        {
            this.paintCanvas = paintCanvas;
        }

        public PaintRectangle(PaintCanvas paintCanvas, Color color, int size)
        {
            this.paintCanvas = paintCanvas;
            this.Color = color;
            this.LineSize = size;
        }

        public Color Color { get => color; set => color = value; }
        public int LineSize { get => lineSize; set => lineSize = value; }

        public void Draw()
        {
            int x = (int)Math.Max(0, Math.Min(startPoint.X, endPoint.X));
            int y = (int)Math.Max(0, Math.Min(startPoint.Y, endPoint.Y));
            int width = (int)Math.Abs(startPoint.X - endPoint.X);
            int height = (int)Math.Abs(startPoint.Y - endPoint.Y);

            polyline.Points.Clear();

            polyline.Points.Add(new Point(x, y));
            polyline.Points.Add(new Point(x, y + height));
            polyline.Points.Add(new Point(x + width, y + height));
            polyline.Points.Add(new Point(x + width, y));
            polyline.Points.Add(new Point(x, y));
            polyline.Points.Add(new Point(x, y + height));

            polyline.Stroke = new SolidColorBrush(this.Color);
            polyline.StrokeThickness = this.lineSize;
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
            this.startPoint = point;
            this.endPoint = this.startPoint;
            this.polyline = new Polyline();
            this.paintCanvas.Children.Add(this.polyline);
            this.Draw();
        }

        public void Clear()
        {
            if (this.polyline != null)
            {
                this.paintCanvas.Children.Remove(this.polyline);
            }
        }
    }
}
