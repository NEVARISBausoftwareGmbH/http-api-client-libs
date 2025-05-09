# http-api-client-libs für NEVARIS Build 2025.1

## Nevaris.Build.ClientApi 5.0.0

Diese .NET-Bibliothek ermöglicht einen typsicheren Zugriff auf die RESTful API
von NEVARIS Build 2025.1 Sie ist auch als [nuget-Paket](https://www.nuget.org/packages/Nevaris.Build.ClientApi/) verfügbar.

## Neuerungen und Breaking Changes ##

### 5.0.0 (für Build 2025.1 Patch 1 – 25.1.25127.703) – _12.05.2025_

#### Neue Funktionen

- Lesender Zugriff auf Leistungsbeschreibungen (ÖNORM):
  - _IStammApi.GetLeistungsbeschreibungen_ (GET /build/global/leistungsbeschreibungen): Liefert alle Leistungsbeschreibungen.
  - _IStammApi.GetLeistungsbeschreibung_ (GET /build/global/leistungsbeschreibungen/{lbId}): Liefert eine Leistungsbeschreibung.
  - _IStammApi.GetLbKnoten_ (GET /build/global/lbpositionen/{positionId}): Liefert einen Knoten einer Leistungsbeschreibung.
  - _IStammApi.GetLbPosition_ (GET /build/global/lbpositionen/{positionId}): Liefert eine Position einer Leistungsbeschreibung.

- Import von Leistungsbeschreibungen (ÖNORM):
  - _IStammApi.ImportiereLeistungsbeschreibung_ (POST /build/global/Leistungsbeschreibungen/ImportiereLeistungsbeschreibung)
  - _IStammApi.ImportiereErgänzungsleistungsbeschreibung_ (POST /build/global/Leistungsbeschreibungen/ImportiereErgaenzungsleistungsbeschreibung)

- Übernahme von LV-Positionen aus einer Leistungsbeschreibung oder einem anderen Leistungsverzeichnis (ÖNORM):
  - _IProjektApi.PasteOnLbDataIntoOnLv_ (POST /build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/PasteOnLbDataIntoOnLv)
  - _IProjektApi.PasteOnLvDataIntoOnLv_ (POST /build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/PasteOnLvDataIntoOnLv)

- Rechnungen:
  - _IProjektApi.GetRechnungReportPositionen_ (GET /build/projekte/{projektId}/rechnungen/{rechnungId}/reports/positionen):
  Ermittelt Positionsdaten einer Rechnung, entsprechend dem Bericht _Rechnung_.

- Kalkulationen:
  - _IProjektApi.GetKalkulationen_ (GET /build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/kalkulationen):
Liefert die Kalkulationen eines Projekts. (Bislang kam man nur per _IProjektApi.GetLeistungsverzeichnis_ an die Kalkulationen.)

#### Neue Properties

- Rechenwerte der Kalkulation (Funktion: _IProjektApi.GetKalkulation_):
  - _Kalkulation.Ergebnisse_: Diese Property liefert ein Objekt vom Typ _KalkulationErgebnisse_, das nun alle
    relevanten Rechenergebnisse der Kalkulation enthält.

- Rechenwerte der Kalkulationsblätter (Funktionen: _IProjektApi.GetKalkulationsBlatt_, _IProjektApi.GetKalkulationsBlätter_):
  - _KalkulationsBlattDetails.Rechenwerte_: Diese Property ersetzt die bisherige Property
_KalkulationsBlattDetails.Details_. Sie liefert ein Objekt vom Typ _KalkulationsBlattDetails_, das alle relevanten
Rechenergebnisse des Kalkulationsblatts enthält.

- Rechnungen:
  - Klasse _Rechnung_: _LeistungszeitraumIds_, _Umsatzsteuer_, _Abrechnungskurzzeichen_, _RechnungsEmpfängerId_,
_DebitorNummer_, _RechnungsKostenstelleNummer_, _ErlöskontoNummer_, _Notiz_ (Eigenschaften aus dem Register _Rechnungskopf_)

- Leistungsverzeichnisse:
  - _OenormLvDetails.LbInfo_: Verknüpfung zu einer Leistungsbeschreibung (nur für Leistungsverzeichnisse mit LG-Struktur).
  - _LvDetails.KreditorNummer_, _LvDetails.Auftragsreferenz_

#### Breaking Changes

- Mehrere abgekündigte (d.h. mit \[Obsolete\] markierte) Funktionen, Klassen und Properties entfernt.
- Zahlreichen Properties (z.B. solche, die Rechenwerte liefert), wurden readonly gemacht, d.h. es gibt keinen Setter
mehr (Beispiel: _Rechenwert.Wert_). Bislang war das Befüllen solcher Properties möglich, hatte aber keine Auswirkung.
- Diverse Properties vom Typ List\<T\> (z.B. _Projekt.Leistungsverzeichnisse_) wurden durch den Typ
IReadOnlyList\<T\> ersetzt, um klarer zu kommunizieren, dass eine Änderung der Werte nicht vorgesehen ist.
- Mehrere Properties wurden abgekündigt (d.h. mit \[Obsolete\] markiert) und sollten nicht mehr genutzt werden:
  - _KalkulationsBlatt.Details_ (stattdessen: _KalkulationsBlatt.Ergebnisse_)
  - _KalkulationsBlatt.Nummer_
  - _KalkulationsBlatt.Bezeichnung_
  - _LeistungsverzeichnisMitImportMeldungen.ImportieresLeistungsverzeichnis_
  - _LvDetails.GlobaleHilfsberechungen_ (der Name enthielt einen Tippfehler, es heißt nun korrekt _GlobaleHilfsberechnungen_)

#### Sonstiges

- [Nullable reference types](https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references) 
sind nun durchgehend aktiviert.

### 4.11.0 (für Build 2024.2 Patch 1 – 24.2.24320.859) – _19.11.2024_

- Unterstützung für dynamische Betriebsmittel: _KalkulationsZeileBetriebsmittelDetails.IstDynamisch_.
- Zusätzliche Komfort-Property _KalkulationsZeileBetriebsmittelDetails.BetriebsmittelNummerKomplett_ (nur Lesezugriff).
- Zugriff auf Kostenstellennummern: _Projekt.KostenstelleNummer_, _LvDetails.KostenstelleNummer_. 
- Lesezugriff auf berechnete Werte von Rechnungen: _Rechnung.Ergebnisse_.
- Zugriff auf Kontierungsdaten einer Rechnung: _Rechnung.Kontierung_ (nur im Fall von aktivierter Finance-Anbindung).
- Zugriff auf LV-Prognosemengen: _NewLvPositionInfo.Prognosemenge1_ usw.
- _IProjektApi.GetKalkulationsBlätter_, _IProjektApi.GetKalkulationsBlatt_: Neuer optionaler Parameter _mengenArt_.

### 4.10.0 (für Build 2024.2 – 24.2.24275.452) – _21.10.2024_

- Unterstützung für authentifizierten Zugriff: Der Businessdienst ermöglicht ab Version 2024.2 über das Setting
_BuildApiAuthenticationRequired = True_, nur noch authentifizierte API-Zugriffe zuzulassen. Die Übergabe der
Authentifizierungsdaten erfolgt an den _NevarisBuildClient_-Konstruktor über die neu hinzugekommenen Properties
_NevarisBuildClientOptions.Username_ und _NevarisBuildClientOptions.Password_.
- Neu: Beim lesenden Zugriff auf ein Projekte per _IProjektApi.GetProjekt_ werden jetzt das Änderungsdatum und der
Änderungsbenutzer mitgeliefert: _Projekt.ModificationDate_, _Projekt.ModificationUser_.
- Neu: Funktion _IProjektApi.GeneriereKalkulation_ (entspricht der Funktion _Kalkulation generieren_ im Modul
_Angeboskalkulation_).

Businessdienst der authentifizierte Zugriff auf die API 

### 4.9.1 (für Build 2024.1 Patch 2 – 24.1.24179.845) – _02.07.2024_

- Erweiterungen und Verbesserungen beim lesenden Zugriff auf Betriebsmittel
  - Neu: Neue Funktionen _IStammApi.GetAllBetriebsmittelEx + IProjekt.GetAllBetriebsmittelEx_: Liefert neben
den Betriebsmitteln auch Informationen zu den Kostenebenen.
   - Neu: Hilfsmethoden _Betriebsmittel.FindKosten_, _Betriebsmittel.FindZuschlag_, _Zuschlagsgruppe.FindWert_
zum Auslesen eines Kostenebenen-abhängigen Werts unter Berücksichtigung der Kostenebenen-Vererbung.
   - Neue Properties _Betriebsmittel.KostenEffektiv + Betriebsmittel.ZuschlägeEffektiv_: Diese werden bei
Lesezugriffen per _IStammApi/IProjektApi.GetAllBetriebsmittel(Ex)_ befüllt und enthalten die
effektiven Kosten- und Zuschlagswerte, d.h. die Werte, die man auch im UI sieht unter Berücksichtigung der
angeforderten Kostenebene und (im Falle von Zuschlägen) der Gruppenvererbung.
   - Neu: Beim Zugriff per _IStammApi/IProjektApi.GetAllBetriebsmittel(Ex)_ enthält _BetriebsmittelZuschlag.Wert_
nun auch für ÖNORM-Betriebsmitteln den Prozentwert, wodurch der Lookup über die Zuschlagsgruppe unnötig wird.
   - Fix: Beim Zugriff per _IStammApi/IProjektApi.GetAllBetriebsmittel_ wurde
_Betriebsmittel.Zuschläge_ nicht befüllt. Diese Property wurde außerdem bei Schreibeoperationen nicht berücksichtigt.
   - Fix: _BetriebsmittelMaterialDetails.Ladezeit_ wurde grundsätzlich nicht befüllt.
- Verschiedene Properties wurden ergänzt: _Kalkulation.IstAktiv, KalkulationsBlattDetails.Zuschlag/ZuschlagGesamt,
AbrechnungsMerkmal.IsAuswertungsKennzeichen, Positionsblock.MengeKorrigiert, Aufmaßzeile.MengeKorrigiert_.
- Fix: _IStammApi.UpdateBetriebsmittelstamm_ führte zu falscher Befüllung von _PreisKatalog.ParentPreisKatalog_.
- Fix: _IStammApi.CreateBetriebsmittelCollection_: Beim Erzeugen von mehreren Materialien mit gleichem Lieferanten
kam es zu einer Exception.

### 4.9.0 (für Build 2024.1 Patch 1 – 24.1.24155.423) – _04.06.2024_

- Zugriff auf Hilfsberechnungen (lesend und schreibend) für die Mengenermittlung:
  - Neue Properties _LvDetails.GlobaleHilfsberechungen_ und _Aufmaßblatt.Hilfsberechnungen_.
  - _IProjektApi.GetAufmaßblätter_: Neuer Parameter _mitDetails_ zum Abrufen der Hilfsberechnungen.
  - _IProjektApi.CreateLeistungsverzeichnis_: Neue Property _NewLvInfo.MengenArt_, um die per _LvDetails.GlobaleHilfsberechungen_
    mitgegebenen Hilfsberechnungen einer Mengenart zuzuordnen.
  - _IProjektApi.UpdateLeistungsverzeichnis_: Neuer Parameter _mengenArt_, der bestimmt, welche globalen Hilgsberechnungen vom Update betroffen sind.
- _Aufmaßzeile_: Neue Properties _Geprüft_ und _Schreibgeschützt_.

### 4.8.0 (für Build 2023.2 Patch 2 – 23.2.23346.830) – _12.12.2023_

- Neue Funktion _IProjektApi/IStammApi.GetBetriebsmittelByNummer_ zum Abfragen eines Betriebsmittel über seine Nummer.

### 4.7.1 (für Build 2023.2 Patch 1 – 23.2.23321.643) – _20.11.2023_

- _Projekt_: Neue Property _ZugeordneteAdressen_: Erlaubt den Zugriff auf die Adressen, die dem Projekt zugeordnet sind.
- _BetriebsmittelKostenDetails_: Neue Property _WarenkorbItems_: Liefert den Warenkorb eines Betriebsmittel, d.h.
eine Auflistung aller in den weiteren Kosten vorkommenen Betriebsmittel (rekursiv aufgelöst) jeweils inklusive der
kumulierten Menge und der kumulierten Kosten. Diese Property wird beim Abrufen eines einzelnen Betriebsmittels per
_IStammApi/IProjektApi.GetBetriebsmittel_ immer befüllt (und ist eine leere Liste im Fall eines Betriebsmittels 
ohne weitere Kosten). Beim Abholen aller Betriebsmittel mittels _IStammApi/IProjektApi.GetAllBetriebsmittel_ ist die
Property nur dann befüllt, wenn die Argumente _mitKosten = true, mitWeiterenKosten = true_ übergeben werden.
- Fix: Beim Abrufen von Betriebsmitteln per _IStammApi/IProjektApi.GetAllBetriebsmittel_ hatte das Argument
_mitKosten = true_ keine Auswirkung, sofern nicht auch _mitWeiterenKosten = true_ übergeben wurde.
Dieser Fehler wurde nun Build-seitig behoben.

### 4.7.0 (für Build 2023.2 – 23.2.23283.383) – _16.10.2023_

- _LvPosition_: Neue Properties _Umsatzsteuer_, _GarantierteAngebotssummeNummer_, _HatGarantierteAngebotssumme_.
- _NewLvInfo_, _NewLvKnotenInfo_, _NewLvPositionInfo_: Es gibt nun zusätzliche Konstruktoren, die ein
bestehendes _Leistungsverzeichnis/LvKnoten/LvPosition_-Objekt entgegennehmen. Damit lässt sich Kopierfunktionalität
einfacher implementieren.
- Betriebsmittel: Die Property _ExternePreiswartung_ ist jetzt auf allen Betriebsmittelklassen verfügbar.
- _KalkulationsZeile_: Neue Property _Markierungskennzeichen_.

### 4.6.0 (für Build 2023.1 Patch 4 – 23.1.23221.927) – _16.08.2023_

- Zugriff auf Projekteinheiten-/Währungen/-Umsatzsteuern (lesend und schreibend) ermöglicht:
  _Projekt.Einheiten_, _Projekt.Währungen_, _Projekt.Umsatzsteuern_.
- Neue Funktion _IProjektApi.CreateLeistungsverzeichnisAusByteArray_ zum Importieren eines LVs eingeführt.
  Diese soll die bisherige Funktion _IProjektApi.CreateLeistungsverzeichnisAusDatentraegerClientDatei_
  erstzen. Die neue Funktion erlaubt die Übergabe des Datenträgers als Byte-Array sowie die Angabe
  der LV-Art (wichtig für den Import in Success X-Projekten, wo
  _ImportLvAusByteArrayInfo.LvArt_ = _LvArt.VereinfachterModus_ übergeben werden sollte).
- Neue Funktion _IProjektApi.CreateBetriebsmittelCollectionFromStamm_ zur Übernahme von Betriebsmitteln
  aus einem Betriebsmittelstamm in ein Projekt.
- Fehlende Property _BetriebsmittelNachunternehmerDetails.AlternativeNummer_ ergänzt.
- _Kalkulation.Nachkommastellen_ abgekündigt. Stattdessen sollten die entsprechenden Properties
  in der _Projekt_-Klasse verwendet werden (z.B. _Projekt.RechengenauigkeitMengen_).

### 4.5.0 (für Build 2023.1 Patch 3 – 23.1.23180.703) – _03.07.2023_

- Neue Methoden für den Schreibzugriff auf Projektadressen: _IProjektApi.CreateAdresse/UpdateAdresse/DeleteAdresse_.
- Neu: Übernahme einer globalen Adresse in ein Projekt. Dazu ist beim Aufruf von _IProjektApi.CreateAdresse_
der Code der globalen Adresse in _NewProjektAdresseInfo.GlobaleAdresseCode_ zu übergeben.
- Neue Properties in _Adresse_:
  - _Nummer_ (die an der Oberfläche angezeigte Nummer der Adresse; ist für Projektadressen änderbar)
  - Für Projektadressen: _KonzernId_, _KonzernId_ (Verweise auf andere Adressen), _Notiz_
  - Für globale Adressen: _BildDetails_ (enthält Bilddaten)
- Beim Anlegen einer neuen globalen Personenadresse per _IStammApi.CreateAdresse_ konnte der Name nicht mitgegeben
werden. Dies wurde behoben: Es gibt nun die Properties _NewAdresseInfo.Nachname_ und _NewAdresseInfo.Vorname_.
Analoges gilt für Projektadressen.
- _IStammApi.GetAdresse_: Neuer optionaler Parameter _mitBildDetails_, um das einer globalen Adresse
  zugeordnete Bild abzurufen (in _Adresse.BildDetails_).
- Der Zugriff auf (globale) Kostenarten funktionierte bislang nicht korrekt in Zusammenhang mit der Einstellung
_Mandantenbezug aktiv_, da es in diesem Fall mehrere Kostenartenstrukturen (maximal eine pro Mandant/Niederlassung) geben
kann. Die bestehenden Operationen für den Zugriff auf Kostenarten
(_IStammApi.GetKostenarten/GetKostenart/CreateKostenart/UpdateKostenart/CreateKostenart_) wurden dazu um
die Parameter _mandantId_ und _niederlassungId_ erweitert. Diese Parameter müssen im Fall von aktivem
Mandantenbezug angegeben werden (_niederlassungId_ darf null sein), im Fall von nicht aktivem Mandantenbezug
muss null übergeben werden. Um alle Kostenartenstrukturen (über alle Mandanten) zu erhalten, gibt
es die neue Funktion _IStammApi.GetKostenartStrukturen_.
- An mehreren Stellen gibt unterstützt die API nun den (lesenden und schreibenen) Zugriff auf Notizen
(im XHTML-Format): _LvItemFormatierteTexte.Notiz_, _LvDetails.RootFormatierteTexte/LvNotiz_,
_Kalkulation.Notiz_, _KalkulationsBlatt.Notiz_, _Adresse.Notiz_.
- Lesender Zugriff auf globale Einheiten: _IStammApi.GetEinheiten_

### 4.4.0 + 4.4.1 + 4.4.2 (für Build 2023.1 Patch 2 – 23.1.23125.540) – _08.05.2023_

- Neue Properties _Projekt.BetriebsmittelStammId_, _Projekt.BetriebsmittelStammNummer_,
  _Projekt.BetriebsmittelStammBezeichnung_: Erlauben die Abfrage des Betriebsmittelstamms, der
  dem Projekt zugeordnet ist (nur lesender Zugriff).
- Neue Properties _GlobaleVariable.Wert_ und _GlobaleVariableAnsatz.Wert_: Ermöglichen das Auslesen
  des berechneten Werts eines Variablenansatzes (im Betriebsmittelstamm oder kostenebenenabhängig
  im Projekt).
- (4.4.1) Fix für _IProjektApi.GetLvPosition/GetLvKnoten_ (fehlende Parameterübergabe an die API)
- (4.4.2) Lesezugriff auf Projektadressen ermöglicht (_IProjektApi.GetAdressen_, _IProjektApi.GetAdresse_)

### 4.3.1 (für Build 2023.1 – 23.1.23075.791) – _20.03.2023_

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
