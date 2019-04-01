using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfPaint
{
    class PaintImage : IPaintObject
    {
        private PaintCanvas paintPanel;
        private Point startPoint;
        private Point movedPoint;

        private Point lastMovePoint;
        private Image image;
        private Image emptyImage;

        private bool paintEmptyBlock = false;

        private bool hasBack = true;

        private bool showRectangle = false;

        private int lineSize;
        private Color color;

        public Point StartPoint { get => startPoint; set => startPoint = value; }
        public Point MovedPoint { get => movedPoint; set => movedPoint = value; }

        public bool HasBack { get => hasBack; set => hasBack = value; }
        public PaintCanvas PaintCanvas { get => paintPanel; set => paintPanel = value; }
        public bool PaintEmptyBlock { get => paintEmptyBlock; set => paintEmptyBlock = value; }
        public int LineSize { get => lineSize; set => lineSize = value; }
        public Color Color { get => color; set => color = value; }
        public bool ShowRectangle { get => showRectangle; set => showRectangle = value; }

        public PaintImage(PaintCanvas paintPanel)
        {
            this.PaintCanvas = paintPanel;
        }

        public PaintImage(PaintCanvas paintPanel, bool paintEmptyBlock)
        {
            this.PaintCanvas = paintPanel;
            this.PaintEmptyBlock = paintEmptyBlock;
        }

        public PaintImage(PaintCanvas paintPanel, BitmapSource bitmapSource, Point startPoint)
        {
            this.PaintCanvas = paintPanel;
            this.StartPoint = startPoint;
            this.MovedPoint = startPoint;

            this.image = new Image();
            image.Source = bitmapSource;
            image.Height = bitmapSource.Height;
            image.Width = bitmapSource.Width;
            image.UpdateLayout();

            paintPanel.Children.Add(image);
            Canvas.SetLeft(image, startPoint.X);
            Canvas.SetTop(image, startPoint.Y);
        }

        public bool BackAndNeedDelete()
        {
            return HasBack;
        }

        public void Draw()
        {
            if (!this.startPoint.Equals(this.movedPoint) && this.emptyImage != null && PaintEmptyBlock)
            {
                Canvas.SetLeft(this.emptyImage, StartPoint.X);
                Canvas.SetTop(this.emptyImage, StartPoint.Y);
            }

            if (ShowRectangle && this.image == null)
            {
                //Тут рисуем рамку
                //int x = (int)Math.Max(0, Math.Min(startPoint.X, lastMovePoint.X));
                //int y = (int)Math.Max(0, Math.Min(startPoint.Y, lastMovePoint.Y));
                //int width = (int)Math.Abs(startPoint.X - lastMovePoint.X);
                //int height = (int)Math.Abs(startPoint.Y - lastMovePoint.Y);
                //Pen pen = new Pen(Color.Black, 1);
                //this.graphics.DrawRectangle(pen, x - 1, y - 1, width + 1, height + 1);
            }
            else if (ShowRectangle && this.image != null)
            {
                //Тут рисуем рамку
                //Pen pen = new Pen(Color.Black, 1);

                //this.graphics.DrawRectangle(pen, this.movedPoint.X - 1, this.movedPoint.Y - 1,
                //    this.Bitmap.Size.Width + 1, this.Bitmap.Height + 1);
            }
            else {

            }

            if (this.image != null && this.MovedPoint != null)
            {
                Canvas.SetLeft(this.image, MovedPoint.X);
                Canvas.SetTop(this.image, MovedPoint.Y);
            }
        }

        public void EndMove(Point point)
        {
            if (startPoint.X == point.X || startPoint.Y == point.Y)
            {
                this.movedPoint = startPoint;
            }
            else if (this.image == null && !startPoint.Equals(point))
            {
                int x = (int)Math.Max(0, Math.Min(startPoint.X, point.X));
                int y = (int)Math.Max(0, Math.Min(startPoint.Y, point.Y));
                int width = (int)Math.Abs(startPoint.X - point.X);
                int height = (int)Math.Abs(startPoint.Y - point.Y);

                emptyImage = new Image();
                emptyImage.Source = Utils.GetWhiteBitmap(width, height);
                emptyImage.Height = height;
                emptyImage.Width = width;

                Canvas.SetLeft(emptyImage, x);
                Canvas.SetTop(emptyImage, y);

                this.PaintCanvas.Children.Add(this.emptyImage);

                RenderTargetBitmap imageRenderBitmap = new RenderTargetBitmap((int)PaintCanvas.RenderSize.Height, (int)PaintCanvas.RenderSize.Width,
                    96d, 96d, PixelFormats.Default);

                imageRenderBitmap.Render(PaintCanvas);

                CroppedBitmap cropedImage = new CroppedBitmap(imageRenderBitmap, new Int32Rect(x, y, width, height));

                this.image = new Image();
                image.Source = cropedImage;
                image.Height = height;
                image.Width = width;

                Canvas.SetLeft(image, x);
                Canvas.SetTop(image, y);

                this.PaintCanvas.Children.Add(this.image);


                this.MovedPoint = new Point(x, y);
                this.StartPoint = new Point(x, y);

                this.Draw();
            }
        }

        public void Move(Point point)
        {
            if (this.image != null)
            {
                //TODO Доделать
                int newX = (int)(point.X - this.startPoint.X);
                int newY = (int)(point.Y - this.startPoint.Y);
                this.movedPoint = new Point(point.X, point.Y);
            }
            this.lastMovePoint = new Point(point.X, point.Y);
            Draw();
        }

        public void StartMove(Point point)
        {
            this.ShowRectangle = true;

            if (image == null)
            {
                this.StartPoint = point;
                this.lastMovePoint = point;
            }
            Draw();
        }

        public bool MouseInImage(Point point)
        {
            return image != null && movedPoint.X <= point.X && movedPoint.X + image.Width >= point.X
                && movedPoint.Y <= point.Y && movedPoint.Y + image.Height >= point.Y; ;
        }

        public void Clear()
        {
            if (this.image != null)
            {
                this.PaintCanvas.Children.Remove(this.image);
            }

            if (this.emptyImage != null)
            {
                this.PaintCanvas.Children.Remove(this.emptyImage);
            }
        }
    }
}
