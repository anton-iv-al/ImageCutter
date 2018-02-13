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
        private string _originDirKey = "origin_dir";
        private string _sizeKey = "size";
        private string _borderKey = "border";

        public MainPresenter(MainWindow view) : base(view) {
            SetWindowFromConfig();
        }

        public void OnCutButtonClick() {
            if (!TryGetParamsFromWindow(out string dir, out int size, out double border)) return;

            var configurationParams = new Dictionary<string, string>() {
                {_originDirKey, dir},
                {_sizeKey, size.ToString()},
                {_borderKey, border.ToString()}
            };
            ConfigurationHelper.SaveConfiguration(configurationParams);
            
            

            Task.Run(() => {
                try {
                    DirectoryHelper.CutAllImagesInDir(dir, size, border, SetProcessText);
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                    SetProcessText(String.Empty);
                }
            });
        }

        private void SetProcessText(string text) => _view.Dispatcher.Invoke(() => _view.SetProgressText(text));

        private bool TryGetParamsFromWindow(out string dir, out int size, out double border) {
            dir = _view.DirectoryText;
            size = 0;
            border = 0;

            try {
                size = _view.SizeText;
            }
            catch (Exception) {
                MessageBox.Show("Size string wrong format");
                return false;
            }

            try {
                border = _view.BorderText;
            }
            catch (Exception) {
                MessageBox.Show("Border string wrong format");
                return false;
            }

            return true;
        }

        private void SetWindowFromConfig() {
            var configParams = ConfigurationHelper.LoadConfiguration();

            if (configParams.ContainsKey(_originDirKey)) {
                _view.DirectoryText = configParams[_originDirKey];
            }

            if (configParams.ContainsKey(_sizeKey)) {
                try {
                    _view.SizeText = int.Parse(configParams[_sizeKey]);
                }
                catch (Exception) { }
            }

            if (configParams.ContainsKey(_borderKey)) {
                try {
                    _view.BorderText = double.Parse(configParams[_borderKey]);
                }
                catch (Exception) { }
            }
        }
    }
}