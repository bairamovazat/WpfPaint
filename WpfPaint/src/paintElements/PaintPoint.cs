//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SharpPaint.src.paintElements
//{
//    class PaintPoint : IPaintObject
//    {
//        private int x;
//        private int y;
//        private Color color = Color.Black;
//        private int lineSize = 1;
//        private Graphics graphics;

//        public PaintPoint(Graphics graphics)
//        {
//            this.X = x;
//            this.Y = y;
//            this.Graphics = graphics;
//        }

//        public PaintPoint(Graphics graphics, Color color, int size) {
//            this.X = x;
//            this.Y = y;
//            this.Graphics = graphics;
//            this.Color = color;
//            this.LineSize = size;
//        }

//        public int X { get => x; set => x = value; }
//        public int Y { get => y; set => y = value; }
//        public Color Color { get => color; set => color = value; }
//        public Graphics Graphics { get => graphics; set => graphics = value; }
//        public int LineSize { get => lineSize; set => lineSize = value; }

//        public void Draw()
//        {
//            Pen pen = new Pen(this.Color);
//            graphics.DrawRectangle(pen, x, y, LineSize, LineSize);
//        }

//        public void EndMove(Point point)
//        {

//        }

//        public void Move(Point point)
//        {

//        }

//        public bool BackAndNeedDelete()
//        {
//            return true;
//        }

//        public void StartMove(Point point)
//        {
//            this.X = point.X;
//            this.Y = point.Y;
//            Draw();
//        }
//    }
//}
