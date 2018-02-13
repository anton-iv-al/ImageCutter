using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageCutter {
    public static class ImageHelper {
        private static Bitmap _image;

        public static Bitmap CutImage(Bitmap image, int newSize, double border) {
            _image = image;

            var rectForCut = RectangleForCut();

            var newImage = NewEmptyImage(rectForCut, newSize, border, out var rectForPaste);

            var graphics = Graphics.FromImage(newImage);
            graphics.DrawImage(_image, rectForPaste, rectForCut, GraphicsUnit.Pixel);

            return newImage;
        }

        private static Bitmap NewEmptyImage(Rectangle rectForCut, int newSize, double border, out Rectangle rectForPaste) {
            int maxSize = Math.Max(rectForCut.Width, rectForCut.Height);
            int minSize = Math.Min(rectForCut.Width, rectForCut.Height);
            int offset = (maxSize - minSize) / 2;
            bool isWidthMax = rectForCut.Width == maxSize;

            int borderPx = (int) (newSize * border);

            double scaleFactor = (double)(newSize - borderPx*2) / maxSize;

            int scaledOffset = (int)(offset * scaleFactor);

            int rectForCutWidthScaled = (int)(rectForCut.Width * scaleFactor);
            int rectForCutHeightScaled = (int)(rectForCut.Height * scaleFactor);
            var rectForPasteSize = new System.Drawing.Size(rectForCutWidthScaled, rectForCutHeightScaled);

            var offsetPoint = isWidthMax ?
                new System.Drawing.Point(borderPx, scaledOffset + borderPx) :
                new System.Drawing.Point(scaledOffset + borderPx, borderPx);

            rectForPaste = new Rectangle(offsetPoint, rectForPasteSize);

            return new Bitmap(newSize, newSize);
        }

        private static Rectangle RectangleForCut() {
            int fromLeft = FirstPixelColumnFromLeft();

            if(fromLeft < 0) return new Rectangle(0, 0, 1, 1);

            int fromRight = FirstPixelColumnFromRight();
            int fromUp = FirstPixelRowFromUp();
            int fromDown = FirstPixelRowFromDown();

            int x = fromLeft;
            int y = fromUp;
            int width = fromRight - fromLeft + 1;
            int height = fromDown - fromUp + 1;

            return new Rectangle(x, y, width, height);
        }

        private static int FirstPixelColumnFromLeft() {
            for (int i = 0; i < _image.Width; i++) {
                if (!IsPixelColumnEmpty(i)) return i;
            }

            return -1;
        }

        private static int FirstPixelColumnFromRight() {
            for (int i = _image.Width - 1; i >= 0; i--) {
                if (!IsPixelColumnEmpty(i)) return i;
            }

            return -1;
        }

        private static int FirstPixelRowFromUp() {
            for (int i = 0; i < _image.Height; i++) {
                if (!IsPixelRowEmpty(i)) return i;
            }

            return -1;
        }

        private static int FirstPixelRowFromDown() {
            for (int i = _image.Height - 1; i >= 0; i--) {
                if (!IsPixelRowEmpty(i)) return i;
            }

            return -1;
        }

        private static bool IsPixelColumnEmpty(int x) {
            for (int i = 0; i < _image.Height; i++) {
                if (!IsColorTransparent(_image.GetPixel(x, i))) return false;
            }

            return true;
        }

        private static bool IsPixelRowEmpty(int y) {
            for (int i = 0; i < _image.Width; i++) {
                if (!IsColorTransparent(_image.GetPixel(i, y))) return false;
            }

            return true;
        }

        private static bool IsColorTransparent(Color color) {
            return color.A == 0;
        }
    }
}