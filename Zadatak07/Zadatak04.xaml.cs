using Microsoft.IdentityModel.Tokens.Saml;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using System.Xml.Schema;

namespace Zadatak07
{
    /// <summary>
    /// Interaction logic for Zadatak04.xaml
    /// </summary>
    public partial class Zadatak04 : Page
    {
        public Zadatak04()
        {
            InitializeComponent();
        }

        private void BtnReadFile_Click(object sender, RoutedEventArgs e)
        {
            DisplayFileContent();
        }

        private void DisplayFileContent()
        {
            string filePath = @"C:\Users\Korisnik\Desktop\IIS-BranimirMaric\Zadatak04,05\Zadatak04\Zadatak04Result.txt";

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
