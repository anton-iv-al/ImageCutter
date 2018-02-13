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

            DirectoryHelper.CutAllImagesInDir(dir, size, border);
        }
    }
}