using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WinForms = System.Windows.Forms;
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
        string DescripcionSeleccionada = "DESCRIPCION";
        string UUIDSeleccionado = "UUID";
        bool OnlySaveImage = false;
        string LocalDir = AppDomain.CurrentDomain.BaseDirectory;

        string ItemsFolderPath; //ruta a la carpeta Items

        string ItemsImageFile; //Nombre de la imagen en la carpeta items con la ruta completa
        string UserImageFile; //Nombre de la imagen seleccionada por el usuario con la ruta completa

        public MainWindow()
        {
            InitializeComponent();
            ReadDefaultItemsPath();
            Borrar.IsEnabled = false;

        }

        private void ReadDefaultItemsPath()
        {

            string configuracion = Path.Combine(LocalDir, "defaultpath.cfg");
            if (File.Exists(configuracion))
            {
                ItemsFolderPath = System.IO.File.ReadAllText(configuracion);
                TextBoxFolder.Text = ItemsFolderPath;
            }
            else
            {
                ItemsFolderPath = null;
            }
        }

        private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            if (ItemID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Esta a punto de borrar el item:" + Environment.NewLine + Environment.NewLine + "PRODUCTO: " + ProductoSeleccionado + Environment.NewLine + "MODELO: " +  ModeloSeleccionado + Environment.NewLine + "CATEGORIA: " + CategoriaSeleccionada + Environment.NewLine + "ARTICULO: " + ArticuloSeleccionado + Environment.NewLine + "DESCRIPCIÓN: " + DescripcionSeleccionada + Environment.NewLine + "VERSION: " + VersionSeleccionada + Environment.NewLine + "UUID: " + UUIDSeleccionado + Environment.NewLine + "Tambíen se quitará la imagen correspondiente en el servidor." + Environment.NewLine + Environment.NewLine + "¿Está seguro?", "Confirmacion de borrado", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
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
            string ArticuloFolder = Path.Combine(ItemsFolderPath, ProductoSeleccionado, ModeloSeleccionado, CategoriaSeleccionada, ArticuloSeleccionado);
            string CategoriaFolder = Path.Combine(ItemsFolderPath, ProductoSeleccionado, ModeloSeleccionado, CategoriaSeleccionada);
            string ModeloFolder = Path.Combine(ItemsFolderPath, ProductoSeleccionado, ModeloSeleccionado);
            string ProductoFolder = Path.Combine(ItemsFolderPath, ProductoSeleccionado);
            if (!Directory.EnumerateFiles(ArticuloFolder).Any())
            {
                Directory.Delete(ArticuloFolder, false);
            }
            else
            {
                return;
            }

            if (!Directory.EnumerateFiles(CategoriaFolder).Any())
            {
                Directory.Delete(CategoriaFolder, false);
            }
            else
            {
                return;
            }

            if (!Directory.EnumerateDirectories(ModeloFolder).Any())
            {
                Directory.Delete(ModeloFolder, false);
            }
            else
            {
                return;
            }

            if (!Directory.EnumerateDirectories(ProductoFolder).Any())
            {
                Directory.Delete(ProductoFolder, false);
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
            if (OnlySaveImage == true)
            {
                SaveNewImage();
                ResetFields();
                return;
            }

            PRDB context = new PRDB();

            if (ComboProducto.Text == "PRODUCTO" || ComboModelo.Text == "MODELO" || ComboArticulo.Text == "ARTICULO" || ComboCategoria.Text == "CATEGORIA" || ComboVersion.Text == "VERSION" || TextBoxDescripcion.Text == "DESCRIPCION")
            {
                System.Windows.MessageBox.Show("No se puede ingresar un registro con campos vacios!" + Environment.NewLine + "El único campo no obligatorio es UUID.", "Guardar nuevo item", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (String.IsNullOrEmpty(ComboArticulo.Text) || String.IsNullOrEmpty(ComboProducto.Text) || String.IsNullOrEmpty(ComboModelo.Text) || String.IsNullOrEmpty(ComboCategoria.Text) || String.IsNullOrEmpty(TextBoxDescripcion.Text) || String.IsNullOrEmpty(ComboVersion.Text) || String.IsNullOrEmpty(TextBoxUUID.Text))
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

            if (TextBoxUUID.Text == "UUID" || String.IsNullOrEmpty(TextBoxUUID.Text))
            {
                UUIDSeleccionado = "N/A";
            }
            else
            {
                UUIDSeleccionado = TextBoxUUID.Text;
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
                    TipoProducto = ComboProducto.Text.ToUpper(),
                    ModeloProducto = ComboModelo.Text.ToUpper(),
                    ArticuloItem = ComboArticulo.Text.ToUpper(),
                    CategoriaItem = ComboCategoria.Text.ToUpper(),
                    DescripcionItem = TextBoxDescripcion.Text.ToUpper(),
                    VersionItem = ComboVersion.Text.ToUpper(),
                    UUID = TextBoxUUID.Text,

                };
                context.Item.Add(nuevoItem);
                context.SaveChanges();
                SaveNewImage();
                System.Windows.MessageBox.Show("El item se agregó a la Base de Datos!", "Item Agregado", MessageBoxButton.OK, MessageBoxImage.Information);
                context.Dispose();
                ResetFields();
                return;
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

        private void Folder_Click(object sender, RoutedEventArgs e)
        {
            SelectItemsFolder();
        }

        private void ResetFields()
        {
            Agregar.Content = "AGREGAR";
            ComboProducto.Text = "PRODUCTO";
            ComboModelo.Text = "MODELO";
            ComboArticulo.Text = "ARTICULO";
            ComboCategoria.Text = "CATEGORIA";
            ComboVersion.Text = "VERSION";
            TextBoxDescripcion.Text = "DESCRIPCION";
            TextBoxUUID.Text = "UUID";
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
            DescripcionSeleccionada = "DESCRIPCION";
            UUIDSeleccionado = "UUID";

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
        private void SelectItemsFolder()
        {
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            WinForms.DialogResult result = folderDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                String sPath = folderDialog.SelectedPath;
                TextBoxFolder.Text = sPath;
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + "defaultpath.cfg", sPath);
                ItemsFolderPath = sPath;
            }
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
        private void SaveNewImage()
        {
            if (ItemsFolderPath == null)
            {
                System.Windows.MessageBox.Show("No se encontró la carpeta Items. Seleccione la ubicación correspondiente a continuación.", "Guardar", MessageBoxButton.OK, MessageBoxImage.Warning);
                SelectItemsFolder();
                return;
            }
            else
            {
                try
                {
                    string NewItemsImageFile = Path.Combine(ItemsFolderPath, ComboProducto.Text, ComboModelo.Text, ComboCategoria.Text, ComboArticulo.Text, ComboVersion.Text + ".JPG");
                    Directory.CreateDirectory(Path.GetDirectoryName(NewItemsImageFile));
                    File.Copy(UserImageFile, NewItemsImageFile, true);
                    System.Windows.MessageBox.Show("La imagen se asignó correctamente!", "Guardar imagen", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString(), "Guardar", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void TextBoxDescripcion_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxDescripcion.Text == "DESCRIPCION")
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
            if (String.IsNullOrEmpty(TextBoxDescripcion.Text))
            {
                TextBoxDescripcion.Text = DescripcionSeleccionada;
            }
        }
        private void TextBoxUUID_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxUUID.Text == "UUID")
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
            if (String.IsNullOrEmpty(TextBoxUUID.Text))
            {
                TextBoxUUID.Text = UUIDSeleccionado;
            }
        }

        private void ComboProducto_DropDownOpened(object sender, EventArgs e)
        {
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
        private void ComboProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboProducto.SelectedValue != null)
            {
                ProductoSeleccionado = ComboProducto.SelectedValue.ToString();

                ComboModelo.Text = "MODELO";
                ComboArticulo.Text = "ARTICULO";
                ComboCategoria.Text = "CATEGORIA";
                ComboVersion.Text = "VERSION";
                TextBoxDescripcion.Text = "DESCRIPCION";
                TextBoxUUID.Text = "UUID";
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
            if (ComboProducto.Text == "PRODUCTO" || string.IsNullOrWhiteSpace(ComboProducto.Text))
            {
                return;
            }
            else
            {
                PRDB context = new PRDB();
                ListaModelos = context.Item.Where(w => w.TipoProducto == ProductoSeleccionado).Select(s => s);
                ComboModelo.ItemsSource = ListaModelos.Select(s => s.ModeloProducto).Distinct().ToList();
            }
        }
        private void ComboModelo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboModelo.SelectedValue != null)
            {
                ModeloSeleccionado = ComboModelo.SelectedValue.ToString();

                ComboArticulo.Text = "ARTICULO";
                ComboCategoria.Text = "CATEGORIA";
                ComboVersion.Text = "VERSION";
                TextBoxDescripcion.Text = "DESCRIPCION";
                TextBoxUUID.Text = "UUID";
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
            if (ComboModelo.Text == "MODELO" || string.IsNullOrWhiteSpace(ComboModelo.Text))
            {
                return;
            }
            else
            {
                ListaArticulos = ListaModelos.Where(w => w.ModeloProducto == ModeloSeleccionado).Select(s => s);
                ComboArticulo.ItemsSource = ListaArticulos.Select(s => s.ArticuloItem).Distinct().ToList();
            }
        }
        private void ComboArticulo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboArticulo.SelectedValue != null)
            {
                ArticuloSeleccionado = ComboArticulo.SelectedValue.ToString();

                ComboCategoria.Text = "CATEGORIA";
                ComboVersion.Text = "VERSION";
                TextBoxDescripcion.Text = "DESCRIPCION";
                TextBoxUUID.Text = "UUID";
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
            if (ComboArticulo.Text == "ARTICULO" || string.IsNullOrWhiteSpace(ComboArticulo.Text))
            {
                return;
            }
            else
            {
                ListaCategorias = ListaArticulos.Where(w => w.ArticuloItem == ArticuloSeleccionado).Select(s => s);
                ComboCategoria.ItemsSource = ListaCategorias.Select(s => s.CategoriaItem).Distinct().ToList();
            }
        }
        private void ComboCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboCategoria.SelectedValue != null)
            {
                CategoriaSeleccionada = ComboCategoria.SelectedValue.ToString();

                ComboVersion.Text = "VERSION";
                TextBoxDescripcion.Text = "DESCRIPCION";
                TextBoxUUID.Text = "UUID";
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
            if (ComboCategoria.Text == "CATEGORIA" || string.IsNullOrWhiteSpace(ComboCategoria.Text))
            {
                return;
            }
            else
            {
                ListaVersiones = ListaCategorias.Where(w => w.CategoriaItem == CategoriaSeleccionada).Select(s => s);
                ComboVersion.ItemsSource = ListaCategorias.Select(s => s.VersionItem).Distinct().ToList();
            }
        }
        private void ComboVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

                if (ItemsFolderPath != null)
                {
                    string ShortImageFile = Path.Combine(ProductoSeleccionado, ModeloSeleccionado, CategoriaSeleccionada, ArticuloSeleccionado, VersionSeleccionada + ".JPG");
                    ItemsImageFile = Path.Combine(ItemsFolderPath, ShortImageFile);
                    if (File.Exists(ItemsImageFile))
                    {
                        Bitmap bmp = new Bitmap(ItemsImageFile);
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
                else
                {
                    System.Windows.MessageBox.Show("No se encontró la carpeta de imágenes. Seleccione primero la carpeta (ITEMS) correspondiente.", "Actualizar item", MessageBoxButton.OK, MessageBoxImage.Warning);
                    SelectItemsFolder();
                }
            }
        }
    }
}
