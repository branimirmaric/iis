using System.Xml;

namespace Zadatak01.Services
{
    public class Validations
    {
        public string ValidateXMLUsingXSD(string xmlPath = @"C:\Users\Korisnik\Desktop\IIS-BranimirMaric\Zadatak01\Schemas\cartoon.xml",
                                          string xsdPath = @"C:\Users\Korisnik\Desktop\IIS-BranimirMaric\Zadatak01\Schemas\cartoon.xsd")
        {
            try
            {
                var settings = new XmlReaderSettings() { ValidationType = ValidationType.Schema };

                settings.Schemas.Add(null, XmlReader.Create(xsdPath));
                settings.ValidationEventHandler += (_, args) => throw new Exception($"Validation error: {args.Message}");

                using var xmlReader = XmlReader.Create(xmlPath, settings);
                while (xmlReader.Read()) { }

                return string.Empty;
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
        }
    }
}
