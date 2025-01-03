using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Zadatak07
{
    public partial class Zadatak01 : Page
    {
        private string selectedFilePath;

        public Zadatak01()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                Title = "Select XML File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
                tbMessage.Text = $"Selected File: {Path.GetFileName(selectedFilePath)}";
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                tbMessage.Text = "Please select XML file first.";
                return;
            }

            using var client = new HttpClient();
            using var multipartContent = new MultipartFormDataContent();

            try
            {
                var xmlFile = new ByteArrayContent(File.ReadAllBytes(selectedFilePath));
                xmlFile.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml");

                multipartContent.Add(xmlFile, "file", Path.GetFileName(selectedFilePath));

                tbMessage.Text = "Uploading file...";
                var response = await client.PostAsync("http://localhost:5097/api/cartoon/xmlupload", multipartContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseMessage = await response.Content.ReadAsStringAsync();
                    tbMessage.Text = $"Success: {responseMessage}";
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    tbMessage.Text = $"Error: {response.StatusCode}, {errorMessage}";
                }
            }
            catch (Exception ex)
            {
                tbMessage.Text = $"Error: {ex.Message}";
            }
        }
    }
}
