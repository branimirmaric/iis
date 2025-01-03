using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Zadatak07
{
    /// <summary>
    /// Interaction logic for Zadatak05.xaml
    /// </summary>
    public partial class Zadatak05 : Page
    {
        public Zadatak05()
        {
            InitializeComponent();
        }

        private void BtnReadFile_Click(object sender, RoutedEventArgs e)
        {
            DisplayFileContent();
        }

        private void DisplayFileContent()
        {
            string filePath = @"C:\Users\Korisnik\Desktop\IIS-BranimirMaric\Zadatak04,05\Zadatak05\Zadatak05Result.txt";

            try
            {
                string fileContent = File.ReadAllText(filePath);
                txtResult.Text = fileContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
