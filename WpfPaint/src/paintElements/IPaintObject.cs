using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfPaint
{
    public interface IPaintObject
    {
        int LineSize { get; set; }
        Color Color{ get; set; }

        void StartMove(Point point);
        void EndMove(Point point);
        void Move(Point point);

        void Clear();
        bool BackAndNeedDelete();
    }
}
