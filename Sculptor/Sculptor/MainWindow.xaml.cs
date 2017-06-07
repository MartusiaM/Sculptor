using Sculptor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sculptor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int defaultModelSize = 10;

        //obracanie modelu
        float moveit = 1;//kat o jaki obracamy przy jednorazowym nacisnieciu strzalki
        AxisAngleRotation3D xAxis;
        AxisAngleRotation3D yAxis;
        Transform3DGroup transforms;
        //wczytywanie modelu z pliku
        string fileName { get; set; }

        //narzedzie
        private ObservableCollection<int> toolsSizes = new ObservableCollection<int>();
        public ObservableCollection<int> ToolsSizes
        {
            get { return toolsSizes; }
            set { toolsSizes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(toolsSizes))); }
        }
        private int selectedSize;
        public int SelectedSize
        {
            get { return selectedSize; }
            set { selectedSize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedSize))); }
        }

        ModelGrid _modelGrid;
        public ModelGrid ModelGrid
        {
            get { return _modelGrid; }
            set
            {
                if (value != _modelGrid)
                {
                    _modelGrid = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_modelGrid)));
                }
            }
        }
        public PerspectiveCamera Camera { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            fileName = null;
            //--------------------------------------------
            //tutaj by trzeba wstawić jakis konstruktor np bez parametrowy, ktory pozwoli na 
            //pozniejsze wyswietlenie dowolnej wielkosci bryly
            ModelGrid = new Model.ModelGrid(15,15,15);
            //ModelGrid = new ModelGrid();

            Camera = new PerspectiveCamera();
            Camera.Position = new Point3D(12, 12, 12);
            Camera.LookDirection = new Vector3D(-1, -1, -1);
            Camera.FieldOfView = 45;
            Camera.UpDirection = new Vector3D(0, 1, 0);
            Camera.NearPlaneDistance = 1;
            Camera.FarPlaneDistance = 40;

            //obrot wokol punktu (0,0,0)
            xAxis= new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0);
            yAxis = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
            transforms = new Transform3DGroup();
            transforms.Children.Add(new RotateTransform3D(xAxis));
            transforms.Children.Add(new RotateTransform3D(yAxis));
            _model.Transform = transforms;

            GenerateTools();
            SelectedSize = 1;

            DataContext = this;

        }

        void GenerateTools()
        {
            for(int i=1;i<=10;i++)
                ToolsSizes.Add(i);
        }

        //poruszanie obiektem
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                ModelGrid.RotateX(-moveit);
                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                ModelGrid.RotateX(moveit);
                e.Handled = true;
            }
            else if (e.Key == Key.Right)
            {
                ModelGrid.RotateY(moveit);
                e.Handled = true;
            }
            else if (e.Key == Key.Left)
            {
                ModelGrid.RotateY(-moveit);
                e.Handled = true;
            }
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            string helpText = "This application gives user a chance to try his hand in curving. With a use of mouse one can change the shape of a shown figure. \n\nAuthors: Marta Marciszewicz, Stanisław Wasilkowski ";
            MessageBox.Show(helpText, "Sculptor - Help", MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void SaveSolid(object sender, RoutedEventArgs e)
        {
            if (fileName == null)
                SaveSolidAs(sender, e);
            Save(fileName);
        }
        private void SaveSolidAs(object sender, RoutedEventArgs e)
        {            
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                fileName = dlg.FileName;
                Save(fileName);               
            }
        }

        private void Save(string fileName)
        {
            //zapisywanie do pliku fileName
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName,
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            try
            {
                formatter.Serialize(stream, _modelGrid);
            }
            catch (SerializationException exc)
            {
                Console.WriteLine("Failed to serialize. Reason: " + exc.Message);
                throw;
            }
            finally
            {
                stream.Close();
            }

        }
        private void LoadSolid(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                //załadowanie informaccji z pliku
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(dlg.FileName,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                
                try
                {
                    ModelGrid OldModelGrid = (ModelGrid)formatter.Deserialize(stream);
                    ModelGrid.SetModelFromFile(OldModelGrid.GetGrid(), OldModelGrid.Width, OldModelGrid.Height, OldModelGrid.Length);

                }
                catch (SerializationException exc)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + exc.Message);
                    throw;
                }
                finally
                {
                    stream.Close();
                }

            }
        }
        private void NewSolid(object sender, RoutedEventArgs e)
        {   
            // Instantiate the dialog box
            NewModelWindow dlg = new NewModelWindow();

            // Configure the dialog box
            dlg.Owner = this;
            dlg.NewHeight = defaultModelSize.ToString();
            dlg.NewLength = defaultModelSize.ToString();
            dlg.NewWidth = defaultModelSize.ToString();

            // Open the dialog box modally 
            dlg.ShowDialog();

            if(dlg.DialogResult == true)
            {
                //tworzenie nowej bryly
                int width = (int)(double.Parse(dlg.NewWidth) * 2);
                int height = (int)(double.Parse(dlg.NewHeight) * 2);
                int length = (int)(double.Parse(dlg.NewLength) * 2);
 
                ModelGrid.SetParams(width,height, length);    

            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to save changes?", "Sculptor", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                //save changes
                if (fileName != null)
                    Save(fileName);
                else SaveSolidAs(sender, e);
            }

            //close application
            Application.Current.Shutdown();
        }

        private void viewport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ModelGrid.BeginSculpturing();
            ModelGrid.Sculpt(e.GetPosition(viewport), viewport.ActualWidth, viewport.ActualHeight);
        }

        private void viewport_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                ModelGrid.Sculpt(e.GetPosition(viewport), viewport.ActualWidth, viewport.ActualHeight);
        }

        private void viewport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ModelGrid.EndSculpturing();
        }
    }
}
