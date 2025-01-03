package hr.algebra.testing;

import java.io.FileWriter;
import java.io.IOException;
import java.net.URL;
import java.util.Scanner;
import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;

/**
 *
 * @author branimir.maric
 */
public class DhmzClient {

    public static void main(String[] args) {
        try (Scanner scanner = new Scanner(System.in)) {
            System.out.print("Unesite ime grada: ");
            String cityName = scanner.nextLine();

            XmlRpcClient client = new XmlRpcClient();
            XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();
            config.setServerURL(new URL("http://localhost:8080"));
            client.setConfig(config);

            Object obj = client.execute("XmlParser.getCity", new Object[]{cityName});

            Object[] response = (Object[]) obj;
            if (response == null) {
                response = new Object[1];
                response[0] = "Grad nije pronaden!";
            }

            try (FileWriter writer = new FileWriter("Zadatak05Result.txt")) {
                for (Object city : response) {
                    writer.write(city.toString());
                    System.out.println(city.toString());
                }
            }
        } catch (XmlRpcException | IOException e) {
            String errorMessage = (e instanceof XmlRpcException)
                    ? "Server communication error: " + e.getMessage()
                    : "Error writing to file: " + e.getMessage();
            System.err.println(errorMessage);

            try (FileWriter writer = new FileWriter("Zadatak05Result.txt")) {
                writer.write(errorMessage);
            } catch (IOException ex) {
                System.err.println("Error writing to file: " + ex.getMessage());
            }
        }
    }
}
