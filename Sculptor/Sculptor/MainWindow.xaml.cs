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
        public MainWindow()
        {
            InitializeComponent();
            _fileName = null;
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
            //ObservableCollection<Point> nowe = new ObservableCollection<Point>();
            //foreach (var p in points)
            //{
            //    nowe.Add(new Point(p.X, p.Y));
            //}
            
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            if (dlg.ShowDialog() == true)
            {
                _fileName = dlg.FileName;
                Save(_fileName);
                
            }
        }

        private void Save(string fileName)
        {
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
            //points.Clear();

            //ObservableCollection<Point> nowe = new ObservableCollection<Point>();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
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

            //foreach (var el in nowe)
            //{
            //    Points nowy = new Points(el.X, el.Y);
            //    points.Add(nowy);
            //}
        }
        private void NewSolid(object sender, RoutedEventArgs e)
        {
            //tworzenie nowej bryly
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
