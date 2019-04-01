using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPaint
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PaintCanvas paintCanvas;
        private String lastPath;

        public MainWindow()
        {
            int height = 400;
            int width = 500;

            InitializeComponent();
            paintCanvas = new PaintCanvas();
            paintPanel.Children.Add(paintCanvas);

            paintCanvas.Initialize(Utils.GetWhiteBitmap(width, height));
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                paintCanvas.BackAction();
            }
        }

        private void Ellipse_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.MouseType = MouseType.Ellipse;
        }

        private void Eraser_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.MouseType = MouseType.Eraser;
        }

        private void Pencil_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.MouseType = MouseType.Pencil;
        }

        private void Line_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.MouseType = MouseType.Line;
        }

        private void HotSpot_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.MouseType = MouseType.HotSpot;
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.MouseType = MouseType.Rectangle;
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.Color = Colors.Red;
        }

        private void Orange_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.Color = Colors.Orange;
        }

        private void Yellow_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.Color = Colors.Yellow;
        }

        private void Green_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.Color = Colors.Green;
        }

        private void Blue_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.Color = Colors.Blue;
        }

        private void DeepSkyBlue_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.Color = Colors.DeepSkyBlue;
        }

        private void Violet_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.Color = Colors.Violet;
        }

        private void Black_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.Color = Colors.Black;
        }

        private void White_Click(object sender, RoutedEventArgs e)
        {
            paintCanvas.Color = Colors.White;
        }

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            TextBlock selectedItem = (TextBlock)comboBox.SelectedItem;
            paintCanvas.LineSize = int.Parse(selectedItem.Text);
        }

   
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.png) | *.jpg; *.jpeg; *.bmp; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                paintCanvas.Initialize(new BitmapImage(new Uri(openFileDialog.FileName)));
                this.lastPath = openFileDialog.FileName;
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.png) | *.jpg; *.jpeg; *.bmp; *.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                SaveImage(saveFileDialog.FileName);
                this.lastPath = saveFileDialog.FileName;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (lastPath == null)
            {
                this.SaveAs_Click(sender, e);
                return;
            }

            SaveImage(lastPath);

        }


        public void SaveImage(string fileName) {
            FileInfo fileInfo = new FileInfo(fileName);
            string type = fileInfo.Extension;
            if (type.ToLower().Equals(".jpg") || type.ToLower().Equals(".jpeg"))
            {
                SaveImage(fileName, new JpegBitmapEncoder());
            }
            else if (type.ToLower().Equals(".bmp"))
            {
                SaveImage(fileName, new BmpBitmapEncoder());
            }
            else if (type.ToLower().Equals(".png")) {
                SaveImage(fileName, new PngBitmapEncoder());
            }

        }
        public void SaveImage(string fileName, BitmapEncoder bitmapEncoder) {
            FileInfo fileInfo = new FileInfo(fileName);
            if (!fileInfo.Exists) {
                File.Create(fileName).Close();
            }
            RenderTargetBitmap targetBitmap = new RenderTargetBitmap((int)paintCanvas.RenderSize.Height, (int)paintCanvas.RenderSize.Width,
             96d, 96d, PixelFormats.Default);

            targetBitmap.Render(paintCanvas);

            BitmapFrame frame = BitmapFrame.Create(targetBitmap);
            bitmapEncoder.Frames.Add(frame);

            using (var stream = File.Create(fileName))
            {
                bitmapEncoder.Save(stream);
            }
        }
    }
}
