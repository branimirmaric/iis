using System.ServiceModel;
using System.Xml;
using Zadatak03.Models;
using Newtonsoft.Json;
using Zadatak03.Interfaces;
using System.Globalization;

namespace Zadatak03.Classes
{
    public class CartoonQueryService : ICartoonService
    {
        public string QueryCartoons(string xpathQuery)
        {
            try
            {
                var cartoonsCollection = GetCartoonsData();

                var doc = new XmlDocument();

                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(xmlDeclaration);

                XmlElement root = doc.CreateElement("cartoons");
                doc.AppendChild(root);

                foreach (var cartoon in cartoonsCollection.Cartoons)
                {
                    XmlElement cartoonElement = doc.CreateElement("cartoon");

                    XmlElement name = doc.CreateElement("name");
                    name.InnerText = cartoon.Name;
                    cartoonElement.AppendChild(name);

                    XmlElement span = doc.CreateElement("span");
                    span.InnerText = cartoon.Span;
                    cartoonElement.AppendChild(span);

                    XmlElement description = doc.CreateElement("description");
                    description.InnerText = cartoon.Description;
                    cartoonElement.AppendChild(description);

                    XmlElement rating = doc.CreateElement("rating");
                    rating.InnerText = cartoon.Rating.ToString(CultureInfo.InvariantCulture); 
                    cartoonElement.AppendChild(rating);

                    root.AppendChild(cartoonElement);
                }

                string tempPath = @"C:\Users\Korisnik\Desktop\IIS-BranimirMaric\Zadatak03\Temp";
                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }

                string filePath = Path.Combine(tempPath, "cartoons.xml");
                doc.Save(filePath);

                var nodes = doc.SelectNodes(xpathQuery);
                if (nodes != null && nodes.Count > 0)
                {
                    var resultXml = new XmlDocument();
                    XmlElement resultRoot = resultXml.CreateElement("cartoons");
                    resultXml.AppendChild(resultRoot);

                    foreach (XmlNode node in nodes)
                    {
                        XmlElement cartoonElement = resultXml.CreateElement("cartoon");

                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            XmlElement element = resultXml.CreateElement(childNode.Name);
                            element.InnerText = childNode.InnerText;
                            cartoonElement.AppendChild(element);
                        }

                        resultRoot.AppendChild(cartoonElement);
                    }

                    return resultXml.OuterXml;
                }
                else
                {
                    return "No results for the XPath query.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        private CartoonsCollection GetCartoonsData() => new()
        {
            Cartoons = new List<Cartoon>
                {
                    new() { Name = "Tom and Jerry", Span = "1950-2024", Description = "A classic cartoon about a cat and a mouse.", Rating = 8.5m },
                    new() { Name = "SpongeBob SquarePants", Span = "1999-Present", Description = "The adventures of a sea sponge and his friends.", Rating = 7.5m },
                    new() { Name = "Looney Tunes", Span = "1930-1969", Description = "Featuring famous characters like Bugs Bunny and Daffy Duck.", Rating = 8.0m }
                }
        };
    }
}
