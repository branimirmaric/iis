package hr.algebra;

import javax.xml.XMLConstants;
import javax.xml.validation.Schema;
import javax.xml.validation.SchemaFactory;
import javax.xml.validation.Validator;
import org.xml.sax.SAXException;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import javax.xml.transform.stream.StreamSource;

/**
 *
 * @author branimir.maric
 */
public class Zadatak04 {

    public static void main(String[] args) {
        try {
            File xsdFile = new File("C:/Users/Korisnik/Desktop/IIS-BranimirMaric/Zadatak04,05/Zadatak04/src/main/resources/xsd/cartoon.xsd");
            File xmlFile = new File("C:/Users/Korisnik/Desktop/IIS-BranimirMaric/Zadatak04,05/Zadatak04/src/main/resources/xml/cartoons.xml");

            SchemaFactory schemaFactory = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);
            Schema schema = schemaFactory.newSchema(xsdFile);

            Validator validator = schema.newValidator();

            try {
                validator.validate(new StreamSource(xmlFile));
                writeResult("XML is valid.", "Zadatak04Result.txt");
            } catch (SAXException e) {
                writeResult("XML is not valid because: " + e.getMessage(), "Zadatak04Result.txt");
            } catch (IOException e) {
                writeResult("IOException: " + e.getMessage(), "Zadatak04Result.txt");
            }
        } catch (SAXException e) {
            System.err.println(String.format("Error: %s", e.getMessage()));
        }
    }

    public static void writeResult(String result, String fileName) {
        try (FileWriter writer = new FileWriter(fileName)) {
            writer.write(result);
        } catch (IOException e) {
            System.err.println(String.format("Error: %s", e.getMessage()));
        }
    }
}
