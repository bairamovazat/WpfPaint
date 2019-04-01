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
    class PaintCurve : IPaintObject
    {

        private Color color = Colors.Black;
        private int lineSize = 1;
        private List<Point> pointLists = new List<Point>();
        private PaintCanvas paintCanvas;
   
        public virtual Color Color { get => color; set => color = value; }
        public int Size { get => LineSize; set => LineSize = value; }
        public virtual int LineSize { get => lineSize; set => lineSize = value; }
        public PaintCanvas PaintCanvas { get => paintCanvas; set => paintCanvas = value; }

        private Polyline line;

        public PaintCurve(PaintCanvas paintCanvas)
        {
            this.PaintCanvas = paintCanvas;
        }

        public PaintCurve(PaintCanvas paintCanvas, Color color, int size)
        {
            this.PaintCanvas = paintCanvas;
            this.Color = color;
            this.LineSize = size;
        }
   
        public void EndMove(Point point)
        {

        }

        public void Move(Point point)
        {
            this.line.Points.Add(point);
        }

        public bool BackAndNeedDelete()
        {
            return true;
        }

        public void StartMove(Point point)
        {
            if (this.line == null)
            {
                this.line = new Polyline();
                this.paintCanvas.Children.Add(line);
                this.line.Stroke = new SolidColorBrush(this.Color);
                this.line.StrokeThickness = this.LineSize;

            }
            this.line.Points.Add(point);
        }

        public void Clear()
        {
            if (this.line != null) {
                this.paintCanvas.Children.Remove(this.line);
            }
        }
    }
}
