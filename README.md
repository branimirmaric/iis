Projektni zadatak iz predmeta: INTEROPERABILNOST INFORMACIJSKIH SUSTAVA

Pokretanje projekta: 
- preuzeti .zip datoteku
- prebaciti unzipanu datoteku na Desktop > kreirati mapu "IIS-BranimirMaric" > unutar mape prekopirati sadržaj preuzete datoteke
- double klik na "IIS-BranimirMaric.sln"
- desni klik na solution > Properties > Multiple startup projects > Apply > Ok
- Start
- unijeti username "admin" i password "12345678"
- sve putanje su zahardkodirane > treba promijeniti putanje u svakome zadatku (od Zadatak01 do Zadatak06)
    > primjerice: Zadatak01 > Controllers > CartoonController.cs > line 21 > var xsdPath = @"C:\Users\Korisnik\Desktop\IIS-BranimirMaric\Zadatak01\Schemas\cartoon.xsd";
    > promijeniti ime korisnika "Korisnik" u ime koje vi koristite

> NOTE: 
 - Zadaci pokrenuti u C#: Zadatak01, Zadatak02, Zadatak03, Zadatak06
 - Zadaci pokrenuti u Javi: Zadatak04, Zadatak05
   
Prema odabranoj temi za projektni zadatak implementirati „backend“ sustav koji implementira sljedeće funkcionalnosti:

1. Zadatak 1: REST API sučelje koje uključuje servis koji će se pozivati POST metodom i slati XML datoteku za spremanje novog entiteta u sustavu koja će se validirati korištenjem XSD datoteke.
    - Select File > IIS-BranimirMaric\data\data > cartoon.xml
    - Submit
    - Poruka: "Success: XML file is valid and successfully processed."
    - Select File > IIS-BranimirMaric\data\data > cartoon.xml > desni klik > Edit with Notepad++ > izbrišite npr. 4 i 5 liniju > Save > povratak na prozor odabira > odaberite "cartoon.xml"
    - Submit
    - Poruka: "Error: BadRequest, XML validation failed: Error: Validation error: The element 'cartoon' has invalid child element 'description'. List of possible elements expected: 'name'"
    - CTRL + Z na prijašnje promjene u "cartoon.xml" > Save
    
2. Zadatak 2: REST API sučelje koje uključuje servis koji će se pozivati POST metodom i slati XML datoteku za spremanje novog entiteta u sustavu koja će se validirati korištenjem RNG datoteke.
   - Isti način kao i Zadatak01
   - Select File > IIS-BranimirMaric\data\data > cartoon.xml
   - Submit
   - Poruka: "Success: XML file is valid and successfully processed."
   - ponovno izbrisati linije 4 i 5 > Save > odabir iste datoteke
   - Poruka "Error: BadRequest, XML validation failed: Validation error: Invalid start tag closing found. LocalName = description, NS = .file://C:/Users/Korisnik/AppData/Local/Temp/tmpCB94.tmp line 5, column 6"

3. Zadatak 3: SOAP sučelje koje uključuje servis koji prima termin po kojem će se pretraživati entitet, a na „backendu“ će generirati XML datoteku koje sadrži informacije o svim entitetima, nakon čega će se ta generirana datoteka pretraživati korištenjem XPath izraza i vratiti rezultate.
    - Query Cartoons > Poruka: "Please enter cartoon name."
    - unosimo: "Tom and Jerry" ili "SpongeBob SquarePants" ili "Looney Tunes"
    - "Tom and Jerry"
    - Poruka:
              - <?xml version="1.0" encoding="utf-16"?>
              -  <cartoons>
              -    <cartoon>
              -      <name>Tom and Jerry</name>
              -      <span>1950-2024</span>
              -      <description>A classic cartoon about a cat and a mouse.</description>
              -      <rating>8.5</rating>
              -    </cartoon>
              -  </cartoons>

4. Zadatak 4: Generiranu datoteku iz prethodnog koraka korištenjem JAXB-a provjeriti je li u skladu sa zadanom XSD datotekom.
- nakon pokretanja svih zadataka opcijom Multiple startup projects imena "Start All" > Zadatak03 kreirao je direktorij "Temp" > "cartoons.xml"
- otvorite IDE za Javu > osobno koristim ApacheNetbeans
- Projects > Open Project... > Desktop\IIS-BranimirMaric\Zadatak04,05 > select Zadatak04 i Zadatak05 > Open Project
- desni klik na Zadatak04 > Clean and Build
- Zadatak04 > Source Packages > hr.algebra > Zadatak04.java > desni klik Run File
- Poruka: "BUILD SUCCESSFUL"
- vratimo se u MainWindow prozor > Click Here > Poruka: "XML is valid"

5. Zadatak 5: Kreirati XML-RPC poslužiteljsku aplikaciju koja će korištenjem DHMZ (https://vrijeme.hr/hrvatska_n.xml) omogućiti dohvat trenutne temperature prema zadanom nazivu grada.
  - Desktop\IIS-BranimirMaric\data > unzip "apache-xmlrpc-3.1.3-bin.zip" + unzip "jaxb-runtime-2.4.0-b180725.0644.jar"
  - Zadatak05 > Clean and Build
  - Zadatak05 > Source Packages > hr.algebra > DhmzServer.java > desni klik Run File
  - Poruka:
            - Startam server ...
            - Pokrenut server.
            - Cekanje zahtjeva ...
  - odlazak na link "https://vrijeme.hr/hrvatska_n.xml" 
  - Zadatak05 > Source Packages > hr.algebra > DhmzClient.java > desni klik Run File
  - Poruka:
            - Unesite ime grada:
  - unesite ime "Bjelovar"
  - Poruka:
            - Grad: Bjelovar
            - Temperatura: 8.4
            - Vlaga: 99
            - Tlak: 1017.9
            - Tendencija tlaka: 0.0
            - Smjer vjetra: NE
            - Brzina vjetra: 0.5
            - Vrijeme: lahor
  > NOTE: podaci mogu odstupati
  - vratimo se u MainWindow prozor > Click Here > Poruka:
                                                          - Grad: Bjelovar
                                                          - Temperatura: 8.4
                                                          - Vlaga: 99
                                                          - Tlak: 1017.9
                                                          - Tendencija tlaka: 0.0
                                                          - Smjer vjetra: NE
                                                          - Brzina vjetra: 0.5
                                                          - Vrijeme: lahor
  
6. Zadatak 6: Implementirati vlastiti REST API koji će implementirati sigurnosne aspekte korištenjem JWT tokena.
  - odlazimo u Swagger Zadatak06 > User > POST /api/user/login > Try it out > username = "admin" > password = "12345678" > Execute
  - gledamo Response body > kopiramo podatak koji se nalazi u "refreshToken"
  - User > POST /api/user/refreshtoken > Try it out > username = "admin" > refreshToken = "[kopirani_refresh_token]" > Execute
  - u Response body-ju dobivamo novi refreshToken
  - povratak u MainWindow > napravljen je prikaz trenutnog Access i Refresh Tokena jednom kada smo se prijavili u aplikaciju
