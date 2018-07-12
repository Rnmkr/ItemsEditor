using System.Windows;
using System.Windows.Media.Imaging;

namespace ItemsEditor
{
    /// <summary>
    /// Interaction logic for ImageDisplayDialog.xaml
    /// </summary>
    public partial class ImageDisplayDialog : Window
    {
        public ImageDisplayDialog(BitmapImage image)
        {
            InitializeComponent();
            ImageDisplay.Source = image;
        }

        private void Window_LostFocus(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
