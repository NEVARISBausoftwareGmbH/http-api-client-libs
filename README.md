# http-api-client-libs für NEVARIS Build 2023.1

## Nevaris.Build.ClientApi 4.4.0

Diese .NET-Bibliothek ermöglicht einen typsicheren Zugriff auf die RESTful API
von NEVARIS Build 2023.1 Sie ist auch als
[nuget-Paket](https://www.nuget.org/packages/Nevaris.Build.ClientApi/) verfügbar.

## Neuerungen und Breaking Changes ##

### 4.4.0 + 4.4.1 + 4.4.2 (für Build 2023.1 Patch 2 – 23.1.23125.540)

- Neue Properties _Projekt.BetriebsmittelStammId_, _Projekt.BetriebsmittelStammNummer_,
  _Projekt.BetriebsmittelStammBezeichnung_: Erlauben die Abfrage des Betriebsmittelstamms, der
  dem Projekt zugeordnet ist (nur lesender Zugriff).
- Neue Properties _GlobaleVariable.Wert_ und _GlobaleVariableAnsatz.Wert_: Ermöglichen das Auslesen
  des berechneten Werts eines Variablenansatzes (im Betriebsmittelstamm oder kostenebenenabhängig
  im Projekt).
- (4.4.1) Fix für _IProjektApi.GetLvPosition/GetLvKnoten_ (fehlende Parameterübergabe an die API)
- (4.4.2) Lesezugriff auf Projektadressen ermöglicht (_IProjektApi.GetAdressen_, _IProjektApi.GetAdresse_)

### 4.3.1 (für Build 2023.1 – 23.1.23075.791)

- Unterstützung für lesenden und schreibenden Zugriff auf die Ordner-Struktur von Datenbank-Speicherorten:
  Dazu gibt es die neue Klasse _SpeicherortOrdner_ sowie passende neue Felder in
  _Speicherort_: _RootOrdnerList_, _RootProjektInfos_. Die Zuordnung eines Projekts zu einem Ordner
  kann per _Projekt.SpeicherortOrdnerId_ und _NewProjektInfo.SpeicherortOrdnerId_ bestimmt werden.
- Die Operation _IStammApi.GetSpeicherort_ wurde um zwei Parameter erweitert: _mitProjektInfos_ und _mitOrdnern_.
- Die Operation _IStammApi.UpdateSpeicherort_ wurde hinzugefügt, mit der Speicherorte verändert werden können
  (einschließlich der Ordner-Struktur).
- Betriebsmittelverwaltung: Zahlreiche Bugfixes sowie fehlende Properties ergänzt,
  insbesondere für den Zugriff auf Projekt-Betriebsmittel (z.B. neue Properties
  wie _Projekt.Zuschlagsarten_ und _Projekt.Gerätefaktoren_).
- Neue Funktionen zum Anlegen eines Leistungsverzeichnisses auf Basis eines Datenträgers:
  _IProjektApi.CreateLeistungsverzeichnisAusDatentraegerServerDateipfad_ und
  _IProjektApi.CreateLeistungsverzeichnisAusDatentraegerClientDatei_.

### 4.2.5 (für Build 2023.0 Patch 3 – 23.0.23011.872)

- Feld _AlternativeNummer_ für die Betriebsmitteltypen Lohn, Gerrät und sonstige Kosten (in den
  Klassen _BetriebsmittelLohnDetails_, _BetriebsmittelGerätDetails_ und _BetriebsmittelSonstigeKostenDetails_).
- Die Funktion _GetAllBetriebsmittel_ (enthalten sowohl in _IStammApi_ als auch _IProjektApi_) verhält sich nun
  etwas anders bei Übergabe von _mitDetails = false_. Bislang führte dieses Argument dazu, dass die
  betriebsmittelspezifischen Detailfelder (z.B. _Betriebsmittel.LohnDetails_ für Lohn) in den zurückgegebenen
  Objekten ungleich null waren, die darin enthaltenen Objekten selbst jedoch nur unvollständig befüllt waren. Das war
  ein Fehlverhalten, das zum Rücksetzen von Werten führen konnte, wenn das zurückgegebene Objekte anschließend
  an _IStammApi.UpdateBetriebsmittel_ (bzw. _IProjektApi.UpdateBetriebsmittel_) übergeben wurde. Ab Version 4.2.5 werden die Detailfelder
  im Fall _mitDetails = false_ die Detailfelder daher nicht mehr befüllt. Allerdings wurde der Defaultwert für den Parameter
  _mitDetails_ von false auf true geändert, da das Auslesen der Detailinformationen üblicherweise erwünscht ist
  und zudem hinsichtlich der Performance keine großen Nachteile mit sich bringt. Die meisten Client-Applikationen
  dürften ohenhin bislang immer explizit _mitDetails = true_ übergeben haben, womit diese Änderung keine
  erkannbaren Auswirkungen hat.

### 4.2.4 (für Build 2023.0 Patch 2 – 23.0.22320.690)

- Unterstützung für Zahlungsbedingungen. Das Leistungsverzeichnis (abrufbar über _IProjektApi.GetLeistungsverzeichnis_)
  erlaubt nun über die Property _LvDetails_ den Zugriff (lesend und schreibend) auf die Zahlungsbedingungen
  (Properties :_ZahlungsbedingungLV_, _ZahlungsbedingungAbschlagsrechnung_, _ZahlungsbedingungSchlussrechnung_).
  Analog dazu gibt es für Rechnungen die neu hinzugekommene Property _Rechnung.Zahlungsbedingung_.

### 4.2.3 (für Build 2023.0 Patch 1 – 23.0.22293.484)

- Der Zugriff auf sonstige Eigenschaften von Materialien (abrufbar z.B. über _IStammApi.GetAllBetriebsmittel_) ist jetzt über die Property _BetriebsmittelMaterialDetails.Sonstiges_ möglich.
- Die Funktion _IStammApi.UpdateBetriebsmittelKostenCollection_ zum Aktualisieren mehrerer Betriebsmittel erlaubt jetzt auch die
  Übergabe von weiteren Kosten (Ansatzzeilen).

### 4.2.0 (für Build 2023.0 – 23.0.22280.498)

- Unterstützung für Individualeigenschaften: Mehrere Klassen haben jetzt eine Property _CustomPropertyValues_, die
  den Zugriff auf Individualeigenschaften erlauben. Die Definition von Individualeigenschaften muss weiterhin über
  den Administrationsbereich von Build erfolgen, die API erlaubt aber den Zugriff auf die Werte.

### 4.0.0 (für Build 2022.2)

Im Vergleich zur Version 2.x (für Build 2022.1) gibt es ab Version 4.0.0
nun die Möglichkeit, Leistungsverzeichnisse über die API zu erstellen und zu manipulieren
(Erzeugen, Ändern und Löschen von Positionen und Knoten). Die relevanten
Funktionen sind:

- _IProjektApi.GetLeistungsverzeichnisse_
- _IProjektApi.GetLeistungsverzeichnis_
- _IProjektApi.GetLvKnoten_
- _IProjektApi.GetLvPosition_

sowie die zugehörigen Update- und Delete-Funktionen.

Beim Auslesen eines Leistungsverzeichnisses per _IProjektApi.GetLeistungsverzeichnis_
hat sich der Aufbau der zurückgegebenen Objekte leicht geändert, daher sind hier
eventuell Anpassungen an bestehenden Client-Applikationen vorzunehmen.

## Beispielcode ##

### Minimale Client-Applikation ###

Die Verwendung der Library lässt sich am besten anhand eines einfachen Clients
demonstrieren (zum Erstellen ist die Einbindung des  [nuget-Pakets](https://www.nuget.org/packages/Nevaris.Build.ClientApi/)
erforderlich):

```C#
using Nevaris.Build.ClientApi;

// Verbindung zum Businessdienst herstellen
using var nevarisBuildClient = new NevarisBuildClient(hostUrl: "http://localhost:8500");

// Build- und API-Version auslesen
var versionInfo = await nevarisBuildClient.StammApi.GetVersion();
Console.WriteLine($"Build-Version: {versionInfo.ProgramVersion}, API-Version: {versionInfo.ApiVersion}");

// Speicherorte auslesen
var speicherorte = await nevarisBuildClient.StammApi.GetSpeicherorte();
foreach (var speicherort in speicherorte)
{
    Console.WriteLine($"{speicherort.Id}: {speicherort.Bezeichnung}");
}
```

### Demo-Apps ###

Die Solution _http-api-client-libs.sln_ enthält neben dem Quellcode der Nevaris.Build.ClientApi
auch einen Ordner _DemoApps_ mit Code für Client-Programme, die den Zugriff auf NEVARIS Build 2023.1 über
die Nevaris.Build.ClientApi demonstrieren.

* *EinfacherApiClient:* Minimale Konsolenapplikation, die die Verwendung der Nevaris.Build.ClientApi zeigt.
* *AdressConsoleApp:* Konsolenapplikation, die den Zugriff auf globale Adressen demonstriert.
  Die notwendigen Einstellungen (unter anderem die Basis-URL des Zielsystems) müssen in der Datei _Settings.json_ eingetragen werden.
* *AbrechnungConsoleApp:* Konsolenapplikation, die den Lesezugriff auf Abrechnungsdaten (Positionsblöcke mit Aufmaßzeilen) eines Projekts demonstriert.
  Die Basis-URL des Zielsystems sowie die Informationen zur Projektidentifikation müssen in _Settings.json_ eingetragen werden.
* *LvKopierenApp:* Konsolenapplikation, die ein Leistungsverzeichnis dupliziert, indem es alle Positionen und Gruppen einzeln kopiert.
  Quelle und Ziel müssen in _Settings.json_ eingetragen werden.
* *LV Viewer:* WPF-Applikation, die das Auslesen eines Leistungsverzeichnisses demonstriert. Speicherort, Projekt und Leistungsverzeichnis
  können über die grafische Oberfläche ausgewählt werden.
* *KalkulationApp:* Ähnlich dem LV Viewer erlaubt diese App die Auswahl eines Leistungsverzeichnisses, darüber hinaus ist ein lesender Zugriff
  auf die enthaltenen Kalkulationen möglich.
* *BetriebsmittelStammApp:* WPF-Applikation, die Betriebsmittelstämme von einem System in ein anderes kopiert.

## Voraussetzungen ##

Um die RESTful API von NEVARIS Build nutzen zu können, muss auf einem geeigneten Server
der NEVARIS Businessdienst installiert werden. Dazu muss das Setup.exe per

_setup.exe /hiddenfeatures restfulapi_

gestartet werden und dann _Benutzerdefiniert_ ausgewählt werden:

![SetupBenutzerdefiniert](Docs/SetupBenutzerdefiniert.png)

und anschließend in der Installationsauswahl _Businessdienst_ und _RESTful API_ angehakt werden:

![SetupAuswahl](Docs/SetupAuswahl.png)

Der Port, über den die RESTful API erreichbar ist, ist konfigurierbar (der Standardwert ist 8500):

![SetupBusinessdienstKonfiguration](Docs/SetupBusinessdienstKonfiguration.png)

Die im Setup getätigten Einstellungen führen dazu, dass in der Nevaris.config
dieser Eintrag erzeugt wird, der den Businessdienst dazu veranlasst, die RESTful API
am Port 8500 bereitzustellen:

````xml
<RestfulApiBaseAddress>http://*:8500</RestfulApiBaseAddress>
````

Nach erfolgreicher Installation wird der Dienstprozess _NEVARIS Build Businessdienst_ gestartet. Dieser stellt
die RESTful API sowie eine HTML-basierte Dokumentation bereit, die über die
URL **http://\<servername\>:8500/api-docs** erreichbar ist (falls 8500 als Portnummer gewählt wurde):

![SwaggerDoku](Docs/SwaggerDoku.png)

Die Doku enthält auch einen Link auf eine swagger.json-Datei, die eine formale Beschreibung der API enthält.
Daraus lässt sich mit geeigneten Tools auch automatisiert ein Client-Proxy erzeugen, falls dies erwünscht ist.
