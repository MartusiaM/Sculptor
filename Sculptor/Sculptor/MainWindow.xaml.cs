using System;
using System.Collections.Generic;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sculptor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _fileName { get; set; }
        Grid _modelGrid;
        public MainWindow()
        {
            InitializeComponent();
            _fileName = null;
            _modelGrid = new Grid();
        }
        private void Help(object sender, RoutedEventArgs e)
        {
            string helpText = "This application gives user a chance to try his hand in curving. With a use of mouse one can change the shape of a shown figure. \n\nAuthors: Marta Marciszewicz, Stanisław Wasilkowski ";
            MessageBox.Show(helpText, "Sculptor - Help", MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void SaveSolid(object sender, RoutedEventArgs e)
        {
            Save(_fileName);
        }
        private void SaveSolidAs(object sender, RoutedEventArgs e)
        {            
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "STL file (*.stl)|*.stl|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                _fileName = dlg.FileName;
                Save(_fileName);               
            }
        }

        private void Save(string fileName)
        {
            //----------------------------------------------------------------
            //zapisywanie do pliku fileName
            //----------------------------------------------------------------
            //FileStream fs = new FileStream(dlg.FileName, FileMode.Create);
            //BinaryFormatter formatter = new BinaryFormatter();
            //try
            //{
            //    formatter.Serialize(fs, nowe);
            //}
            //catch (SerializationException exc)
            //{
            //    Console.WriteLine("Failed to serialize. Reason: " + exc.Message);
            //    throw;
            //}
            //finally
            //{
            //    fs.Close();
            //}
        }
        private void LoadSolid(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "STL files (*.stl)|*.stl|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                //-------------------------------------------------------
                //załadowanie informaccji z pliku
                //--------------------------------------------------------
                //FileStream fs = new FileStream(dlg.FileName, FileMode.Open);
                //BinaryFormatter formatter = new BinaryFormatter();
                //try
                //{
                //    nowe = (ObservableCollection<Point>)formatter.Deserialize(fs);
                //}
                //catch (SerializationException exc)
                //{
                //    Console.WriteLine("Failed to serialize. Reason: " + exc.Message);
                //    throw;
                //}
                //finally
                //{
                //    fs.Close();
                //}

            }
        }
        private void NewSolid(object sender, RoutedEventArgs e)
        {   
            // Instantiate the dialog box
            NewModelWindow dlg = new NewModelWindow();

            // Configure the dialog box
            dlg.Owner = this;
            //dlg.DocumentMargin = this.documentTextBox.Margin;

            // Open the dialog box modally 
            dlg.ShowDialog();

            if(dlg.DialogResult == true)
            {
                //tworzenie nowej bryly
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to save changes?", "Sculptor", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                //save changes
                if (_fileName != null)
                    Save(_fileName);
                else SaveSolidAs(sender, e);
            }

            //close application
            Application.Current.Shutdown();
        }
    }
}
