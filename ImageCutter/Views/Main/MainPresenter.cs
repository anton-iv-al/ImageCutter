using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace ImageCutter.Views.Main {
    public class MainPresenter : Presenter<MainWindow> {
        public MainPresenter(MainWindow view) : base(view) { }

        public void OnCutButtonClick() {
            string dir = @"C:\Users\Anton\Downloads\tmp\";
            string path = @"C:\Users\Anton\Downloads\tmp\test.png";

            string newDir = dir + @"cutted\";
            string newPath = newDir + "test_copy.png";
            Directory.CreateDirectory(newDir);

            var image = Image.FromFile(path) as Bitmap;

            int size;
            double border;

            try {
                size = _view.SizeText;
            }
            catch (Exception) {
                MessageBox.Show("Size string wrong format");
                return;
            }

            try {
                border = _view.BorderText;
            }
            catch (Exception) {
                MessageBox.Show("Border string wrong format");
                return;
            }

            image = ImageHelper.CutImage(image, size, border);

            image.Save(newPath, ImageFormat.Png);

            image.Dispose();
        }
    }
}