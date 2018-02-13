using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageCutter {
    public static class DirectoryHelper {
        private static int _size;
        private static double _border;

        private static string _dirForCutted;

        public static void CutAllImagesInDir(string dir, int size, double border) {
            _size = size;
            _border = border;

            _dirForCutted = DirPathForCutted(dir);
            Directory.CreateDirectory(_dirForCutted);

            var files = Directory.GetFiles(dir);

            foreach (var filePath in files) {
                CutOneImage(filePath);
            }
        }

        private static void CutOneImage(string filePath) {
            Bitmap image;
            try {
                image = Image.FromFile(filePath) as Bitmap;
            }
            catch (Exception) {
                return;
            }

            string newPath = Path.Combine(_dirForCutted, Path.GetFileName(filePath));

            image = ImageHelper.CutImage(image, _size, _border);

            image.Save(newPath, ImageFormat.Png);

            image.Dispose();
        }

        private static string DirPathForCutted(string originDirPath) {
            return Path.Combine(originDirPath, "cutted");
        }
    }
}