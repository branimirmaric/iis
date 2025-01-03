using System;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using ServiceReference;

namespace Zadatak07
{
    /// <summary>
    /// Interaction logic for Zadatak03.xaml
    /// </summary>
    public partial class Zadatak03 : Page
    {
        private readonly CartoonServiceClient _cartoonServiceClient;

        public Zadatak03()
        {
            InitializeComponent();
            _cartoonServiceClient = new CartoonServiceClient(CartoonServiceClient.EndpointConfiguration.BasicHttpBinding_ICartoonService);
        }

        private async void QueryCartoons_Click(object sender, RoutedEventArgs e)
        {
            string cartoonName = tbXPathQuery.Text;

            if (string.IsNullOrWhiteSpace(cartoonName))
            {
                tbResults.Text = "Please enter a cartoon name.";
                return;
            }

            string xpathQuery = $"/cartoons/cartoon[name='{cartoonName}']";

            try
            {
                string result = await QueryCartoonsAsync(xpathQuery);
                string formattedResult = FormatXml(result);

                tbResults.Text = formattedResult;
            }
            catch (Exception ex)
            {
                tbResults.Text = $"Error formatting XML: {ex.Message}";
            }
        }

        private string FormatXml(string xml)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                using (var writer = new StringWriter())
                using (var xmlWriter = new XmlTextWriter(writer))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    doc.Save(xmlWriter);

                    return writer.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"Error formatting XML: {ex.Message}";
            }
        }

        private async Task<string> QueryCartoonsAsync(string xpathQuery)
        {
            try
            {
                return await _cartoonServiceClient.QueryCartoonsAsync(xpathQuery);
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
