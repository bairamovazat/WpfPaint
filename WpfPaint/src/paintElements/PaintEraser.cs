using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfPaint
{
    class PaintEraser : PaintCurve
    {


        public PaintEraser(PaintCanvas paintCanvas): base(paintCanvas)
        {
        }

        public PaintEraser(PaintCanvas paintCanvas, Color color, int size) : base(paintCanvas, color, size)
        {
        }

        public override int LineSize { get => base.LineSize * 2; set => base.LineSize = value; }

        public override Color Color { get => Colors.White; set => base.Color = value; }

    }
}
