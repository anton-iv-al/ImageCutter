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

            image = ImageHelper.CutImage(image);

            image.Save(newPath, ImageFormat.Png);

            image.Dispose();
        }
    }
}