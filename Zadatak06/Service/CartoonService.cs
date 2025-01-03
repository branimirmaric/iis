using System.Xml;
using System.Xml.Serialization;
using Zadatak06.Model;

namespace Zadatak06.Service
{
    public class CartoonService
    {
        private readonly string _xmlPath = @"C:\Users\Korisnik\Desktop\IIS-BranimirMaric\Zadatak06\Schemas\cartoon.xml";
        private readonly string _xsdPath = @"C:\Users\Korisnik\Desktop\IIS-BranimirMaric\Zadatak06\Schemas\cartoon.xsd";
        private List<Cartoon> _cartoons;

        public CartoonService()
        {
            _cartoons = LoadCartoons();
        }

        public List<Cartoon> GetAll() => _cartoons;

        public Cartoon? GetByName(string name) =>
            _cartoons.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public void Add(Cartoon cartoon)
        {
            _cartoons.Add(cartoon);
            SaveCartoons();
        }

        public void Update(string name, Cartoon updatedCartoon)
        {
            var cartoon = GetByName(name);
            if (cartoon != null)
            {
                cartoon.Span = updatedCartoon.Span;
                cartoon.Description = updatedCartoon.Description;
                cartoon.Rating = updatedCartoon.Rating;
                SaveCartoons();
            }
        }

        public void Delete(string name)
        {
            var cartoon = GetByName(name);
            if (cartoon != null)
            {
                _cartoons.Remove(cartoon);
                SaveCartoons();
            }
        }

        private List<Cartoon> LoadCartoons()
        {
            var cartoons = new List<Cartoon>();

            var settings = new XmlReaderSettings();
            settings.Schemas.Add(null, _xsdPath);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += (sender, e) =>
            {
                throw new Exception($"XML Validation Error: {e.Message}");
            };

            using var reader = XmlReader.Create(_xmlPath, settings);
            var document = new XmlDocument();
            document.Load(reader);

            foreach (XmlNode node in document.SelectNodes("//cartoon")!)
            {
                cartoons.Add(new Cartoon
                {
                    Name = node["name"]!.InnerText,
                    Span = node["span"]!.InnerText,
                    Description = node["description"]!.InnerText,
                    Rating = decimal.Parse(node["rating"]!.InnerText)
                });
            }

            return cartoons;
        }

        private void SaveCartoons()
        {
            var doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(xmlDeclaration);

            var root = doc.CreateElement("cartoons");

            foreach (var cartoon in _cartoons)
            {
                var cartoonNode = doc.CreateElement("cartoon");

                var nameNode = doc.CreateElement("name");
                nameNode.InnerText = cartoon.Name;

                var spanNode = doc.CreateElement("span");
                spanNode.InnerText = cartoon.Span;

                var descriptionNode = doc.CreateElement("description");
                descriptionNode.InnerText = cartoon.Description;

                var ratingNode = doc.CreateElement("rating");
                ratingNode.InnerText = cartoon.Rating.ToString();

                cartoonNode.AppendChild(nameNode);
                cartoonNode.AppendChild(spanNode);
                cartoonNode.AppendChild(descriptionNode);
                cartoonNode.AppendChild(ratingNode);
                root.AppendChild(cartoonNode);
            }

            doc.AppendChild(root);
            doc.Save(_xmlPath);
        }
    }
}
