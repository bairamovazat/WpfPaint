using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfPaint
{
    class PaintLine : IPaintObject
    {
        private Point startPoint;
        private Point endPoint;

        private Color color = Colors.Black;
        private int lineSize = 1;
        private PaintCanvas paintCanvas;
        private Line line;

        public PaintLine(PaintCanvas paintCanvas)
        {
            this.paintCanvas = paintCanvas;
        }

        public PaintLine(PaintCanvas paintCanvas, Color color, int size)
        {
            this.paintCanvas = paintCanvas;
            this.Color = color;
            this.LineSize = size;
        }

        public Color Color { get => Color1; set => Color1 = value; }
        public int Size { get => LineSize; set => LineSize = value; }
        public Color Color1 { get => color; set => color = value; }
        public int LineSize { get => lineSize; set => lineSize = value; }

        public void Draw()
        {
            this.line.X1 = startPoint.X;
            this.line.Y1 = startPoint.Y;
            this.line.X2 = endPoint.X;
            this.line.Y2 = endPoint.Y;
            this.line.Stroke = new SolidColorBrush(this.Color);
            this.line.StrokeThickness = this.lineSize;
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
            this.line = new Line();
            this.paintCanvas.Children.Add(this.line);
            this.Draw();
        }

        public void Clear()
        {
            if (this.line != null)
            {
                this.paintCanvas.Children.Remove(this.line);
            }
        }
    }
}
