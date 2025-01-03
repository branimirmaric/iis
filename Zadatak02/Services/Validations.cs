using Commons.Xml.Relaxng;
using System.Xml;

namespace Zadatak02.Services
{
    public class Validations
    {
        public string ValidateXMLUsingRNG(
            string xmlPath = @"C:\Users\Korisnik\Desktop\IIS-BranimirMaric\Zadatak02\Schemas\cartoon.xml",
            string rngPath = @"C:\Users\Korisnik\Desktop\IIS-BranimirMaric\Zadatak02\Schemas\cartoon.rng")
        {
            try
            {
                using var xmlReader = XmlReader.Create(xmlPath);
                using var rngReader = XmlReader.Create(rngPath);
                using var validator = new RelaxngValidatingReader(xmlReader, rngReader);

                while (validator.Read()) { }

                return string.Empty;
            }
            catch (RelaxngException ex)
            {
                return $"Validation error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
