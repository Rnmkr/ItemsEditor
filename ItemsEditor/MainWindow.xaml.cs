using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ItemsEditor.DataAccessLayer;
using System.Net;
using readconfig;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace ItemsEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IQueryable<Item> ListaModelos;
        IQueryable<Item> ListaArticulos;
        IQueryable<Item> ListaCategorias;
        IQueryable<Item> ListaVersiones;
        int ItemID = 0;
        string ProductoSeleccionado;
        string ModeloSeleccionado;
        string ArticuloSeleccionado;
        string CategoriaSeleccionada;
        string VersionSeleccionada;
        string DescripcionSeleccionada = "INGRESAR DESCRIPCION";
        string UUIDSeleccionado = "INGRESAR UUID";
        bool OnlySaveImage = false;
        string serverip;
        string netPath;
        string tmpFile;
        NetworkCredential credentials = new NetworkCredential("EXO", "YQCAkrALNxBN9Mfn");

        string ItemsImageFile; //Nombre de la imagen en la carpeta items con la ruta completa
        string UserImageFile; //Nombre de la imagen seleccionada por el usuario con la ruta completa


        public MainWindow()
        {
            InitializeComponent();
            Borrar.IsEnabled = false;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PRDB"].ConnectionString.ToString();
            serverip = @"\\" + connectionString.Between("data source=", ";initial");
            netPath = serverip + @"\rma$\items\";
        }

        private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            if (ItemID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Esta a punto de borrar el item:" + Environment.NewLine + Environment.NewLine + "PRODUCTO: " + ProductoSeleccionado + Environment.NewLine + "MODELO: " + ModeloSeleccionado + Environment.NewLine + "CATEGORIA: " + CategoriaSeleccionada + Environment.NewLine + "ARTICULO: " + ArticuloSeleccionado + Environment.NewLine + "DESCRIPCIÓN: " + DescripcionSeleccionada + Environment.NewLine + "VERSION: " + VersionSeleccionada + Environment.NewLine + "UUID: " + UUIDSeleccionado + Environment.NewLine + "Tambíen se quitará la imagen correspondiente en el servidor." + Environment.NewLine + Environment.NewLine + "¿Está seguro?", "Confirmacion de borrado", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    int tempID = ItemID;
                    PRDB context = new PRDB();

                    Item item = context.Item.Where(w => w.ID == ItemID).First();
                    context.Item.Remove(item);

                    try
                    {
                        DeleteFileAndDirectory();
                    }
                    catch (Exception)
                    {
                        System.Windows.MessageBox.Show("No se pudo borrar la Imagen de la base de datos!" + Environment.NewLine + "No se borrará el registro. Intente nuevamente", "Error eliminando imagen", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    context.SaveChanges();

                    if (context.Item.Any(o => o.ID == ItemID))
                    {
                        System.Windows.MessageBox.Show("No se pudo borrar el Item en la base de datos!", "Error eliminando registro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        ItemID = 0;
                        System.Windows.MessageBox.Show("El registro se elimino correctamente!", "Registro eliminado!", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetFields();
                    }
                }
                return;
            }
            else
            {
                System.Windows.MessageBox.Show("No hay un item seleccionado!" + Environment.NewLine + "Seleccione todos los campos primero!", "Borrar Item", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void DeleteFileAndDirectory()
        {
            if (File.Exists(ItemsImageFile))
            {
                File.Delete(Path.Combine(ItemsImageFile));
            }
            string ArticuloFolder = Path.Combine(netPath, ProductoSeleccionado, ModeloSeleccionado, CategoriaSeleccionada, ArticuloSeleccionado);
            string CategoriaFolder = Path.Combine(netPath, ProductoSeleccionado, ModeloSeleccionado, CategoriaSeleccionada);
            string ModeloFolder = Path.Combine(netPath, ProductoSeleccionado, ModeloSeleccionado);
            string ProductoFolder = Path.Combine(netPath, ProductoSeleccionado);

            if (!Directory.EnumerateFiles(ArticuloFolder).Any())
            {
                using (new NetworkConnection(serverip, credentials))
                {
                    Directory.Delete(ArticuloFolder, false);
                }
            }
            else
            {
                return;
            }

            if (!Directory.EnumerateFiles(CategoriaFolder).Any())
            {
                using (new NetworkConnection(serverip, credentials))
                {
                    Directory.Delete(CategoriaFolder, false);
                }
            }
            else
            {
                return;
            }

            if (!Directory.EnumerateDirectories(ModeloFolder).Any())
            {
                using (new NetworkConnection(serverip, credentials))
                {
                    Directory.Delete(ModeloFolder, false);
                }
            }
            else
            {
                return;
            }

            if (!Directory.EnumerateDirectories(ProductoFolder).Any())
            {

                using (new NetworkConnection(serverip, credentials))
                {
                    Directory.Delete(ProductoFolder, false);
                }
            }
            else
            {
                return;
            }
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (ComboProducto.Text.Length > 30)
            {
                System.Windows.MessageBox.Show("PRODUCTO supera los 30 caracteres permitidos!", "Guardar nuevo item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ComboModelo.Text.Length > 30)
            {
                System.Windows.MessageBox.Show("MODELO supera los 30 caracteres permitidos!", "Guardar nuevo item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!char.IsDigit(ComboArticulo.Text, ComboArticulo.Text.Length - 1))
            {
                System.Windows.MessageBox.Show("ARTICULO solo acepta numeros!", "Guardar nuevo item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ComboArticulo.Text.Length > 10)
            {
                System.Windows.MessageBox.Show("ARTICULO supera los 10 caracteres permitidos!", "Guardar nuevo item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ComboCategoria.Text.Length > 5)
            {
                System.Windows.MessageBox.Show("CATEGORIA supera los 5 caracteres permitidos!", "Guardar nuevo item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ComboVersion.Text.Length > 30)
            {
                System.Windows.MessageBox.Show("VERSION supera los 30 caracteres permitidos!", "Guardar nuevo item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (TextBoxDescripcion.Text.Length > 30)
            {
                System.Windows.MessageBox.Show("DESCRIPCION supera los 30 caracteres permitidos!", "Guardar nuevo item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (TextBoxUUID.Text.Length > 30)
            {
                System.Windows.MessageBox.Show("UUID supera los 30 caracteres permitidos!", "Guardar nuevo item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (OnlySaveImage == true)
            {
                if (!SaveNewImage())
                {
                    return;
                }
                else
                {
                    ResetFields();
                    return;
                }

            }


            PRDB context = new PRDB();

            if (ComboProducto.Text == "SELECCIONAR" || ComboModelo.Text == "SELECCIONAR" || ComboArticulo.Text == "SELECCIONAR" || ComboCategoria.Text == "SELECCIONAR" || ComboVersion.Text == "SELECCIONAR" || TextBoxDescripcion.Text == "INGRESAR DESCRIPCION")
            {
                System.Windows.MessageBox.Show("No se puede ingresar un registro con campos vacios!" + Environment.NewLine + "El único campo no obligatorio es UUID.", "Guardar nuevo item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (String.IsNullOrWhiteSpace(ComboArticulo.Text) || String.IsNullOrWhiteSpace(ComboProducto.Text) || String.IsNullOrWhiteSpace(ComboModelo.Text) || String.IsNullOrWhiteSpace(ComboCategoria.Text) || String.IsNullOrWhiteSpace(TextBoxDescripcion.Text) || String.IsNullOrWhiteSpace(ComboVersion.Text) || String.IsNullOrWhiteSpace(TextBoxUUID.Text))
            {
                System.Windows.MessageBox.Show("No se puede ingresar un registro con campos vacios!" + Environment.NewLine + "El único campo no obligatorio es UUID.", "Guardar nuevo item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (context.Item.Any(o => o.TipoProducto == ComboProducto.Text && o.ModeloProducto == ComboModelo.Text && o.ArticuloItem == ComboArticulo.Text && o.CategoriaItem == ComboCategoria.Text && o.VersionItem == ComboVersion.Text))
            {
                System.Windows.MessageBox.Show("El item que desea agregar ya existe en la Base de Datos!", "Item duplicado!", MessageBoxButton.OK, MessageBoxImage.Warning);
                context.Dispose();
                return;
            }

            if (TextBoxUUID.Text == "INGRESAR UUID" || String.IsNullOrWhiteSpace(TextBoxUUID.Text))
            {
                UUIDSeleccionado = "N/A";
            }
            else
            {
                UUIDSeleccionado = TextBoxUUID.Text.TrimEnd();
            }

            if (UserImageFile == null)
            {
                System.Windows.MessageBox.Show("No se puede ingresar el registro sin asignar una imagen!", "Guardar nuevo item", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadNewImage();
                return;
            }

            try
            {
                Item nuevoItem = new Item
                {
                    TipoProducto = ComboProducto.Text.ToUpper().TrimEnd(),
                    ModeloProducto = ComboModelo.Text.ToUpper().TrimEnd(),
                    ArticuloItem = ComboArticulo.Text.ToUpper().TrimEnd(),
                    CategoriaItem = ComboCategoria.Text.ToUpper().TrimEnd(),
                    DescripcionItem = TextBoxDescripcion.Text.ToUpper(),
                    VersionItem = ComboVersion.Text.ToUpper().TrimEnd(),
                    UUID = UUIDSeleccionado,

                };
                if (!SaveNewImage())
                {
                    return;
                }
                else
                {
                    context.Item.Add(nuevoItem);
                    context.SaveChanges();

                    System.Windows.MessageBox.Show("El item se agregó a la Base de Datos!", "Item Agregado", MessageBoxButton.OK, MessageBoxImage.Information);
                    context.Dispose();
                    ResetFields();
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "¿Alguno de los datos ingresados no es válido?", MessageBoxButton.OK, MessageBoxImage.Warning);
                context.Dispose();
                return;
            }
        }
        private void File_Click(object sender, RoutedEventArgs e)
        {
            LoadNewImage();
        }

        private void ResetFields()
        {
            Agregar.Content = "AGREGAR";
            ComboProducto.Text = "SELECCIONAR";
            ComboModelo.Text = "SELECCIONAR";
            ComboArticulo.Text = "SELECCIONAR";
            ComboCategoria.Text = "SELECCIONAR";
            ComboVersion.Text = "SELECCIONAR";
            TextBoxDescripcion.Text = "INGRESAR DESCRIPCION";
            TextBoxUUID.Text = "INGRESAR UUID";
            TextBoxFile.Text = "SELECCIONAR ARCHIVO .JPG (960X480)";
            //TextBoxFolder.Text = "SELECCIONAR CARPETA DESTINO (ITEMS)";

            ComboProducto.SelectedIndex = -1;
            ComboModelo.SelectedIndex = -1;
            ComboArticulo.SelectedIndex = -1;
            ComboCategoria.SelectedIndex = -1;
            ComboVersion.SelectedIndex = -1;

            ComboProducto.ItemsSource = null;
            ComboModelo.ItemsSource = null;
            ComboArticulo.ItemsSource = null;
            ComboCategoria.ItemsSource = null;
            ComboVersion.ItemsSource = null;

            ModeloSeleccionado = null;
            ArticuloSeleccionado = null;
            CategoriaSeleccionada = null;
            VersionSeleccionada = null;
            DescripcionSeleccionada = "SELECCIONAR";
            UUIDSeleccionado = "INGRESAR UUID";

            Borrar.IsEnabled = false;
            OnlySaveImage = false;
            ItemID = 0;
            UserImageFile = null;
            ImageDisplay.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/ItemsEditor;component/Resources/nophoto.png") as ImageSource;
        }
        private void HalfReset(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Borrar.IsEnabled = false;
            ItemID = 0;
            OnlySaveImage = false;
            TextBoxFile.Text = "SELECCIONAR ARCHIVO .JPG (960X480)";
            UserImageFile = null;
            ImageDisplay.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/ItemsEditor;component/Resources/nophoto.png") as ImageSource;
        }

        private void LoadNewImage()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Sólo Archivos JPG (*.jpg, *.jpg)|*.jpg";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                UserImageFile = dlg.FileName;
                var bitmapFrame = BitmapFrame.Create(new Uri(UserImageFile), BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
                int width = bitmapFrame.PixelWidth;
                int height = bitmapFrame.PixelHeight;
                if (width > 960)
                {
                    System.Windows.MessageBox.Show("...el ancho de la imagen es mayor a 960 píxeles, considere editar la imagen para dejarlo cerca de este valor y asi reducir su peso.", "La imagen se cargará, pero...", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (height > 480)
                {
                    System.Windows.MessageBox.Show("...el alto de la imagen es mayor a 480 píxeles, considere editar la imagen para dejarlo cerca de este valor y asi reducir su peso.", "La imagen se cargará, pero...", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                TextBoxFile.Text = UserImageFile;
                ImageDisplay.Source = (new ImageSourceConverter()).ConvertFromString(UserImageFile) as ImageSource;
                if (ItemID != 0)
                {
                    OnlySaveImage = true;
                    Agregar.Content = "ACTUALIZAR IMAGEN";
                }
            }
        }

        private bool SaveNewImage()
        {

            try
            {
                using (new NetworkConnection(serverip, credentials))
                {
                    string NewItemsImageFile = Path.Combine(netPath, ComboProducto.Text.TrimEnd(), ComboModelo.Text.TrimEnd(), ComboCategoria.Text.TrimEnd(), ComboArticulo.Text.TrimEnd(), ComboVersion.Text.TrimEnd() + ".JPG").ToUpper(); ;
                    Directory.CreateDirectory(Path.GetDirectoryName(NewItemsImageFile));
                    File.Copy(UserImageFile, NewItemsImageFile, true);
                    System.Windows.MessageBox.Show("La imagen se asignó correctamente!", "Guardar imagen", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return true;
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Ocurrió un error al guardar, verifique que la IP del servidor sea correcta", "Guardar", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }


        private void ComboProducto_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ComboProducto.Text == "SELECCIONAR")
            {
                ComboProducto.Text = "";
            }
        }
        private void ComboProducto_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ComboProducto.Text))
            {
                ComboProducto.Text = "SELECCIONAR";
            }
        }
        private void ComboModelo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ComboModelo.Text == "SELECCIONAR")
            {
                ComboModelo.Text = "";
            }
        }
        private void ComboModelo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ComboModelo.Text))
            {
                ComboModelo.Text = "SELECCIONAR";
            }
        }
        private void ComboArticulo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ComboArticulo.Text == "SELECCIONAR")
            {
                ComboArticulo.Text = "";
            }
        }
        private void ComboArticulo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ComboArticulo.Text))
            {
                ComboArticulo.Text = "SELECCIONAR";
            }
        }
        private void ComboCategoria_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ComboCategoria.Text == "SELECCIONAR")
            {
                ComboCategoria.Text = "";
            }
        }
        private void ComboCategoria_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ComboCategoria.Text))
            {
                ComboCategoria.Text = "SELECCIONAR";
            }
        }
        private void ComboVersion_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ComboVersion.Text == "SELECCIONAR")
            {
                ComboVersion.Text = "";
            }
        }
        private void ComboVersion_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ComboVersion.Text))
            {
                ComboVersion.Text = "SELECCIONAR";
            }
        }
        private void TextBoxDescripcion_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxDescripcion.Text == "INGRESAR DESCRIPCION")
            {
                TextBoxDescripcion.Text = "";
            }

            if (TextBoxDescripcion.Text == "N/A")
            {
                TextBoxDescripcion.Text = "";
            }
        }
        private void TextBoxDescripcion_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBoxDescripcion.Text))
            {
                TextBoxDescripcion.Text = DescripcionSeleccionada;
            }
        }
        private void TextBoxUUID_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxUUID.Text == "INGRESAR UUID")
            {
                TextBoxUUID.Text = "";
            }

            if (TextBoxUUID.Text == "N/A")
            {
                TextBoxUUID.Text = "";
            }
        }
        private void TextBoxUUID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBoxUUID.Text))
            {
                TextBoxUUID.Text = UUIDSeleccionado;
            }
        }

        private void ComboProducto_DropDownOpened(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                if (SimplePing() == false)
                {
                    MessageBox.Show("No se encontró el servidor." + Environment.NewLine + "Revise la conexión con la Base de Datos y reintente.", "Conectando al servidor", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (ComboProducto.HasItems == false)
                {
                    try
                    {
                        PRDB context = new PRDB();
                        ComboProducto.ItemsSource = context.Item.Select(s => s.TipoProducto).Distinct().ToList();
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show("Ocurrió un error contactando al servidor. Revise la conexión con la Base de Datos.", "Error contactando la Base de Datos", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }
        private void ComboProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboProducto.SelectedValue != null)
            {
                ProductoSeleccionado = ComboProducto.SelectedValue.ToString();

                ComboModelo.Text = "SELECCIONAR";
                ComboArticulo.Text = "SELECCIONAR";
                ComboCategoria.Text = "SELECCIONAR";
                ComboVersion.Text = "SELECCIONAR";
                TextBoxDescripcion.Text = "INGRESAR DESCRIPCION";
                TextBoxUUID.Text = "INGRESAR UUID";
                ComboModelo.SelectedIndex = -1;
                ComboArticulo.SelectedIndex = -1;
                ComboCategoria.SelectedIndex = -1;
                ComboVersion.SelectedIndex = -1;
                ComboModelo.ItemsSource = null;
                ComboArticulo.ItemsSource = null;
                ComboCategoria.ItemsSource = null;
                ComboVersion.ItemsSource = null;
                ModeloSeleccionado = null;
                ArticuloSeleccionado = null;
                CategoriaSeleccionada = null;
                VersionSeleccionada = null;
                ItemID = 0;
                Borrar.IsEnabled = false;
                OnlySaveImage = false;
                TextBoxFile.Text = "SELECCIONAR ARCHIVO .JPG (960X480)";
                UserImageFile = null;
                ImageDisplay.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/ItemsEditor;component/Resources/nophoto.png") as ImageSource;
            }
        }
        private void ComboModelo_DropDownOpened(object sender, EventArgs e)
        {
            if (ComboProducto.Text == "SELECCIONAR" || string.IsNullOrWhiteSpace(ComboProducto.Text))
            {
                return;
            }
            else
            {
                using (new WaitCursor())
                {
                    PRDB context = new PRDB();

                    ListaModelos = context.Item.Where(w => w.TipoProducto == ProductoSeleccionado).Select(s => s);
                    ComboModelo.ItemsSource = ListaModelos.Select(s => s.ModeloProducto).Distinct().ToList();
                }
            }
        }
        private void ComboModelo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboModelo.SelectedValue != null)
            {
                ModeloSeleccionado = ComboModelo.SelectedValue.ToString();


                ComboArticulo.Text = "SELECCIONAR";
                ComboCategoria.Text = "SELECCIONAR";
                ComboVersion.Text = "SELECCIONAR";
                TextBoxDescripcion.Text = "INGRESAR DESCRIPCION";
                TextBoxUUID.Text = "INGRESAR UUID";
                ComboArticulo.SelectedIndex = -1;
                ComboCategoria.SelectedIndex = -1;
                ComboVersion.SelectedIndex = -1;
                ComboArticulo.ItemsSource = null;
                ComboCategoria.ItemsSource = null;
                ComboVersion.ItemsSource = null;
                ArticuloSeleccionado = null;
                CategoriaSeleccionada = null;
                VersionSeleccionada = null;
                Borrar.IsEnabled = false;
                ItemID = 0;
                OnlySaveImage = false;
                TextBoxFile.Text = "SELECCIONAR ARCHIVO .JPG (960X480)";
                UserImageFile = null;
                ImageDisplay.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/ItemsEditor;component/Resources/nophoto.png") as ImageSource;
            }
        }
        private void ComboArticulo_DropDownOpened(object sender, EventArgs e)
        {
            if (ComboModelo.Text == "SELECCIONAR" || string.IsNullOrWhiteSpace(ComboModelo.Text))
            {
                return;
            }
            else
            {
                using (new WaitCursor())
                {
                    ListaArticulos = ListaModelos.Where(w => w.ModeloProducto == ModeloSeleccionado).Select(s => s);
                    ComboArticulo.ItemsSource = ListaArticulos.Select(s => s.ArticuloItem).Distinct().ToList();
                }
            }
        }
        private void ComboArticulo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboArticulo.SelectedValue != null)
            {
                ArticuloSeleccionado = ComboArticulo.SelectedValue.ToString();

                ComboCategoria.Text = "SELECCIONAR";
                ComboVersion.Text = "SELECCIONAR";
                TextBoxDescripcion.Text = "INGRESAR DESCRIPCION";
                TextBoxUUID.Text = "INGRESAR UUID";
                ComboCategoria.SelectedIndex = -1;
                ComboVersion.SelectedIndex = -1;
                ComboCategoria.ItemsSource = null;
                ComboVersion.ItemsSource = null;
                CategoriaSeleccionada = null;
                VersionSeleccionada = null;
                Borrar.IsEnabled = false;
                ItemID = 0;
                OnlySaveImage = false;
                TextBoxFile.Text = "SELECCIONAR ARCHIVO .JPG (960X480)";
                UserImageFile = null;
                ImageDisplay.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/ItemsEditor;component/Resources/nophoto.png") as ImageSource;
            }
        }
        private void ComboCategoria_DropDownOpened(object sender, EventArgs e)
        {
            if (ComboArticulo.Text == "SELECCIONAR" || string.IsNullOrWhiteSpace(ComboArticulo.Text))
            {
                return;
            }
            else
            {
                using (new WaitCursor())
                {
                    ListaCategorias = ListaArticulos.Where(w => w.ArticuloItem == ArticuloSeleccionado).Select(s => s);
                    ComboCategoria.ItemsSource = ListaCategorias.Select(s => s.CategoriaItem).Distinct().ToList();
                }
            }
        }
        private void ComboCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboCategoria.SelectedValue != null)
            {
                CategoriaSeleccionada = ComboCategoria.SelectedValue.ToString();

                ComboVersion.Text = "SELECCIONAR";
                TextBoxDescripcion.Text = "INGRESAR DESCRIPCION";
                TextBoxUUID.Text = "INGRESAR UUID";
                ComboVersion.SelectedIndex = -1;
                ComboVersion.ItemsSource = null;
                VersionSeleccionada = null;
                Borrar.IsEnabled = false;
                ItemID = 0;
                OnlySaveImage = false;
                TextBoxFile.Text = "SELECCIONAR ARCHIVO .JPG (960X480)";
                UserImageFile = null;
                ImageDisplay.Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/ItemsEditor;component/Resources/nophoto.png") as ImageSource;
            }
        }
        private void ComboVersion_DropDownOpened(object sender, EventArgs e)
        {
            if (ComboCategoria.Text == "SELECCIONAR" || string.IsNullOrWhiteSpace(ComboCategoria.Text))
            {
                return;
            }
            else
            {
                using (new WaitCursor())
                {
                    ListaVersiones = ListaCategorias.Where(w => w.CategoriaItem == CategoriaSeleccionada).Select(s => s);
                    ComboVersion.ItemsSource = ListaVersiones.Select(s => s.VersionItem).Distinct().ToList();
                }
            }
        }
        private void ComboVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (new WaitCursor())
            {
                if (SimplePing() == false)
                {
                    MessageBox.Show("Se perdió la conexion con el servidor." + Environment.NewLine + "Revise la conexión con la Base de Datos y reintente.", "Conectando al servidor", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (ComboVersion.SelectedValue != null)
                {
                    VersionSeleccionada = ComboVersion.SelectedValue.ToString();

                    Item item = ListaVersiones.Where(w => w.VersionItem == VersionSeleccionada).Select(s => s).Single();
                    DescripcionSeleccionada = item.DescripcionItem;
                    UUIDSeleccionado = item.UUID;
                    TextBoxDescripcion.Text = DescripcionSeleccionada;
                    TextBoxUUID.Text = UUIDSeleccionado;
                    ItemID = item.ID;
                    Borrar.IsEnabled = true;

                    if (netPath != null)
                    {
                        using (new NetworkConnection(serverip, credentials))
                        {
                            string ShortImageFile = Path.Combine(ProductoSeleccionado, ModeloSeleccionado, CategoriaSeleccionada, ArticuloSeleccionado, VersionSeleccionada + ".JPG");
                            ItemsImageFile = Path.Combine(netPath, ShortImageFile);

                            if (File.Exists(ItemsImageFile))
                            {
                                string uniqueFileName = $@"{Guid.NewGuid()}.JPG";
                                tmpFile = Path.Combine(Path.GetTempPath(), uniqueFileName);
                                File.Copy(ItemsImageFile, tmpFile, true);

                                Bitmap bmp = new Bitmap(tmpFile);
                                MemoryStream memoryStream = new MemoryStream();
                                bmp.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                                BitmapImage bitmapImage = new BitmapImage();
                                bitmapImage.BeginInit();
                                bitmapImage.StreamSource = new MemoryStream(memoryStream.ToArray());
                                bitmapImage.EndInit();
                                ImageDisplay.Source = bitmapImage;
                                TextBoxFile.Text = (@"\\" + "ITEMS" + @"\" + ShortImageFile);
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("No se encontró la imágen correspondiente a " + TextBoxDescripcion.Text + " de " + ModeloSeleccionado + " version " + VersionSeleccionada + "." + Environment.NewLine + Environment.NewLine + "Seleccione una a continuación.", "Imagen del Item", MessageBoxButton.OK, MessageBoxImage.Warning);
                                LoadNewImage();
                                OnlySaveImage = true;
                                Agregar.Content = "ACTUALIZAR IMAGEN";
                            }
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("No se encontró la carpeta de imágenes. Verifique que el servidor este en linea y reintente", "Actualizar item", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void ComboProducto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9a-zA-Z-]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void ComboModelo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9a-zA-Z-]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void ComboArticulo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void ComboCategoria_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9a-zA-Z-]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void ComboVersion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9a-zA-Z-]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void TextBoxDescripcion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9a-zA-Z-]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void TextBoxUUID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9a-zA-Z-]");
            e.Handled = regex.IsMatch(e.Text);
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
            string ServerIP = connectionString.Between("data source=", ";initial");
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(ServerIP);

            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
