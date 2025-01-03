package hr.algebra.parser;

import hr.algebra.model.City;
import hr.algebra.model.City.TagGroup;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import javax.xml.stream.XMLEventReader;
import javax.xml.stream.XMLInputFactory;
import javax.xml.stream.XMLStreamConstants;
import javax.xml.stream.XMLStreamException;
import javax.xml.stream.events.XMLEvent;

/**
 *
 * @author branimir.maric
 */
public class XmlParser {

    private static final String LINK = "https://vrijeme.hr/hrvatska_n.xml";

    public List<String> getCity(String cityName) throws IOException, XMLStreamException {
        List<City> cities = new ArrayList<>();

        HttpURLConnection con = (HttpURLConnection) new URL(LINK).openConnection();
        con.setConnectTimeout(10000);
        con.setReadTimeout(10000);
        con.setRequestMethod("GET");
        con.setRequestProperty("User-Agent", "Mozilla/5.0");

        try (InputStream is = con.getInputStream()) {
            XMLEventReader reader = XMLInputFactory.newFactory().createXMLEventReader(is);
            City city = null;
            Optional<TagGroup> tagType = Optional.empty();
            while (reader.hasNext()) {
                XMLEvent event = reader.nextEvent();
                switch (event.getEventType()) {
                    case XMLStreamConstants.START_ELEMENT -> {
                        tagType = TagGroup.from(event.asStartElement().getName().getLocalPart());
                        if (tagType.isPresent() && tagType.get() == TagGroup.GRAD) {
                            city = new City();
                            cities.add(city);
                        }
                    }
                    case XMLStreamConstants.CHARACTERS -> {
                        if (city != null && tagType.isPresent()) {
                            String data = event.asCharacters().getData().trim();
                            if (!data.isEmpty()) {
                                switch (tagType.get()) {
                                    case GRAD ->
                                        city.setGrad(data);
                                    case TEMPERATURA ->
                                        city.setTemp(data);
                                    case VLAGA ->
                                        city.setVlaga(data);
                                    case TLAK ->
                                        city.setTlak(data);
                                    case TENDENCIJA_TLAKA ->
                                        city.setTlakTend(data);
                                    case SMJER_VJETRA ->
                                        city.setSmjerVjetra(data);
                                    case BRZINA_VJETRA ->
                                        city.setBrzinaVjetra(data);
                                    case VRIJEME ->
                                        city.setVrijeme(data);
                                }
                            }
                        }
                    }
                }
            }
        }

        return cities.stream()
                .filter(c -> c.getGrad().contains(cityName))
                .map(City::toString)
                .collect(Collectors.toList());
    }
}
