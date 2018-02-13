using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ImageCutter.Views.Main {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private MainPresenter _presenter;

        public MainWindow() {
            InitializeComponent();
            _presenter = new MainPresenter(this);

            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }

        public string DirectoryText {
            get => DirectoryControl.Text;
            set => DirectoryControl.Text = value;
        }

        public int SizeText {
            get => int.Parse(SizeControl.Text);
            set => SizeControl.Text = value.ToString();
        }

        public double BorderText {
            get => double.Parse(BorderControl.Text);
            set => BorderControl.Text = value.ToString();
        }

        private void CutButtonControl_Click(object sender, RoutedEventArgs e) {
            _presenter.OnCutButtonClick();
        }
    }
}