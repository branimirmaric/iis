package hr.algebra.model;

import java.util.Optional;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;

/**
 *
 * @author branimir.maric
 */
public class City {

    @XmlAttribute
    @XmlElement(name = "GradIme")
    private String grad;

    @XmlElement(name = "Temp")
    private String temp;

    @XmlElement(name = "Vlaga")
    private String vlaga;

    @XmlElement(name = "Tlak")
    private String tlak;

    @XmlElement(name = "TlakTend")
    private String tendencijaTlaka;

    @XmlElement(name = "VjetarSmjer")
    private String smjerVjetra;

    @XmlElement(name = "VjetarBrzina")
    private String brzinaVjetra;

    @XmlElement(name = "Vrijeme")
    private String vrijeme;

    public String getGrad() {
        return grad;
    }

    public void setGrad(String grad) {
        this.grad = grad;
    }

    public String getTemp() {
        return temp;
    }

    public void setTemp(String temp) {
        this.temp = temp;
    }

    public String getVlaga() {
        return vlaga;
    }

    public void setVlaga(String vlaga) {
        this.vlaga = vlaga;
    }

    public String getTlak() {
        return tlak;
    }

    public void setTlak(String tlak) {
        this.tlak = tlak;
    }

    public String getTlakTend() {
        return tendencijaTlaka;
    }

    public void setTlakTend(String tendencijaTlaka) {
        this.tendencijaTlaka = tendencijaTlaka;
    }

    public String getSmjerVjetra() {
        return smjerVjetra;
    }

    public void setSmjerVjetra(String smjerVjetra) {
        this.smjerVjetra = smjerVjetra;
    }

    public String getBrzinaVjetra() {
        return brzinaVjetra;
    }

    public void setBrzinaVjetra(String brzinaVjetra) {
        this.brzinaVjetra = brzinaVjetra;
    }

    public String getVrijeme() {
        return vrijeme;
    }

    public void setVrijeme(String vrijeme) {
        this.vrijeme = vrijeme;
    }

    @Override
    public String toString() {
        return "Grad: " + grad + "\nTemperatura: " + temp + "\nVlaga: " + vlaga
                + "\nTlak: " + tlak + "\nTendencija tlaka: " + tendencijaTlaka
                + "\nSmjer vjetra: " + smjerVjetra + "\nBrzina vjetra: " + brzinaVjetra
                + "\nVrijeme: " + vrijeme + "\n";
    }

    public enum TagGroup {
        GRAD("GradIme"), TEMPERATURA("Temp"), VLAGA("Vlaga"), TLAK("Tlak"),
        TENDENCIJA_TLAKA("TlakTend"), SMJER_VJETRA("VjetarSmjer"),
        BRZINA_VJETRA("VjetarBrzina"), VRIJEME("Vrijeme");

        private final String name;

        TagGroup(String name) {
            this.name = name;
        }

        public static Optional<TagGroup> from(String name) {
            for (TagGroup value : values()) {
                if (value.name.equals(name)) {
                    return Optional.of(value);
                }
            }
            return Optional.empty();
        }
    }
}
