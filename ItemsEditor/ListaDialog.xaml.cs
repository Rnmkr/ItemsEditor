using ItemsEditor.DataAccessLayer;
using readconfig;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ItemsEditor
{
    /// <summary>
    /// Interaction logic for ListaDialog.xaml
    /// </summary>
    public partial class ListaDialog : Window
    {
        private string nPath;
        public ListaDialog(IQueryable<Item> Listado, string netPath)
        {
            InitializeComponent();
            var li = Listado.ToList();
            DataGr.ItemsSource = li;
            nPath = netPath;
        }


        private void showImage()
        {
            Item it = (Item)DataGr.SelectedItem;

            using (new WaitCursor())
            {
                if (SimplePing() == false)
                {
                    MessageBox.Show("Se perdió la conexion con el servidor." + Environment.NewLine + "Revise la conexión con la Base de Datos y reintente.", "Conectando al servidor", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            if (it.VersionItem != null)
            {
                if (nPath != null)
                {
                    string ShortImageFile = Path.Combine(it.TipoProducto, it.ModeloProducto, it.CategoriaItem, it.ArticuloItem, it.VersionItem + ".JPG");
                    string ItemsImageFile = Path.Combine(nPath, ShortImageFile);

                    if (File.Exists(ItemsImageFile))
                    {
                        string uniqueFileName = $@"{Guid.NewGuid()}.JPG";
                        string tmpFile = Path.Combine(Path.GetTempPath(), uniqueFileName);
                        File.Copy(ItemsImageFile, tmpFile, true);

                        Bitmap bmp = new Bitmap(tmpFile);
                        MemoryStream memoryStream = new MemoryStream();
                        bmp.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(memoryStream.ToArray());
                        bitmapImage.EndInit();
                        ImageDisplayDialog idd = new ImageDisplayDialog(bitmapImage);
                        idd.ShowDialog();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("No se encontró la imágen correspondiente a este item", "Imagen del Item", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("No se encontró la carpeta de imágenes. Verifique que el servidor este en linea y reintente", "Actualizar item", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        public class WaitCursor : IDisposable
        {
            private Cursor _previousCursor;

            public WaitCursor()
            {
                _previousCursor = Mouse.OverrideCursor;

                Mouse.OverrideCursor = Cursors.Wait;
            }

            #region IDisposable Members

            public void Dispose()
            {
                Mouse.OverrideCursor = _previousCursor;
            }

            #endregion
        }
        public static bool SimplePing()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PRDB"].ConnectionString.ToString();
            string HostName = connectionString.Between("data source=", ";initial");
            try
            {
                IPAddress[] ip = Dns.GetHostAddresses(HostName);
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(ip[0]);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            showImage();
        }
    }
}
