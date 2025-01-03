package hr.algebra;

import hr.algebra.parser.XmlParser;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.server.PropertyHandlerMapping;
import org.apache.xmlrpc.server.XmlRpcServer;
import org.apache.xmlrpc.server.XmlRpcServerConfigImpl;
import org.apache.xmlrpc.webserver.WebServer;

/**
 *
 * @author branimir.maric
 */
public class DhmzServer {

    public static void main(String[] args) {
        try {
            System.out.println("Startam server ...");
            WebServer server = new WebServer(8080);

            XmlRpcServer xmlServer = server.getXmlRpcServer();
            PropertyHandlerMapping phm = new PropertyHandlerMapping();
            phm.addHandler("XmlParser", XmlParser.class);
            xmlServer.setHandlerMapping(phm);

            XmlRpcServerConfigImpl config = (XmlRpcServerConfigImpl) xmlServer.getConfig();
            config.setEnabledForExtensions(true);
            config.setContentLengthOptional(false);
            config.setEnabledForExtensions(true);

            server.start();
            System.out.println("Pokrenut server.");
            System.out.println("Cekanje zahtjeva ...");
        } catch (XmlRpcException | IOException ex) {
            Logger.getLogger(XmlParser.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}
