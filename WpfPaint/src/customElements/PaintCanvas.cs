using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfPaint
{
    public class PaintCanvas : Canvas
    {
        private bool paint = false;
        private List<IPaintObject> paintObjectList = new List<IPaintObject>();
        private IPaintObject currentPaintObject;
        private MouseType mouseType = MouseType.Pencil;
        private Color color = Colors.Black;
        private int lineSize = 3;

        internal IPaintObject CurrentPaintObject { get => currentPaintObject; set => currentPaintObject = value; }
        public MouseType MouseType { get => mouseType; set => mouseType = value; }
        public Color Color { get => color; set => color = value; }
        public int LineSize { get => lineSize; set => lineSize = value; }

        public PaintCanvas()
        {
            //< Canvas x: Name = "canvas" ClipToBounds = "true" Background = "Red" Height = "460" VerticalAlignment = "Top" HorizontalAlignment = "Left" Width = "548" />
            this.Name = "canvas";
            this.Focusable = true;
            this.ClipToBounds = true;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
        }

        public void Initialize(BitmapSource image)
        {
            ClearPaintObjectList();

            PaintImage paintImage = new PaintImage(this, image, new Point(0, 0));
            this.Width = image.Width;
            this.Height = image.Height;

            paintImage.HasBack = false;
            paintObjectList.Add(paintImage);

            this.MouseDown -= PaintCanvas_MouseDown;
            this.MouseUp -= PaintCanvas_MouseUp;
            this.MouseMove -= PaintCanvas_MouseMove;

            this.MouseDown += PaintCanvas_MouseDown;
            this.MouseUp += PaintCanvas_MouseUp;
            this.MouseMove += PaintCanvas_MouseMove;
        }

        public void BackAction()
        {
            if (paintObjectList.Count > 0)
            {
                if (paintObjectList.Last().BackAndNeedDelete())
                {
                    ClearPaintObject(paintObjectList[paintObjectList.Count - 1]);
                }
            }
        }

        public void InsertImage(BitmapSource bitmapSource)
        {
            this.paint = false;
            CurrentPaintObject = new PaintImage(this, bitmapSource, new Point(0, 0));
            paintObjectList.Add(CurrentPaintObject);
        }

        public void ClearPaintObjectList()
        {
            paintObjectList.ForEach(e =>
            {
                e.Clear();
            });
            paintObjectList.Clear();
        }

        public void ClearPaintObject(IPaintObject paintObject)
        {
            paintObject.Clear();
            paintObjectList.RemoveAt(paintObjectList.IndexOf(paintObject));
        }


        private IPaintObject GetCurrentPaintObject()
        {
            switch (mouseType)
            {
                case (MouseType.Pencil):
                    return new PaintCurve(this);
                case (MouseType.HotSpot):
                    return new PaintImage(this, true);
                case (MouseType.Rectangle):
                    return new PaintRectangle(this);
                case (MouseType.Ellipse):
                    return new PaintEllipse(this);
                case (MouseType.Line):
                    return new PaintLine(this);
                case (MouseType.Eraser):
                    return new PaintEraser(this);
                default: return null;
            }
        }
        private void PaintCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            paint = true;
            if (CurrentPaintObject == null || !(CurrentPaintObject is PaintImage)
                || !((PaintImage)CurrentPaintObject).MouseInImage(new Point(e.GetPosition(this).X, e.GetPosition(this).Y)))
            {
                if (CurrentPaintObject is PaintImage)
                {
                    ((PaintImage)CurrentPaintObject).ShowRectangle = false;
                }
                CurrentPaintObject = GetCurrentPaintObject();
                if (CurrentPaintObject != null)
                {
                    CurrentPaintObject.LineSize = LineSize;
                    CurrentPaintObject.Color = Color;
                    paintObjectList.Add(CurrentPaintObject);
                }
            }
            CurrentPaintObject.StartMove(new Point(e.GetPosition(this).X, e.GetPosition(this).Y));
        }

        private void PaintCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            paint = false;
            if (CurrentPaintObject != null)
            {
                CurrentPaintObject.EndMove(new Point(e.GetPosition(this).X, e.GetPosition(this).Y));
            }
        }

        private void PaintCanvas_MouseMove(object sender, MouseEventArgs e)
        {

            if (paint && CurrentPaintObject != null)
            {
                CurrentPaintObject.Move(new Point(e.GetPosition(this).X, e.GetPosition(this).Y));
            }
        }
    }
}
