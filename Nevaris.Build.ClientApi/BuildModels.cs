using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nevaris.Build.ClientApi;

public class VersionInfo : BaseObject
{
    /// <summary>
    /// Die NEVARIS Build-Programmversion.
    /// </summary>
    public string ProgramVersion { get; set; }

    /// <summary>
    /// Die Versionsnummer der HTTP API. Diese folgt den Regeln der sematischen Versionierung.
    /// </summary>
    public string ApiVersion { get; set; }
}

#region Adressen

public enum AdressArt
{
    Organisation,
    Person
}

public enum GesperrtArt
{
    Nein = 0,
    Ja,
    Bereich,
    Hinweis
}

/// <summary>
/// Ein Mandant. Im integrierten Betrieb kommt dieser aus Finance.
/// </summary>
public class Mandant : BaseObject
{
    /// <summary>
    /// Die Mandanten-ID. Im integrierten Betrieb ist dies ein lesbares Kürzel, ansonsten
    /// eine generierte Zufalls-ID.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Mandantenbezeichnung, wie sie in der Benutzeroberfläche angezeigt wird.
    /// </summary>
    public string AnzeigeText { get; set; }

    /// <summary>
    /// Die Mandatenadresse, falls definiert.
    /// </summary>
    public AdresseKurzInfo Adresse { get; set; }

    /// <summary>
    /// Liste von zugeordneten Niederlassungen.
    /// </summary>
    public List<Niederlassung> Niederlassungen;

    public override string ToString() => AnzeigeText;
}

/// <summary>
/// Eine Niederlassung. Im integrierten Betrieb kommt diese aus Finance.
/// </summary>
public class Niederlassung : BaseObject
{
    /// <summary>
    /// Die Niederlassungs-ID. Im integrierten Betrieb ist dies ein lesbares Kürzel, ansonsten
    /// eine generierte Zufalls-ID.
    /// </summary>
    public string Id { get; set; }

    public string Bezeichnung { get; set; }

    public string Suchbegriff { get; set; }

    /// <summary>
    /// Niederlassungsbezeichnung, wie sie in der Benutzeroberfläche angezeigt wird.
    /// </summary>
    public string AnzeigeText { get; set; }

    /// <summary>
    /// Die Niederlassungsadresse, falls definiert.
    /// </summary>
    public AdresseKurzInfo Adresse { get; set; }

    public override string ToString() => AnzeigeText;
}

/// <summary>
/// Eine Adresse, wie sie in <see cref="Mandant"/> und <see cref="Niederlassung"/> verwendet wird.
/// Enthält nicht alle Felder des vollständigen Adresstyps <see cref="Adresse"/>.
/// </summary>
public class AdresseKurzInfo : BaseObject
{
    public string Code { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public AdressArt AdressArt { get; set; }

    public string Name { get; set; }

    public string Vorname { get; set; }

    public string Zusatz1 { get; set; }

    public string Zusatz2 { get; set; }

    public string Zusatz3 { get; set; }

    public string LandCode { get; set; }

    public string Plz { get; set; }

    public string Ort { get; set; }

    public string Straße { get; set; }

    public GesperrtArt GesperrtArt { get; set; }

    public override string ToString() => $"{Code}: {Name}";
}

public class GeoCoordinate
{
    public double Latitude { get; set; }

    public double Longitude { get; set; }
}

public class NewAdresseInfo
{
    public AdressArt AdressArt { get; set; }

    public string Name { get; set; }
}

public enum ZugeordneteAdresseRolle
{
    Auftraggeber = 0,
    Auftragnehmer = 1,
    LvErsteller = 2,
    VergebendeStelle = 3,
    Planer = 4,
    Bauleiter = 5,
    Kalkulant = 6,
    BaustellenAdresse = 7,
    Oberbauleiter = 8,
    Rechnungsempfaenger = 9,
    Architekt = 10,
    Bieter = 11,
    // Nur für GAEB
    Bedarfstraeger = 12,
    Ausfuehrungsort = 13,
    Benachrichtigungsort = 14,
    Abgabeort = 15,
    // In Inform benötigt
    Mitbewerber = 50
}

public class LvZugeordneteAdresse
{
    public ZugeordneteAdresseRolle Rolle { get; set; }
    
    public Guid? AdressId { get; set; }

    public Guid? AnsprechpartnerId { get; set; }
}

/// <summary>
/// Eine globale oder projektbezogene Adresse.
/// </summary>
public class Adresse : BaseObject
{
    /// <summary>
    /// Für globale Adressen: Der Code, der die Adresse identifiziert.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Für Projektadressen: Die GUID, die die Adresse identifiziert.
    /// </summary>
    public Guid? Id { get; set; }
    
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public AdressArt AdressArt { get; set; }

    public string Name { get; set; }
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public string Kürzel { get; set; }
    public string LandCode { get; set; }
    public bool IstPostfachInVerwendung { get; set; }
    public string Titel { get; set; }
    public string UstId { get; set; }
    public string Telefon { get; set; }
    public string Fax { get; set; }
    public string EMail { get; set; }
    public string Name2 { get; set; }
    public string Name3 { get; set; }
    public string Name4 { get; set; }
    public string Ort { get; set; }
    public string Straße { get; set; }
    public string Plz { get; set; }
    public string Postfach { get; set; }
    public string PostfachPlz { get; set; }
    public string PostfachOrt { get; set; }
    public string MobilFirma { get; set; }
    public string MobilPrivat { get; set; }
    public string Internet { get; set; }
    public bool IstBauleistender { get; set; }
    public string Skype { get; set; }
    public string LoginName { get; set; }
    public string Suchbegriff { get; set; }
    public string Briefanschrift { get; set; }
    public string SozialesNetzwerk1 { get; set; }
    public string SozialesNetzwerk1Name { get; set; }
    public string SozialesNetzwerk2 { get; set; }
    public string SozialesNetzwerk2Name { get; set; }
    public string Adresszusatz { get; set; }
    public string BundeslandCode { get; set; }
    public string LandkreisCode { get; set; }
    public string AnredeCode { get; set; }
    public string KonzernCode { get; set; }
    public string ZentraleCode { get; set; }
    public string SperrhinweisCode { get; set; }
    public string SpracheCode { get; set; }
    public string AdressQuelleCode { get; set; }

    public string VerweisAufAdresseCode { get; set; }

    public bool IstDebitorVorhanden { get; set; }
    public bool IstKreditorVorhanden { get; set; }
    public string Handelsregister { get; set; }
    public string UnsereKundenNummerDort { get; set; }
    public string SteuernummerGesellschaft { get; set; }
    public string UrsprungsCode { get; set; }
    public string TitelImAnschreiben { get; set; }
    public DateTime? Geburtsdatum { get; set; }
    public GesperrtArt GesperrtArt { get; set; }
    public DateTime? GültigAb { get; set; }
    public DateTime? GültigBis { get; set; }
    public bool IstDuplikat { get; set; }
    public int? VollständigkeitInProzent { get; set; }
    public decimal? Saldo { get; set; }
    public string ExternerCode { get; set; }
    public string Auslandsvorwahl { get; set; }
    public string DurchwahlFax { get; set; }
    public string DurchwahlZentrale { get; set; }
    public Guid? Guid { get; set; }
    public string Hauptanschlussnummer { get; set; }
    public bool? IsReadOnlyNumber { get; set; }
    public string Ortskennzahl { get; set; }
    public string OutlookEntryId { get; set; }
    public int? Ähnlichkeit { get; set; }
    public GeoCoordinate GeoPosition { get; set; }
    public string Notiz { get; set; }
    public string Beschreibung { get; set; }

    public List<Adressat> Adressaten { get; set; }
    public List<Bankverbindung> Bankverbindungen { get; set; }
    public List<AdressBranche> Branchen { get; set; }
    public List<AdressGewerk> Gewerke { get; set; }
    
    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die dieser Adresse zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue> CustomPropertyValues { get; set; }
}

public class Adressat : BaseObject
{
    /// <summary>
    /// Für globale Adressen: Der Code, der den Adressaten identifiziert.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Für Projektadressen: Die GUID, die den Adressaten identifiziert.
    /// </summary>
    public Guid? Id { get; set; }

    public string AnredeCode { get; set; }

    public string PrivatadresseCode { get; set; }
    
    public Guid? PrivatadresseId { get; set; }
    
    public string Titel { get; set; }
    
    public string TitelImAnschreiben { get; set; }
    
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public string Telefon { get; set; }
    public string Fax { get; set; }
    public string Mobil { get; set; }
    public string EMail { get; set; }
    public string EMailAbteilung { get; set; }
    public string Url { get; set; }
    public string Skype { get; set; }
    public string AbteilungCode { get; set; }
    public string FunktionCode { get; set; }
    public bool IstInaktiv { get; set; }
    public DateTime? Austrittsdatum { get; set; }
    public string Raum { get; set; }
    public string Info { get; set; }
    public string Beschreibung { get; set; }
    public string Notiz { get; set; }
    public string SpracheCode { get; set; }
    public string Briefanschrift { get; set; }
    public string Durchwahl { get; set; }
    public string DurchwahlFax { get; set; }
    public Guid? Guid { get; internal set; }
    
    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die diesem Adressaten zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue> CustomPropertyValues { get; set; }
}

public class Bankverbindung : BaseObject
{
    public string Iban { get; set; }
    public string Bic { get; set; }
    public string Bankname { get; set; }
}

public class AdressBranche : BaseObject
{
    public string BrancheCode { get; set; }
    public string Bezeichnung { get; set; }
    public string Beschreibung { get; set; }
}

public class AdressGewerk : BaseObject
{
    public string GewerkCode { get; set; }
    public string Bezeichnung { get; set; }
    public string Beschreibung { get; set; }
}

#endregion Adressen

#region Projekte

/// <summary>
/// Ein Speicherort (Ordner oder Datenbank).
/// </summary>
public class Speicherort : BaseObject
{
    public Guid Id { get; set; }

    public string Bezeichnung { get; set; }

    /// <summary>
    /// Falls der Speicherort ein Ordner ist, enthält dieses Objekt die passenden Informationen.
    /// </summary>
    public OrdnerInfo OrdnerInfo { get; set; }

    /// <summary>
    /// Falls der Speicherort eine Datenbank ist, enthält dieses Objekt die passenden Informationen.
    /// </summary>
    public DatenbankInfo DatenbankInfo { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Projekten an diesem Speicherort.
    /// </summary>
    public List<ProjektInfo> ProjektInfos { get; set; }
}

public class OrdnerInfo : BaseObject
{
    public string Pfad { get; set; }
}

public class DatenbankInfo : BaseObject
{
    public string Server { get; set; }
    public string Datenbank { get; set; }
    public string Benutzername { get; set; }
    public string Passwort { get; set; }
    public bool IntegratedSecurity { get; set; }
}

/// <summary>
/// Beschreibt ein Projekt. Im Gegensatz zu <see cref="Projekt"/> enthält dieser Typ nur ID,
/// Nummer und Bezeichnung des Projekts und sonst keine Projektinhalte.
/// </summary>
public class ProjektInfo : BaseObject
{
    public string Id { get; set; }

    public string Nummer { get; set; }

    public string Bezeichnung { get; set; }
}

/// <summary>
/// Ein Projekt.
/// </summary>
public class Projekt : BaseObject
{
    public string Id { get; set; }

    /// <summary>
    /// Die ID des Mandanten, dem das Projekt zugeordnet ist (optional).
    /// </summary>
    public string MandantId { get; set; }

    /// <summary>
    /// Identifiziert in Kombination mit MandantId die Niederlassung, der das Projekt zugeordnet ist (optional).
    /// </summary>
    public string NiederlassungId { get; set; }

    public bool IstVorlageprojekt { get; set; }

    /// <summary>
    /// Die Nummer des Projekts (optional).
    /// </summary>
    public string Nummer { get; set; }

    /// <summary>
    /// Die Bezeichnung des Projekts (optional).
    /// </summary>
    public string Bezeichnung { get; set; }

    public DateTime? Baubeginn { get; set; }
    public DateTime? Bauende { get; set; }
    public string Ampel { get; set; }
    public string Art { get; set; }
    public string Ausschreibungsart { get; set; }
    public string Status { get; set; }
    public string Sparte { get; set; }
    public string Typ { get; set; }

    /// <summary>
    /// Liste von Leistungsverzeichnissen, die in diesem Projekt enthalten sind.
    /// </summary>
    public List<Leistungsverzeichnis> Leistungsverzeichnisse { get; set; }

    /// <summary>
    /// Liste von Gliederungskatalogen, die in diesem Projekt enthalten sind.
    /// </summary>
    public List<Gliederungskatalog> Gliederungskataloge { get; set; }

    /// <summary>
    /// Liste von Leistungszeiträumen, die in diesem Projekt enthalten sind.
    /// </summary>
    public List<Leistungszeitraum> Leistungszeiträume { get; set; }
    
    /// <summary>
    /// Die Individualeigenschaften, die diesem Projekt zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue> CustomPropertyValues { get; set; }
}

/// <summary>
/// Beschreibt ein neu anzulegendes Projekt.
/// </summary>
public class NewProjektInfo : BaseObject
{
    /// <summary>
    /// Die ID des Mandanten, dem das Projekt zugeordnet ist (optional).
    /// </summary>
    public string MandantId { get; set; }

    /// <summary>
    /// Identifiziert in Kombination mit MandantId die Niederlassung, der das Projekt zugeordnet ist (optional).
    /// </summary>
    public string NiederlassungId { get; set; }

    /// <summary>
    /// Die Nummer des Projekts (optional).
    /// </summary>
    public string Nummer { get; set; }

    /// <summary>
    /// Die Bezeichnung des Projekts (optional).
    /// </summary>
    public string Bezeichnung { get; set; }

    public bool IstVorlageprojekt { get; set; }
}

#endregion Projekte

#region Betriebsmittel

/// <summary>
/// Ein Betriebsmittelstamm (auch Betriebsmittelkatalog genannt).
/// </summary>
public class BetriebsmittelStamm : BaseObject
{
    public Guid Id { get; set; }

    /// <summary>
    /// Die ID des Mandanten, dem dieser Betriebsmittelstamm zugeordnet ist (optional).
    /// </summary>
    public string MandantId { get; set; }

    /// <summary>
    /// Identifiziert in Kombination mit MandantId die Niederlassung, dem dieser Betriebsmittelstamm zugeordnet ist (optional).
    /// </summary>
    public string NiederlassungId { get; set; }

    public string Nummer { get; set; }

    public string Bezeichnung { get; set; }

    public string Beschreibung { get; set; }

    public BetriebsmittelStammArt? Art { get; set; }

    /// <summary>
    /// Für ÖNORM-Stämme: Die Kalkulationsversion (kann nicht nachträglich geändert werden).
    /// </summary>
    public BetriebsmittelStammKalkulationsVersion? KalkulationsVersion { get; set; }

    public int? RechengenauigkeitMengen { get; set; } // = NachkommastellenAnsatz
    public int? RechengenauigkeitBeträge { get; set; } // = NachkommastellenKostenPreise
    public int? DarstellungsgenauigkeitMengen { get; set; } // = NachkommastellenAnsatzUI
    public int? DarstellungsgenauigkeitBeträge { get; set; } // = NachkommastellenKostenPreiseUI

    public Guid LohnRootGruppeId { get; set; }
    public Guid MaterialRootGruppeId { get; set; }
    public Guid GerätRootGruppeId { get; set; }
    public Guid SonstigeKostenRootGruppeId { get; set; }
    public Guid NachunternehmerRootGruppeId { get; set; }
    public Guid BausteinRootGruppeId { get; set; }

    /// <summary>
    /// (Detailinfo) Enthält Kostenanteilbezeichnungen.
    /// </summary>
    public BetriebsmittelStammBezeichnungen Bezeichnungen { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Kostenkatalogen.
    /// </summary>
    public List<Kostenkatalog> Kostenkataloge { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Zuschlagskatalogen.
    /// </summary>
    public List<Zuschlagskatalog> Zuschlagskataloge { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Zuschlagsgruppen. Legt fest, welche Zuschläge zur Verfügung stehen.
    /// </summary>
    public List<Zuschlagsgruppe> Zuschlagsgruppen { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Zuschlagsarten (entspricht dem Reiter "Zuschläge" in Build).
    /// Über dieses Feld wird bestimmt, wie viele Zuschlagsspalten in den Kosten- und Zuschlagskatalogen angeboten werden und wie diese heißen.
    /// Für Betriebsmittelstämme mit der Kalkulationsversion B2061_2020 ist hier immer genau eine vordefinierte
    /// Zuschlagsart enthalten (es darf nicht mehr geben).
    /// </summary>
    public List<Zuschlagsart> Zuschlagsarten { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Gerätefaktoren.
    /// </summary>
    public List<Gerätefaktor> Gerätefaktoren { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von globalen Variablen.
    /// </summary>
    public List<GlobaleVariable> GlobaleVariablen { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Warengruppen.
    /// </summary>
    public List<Warengruppe> Warengruppen { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von DbBetriebsmittelGruppen.
    /// </summary>
    public List<DbBetriebsmittelGruppe> DbBetriebsmittelGruppen { get; set; }

    /// <summary>
    /// (Detailinfo) Die Einträge im Grid "Zuschlagsberechnung" (nur GAEB-Stämme).
    /// </summary>
    public List<ZuschlagsartGruppe> ZuschlagsartGruppen { get; set; }
    
    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die diesem Betriebsmittelstamm zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue> CustomPropertyValues { get; set; }
}

public enum BetriebsmittelStammArt
{
    FreieForm,
    Aut,
    Ger
}

public enum BetriebsmittelStammKalkulationsVersion
{
    B2061_1999,
    B2061_2020
}

/// <summary>
/// Enthält die Kostenanteilbezeichnungen.
/// </summary>
public class BetriebsmittelStammBezeichnungen : BaseObject
{
    public string LohnKostenanteil1 { get; set; }
    public string LohnKostenanteil2 { get; set; }

    public string ListenpreisGeraetKostenanteil1 { get; set; }
    public string ListenpreisGeraetKostenanteil2 { get; set; }

    public string SonstigeKostenKostenanteil1 { get; set; }
    public string SonstigeKostenKostenanteil2 { get; set; }
    public string SonstigeKostenKostenanteil3 { get; set; }
    public string SonstigeKostenKostenanteil4 { get; set; }
    public string SonstigeKostenKostenanteil5 { get; set; }
    public string SonstigeKostenKostenanteil6 { get; set; }

    public string NachunternehmerKostenanteil1 { get; set; }
    public string NachunternehmerKostenanteil2 { get; set; }
    public string NachunternehmerKostenanteil3 { get; set; }
    public string NachunternehmerKostenanteil4 { get; set; }
    public string NachunternehmerKostenanteil5 { get; set; }
    public string NachunternehmerKostenanteil6 { get; set; }
}

/// <summary>
/// Informationen zu einem neu zu erzeugenden Betriebsmittelstamm.
/// </summary>
public class NewBetriebsmittelStammInfo : BaseObject
{
    public string Nummer { get; set; }

    public string Bezeichnung { get; set; }

    public BetriebsmittelStammArt? Art { get; set; }

    /// <summary>
    /// Die Kalkulationsversion (nur für Betriebsmittelstämme der Art "Aut" relevant).
    /// </summary>
    public BetriebsmittelStammKalkulationsVersion? KalkulationsVersion { get; set; }
}

/// <summary>
/// Eine Kostenart. Kann auch eine Kostenartengruppe sein.
/// </summary>
public class Kostenart : BaseObject
{
    /// <summary>
    /// Die Bezeichung der Kostenart.
    /// </summary>
    public string Bezeichnung { get; set; }

    /// <summary>
    /// Die Nummer der Kostenart innerhalb der enthaltenden Gruppe (z.B. "2").
    /// </summary>
    public string NummerLokal { get; set; }

    /// <summary>
    /// Die vollständige Nummer (z.B. "3.1.2").
    /// </summary>
    public string Nummer { get; set; }

    /// <summary>
    /// Falls true, ist dies eine Kostenartenguppe.
    /// </summary>
    public bool IstGruppe { get; set; }

    /// <summary>
    /// Befüllt im Fall IstGruppe == true. Enthält die Child-Kostenarten.
    /// </summary>
    public List<Kostenart> Kostenarten { get; set; }
}

/// <summary>
/// Informationen zu einer neu zu erzeugenden Kostenart.
/// </summary>
public class NewKostenartInfo
{
    public string Bezeichnung { get; set; }
    public string Nummer { get; set; }
    public bool IstGruppe { get; set; }
}

/// <summary>
/// Objekt für einen Bautagesbericht
/// </summary>
public class Bauarbeitsschluessel : BaseObject
{
    /// <summary>
    /// Die eindeutige Nummer des Bauarbeitsschluessels (BAS).
    /// </summary>
    public string Nummer { get; set; }

    /// <summary>
    /// Die Bezeichnung des Bauarbeitsschluessels (BAS).
    /// </summary>
    public string Bezeichnung { get; set; }

    /// <summary>
    /// Falls true, ist der Bauarbeitsschlüssel (BAS) als Standard gekennzeichnet.
    /// </summary>
    public bool IsStandard { get; set; }
}

public enum GeräteArt
{
    Vorhaltegerät,
    Leistungsgerät,
    Investitionsgerät,
    Listenpreisgerät
}

public class Gerätefaktor : BaseObject
{
    public string Nummer { get; set; }
    public GeräteArt? Art { get; set; }
    public string Bezeichnung { get; set; }
    public decimal? AbminderungsfaktorAV { get; set; }
    public decimal? AbminderungsfaktorRepLohn { get; set; }
    public decimal? AbminderungsfaktorRepMaterial { get; set; }
    public decimal? StundenProMonat { get; set; }
}

public class GlobaleVariable : BaseObject
{
    public string Variable { get; set; }
    public bool? IstKalkulationsVariable { get; set; }
    public string Ansatz { get; set; }
}

public class Warengruppe : BaseObject
{
    public int Nummer { get; set; }
    public string Bezeichnung { get; set; }
}

public class NewKostenkatalogInfo : BaseObject
{
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
}

public class NewZuschlagskatalogInfo : BaseObject
{
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
}

public class NewZuschlagsartInfo : BaseObject
{
    public int Index { get; set; }
    public string Bezeichnung { get; set; }
    public ZuschlagsTyp? Zuschlagstyp { get; set; }
}

public class DbBetriebsmittelGruppe
{
    public string Bezeichnung { get; set; }
    public BetriebsmittelArt? Art { get; set; }
}

public class ZuschlagsartGruppe : BaseObject
{
    public ZuschlagsTyp? Art { get; set; }
    public ZuschlagsBasis? Berechnung { get; set; }
    public bool HatZwischensumme { get; set; }
}

public enum ZuschlagsBasis
{
    AufHundert,
    ImHundert,
}

/// <summary>
/// Beschreibt einen Zuschläg, der in einem Betriebsmittelstamm zur Verfügung steht. Der Wert des Zuschlags
/// wird per Zuschlagskatalog.ZuschlagsgruppenWerte festgelegt.
/// </summary>
public class Zuschlagsgruppe : BaseObject
{
    public string Nummer { get; set; }

    public string Bezeichnung { get; set; }

    /// <summary>
    /// Wird derzeit nicht genutzt.
    /// </summary>
    public int? Stufe { get; set; }
}

/// <summary>
/// Der Wert einer Zuschlagsgruppe innerhalb eines Zuschlagskatalogs.
/// </summary>
public class ZuschlagsgruppenWert : BaseObject
{
    /// <summary>
    /// Verweist auf eine Zuschlagsgruppe.
    /// </summary>
    public string ZuschlagsgruppenNummer { get; set; }

    public decimal? Wert { get; set; }
}

/// <summary>
/// Eine Zuschlagsart. Beschreibt eine Zuschlagsspalte in den Kosten- und Zuschlagskatalogen.
/// </summary>
public class Zuschlagsart : BaseObject
{
    public int Index { get; set; }
    public string Bezeichnung { get; set; }
    public ZuschlagsTyp? Typ { get; set; }
}

public enum ZuschlagsTyp
{
    Agk, Bgk, Gewinn, Wagnis
}

public class Kostenkatalog : BaseObject
{
    public Guid Id { get; set; }
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
    public string Beschreibung { get; set; }
    public bool IstStandard { get; set; }

    public Guid? ParentKostenkatalogId { get; set; }
}

public class Zuschlagskatalog : BaseObject
{
    public Guid Id { get; set; }
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
    public string Beschreibung { get; set; }
    public bool IstStandard { get; set; }

    public List<ZuschlagsgruppenWert> ZuschlagsgruppenWerte { get; set; }
}

public enum BetriebsmittelArt
{
    Lohn,
    Material,
    Gerät,
    SonstigeKosten,
    Nachunternehmer,
    Baustein,

    LohnGruppe,
    MaterialGruppe,
    GerätGruppe,
    SonstigeKostenGruppe,
    NachunternehmerGruppe,
    BausteinGruppe
}

/// <summary>
/// Informationen zu einem neu zu erzeugenden Betriebsmittel.
/// </summary>
public class NewBetriebsmittelInfo : BaseObject
{
    /// <summary>
    /// Die ID der Betriebsmittelgruppe, unter dem das neue Betriebsmittel angelegt wird.
    /// Falls nicht befüllt, wird das neue Betriebsmittel unter der Root-Gruppe angelegt.
    /// </summary>
    public Guid? ParentGruppeId { get; set; }

    /// <summary>
    /// Die Art des zu erzeugenden Betriebsmittels. Kann eine Gruppe sein.
    /// </summary>
    public BetriebsmittelArt Art { get; set; }

    public string Nummer { get; set; }

    public string Bezeichnung { get; set; }
}

/// <summary>
/// Rückgabeobjekt der Operation POST /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel_collection.
/// </summary>
public class BetriebsmittelCollectionResult : BaseObject
{
    /// <summary>
    /// Die IDs der erzeugten Betriebsmittel (einschließlich Grupper), ohne untergeordnete Betriebsmittel.
    /// </summary>
    public List<Guid> NewRootBetriebsmittelIds { get; set; }
}

/// <summary>
/// Enthält die Informationen zu einem Update der Kosten eines Betriebsmittels.
/// Wird der /build/global/betriebsmittel/kosten_collection_update als Parameter mitgegeben.
/// </summary>
public class BetriebsmittelKostenUpdateInfo : BaseObject
{
    /// <summary>
    /// Die ID des Betriebsmittels.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Die neu zu setzenden Kosten. Enthält ein Kostenobjekt pro Kostenebene (z.B. Kostenkatalog).
    /// Die eigentlichen Kostenwerte werden aus dem jeweiligen Detailobjekt
    /// geholt, z.B. LohnDetails für Löhne. Falls das Detailobjekt Objekt null ist,
    /// werden die Kosten nicht verändert.
    /// Das KostenebeneId-Feld muss befüllt sein, denn es identifiziert dieses Kostenobjekt
    /// innerhalb des zugehörigen Betriebsmittels. Kostenobjekte auf dem Betriebsmittel, die im System
    /// vorhanden sind, aber keine Entsprechung in dieser Liste haben, werden gelöscht.
    /// Eine leere Liste führt zur Löschung aller Kostenobjekte. Falls null, passiert dagegen nichts.
    /// </summary>
    public List<BetriebsmittelKosten> Kosten { get; set; }
    
    /// <summary>
    /// Liste mit weiteren Kosten (Ansatzzeilen) für das Betriebsmittel. Falls ungleich null, werden die
    /// bestehenden Ansatzzeilen aktualsiert. Bestehende Ansatzzeilen auf dem Betriebsmittel, die keine
    /// Entsprechung in dieser Liste haben, werden gelöscht, neu in dieser Liste hinzugekommene
    /// werden neu erzeugt. Eine leere Liste führt zur Löschung aller Ansatzzeilen.
    /// Falls null, passiert dagegen nichts.
    /// </summary>
    public List<KalkulationsZeile> WeitereKosten { get; set; }
}

/// <summary>
/// Ein Betriebsmittel (kann auch eine Betriebsmittelgruppe sein).
/// </summary>
public class Betriebsmittel : BaseObject
{
    /// <summary>
    /// Die ID des Betriebsmittels.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Die Id der Gruppe, unter dem dieses Betriebsmittel hängt. Ist null, wenn dieses Betriebsmittel
    /// selbst eine Gruppe auf oberste Ebene ist.
    /// </summary>
    public Guid? ParentGruppeId { get; set; }

    public BetriebsmittelArt Art { get; set; }

    public string Nummer { get; set; }

    /// <summary>
    /// Vollständige Nummer des Betriebsmittls (einschließlich Präfix), z.B. "M24.211".
    /// </summary>
    public string NummerKomplett { get; set; }

    public string Bezeichnung { get; set; }


    public bool? Leistungsfähig { get; set; }

    public string Einheit { get; set; }

    /// <summary>
    /// Liste von Kosten (eine pro Kostenebene, auf der die Kosten für dieses Betriebsmittel definiert sind).
    /// Ist normalerweise eine Detailinfo, das heißt, dieses Feld ist nur im Fall von Einzelabfragen befüllt.
    /// Allerdings erlaubt der Aufruf /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel
    /// über den "mitKosten"-Parameter das Auslesen meherer Betriebsmittel einschließlich Kosten.
    /// </summary>
    public List<BetriebsmittelKosten> Kosten { get; set; }

    /// <summary>
    /// Enthält berechnete Kosten und Preise. Diese sind abhängig von der gewählten Kosten- und Zuschlagsebene.
    /// Ist normalerweise eine Detailinfo, das heißt, dieses Feld ist nur im Fall von Einzelabfragen befüllt.
    /// Allerdings erlaubt der Aufruf /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel
    /// über den "mitKosten"-Parameter das Auslesen meherer Betriebsmittel einschließlich berechneter Kosten und Preise.
    /// Wird bei Schreiboperationen ignoriert.
    /// </summary>
    public BetriebsmittelKostenDetails KostenDetails { get; set; }

    /// <summary>
    /// (Detailinfo) Liste mit weiteren Kosten.
    /// </summary>
    public List<KalkulationsZeile> WeitereKosten { get; set; }

    /// <summary>
    /// (Detailinfo) Die Zuschläge, die auf diesem Betriebsmittel definiert sind.
    /// </summary>
    public List<BetriebsmittelZuschlag> Zuschläge { get; set; }

    /// <summary>
    /// (Detailinfo) Spezielle Eigenschaften, die nur bei Einzelabfragen geladen werden.
    /// </summary>
    public BetriebsmittelDetails Details { get; set; }

    /// <summary>
    /// Falls das Betriebsmittel eine Gruppe ist, enthält dieses Objekt die passenden Eigenschaften.
    /// </summary>
    public BetriebsmittelGruppeDetails GruppeDetails { get; set; }

    /// <summary>
    /// Falls das Betriebsmittel ein Lohn ist, enthält dieses Objekt die passenden Eigenschaften.
    /// </summary>
    public BetriebsmittelLohnDetails LohnDetails { get; set; }

    /// <summary>
    /// Falls das Betriebsmittel ein Material ist, enthält dieses Objekt die passenden Eigenschaften.
    /// </summary>
    public BetriebsmittelMaterialDetails MaterialDetails { get; set; }

    /// <summary>
    /// Falls das Betriebsmittel eine Gerät ist, enthält dieses Objekt die passenden Eigenschaften.
    /// </summary>
    public BetriebsmittelGerätDetails GerätDetails { get; set; }

    /// <summary>
    /// Falls das Betriebsmittel ein Sonstige-Kosten-Objekt ist, enthält dieses Objekt die passenden Eigenschaften.
    /// </summary>
    public BetriebsmittelSonstigeKostenDetails SonstigeKostenDetails { get; set; }

    /// <summary>
    /// Falls das Betriebsmittel ein Nachunternehmer ist, enthält dieses Objekt die passenden Eigenschaften.
    /// </summary>
    public BetriebsmittelNachunternehmerDetails NachunternehmerDetails { get; set; }

    /// <summary>
    /// Falls das Betriebsmittel ein Baustein ist, enthält dieses Objekt die passenden Eigenschaften.
    /// </summary>
    public BetriebsmittelBausteinDetails BausteinDetails { get; set; }

    /// <summary>
    /// Zeigt an ob es sich um ein Freies Betriebmsittel handelt.
    /// </summary>
    public bool IsFreiesBetriebsmittel { get; set; }
    
    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die diesem Betriebsmittel zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue> CustomPropertyValues { get; set; }
}

public class BetriebsmittelDetails : BaseObject
{
    /// <summary>
    /// Die Beschreibung als Text (ohne Formatierungen).
    /// </summary>
    public string Beschreibung { get; set; }

    public string Stichwörter { get; set; }

    public string Markierungskennzeichen { get; set; }

    public string StandardAnsatz { get; set; }

    public string DbBetriebsmittelGruppeBezeichnung; // = DBBetriebsmittelgruppe

}

/// <summary>
/// Ein Zuschlag , der auf einem Betriebsmittel definiert ist.
/// </summary>
public class BetriebsmittelZuschlag : BaseObject
{
    /// <summary>
    /// Verweist auf eine ZuschlagsartGruppe.
    /// </summary>
    public string ZuschlagsgruppenNummer { get; set; }

    /// <summary>
    /// Verweist auf eine Zuschlagsart.
    /// </summary>
    public int ArtIndex { get; set; }

    /// <summary>
    /// Die ID des Kosten- oder Zuschlagskatalogs.
    /// </summary>
    public Guid ZuschlagsebeneId { get; set; }
}

public class BetriebsmittelGruppeDetails : BaseObject
{
    /// <summary>
    /// Die in dieser Gruppe enthaltenen Child-Betriebsmittel.
    /// </summary>
    public List<Betriebsmittel> BetriebsmittelList { get; set; }
}

public class BetriebsmittelLohnDetails : BaseObject
{
    public bool? IstProduktiv { get; set; }

    /// <summary>
    /// Umrechnung auf Stunden(1/x)
    /// </summary>
    public decimal? EinheitenFaktor { get; set; }

    /// <summary>
    /// Nummer des Bauarbeitsschlüssels
    /// </summary>
    public string BasNummer { get; set; }

    public string KostenartKostenanteil1 { get; set; }

    public string KostenartKostenanteil2 { get; set; }

    public string KostenartGemeinkosten { get; set; }

    public string KostenartUmlagekosten { get; set; }

    public int? WarengruppeKostenanteil1 { get; set; }

    public int? WarengruppeKostenanteil2 { get; set; }

    public int? WarengruppeGemeinkosten { get; set; }

    public int? WarengruppeUmlagekosten { get; set; }
}

public class BetriebsmittelMaterialDetails : BaseObject
{
    public decimal? Ladezeit { get; set; }

    public string Kostenart { get; set; }

    public string KostenartPreisAbLieferer { get; set; }

    public string KostenartTransport { get; set; }

    public string KostenartGemeinkosten { get; set; }

    public string KostenartManipulation { get; set; }

    public string KostenartNebenmaterial { get; set; }

    public int? WarengruppePreisAbLieferer { get; set; }

    public int? WarengruppeTransport { get; set; }

    public int? WarengruppeManipulation { get; set; }

    public int? WarengruppeGemeinkosten { get; set; }

    public int? WarengruppeNebenmaterial { get; set; }
    
    /// <summary>
    /// (Detailinfo) Enthält zusätzliche Material-Eigenschaften.
    /// </summary>
    public BetriebsmittelMaterialDetailsSonstiges Sonstiges { get; set; }
}

public class BetriebsmittelMaterialDetailsSonstiges : BaseObject
{
    public decimal? GewichtJeEinheitInT { get; set; }
    
    public decimal? TransportvolumenJeEinheitInM3 { get; set; }
    
    public string Rabattgruppe { get; set; }
    
    public string Lieferant { get; set; }
    
    public string AlternativeNummer { get; set; }
    
    public bool? Markierung { get; set; }
    
    public bool? ExternePreiswartung { get; set; }
}

public class BetriebsmittelGerätDetails : BaseObject
{
    public BglArt? BasisBgl { get; set; } // = BGLArt

    public string GerätefaktorNummer { get; set; }

    public string KostenartAV { get; set; }

    public string KostenartRepLohn { get; set; }

    public string KostenartRepMaterial { get; set; }


    public string KostenartGemeinkostenAV { get; set; }

    public string KostenartGemeinkostenLohn { get; set; }

    public string KostenartGemeinkostenSonstiges { get; set; }

    public string KostenartAndereKostenLohn { get; set; }

    public string KostenartAndereKostenSonstiges { get; set; }

    public string KostenartListenpreisgerätLohn { get; set; }

    public string KostenartListenpreisgerätSonstiges { get; set; }

    public string KostenartListenpreisgerätGemeinkosten { get; set; }

    public string KostenartListenpreisGeraetKostenanteil1 { get; set; }

    public string KostenartListenpreisGeraetKostenanteil2 { get; set; }

    public int? WarengruppeAbschreibungVerzinsung { get; set; }

    public int? WarengruppeReparaturLohn { get; set; }

    public int? WarengruppeReparaturMaterial { get; set; }

    public int? WarengruppeListenpreisGeraetKostenanteil1 { get; set; }

    public int? WarengruppeListenpreisGeraetKostenanteil2 { get; set; }

    public int? WarengruppeGemeinkostenAV { get; set; }

    public int? WarengruppeGemeinkostenLohn { get; set; }

    public int? WarengruppeGemeinkostenSonstiges { get; set; }

    public int? WarengruppeAndereKostenLohn { get; set; }

    public int? WarengruppeAndereKostenSonstiges { get; set; }

    public int? WarengruppeListenpreisgerätLohn { get; set; }

    public int? WarengruppeListenpreisgerätSonstiges { get; set; }

    public int? WarengruppeListenpreisgerätGemeinkosten { get; set; }

    /// <summary>
    /// (Detailinfo) Enthält zusätzliche Geräte-Eigenschaften.
    /// </summary>
    public BetriebsmittelGerätDetailsSonstiges Sonstiges { get; set; }
}

public enum Antriebsart
{
    Elektro,
    Diesel,
    Gas,
    Benzin
}

public class BetriebsmittelGerätDetailsSonstiges : BaseObject
{
    public string BglNummer { get; set; } // = BGLNummer
    public string EdvKurztext { get; set; } // = BGLBezeichnung

    // Sonstiges
    public decimal? Vorhaltemenge { get; set; }
    public decimal? Vorhaltedauer { get; set; }
    public Antriebsart? Antriebsart1 { get; set; } // = AntriebsArt
    public Antriebsart? Antriebsart2 { get; set; }
    public decimal? Gewicht { get; set; }
    public decimal? TransportVolumen { get; set; }
    public decimal? Leistung1 { get; set; } // = GeraeteLeistung
    public decimal? Leistung2 { get; set; } // = GeraeteLeistung2

    // Bahnspezifische Angaben
    public string Fabrikat { get; set; }
    public int? Mindestradius { get; set; }
    public int? BeherrschbareSteigung { get; set; }
    public string Schallemissionspegel { get; set; }
    public int? Arbeitsgeschwindigkeit { get; set; }
    public int? GegenseitigeUeberhoehung { get; set; }
}

public class BetriebsmittelSonstigeKostenDetails : BaseObject
{
    public string Kostenart1 { get; set; }

    public string Kostenart2 { get; set; }

    public string Kostenart3 { get; set; }

    public string Kostenart4 { get; set; }

    public string Kostenart5 { get; set; }

    public string Kostenart6 { get; set; }

    public string KostenartGemeinkosten { get; set; }

    public int? WarengruppeKostenanteil1 { get; set; }

    public int? WarengruppeKostenanteil2 { get; set; }

    public int? WarengruppeKostenanteil3 { get; set; }

    public int? WarengruppeKostenanteil4 { get; set; }

    public int? WarengruppeKostenanteil5 { get; set; }

    public int? WarengruppeKostenanteil6 { get; set; }

    public int? WarengruppeKostenanteil7 { get; set; }

    public int? WarengruppeKostenanteil8 { get; set; }

    public int? WarengruppeGemeinkosten { get; set; }
}

public class BetriebsmittelNachunternehmerDetails : BaseObject
{
    public string Kostenart1 { get; set; }

    public string Kostenart2 { get; set; }

    public string Kostenart3 { get; set; }

    public string Kostenart4 { get; set; }

    public string Kostenart5 { get; set; }

    public string Kostenart6 { get; set; }

    public string Kostenart7 { get; set; }

    public string Kostenart8 { get; set; }

    public string KostenartGemeinkosten { get; set; }

    public int? WarengruppeKostenanteil1 { get; set; }

    public int? WarengruppeKostenanteil2 { get; set; }

    public int? WarengruppeKostenanteil3 { get; set; }

    public int? WarengruppeKostenanteil4 { get; set; }

    public int? WarengruppeKostenanteil5 { get; set; }

    public int? WarengruppeKostenanteil6 { get; set; }

    public int? WarengruppeKostenanteil7 { get; set; }

    public int? WarengruppeKostenanteil8 { get; set; }

    public int? WarengruppeGemeinkosten { get; set; }
}

public class BetriebsmittelBausteinDetails : BaseObject
{
}

/// <summary>
/// Enthält berechnete Betriebsmittelkosten.
/// </summary>
public class BetriebsmittelKostenDetails : BaseObject
{
    /// <summary>
    /// Die Betriebsmittelkosten (ohne Berücksichtigung der weiteren Kosten).
    /// </summary>
    public Money Kosten { get; set; }

    /// <summary>
    /// Der Betriebsmittelpreis (ohne Berücksichtigung der weiteren Kosten).
    /// </summary>
    public Money Preis { get; set; }

    /// <summary>
    /// Die Gesamtbetrag der weiteren Kosten.
    /// </summary>
    public Money WeitereKosten { get; set; }

    /// <summary>
    /// Die Gesamtkosten.
    /// </summary>
    public Money KostenGesamt { get; set; }

    /// <summary>
    /// Der Gesamtpreis.
    /// </summary>
    public Money PreisGesamt { get; set; }
}

/// <summary>
/// Enthält die Kostenebenen-spezifischen Kosten eines Betriebsmittels.
/// </summary>
public class BetriebsmittelKosten : BaseObject
{
    /// <summary>
    /// Die ID der Kostenebene (z.B. ein Kostenkatalog), auf der die Kosten für das Betriebsmittel definiert sind.
    /// </summary>
    public Guid KostenebeneId { get; set; }

    /// <summary>
    /// Der Typ der Kostenebene. Wird nur bei Leseoperationen befüllt und bei Schreiboperationen ignoriert.
    /// </summary>
    public KostenebeneTyp? KostenebeneTyp { get; set; }

    /// <summary>
    /// Befüllt, wenn das Betriebsmittel ein Lohn ist.
    /// </summary>
    public BetriebsmittelKostenLohnDetails LohnDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn das Betriebsmittel ein Material ist.
    /// </summary>
    public BetriebsmittelKostenMaterialDetails MaterialDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn das Betriebsmittel ein Gerät ist.
    /// </summary>
    public BetriebsmittelKostenGerätDetails GerätDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn das Betriebsmittel ein Sonstige-Kosten-Objekt ist.
    /// </summary>
    public BetriebsmittelKostenSonstigeKostenDetails SonstigeKostenDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn das Betriebsmittel ein Nachunternehmer ist.
    /// </summary>
    public BetriebsmittelKostenNachunternehmerDetails NachunternehmerDetails { get; set; }
}

public enum KostenebeneTyp
{
    Kostenkatalog,
    Zuschlagskatalog,
    Kalkulation,
    Lv,
    Umlagegruppe,
    Projekt,
    Unterprojekt,
    BetriebsmittelKalkulationsZeile
}

public class BetriebsmittelKostenLohnDetails : BaseObject
{
    public Money Kostenanteil1 { get; set; }

    /// <summary>
    /// Kostenanteil 2: Nur für B2061_1999 verfügbar.
    /// </summary>
    public Money Kostenanteil2 { get; set; }

    public Money Umlagekosten { get; set; }

    public decimal? Gemeinkosten { get; set; }
}

public class BetriebsmittelKostenMaterialDetails : BaseObject
{
    public Money Listenpreis { get; set; }

    public decimal? Rabatt { get; set; }

    public decimal? Verlust { get; set; }

    public Money Manipulation { get; set; }

    public Money Transportkosten { get; set; }

    public decimal? Gemeinkosten { get; set; }

    public decimal? Nebenmaterial { get; set; }
}

public class BetriebsmittelKostenGerätDetails : BaseObject
{
    public Money Neuwert { get; set; } // = Mittlerer Neuwert

    public Money Kostenanteil1 { get; set; }

    public Money Kostenanteil2 { get; set; }

    public decimal? AbschreibungVerzinsung { get; set; } // = A + V

    public decimal? Reparaturkosten { get; set; }

    public decimal? GemeinkostenListenpreisGeraet { get; set; }

    public decimal? GemeinkostenAV { get; set; }

    public decimal? GemeinkostenRepLohn { get; set; }

    public decimal? GemeinkostenRepSonstiges { get; set; }

    public Money AndereKostenLohn { get; set;  }

    public Money AndereKostenSonstiges { get; set; }
}

public class BetriebsmittelKostenSonstigeKostenDetails : BaseObject
{
    public Money Kostenanteil1 { get; set; }

    public Money Kostenanteil2 { get; set; }

    public Money Kostenanteil3 { get; set; }

    public Money Kostenanteil4 { get; set; }

    public Money Kostenanteil5 { get; set; }

    public Money Kostenanteil6 { get; set; }

    public decimal? Gemeinkosten { get; set; }
}

public class BetriebsmittelKostenNachunternehmerDetails : BaseObject
{
    public Money Kostenanteil1 { get; set; }

    public Money Kostenanteil2 { get; set; }

    public Money Kostenanteil3 { get; set; }

    public Money Kostenanteil4 { get; set; }

    public Money Kostenanteil5 { get; set; }

    public Money Kostenanteil6 { get; set; }

    public decimal? Gemeinkosten { get; set; }
}

#endregion Betriebsmittel

#region  Kalkulation

public class KalkulationsZeileDetails : BaseObject
{
    public decimal? Ergebnis { get; set; }

    public decimal? MengeGesamt { get; set; }

    public decimal? LeistungsMenge { get; set; }

    public Money KostenProEinheit { get; set; }

    public Money Kosten { get; set; }

    public Money Preis { get; set; }

    public decimal? StundenProduktiv { get; set; }

    public Money ZuschlagGesamt { get; set; }
}

/// <summary>
/// Eine Zeile in den weiteren Kosten oder in einem Kalkulationsblatt der Detailkalkulation.
/// </summary>
public class KalkulationsZeile : BaseObject
{
    /// <summary>
    /// Die ID ist bei GET-Zugriffen immer befüllt. Für PUT-Operationen, d.h. für
    /// PUT /build/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter/{positionId} und
    /// PUT /build/global/betriebsmittel/{betriebsmittelId}
    /// kann sie fehlen. In diesem Fall wird die Zeile neu angelegt.
    /// </summary>
    public Guid? Id { get; set; }

    public string Nummer { get; set; }

    public string Bezeichnung { get; set; }

    public string Kommentar { get; set; }

    public string Einheit { get; set; }

    public bool IstInaktiv { get; set; }

    /// <summary>
    /// (Detailinfo) Enthält weitere Eigenschaften der Kalkulationszeile, insbesondere berechnete Werte.
    /// </summary>
    public KalkulationsZeileDetails Details { get; set; }

    /// <summary>
    /// Befüllt, wenn die Zeile einen Verweis auf ein Betriebsmittel enthält.
    /// </summary>
    public KalkulationsZeileBetriebsmittelDetails BetriebsmittelDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn die Zeile einen Variablenansatz enthält.
    /// </summary>
    public KalkulationsZeileVariablenDetails VariablenDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn es sich um eine Kommentarzeile handelt.
    /// </summary>
    public KalkulationsZeileKommentarDetails KommentarDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn es sich um eine Unterposition handelt. Diese kann mehrere Unterzeilen enthalten.
    /// </summary>
    public KalkulationsZeileUnterpositionDetails UnterpositionDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn es sich um einen Rückgriff handelt.
    /// </summary>
    public RückgriffZeileDetails RückgriffDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn es sich um eine Summenzeile handelt.
    /// </summary>
    public SummenKalkulationsZeileDetails SummenDetails { get; set; }
}

public class KalkulationsZeileBetriebsmittelDetails : BaseObject
{
    public Guid BetriebsmittelId { get; set; }
    public BetriebsmittelArt? BetriebsmittelArt { get; set; } // aus Performancegründen speichern wir hier auch (optional) die Betriebsmittelart ab

    public string Ansatz { get; set; }
    public string Variable { get; set; }
    public string BasNummer { get; set; }
}

public class KalkulationsZeileVariablenDetails : BaseObject
{
    public string Variable { get; set; }
    public string Ansatz { get; set; }
}

public class KalkulationsZeileKommentarDetails : BaseObject
{
}

public class RückgriffZeileDetails : BaseObject
{
    public Guid PositionId { get; set; }

    public Guid? UnterpositionsZeileId { get; set; }

    public string Ansatz { get; set; }
    public string Variable { get; set; }
}

public enum SummenKalkulationsZeileArt
{
    Relativ = 0,
    Absolut = 1,
}

public class SummenKalkulationsZeileDetails : BaseObject
{
    public SummenKalkulationsZeileArt? Art { get; set; }
    public string Modifikator { get; set; }
    public Money Kosten { get; set; }
    public Money Preis { get; set; }
    public decimal? StundenProduktiv { get; set; }
}

public class KalkulationsZeileUnterpositionDetails : BaseObject
{
    public string Ansatz { get; set; }
    public string Variable { get; set; }
    public string BasNummer { get; set; }

    /// <summary>
    /// Zeilen, die in dieser Zeile enthalten sind.
    /// </summary>
    public List<KalkulationsZeile> Zeilen { get; set; }
}

public enum BglArt
{
    Keine,
    Oebgl,
    Dbgl
}

public enum Norm
{
    Oenorm,
    Gaeb,
    Frei
}

public enum NormExakt
{
    None = 0,
    OENORMA2063 = 1,
    OENORMB2063BIS2018R1 = 2,
    GAEB90 = 3,
    GAEBXML = 4,
    GAEB2000 = 5,
    FREIEFORM = 6,
    GAEBXML32 = 7,
    OENORMA2063V2015 = 8,
    OENORMB2063 = 9,
    OENORMA2063V2021 = 10,
    GAEBXML33 = 100
}

public enum LvArt
{
    Ausschreibung,
    FreieAusschreibung,
    Vergabe,
    AuftragErteilt,
    NUAuftragErteilt,
    FreierAuftragErteilt,
    NUFreierAuftragErteilt,
    FreierAuftragErhalten,
    
    Kostenschaetzung,
    Anfrage,
    Angebot,
    FreiesAngebot,
    AuftragErhalten,
    Subvergabe,
    SubVergabeAusfuehren,
    VereinfachterModus,
}

public enum GliederungsArt
{
    /// <summary>
    /// Losgliederung (für GAEB)
    /// </summary>
    LosGliederung = 0,

    /// <summary>
    /// Titelgliederung (für GAEB)
    /// </summary>
    TitelGliederung = 1,

    /// <summary>
    /// Ohne Gliederung
    /// </summary>
    OhneGliederung = 2,

    /// <summary>
    /// Hauptgruppengliederung (für ÖNORM) 
    /// </summary>
    HgGliederung = 3,

    /// <summary>
    /// Obergruppengliederung (für ÖNORM)
    /// </summary>
    OgGliederung = 4,

    /// <summary>
    /// Leistungsgruppengliederung (für ÖNORM)
    /// </summary>
    LgGliederung = 5,
}

public enum KalkulationsArt
{
    NullKalkulation,
    AbgestimmteNullKalkulation,
    OptimierteKalkulation,
    AngebotsKalkulation,
    AuftragsKalkulation,
    Arbeitskalkulation
}

public class NewKalkulationInfo : BaseObject
{
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
    public KalkulationsArt? KalkulationsArt { get; set; }
    public Guid BetriebsmittelStammId { get; set; }
    public Guid? KostenkatalogId { get; set; }
    public Guid? ZuschlagskatalogId { get; set; }
}

/// <summary>
/// Die Kalkulations-Rechenergebnisse eines einzelnen Betriebsmittels.
/// </summary>
public class BetriebsmittelErgebnis : BaseObject
{
    /// <summary>
    /// Id des Betriebsmittels.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Vollständige Nummer des Betriebsmittls (einschließlich Präfix), z.B. "M24.211".
    /// </summary>
    public string NummerKomplett { get; set; }

    /// <summary>
    /// Die berechnete Warenkorbmenge. Dies ist aktuell der einzige gelieferte Rechenwert.
    /// </summary>
    public decimal WarenkorbMenge { get; set; }
}

/// <summary>
/// Die Kalkulations-Rechenergebnisse eines einzelnen Kalkulationsblattes.
/// </summary>
public class KalkulationsBlattErgebnis : BaseObject
{
    /// <summary>
    /// Id des Kalkulationsblattes.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Vollständige Nummer der dazugehörigen Position
    /// </summary>
    public string Positionsnummer { get; set; }

    /// <summary>
    /// Die Ergebnisse der im Kalkulationsblatt verwendeten Betriebsmittel
    /// </summary>
    public List<BetriebsmittelErgebnis> BetriebsmittelErgebnisse { get; set; }

}

public class KalkulationErgebnisse : BaseObject
{
    /// <summary>
    /// Die kalkulierte Angebotssumme für die das gesamte LV (ohne Nachträge).
    /// </summary>
    public decimal? Angebotssumme { get; set; }

    /// <summary>
    /// Die Ergebnisse der verwendeten Betriebsmittel
    /// </summary>
    public List<BetriebsmittelErgebnis> BetriebsmittelErgebnisse { get; set; }

    /// <summary>
    /// Die Ergebnisse der Kalkulationsblätter
    /// </summary>
    public List<KalkulationsBlattErgebnis> KalkulationsblattErgebnisse { get; set; }
}

/// <summary>
/// Eine zu einem Leistungsverzeichnis gehörende Kalkulation.
/// </summary>
public class Kalkulation : BaseObject
{
    public Guid Id { get; set; }
    public Guid LvId { get; set; }
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
    public KalkulationsArt? Art { get; set; }

    /// <summary>
    /// Rechenergebnisse (ist befüllt, wenn die Kalkultion per /build/{projektId}/kalkulationen/{kalkulationId}?mitErgebnissen=true
    /// abgerufen wurde).
    /// </summary>
    public KalkulationErgebnisse Ergebnisse { get; set; }

    /// <summary>
    /// Liste von untergeordneten Kalkulationen. Ist nur befüllt, wenn die Kalkulationen
    /// als Teil eines Leistungsverzeichnisses, d.h. per /build/{projektId}/leistungsverzeichnisse/{lvId} geladen wurden.
    /// </summary>
    public List<Kalkulation> Kalkulationen { get; set; }

    /// <summary>
    /// Enthält Informationen über die Zahl der Nachkommastellen verschiedener Feldtypen.
    /// </summary>
    public KalkulationNachkommastellen Nachkommastellen { get; set; }
}

public class KalkulationNachkommastellen : BaseObject
{
    public int? Ansatz { get; set; }
    public int? AnsatzUI { get; set; }
    public int? KostenPreise { get; set; }
    public int? KostenPreiseUI { get; set; }
}

/// <summary>
/// Ein Kalkulationsblatt. Dieses enthält die Kalkulationszeilen zu einer Position.
/// Als Id fungiert das Tupel (KalkulationId, PositionId).
/// </summary>
public class KalkulationsBlatt : BaseObject
{
    public Guid KalkulationId { get; set; }

    public Guid PositionId { get; set; }

    public string Nummer { get; set; }

    public string Bezeichnung { get; set; }

    /// <summary>
    /// (Detailinfo) Objekt mit weiteren Eigenschaften, insbesondere berechnete Werte (z.B. Einheitspreis).
    /// </summary>
    public KalkulationsBlattDetails Details { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Kalkulationszeilen (hierarchisch aufgebaut).
    /// </summary>
    public List<KalkulationsZeile> Zeilen { get; set; }
}

/// <summary>
/// Detailinformationen (insbesondere berechnete Werte) eines Kalkulationsblattes
/// </summary>
public class KalkulationsBlattDetails : BaseObject
{
    public string PositionsNummerKomplett { get; set; }

    public decimal? Menge { get; set; }
    public decimal? StundenProduktiv { get; set; }
    public decimal? StundenProduktivGesamt { get; set; }
    public Money Kosten { get; set; }
    public Money KostenGesamt { get; set; }
    public Money Preis { get; set; }
    public Money PreisGesamt { get; set; }
    public Money Einheitspreis { get; set; }
    public Money EinheitspreisGesamt { get; set; }

    public Dictionary<string, Money> Preisanteile { get; set; }
}

#endregion  Kalkulation

#region Leistungsverzeichnis

/// <summary>
/// Definition eines Preisanteils.
/// </summary>
public class PreisanteilInfo : BaseObject
{
    /// <summary>
    /// Der Code, der den Preisanteil definiert (z.B. "L" für Lohn").
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// Optionale Bezeichnung (z.B. "Lohn").
    /// </summary>
    public string Bezeichnung { get; set; }
}

/// <summary>
/// Ein Gliederungskatalog.
/// </summary>
public class Gliederungskatalog : BaseObject
{
    public Guid Id { get; set; }

    public string Kennung { get; set; }
    
    public int? Versionsnummer { get; set; }
    
    public DateTime? Versionsdatum { get; set; }
    
    public string Bezeichnung { get; set; }
    
    /// <summary>
    /// (Detailinfo) Die Wurzelknoten des Katalogs.
    /// </summary>
    public List<Gliederungsknoten> Knoten { get; set; }
}

/// <summary>
/// Ein Knoten eines Gliederungskatalogs.
/// </summary>
public class Gliederungsknoten : BaseObject
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// Die vollständige Knotennummer (z.B. "2.7.8").
    /// </summary>
    public string NummerKomplett { get; set; }
    
    public string Bezeichnung { get; set; }
    
    /// <summary>
    /// Die untergeordneten Knoten.
    /// </summary>
    public List<Gliederungsknoten> Knoten { get; set; }
}

/// <summary>
/// Objekt, das beim Anleigen eines neuen LV übergeben wird und alle grundlegenden Informationen
/// zum LV enthält (nicht jedoch den eigentlichen Inhalt wie Positionen und Gruppierungselemente).
/// </summary>
public class NewLvInfo : BaseObject
{
    /// <summary>
    /// LV-Nummer
    /// </summary>
    public string Nummer { get; set; }
    
    /// <summary>
    /// LV-Bezeichnung
    /// </summary>
    public string Bezeichnung { get; set; }
    
    public LvArt? Art { get; set; }

    /// <summary>
    /// Die exakte Norm, z.B. A2063:2021.
    /// </summary>
    public NormExakt NormExakt { get; set; }

    public LvStatus? Status { get; set; }

    /// <summary>
    /// Erforderliches Objekt mit Detailinformationen zum neuen LV.
    /// </summary>
    public LvDetails LvDetails { get; set; }
    
    /// <summary>
    /// Falls es sich um ein ÖNorm-LV handelt: Normspezifische Detailinformationen (optional).
    /// </summary>
    public OenormLvDetails OenormLvDetails { get; set; }
    
    /// <summary>
    /// Falls es sich um ein GAEB-LV handelt: Normspezifische Detailinformationen (optional).
    /// </summary>
    public GaebLvDetails GaebLvDetails { get; set; }
    
    /// <summary>
    /// Optionales Objekt, das das LV-Bild enthält.
    /// </summary>
    public LvBildDetails BildDetails { get; set; }
}

/// <summary>
/// Ein Leistungsverzeichnis (GAEB oder ÖNORM).
/// </summary>
public class Leistungsverzeichnis : BaseObject
{
    public Guid Id { get; set; } // ist die ID der Box
    
    /// <summary>
    /// Die Nummer des LV (optional).
    /// </summary>
    public string Nummer { get; set; }

    /// <summary>
    /// Die Bezeichnung des LV.
    /// </summary>
    public string Bezeichnung { get; set; }
    
    /// <summary>
    /// Die Norm (ÖNorm oder GAEB). Für eine genauere Angabe, siehe NormExakt.
    /// </summary>
    public Norm Norm { get; set; }
    
    /// <summary>
    /// Die exakte Norm, z.B. ÖNorm A2063:2021.
    /// </summary>
    public NormExakt NormExakt { get; set; }
    
    public LvArt? Art { get; set; }
    
    public LvStatus? Status { get; set; }

    /// <summary>
    /// Detailinformationen zum LV.
    /// </summary>
    public LvDetails LvDetails { get; set; }
    
    /// <summary>
    /// Nur für ÖNorm-LVs: Normspezifische Informationen.
    /// </summary>
    public OenormLvDetails OenormLvDetails { get; set; }
    
    /// <summary>
    /// Nur für GAEB-LVs: Normspezifische Informationen.
    /// </summary>
    public GaebLvDetails GaebLvDetails { get; set; }

    /// <summary>
    /// Enthält Binärdaten für Grafiken.
    /// </summary>
    public LvBildDetails BildDetails { get; set; }
    
    /// <summary>
    /// (Detailinfo) Die Knoten der obersten Ebene (z.B. Leistungsgruppen) einschließlich untergeordneter Knoten und Positionen.
    /// </summary>
    public List<LvKnoten> RootKnotenListe { get; set; }
    
    /// <summary>
    /// Nur für GAEB: Positionen auf der obersten Ebene (z.B. Hinweistexte).
    /// </summary>
    public List<LvPosition> RootPositionen { get; set; }
    
    /// <summary>
    /// (Detailinfo) Die Wurzelkalkulationen einschließlich untergeordneter Kalkulationen.
    /// </summary>
    public List<Kalkulation> RootKalkulationen { get; set; }

    /// <summary>
    /// Die Rechenergebnisse auf oberster Ebene. 
    /// </summary>
    public LvItemErgebnisse Ergebnisse { get; set; }
}

/// <summary>
/// Enthält Detailinformationen zu einem LV.
/// </summary>
public class LvDetails : BaseObject
{
    public bool IstNummerSchreibgeschützt { get; set; }
    
    public DateTime? Auftragsdatum { get; set; }
    
    public string Auftragsnummer { get; set; }
    
    public DateTime? Baubeginn { get; set; }
    
    public DateTime? Bauende { get; set; }
    
    public DateTime? Projektbeginn { get; set; }
    
    public DateTime? Projektende { get; set; }
    
    public DateTime? DatumAngebotseröffnung { get; set; }

    public DateTime? DatumAngebotsabgabe { get; set; }
    
    public DateTime? Abnahmedatum { get; set; }
    
    public DateTime? Vergabedatum { get; set; }

    public DateTime? DatumZuschlagsfrist { get; set; }
    
    public DateTime? GewährleistungBeginn { get; set; }
    
    public int? GewährleistungDauer { get; set; }
    
    public DateTime? GewährleistungEnde { get; set; }
    
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public GewaehrleistungEinheit? GewährleistungEinheit { get; set; }
    
    public DateTime? Angebotsfrist { get; set; }
    
    public DateTime? Preisbasis { get; set; }

    public DateTime? Bearbeitungsstand { get; set; }

    public int? NachkommastellenMengen { get; set; }
    
    public int? NachkommastellenPreisanteile { get; set; }

    public GliederungsArt GliederungsArt { get; set; } = GliederungsArt.OhneGliederung;

    public string Währung { get; set; }
    
    public string Umsatzsteuer { get; set; }

    public string Ausschreibungsart { get; set; }
    
    public string Sparte { get; set; }

    [Obsolete("Wird künftig nicht mehr unterstützt." +
              " Bitte stattdessen die gleichnamige Eigenschaft in OenormLvDetails verwenden.")]
    public string Vorhaben { get; set; }

    [Obsolete("Wird künftig nicht mehr unterstützt." +
              " Bitte stattdessen die gleichnamige Eigenschaft in OenormLvDetails verwenden.")]
    public int? Alternativangebotsnummer { get; set; }

    [Obsolete("Wird künftig nicht mehr unterstützt." +
              " Bitte stattdessen die gleichnamige Eigenschaft in OenormLvDetails verwenden.")]
    public int? Abänderungsangebotsnummer { get; set; }

    /// <summary>
    /// Die IDs der Gliederungskataloge, die dem LV zugeordnet sind.
    /// </summary>
    public List<Guid> GliederungskatalogIds { get; set; }
    
    /// <summary>
    /// Die Preisanteil-Arten, die in diesem LV unterstützt werden.
    /// </summary>
    public List<PreisanteilInfo> PreisanteilInfos { get; set; }
    
    /// <summary>
    /// Die möglichen Variantenzusammenstellungen (ohne die implizit
    /// definierte Standard-Variantenzusammenstellung).
    /// </summary>
    public List<Variantenzusammenstellung> Variantenzusammenstellungen { get; set; }
    
    /// <summary>
    /// Die möglichen Zuordnungskennzeichen.
    /// </summary>
    public List<Zuordnungskennzeichen> ZuordnungskennzeichenList { get; set; }

    /// <summary>
    /// Die Nummer der aktiven Variantenzusammenstellung (oder null im Fall der
    /// Standard-Variantenzusammenstellung).
    /// </summary>
    public string AktiveVariantenzusammenstellungNummer { get; set; }

    /// <summary>
    /// Aufschläge/Nachlässe, die auf der LV-Ebene definiert sind.
    /// </summary>
    public NachlassInfo NachlassInfo { get; set; }
    
    public List<LvZugeordneteAdresse> ZugeordneteAdressen { get; set; }

    /// <summary>
    /// Die Individualeigenschaften, die diesem Leistungsverzeichnis zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue> CustomPropertyValues { get; set; }
}

/// <summary>
/// Einheit der Gewährleistung
/// </summary>
public enum GewaehrleistungEinheit
{
    Jahre,
    Monate
}

/// <summary>
/// Enthält BInärdaten zu Grafiken, die dem LV zugeordnet sind.
/// </summary>
public class LvBildDetails
{
    /// <summary>
    /// Die Bild-Daten des LV-Bildes, falls vorhanden.
    /// </summary>
    public Bild Bild { get; set; }
}

/// <summary>
/// Ein Bild im Format png, jpeg, svg oder gif.
/// </summary>
public class Bild
{
    /// <summary>
    /// Name des Bilds (üblicherweise Dateiname ohne Verzeichnispfad)
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Das Bildformat (png, jpeg, svg oder gif)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public BildFormat Format {get;set;}
    
    /// <summary>
    /// Die Bilddaten
    /// </summary>
    public byte[] Daten { get; set; }
}

public enum BildFormat
{
    Png,
    Jpeg,
    Gif,
    Svg
}

public class Variantenzusammenstellung
{
    public string Nummer { get; set; }
    
    public string Bezeichnung { get; set; }

    public string Beschreibung { get; set; }
}

public class Zuordnungskennzeichen
{
    public string Nummer { get; set; }
    
    public string Bezeichnung { get; set; }

    /// <summary>
    /// Liste mit Varianten.
    /// </summary>
    public List<Variante> Varianten { get; set; }

    /// <summary>
    /// Ordnet einer Variantenzusammenstellung (identifiziert über die Nummer der Variantenzusammenstellung
    /// eine Variante zu (identifiziert über die Nummer der Variante).  
    /// </summary>
    public Dictionary<string, int> VariantenzusammenstellungToVariante { get; set; }
}

public class Variante
{
    /// <summary>
    /// Nummer der Variante (identifiziert die Variante innerhalb ihres Zuordnungskennzeichen).
    /// </summary>
    public int Nummer { get; set; }
    
    public string Bezeichnung { get; set; }
}

public enum LvStatus
{
    /// <summary>
    /// Das Projekt, Unterprojekt oder der Auftrag wurde abgeschlossen.
    /// </summary>
    Abgeschlossen = 0,
    /// <summary>
    /// Wert für einen Auftrag oder Anfrage.
    /// </summary>
    Erhalten = 1,
    /// <summary>
    /// Wert für eine Anfrage.
    /// </summary>
    Kalkuliert = 2,
    /// <summary>
    /// Wert für eine Anfrage oder Auftrag.
    /// </summary>
    InArbeit = 3,
    /// <summary>
    /// Wert für einen Angebot ind Ausschreibung/Vergabe/Subanfrage.
    /// </summary>
    Erstellt = 4,
    /// <summary>
    /// Wert für ein Angebot.
    /// </summary>
    Angeboten = 5,
    /// <summary>
    /// Wert für ein Angebot oder ein Auftrag vergeben wird.
    /// </summary>
    Beauftragt = 6,
    /// <summary>
    /// Wird für eine Anfrage gesetzt, wenn diese verschickt wird.
    /// </summary>
    Verschickt = 7,
    /// <summary>
    /// Statusdefinition für Angebot: "Nicht beauftragt"
    /// </summary>
    NichtBeauftragt = 8
}

public class OenormLvDetails : BaseObject
{
    public OenormLvArt? Art { get; set; }

    public bool NachlässeAufEinheitspreis { get; set; }
    
    public bool NachlässeAufPreisanteilen { get; set; }
    
    public bool NachlässeAufHauptgruppen { get; set; }
    
    public bool NachlässeAufObergruppen { get; set; }
    
    public bool NachlässeAufLeistungsgruppen { get; set; }
    
    public bool NachlässeAufUnterleistungsgruppen { get; set; }
    
    public bool NachlässeAufLeistungsverzeichnis { get; set; }
    
    public string Vorhaben { get; set; }
    
    public int? Alternativangebotsnummer { get; set; }

    public int? Abänderungsangebotsnummer { get; set; }
}

public class GaebLvDetails : BaseObject
{
    public List<GaebGliederungsebene> Gliederungsebenen { get; set; }
    
    public string Füllzeichen { get; set; }
    
    public GaebVergabeart? GaebVergabeArt { get; set; }
    
    /// <summary>
    /// Vergabenummer des Auftraggebers
    /// </summary>
    public string VergabenummerAG { get; set; }
    
    /// <summary>
    /// Vergabenummer des Auftragnehmers
    /// </summary>
    public string VergabenummerAN { get; set; }
    
    public string DVNummerAG { get; set; }
    
    public string Abgabeort { get; set; }
    
    public string Zeiteinheit { get; set; }
    
    public bool BieterkommentareErlaubt { get; set; }

    public string LeitwegId { get; set; }
}

/// <summary>
/// Vergabeart
/// </summary>
public enum GaebVergabeart
{
    /// <summary>
    /// Offenes Verfahren
    /// </summary>
    OpenProc = 0,
    /// <summary>
    /// Nichtoffenes Verfahren
    /// </summary>
    ClosedProc = 1,
    /// <summary>
    /// Verhandlungsverfahren
    /// </summary>
    NegProc = 2,
    /// <summary>
    /// Öffentliche Ausschreibung
    /// </summary>
    OpenCall = 3,
    /// <summary>
    /// Beschränkte Ausschreibung
    /// </summary>
    SelectCall = 4,
    /// <summary>
    /// Freihändige Vergabe
    /// </summary>
    NegCont = 5,
    /// <summary>
    /// Internationale NATO-Ausschreibung
    /// </summary>
    IntNATO = 6,
    /// <summary>
    /// Beschränkte Ausschreibung nach öffentlichem Teilnahmewettbewerb
    /// </summary>
    SelectCallPostOpen = 7,
    /// <summary>
    /// Verhandlungsverfahren mit Vergabebekanntmachung
    /// </summary>
    NegProcOpen = 8,
    /// <summary>
    /// Wettbewerblicher Dialog
    /// </summary>
    CompetDialog = 9,
    /// <summary>
    /// Innovationspartnerschaft
    /// </summary>
    InnovationPartnership = 10
}

public class GaebGliederungsebene : BaseObject
{
    // public int EbeneNummer { get; set; }
    
    public GaebGliederungsebeneTyp? Typ { get; set; }
    
    public string Bezeichnung { get; set; }

    public int? Stellenzahl { get; set; }

    public bool IstNumerisch { get; set; }
    
    public string Startwert { get; set; }
    
    public int? Schrittweite { get; set; }
}

public enum GaebGliederungsebeneTyp
{
    Los,
    Titel,
    Position,
    Index
}

public enum OenormLvArt
{
    EntwurfsLV,
    KostenschaetzungsLV,
    AusschreibungsLV,
    AngebotsLV,
    AlternativangebotsLV,
    AbaenderungsangebotsLV,
    VertragsLV,
    AbrechnungsLV,
    ZusatzangebotsLV,
    VertragsanpassungsLV,
};

public enum LvItemTyp
{
    None,
    
    OnLeistungsposition,
    OnVorbemerkungsposition,
    OnHauptgruppe,
    OnObergruppe,
    OnLeistungsgruppe,
    OnUntergruppe,
    OnGrundtext,
    
    GaebLeistungsposition,
    GaebZuschlagsposition,
    GaebAusfuehrungsbeschreibung,
    GaebAusfuehrungsbeschreibungText,
    GaebHinweistext,
    GaebTitel,
    GaebLos,
    GaebUnterbeschreibung,
    GaebZusatztext
}

public enum OnÄnderungsumfang
{
    Unverändert,
    GeringfügigGeändert,
    Geändert
}

public enum Herkunftskennzeichen
{
    /// <summary>
    /// Position aus einem Leistungsbuch (LB).
    /// Dieser Wert ist auch für GAEB-Position gesetzt, wird dort aber ignoriert.
    /// </summary>
    LB = 0,

    /// <summary>
    /// + ... Position aus einem Ergänzungs-LB
    /// </summary>
    ErgLB = 1,

    /// <summary>
    /// Z ... Frei formulierte Position (Z-Position)
    /// </summary>
    Z = 2
}

/// <summary>
/// Ein Knoten oder eine Position eines Leistungsverzeichnisses.
/// </summary>
public class NewLvItemInfo : BaseObject
{
    protected NewLvItemInfo()
    {
    }

    public LvItemTyp ItemTyp { get; set; }

    public string Nummer { get; set; }

    public string Stichwort { get; set; }

    public LvItemFormatierteTexte FormatierteTexte { get; set; }

    public string Teilleistungsnummer { get; set; }

    public string Markierungskennzeichen { get; set; }

    public Herkunftskennzeichen? Herkunftskennzeichen { get; set; }

    public bool IstFixpreis { get; set; }

    public bool IstIntern { get; set; }

    public bool Schreibgeschützt { get; set; }

    public LvItemLbInfo LbInfo { get; set; }

    public int? Variante { get; set; }

    public string Zuordnungskennzeichen { get; set; }

    public NachlassInfo NachlassInfo { get; set; }
    
    /// <summary>
    /// Entfällt-Flag. Nur für GAEB.
    /// </summary>
    public bool Entfällt { get; set; }
    
    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die diesem LV-Item zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue> CustomPropertyValues { get; set; }
}

public class NewLvKnotenInfo : NewLvItemInfo
{
    public Guid? ParentKnotenId { get; set; }
}

public class NewLvPositionInfo : NewLvItemInfo
{
    /// <summary>
    /// Die ID des Knoten, unter dem die Position erzeugt wird. Für GAEB-LVs ist auch null erlaubt, um
    /// Positonen (z.B. Zusatztexte) auf oberster Ebene zu erzeugen.
    /// </summary>
    public Guid? ParentKnotenId { get; set; }

    public string Einheit { get; set; }

    public LvPositionsart? Positionsart { get; set; }
    
    public decimal? LvMenge { get; set; }
    
    public bool EinheitSchreibgeschützt { get; set; }

    public bool EinheitspreisSchreibgeschützt { get; set; }

    public bool TexteSchreibgeschützt { get; set; }

    public bool LvMengeSchreibgeschützt { get; set; }

    public string Mehrfachverwendung { get; set; }
    
    public string Stichwortluecke { get; set; }

    public Dictionary<string, Money> Preisanteile { get; set; }
    
    public List<LvPositionGliederungsKnoten> GliederungsKnotenList { get; set; }
    
    /// <summary>
    /// Nur ÖNorm
    /// </summary>
    public bool IstWesentlichePosition { get; set; }

    /// <summary>
    /// Nur ÖNorm
    /// </summary>
    public bool IstNichtAngeboten { get; set; }
    
    /// <summary>
    /// Nur für GAEB
    /// </summary>
    public bool IstBeauftragt { get; set; }

    /// <summary>
    /// Nur für GAEB
    /// </summary>
    public bool IstSchwerpunktposition { get; set; }

    /// <summary>
    /// Nur für GAEB
    /// </summary>
    public bool IstStundenlohnarbeiten { get; set; }

    /// <summary>
    /// Nur für GAEB
    /// </summary>
    public bool IstFreieBietermenge { get; set; }
    
    /// <summary>
    /// Nur für GAEB
    /// </summary>
    public BedarfspositionArt BedarfspositionArt { get; set; }

    /// <summary>
    /// Nur für GAEB: Zuschlagsprozentsatz (für Zuschlagspositionen).
    /// </summary>
    public decimal? Zuschlagsprozentsatz { get; set; }

    /// <summary>
    /// Nur für GAEB: Zuschlagsart (für Zuschlagspositionen)
    /// </summary>
    public GaebZuschlagsart? Zuschlagsart { get; set; }

    /// <summary>
    /// Nur für GAEB: Markierung von zu bezuschlagenden Leistungspositionen.
    /// Zuschlagspositionen der Art "AufMarkierteDavor" nehmen darauf Bezug.
    /// </summary>
    public bool WirdBezuschlagt { get; set; }
    
    /// <summary>
    /// Nur für GAEB: Liste von zu bezuschlagenden Positionen (für Zuschlagspositionen der Art "GemäßListe").
    /// </summary>
    public List<ZuBezuschlagendePosition> ZuBezuschlagendePositionen { get; set; }
    
    /// <summary>
    /// Nur für GAEB: Liste von Positionen, die dieser Position untergeordnet sind (Unterbeschreibungen unter
    /// einer Leistungsposition oder Ausführungsbeschreibungstexte unter einer Ausführungsbeschreibung).
    /// </summary>
    public List<LvPosition> Unterpositionen { get; set; }
}

/// <summary>
/// Für GAEB-Zuschlagspositionen: Art des Zuschlags.
/// </summary>
public enum GaebZuschlagsart
{
    /// <summary>
    /// Zuschlag auf auf alle davorstehenden Positionen.
    /// </summary>
    AufAlleDavor,
    
    /// <summary>
    /// Zuschlag auf alle als "ZuBezuschlagen" markierten davorstehenden Positionen.
    /// </summary>
    AufMarkierteDavor,

    /// <summary>
    /// Zuschlag auf gelistete Positionen.
    /// </summary>
    GemäßListe
}

public class ZuBezuschlagendePosition : BaseObject
{
    public Guid PositionsId { get; set; }

    public decimal? Menge { get; set; }
}

/// <summary>
/// Ein LV-Item, d.h. ein Gruppenelement (z.B. Leistungsgruppe oder Titel) oder eine Position.
/// </summary>
public class LvItemBase : BaseObject
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// Art der Position oder Gruppe.
    /// </summary>
    public LvItemTyp ItemTyp { get; set; }

    /// <summary>
    /// Die lokale Nummer (z.B. "03").
    /// </summary>
    public string Nummer { get; set; }
    
    /// <summary>
    /// Die vollständige Nummer (z.B. "01.04.03").
    /// </summary>
    public string NummerKomplett { get; set; }
    
    /// <summary>
    /// Das Stichwort. Für GAEB kann dieser String nur ausgelesen werden.
    /// Zum Manipulieren gibt es FormatierteTexte.Kurztext.
    /// </summary>
    public string Stichwort { get; set; }

    /// <summary>
    /// (Detailinfo) Objekt mit allen formatierten Texten (z.B. Langtext).
    /// </summary>
    public LvItemFormatierteTexte FormatierteTexte { get; set; }
    
    public string Teilleistungsnummer { get; set; }
    
    public string Markierungskennzeichen { get; set; }

    /// <summary>
    /// Nur für ÖNorm: Das Herkunftskennzeichen (gibt an, ob es einen LB-Bezug gibt).
    /// </summary>
    public Herkunftskennzeichen Herkunftskennzeichen { get; set; } = Herkunftskennzeichen.LB;
    
    public LvItemLbInfo LbInfo { get; set; }
    
    /// <summary>
    /// Enthält berechnete Werte. Ist nur befüllt, wenn das gesamte LV abgerufen wird
    /// (per /build/projekte/{projektId}/leistungsverzeichnisse/{lvId}).
    /// </summary>
    public LvItemErgebnisse Ergebnisse { get; set; }
    
    public bool Schreibgeschützt { get; set; }
    
    public bool IstFixpreis { get; set; }
    
    public bool IstIntern { get; set; }

    /// <summary>
    /// Im Fall einer Wahlposition: Die gewünschte Variante (identifiziert über die Nummer).
    /// (Diese Eigenschaft ist auch für Gruppenelemente verfügbar, da dies in der GAEB-Norm
    /// unterstützt wird.)
    /// </summary>
    public int? Variante { get; set; }

    /// <summary>
    /// Fall Variante befüllt ist: Das Zuordnungskennzeichen, zu dem die Variante gehört.
    /// </summary>
    public string Zuordnungskennzeichen { get; set; }

    /// <summary>
    /// Aufschläge/Nachlässe, falls vorhanden.
    /// </summary>
    public NachlassInfo NachlassInfo { get; set; }
    
    /// <summary>
    /// Entfällt-Flag. Nur für GAEB.
    /// </summary>
    public bool Entfällt { get; set; }
    
    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die diesem LV-Item zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue> CustomPropertyValues { get; set; }
}

public class NachlassInfo
{
    /// <summary>
    /// Aufschläge/Nachlässe pro Preisanteil (idertifiziert über den Preisanteil-Code, z.B. "L" für Lohn).
    /// </summary>
    public Dictionary<string, Nachlass> PreisanteilNachlässe { get; set; }
    
    /// <summary>
    /// Falls Aufschläge/Nachlässe auf dem Einheitspreis unterstützt werden, kommt diese
    /// Eigenschaft zum Einsatz.
    /// </summary>
    public Nachlass EinheitspreisNachlass { get; set; }
}

public class Nachlass
{
    public AufschlagNachlassArt Art { get; set; }
    
    public decimal? Wert { get; set; }
}

public enum AufschlagNachlassArt
{
    Prozent = 0,
    Pauschal = 1
}

/// <summary>
/// Enthält Daten für die formatierten Texte eines LV-Elements (Position oder Knoten).
/// </summary>
public class LvItemFormatierteTexte : BaseObject
{
    /// <summary>
    /// Der Langtext (im ÖNorm-Format, d.h. mit eingebetteten HTML-Tags).
    /// </summary>
    public string Langtext { get; set; }
    
    /// <summary>
    /// Die Baubeschreibung (im ÖNorm-Format, d.h. mit eingebetteten HTML-Tags).
    /// </summary>
    public string Baubeschreibung { get; set; }

    /// <summary>
    /// Nur für GAEB-LVs: Der Kurztext (im ÖNorm-Format, d.h. mit eingebetteten HTML-Tags).
    /// Für ÖNorm-LVs wird diese Property ignoriert, da dort für Kurztexte
    /// keine Formatierungen unterstützt werden. 
    /// </summary>
    public string Kurztext { get; set; }
}

public class LvItemLbInfo : BaseObject
{
    public string LbKennung { get; set; }
    public int? LbVersion { get; set; }
    public DateTime? LbVersionsDatum { get; set; }

    public string ErgLbKennung { get; set; }
    public int? ErgLbVersion { get; set; }
    public DateTime? ErgLbVersionsDatum { get; set; }

    public string LbLgNummer { get; set; }
    public string LbUlgNummer { get; set; }
    public string LbGtNummer { get; set; }
    public string LbPosNummer { get; set; }
    
    public OnÄnderungsumfang? AenderungsUmfang { get; set; }
}

/// <summary>
/// Ein LV-Knoten (z.B. Titel oder Leistungsgruppe).
/// </summary>
public class LvKnoten : LvItemBase
{
    public List<LvKnoten> Knoten { get; set; }
    
    public List<LvPosition> Positionen { get; set; }
}

public enum LvPositionsart
{
    Normalposition = 0,
    
    /// <summary>
    /// Wahlposition (für ÖNorm)
    /// </summary>
    Wahlposition = 1,

    /// <summary>
    /// Eventualposition (für ÖNorm)
    /// </summary>
    Eventualposition = 2,

    /// <summary>
    /// Grundposition (für GAEB)
    /// </summary>
    Grundposition = 3,

    /// <summary>
    /// Alternativposition (für GAEB)
    /// </summary>
    Alternativposition = 4
}

/// <summary>
/// Eine LV-Position.
/// </summary>
public class LvPosition : LvItemBase
{
    public string Einheit { get; set; }

    public LvPositionsart Positionsart { get; set; } = LvPositionsart.Normalposition;
    
    public decimal? LvMenge { get; set; }
    
    public bool EinheitSchreibgeschützt { get; set; }

    public bool EinheitspreisSchreibgeschützt { get; set; }

    public bool TexteSchreibgeschützt { get; set; }

    public bool LvMengeSchreibgeschützt { get; set; }

    public string Mehrfachverwendung { get; set; }
    
    public string Stichwortluecke { get; set; }
    
    public Dictionary<string, Money> Preisanteile { get; set; }
    
    public List<LvPositionGliederungsKnoten> GliederungsKnotenList { get; set; }

    /// <summary>
    /// Nur ÖNorm
    /// </summary>
    public bool IstWesentlichePosition { get; set; }

    /// <summary>
    /// Nur ÖNorm
    /// </summary>
    public bool IstNichtAngeboten { get; set; }
    
    /// <summary>
    /// Nur für GAEB
    /// </summary>
    public bool IstBeauftragt { get; set; }

    /// <summary>
    /// Nur für GAEB
    /// </summary>
    public bool IstSchwerpunktposition { get; set; }

    /// <summary>
    /// Nur für GAEB
    /// </summary>
    public bool IstStundenlohnarbeiten { get; set; }

    /// <summary>
    /// Nur für GAEB
    /// </summary>
    public bool IstFreieBietermenge { get; set; }
    
    /// <summary>
    /// Nur für GAEB
    /// </summary>
    public BedarfspositionArt BedarfspositionArt { get; set; }
    
    /// <summary>
    /// Nur für GAEB: Zuschlagsprozentsatz (für Zuschlagspositionen).
    /// </summary>
    public decimal? Zuschlagsprozentsatz { get; set; }

    /// <summary>
    /// Nur für GAEB: Zuschlagsart (für Zuschlagspositionen)
    /// </summary>
    public GaebZuschlagsart? Zuschlagsart { get; set; }
    
    /// <summary>
    /// Nur für GAEB: Markierung von zu bezuschlagenden Leistungspositionen.
    /// Zuschlagspositionen der Art "AufMarkierteDavor" nehmen darauf Bezug.
    /// </summary>
    public bool WirdBezuschlagt { get; set; }
    
    /// <summary>
    /// Nur für GAEB: Liste von zu bezuschlagenden Positionen (für Zuschlagspositionen der Art "GemäßListe").
    /// </summary>
    public List<ZuBezuschlagendePosition> ZuBezuschlagendePositionen { get; set; }
    
    /// <summary>
    /// Nur für GAEB: Liste von Positionen, die dieser Position untergeordnet sind (Unterbeschreibungen unter
    /// einer Leistungsposition oder Ausführungsbeschreibungstexte unter einer Ausführungsbeschreibung).
    /// </summary>
    public List<LvPosition> Unterpositionen { get; set; }
}

/// <summary>
/// Art der Bedarfsposition (nur für GAEB).
/// </summary>
public enum BedarfspositionArt
{
    /// <summary>
    /// Keine Bedarfsposition
    /// </summary>
    Keine,

    /// <summary>
    /// Gesamtbetrag geht nicht in die LV-Summe ein
    /// </summary>
    OhneGesamtbetrag,
    
    /// <summary>
    /// Gesamtbetrag geht in die LV-Summe ein
    /// </summary>
    MitGesamtbetrag
}

public class LvPositionGliederungsKnoten : BaseObject
{
    public Guid KatalogId { get; set; }
    
    public Guid KnotenId { get; set; }
}

public class LvItemErgebnisse : BaseObject
{
    public Money Einheitspreis { get; set; }

    /// <summary>
    /// Gesamtbetrag (netto) inkl. Aufschläge/Nachlässe.
    /// </summary>
    public Money Betrag { get; set; }

    /// <summary>
    /// Gesamtbetrag (brutto) inkl. Aufschläge/Nachlässe.
    /// </summary>
    public Money BetragBrutto { get; set; }

    /// <summary>
    /// Gesamtbetrag (netto) ohne Aufschläge/Nachlässe.
    /// </summary>
    public Money BetragInklAN { get; set; }

    /// <summary>
    /// Gesamtbetrag (brutto) ohne Aufschläge/Nachlässe.
    /// </summary>
    public Money BetragBruttoInklAN { get; set; }

    /// <summary>
    /// Die für die Berechnung verwendete Menge.
    /// </summary>
    public decimal? Menge { get; set; }
}

#endregion Leistungsverzeichnis

#region Mengenermittlung

public class Aufmaßblatt : BaseObject
{
    public Guid Id { get; set; }
    public MengenArt? MengenArt { get; set; }
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
}

public enum DBKostenart
{
    EinmaligeKosten = 100,
    MengenabhaengigeKosten = 200,
    ZeitabhaengigeKosten = 300,
    UmsatzabhaengigeKosten = 400,
}

public enum RechnungsStatus
{
    Eingegangen = 0,
    Geprüft = 1,
    Freigegeben = 2,
    Bezahlt = 3,
    Ersetzt = 4,
    Gesendet = 10,
    Erfasst = 11,
}

public enum RechnungsArt
{
    Einzelrechnung = 0,
    Abschlagsrechnung = 1,
    Teilschlussrechnung = 2,
    Schlussrechnung = 3,
    Regierechnung = 4,
    Vorauszahlung = 6,
    Pauschalrechnung = 7,
    AbschlagsrechnungPauschal = 8,
    AbschlagsrechnungNichtKumuliert = 9,
    AbschlagsrechnungNichtKumuliertPauschal = 10,
    SchlussrechnungPauschal = 11,
    TeilschlussrechnungPauschal = 12,
}

public class NewRechnungInfo : BaseObject
{
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
}

public class Rechnung : BaseObject
{
    public Guid Id { get; set; }
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
    public DateTime? Rechnungsdatum { get; set; }
    public string ExterneRechnungsnummer { get; set; }
    public DateTime? Eingangsdatum { get; set; }
    public DateTime? GesendetAm { get; set; }
    public RechnungsStatus? Status { get; set; }
    public List<Zahlung> Zahlungen { get; set; }
    public int? LaufendeNummerKapsel { get; set; }
    public RechnungsArt? Art { get; set; }
    public decimal? ProzentDerAuftragssumme { get; set; }
    public bool? IstInforechnung { get; set; }
    public decimal? NettoUngeprüft { get; set; }
    public decimal? NettoForderungKorrektur { get; set; }
    public bool? IstGeschützt { get; set; }
    public bool? BürgschaftBankhaftbrief { get; set; }
    public DateTime? GewährleistungBeginn { get; set; }
    public DateTime? GewährleistungBis { get; set; }
    public DateTime? RückgabeGewährleistungseinbehaltBar { get; set; }
    
    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die dieser Rechnung zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue> CustomPropertyValues { get; set; }
}

public class Zahlung : BaseObject
{
    public Guid Id { get; set; }
    public DateTime? Zahlungsdatum { get; set; }
    public decimal? Zahlbetrag { get; set; }
    public decimal? Skontobetrag { get; set; }
    public string Bemerkung { get; set; }
}

public class AbrechnungsMerkmal : BaseObject
{
    public Guid Id { get; set; }
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
    public string BezeichnungKomplett { get; set; }
    public List<AbrechnungsMerkmal> Merkmale { get; set; }
}

public class Leistungszeitraum : BaseObject
{
    public Guid Id { get; set; }
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
    public DateTime? Beginn { get; set; }
    public DateTime? Ende { get; set; }
}

public class NewPositionsblockInfo : BaseObject
{
    public MengenArt? MengenArt { get; set; }
    public Guid? PositionId { get; set; }
    public Guid? VerherigePositionId { get; set; }
    public Guid? AufmaßblattId { get; set; }
    public Guid? LeistungszeitraumId { get; set; }
    public Guid? RechnungId { get; set; }
}

public class Positionsblock : BaseObject
{
    public Guid Id { get; set; }
    public Guid? PositionId { get; set; }
    public Guid? PositionKorrigiertId { get; set; }

    /// <summary>
    /// Die vollständige Positionsnummer.
    /// </summary>
    public string Nummer { get; set; }
    public string NummerKorrigiert { get; set; }
    public string Kurztext { get; set; }
    public string Einheit { get; set; }

    public Guid? AufmaßblattId { get; set; }
    public Guid? LeistungszeitraumId { get; set; }
    public Guid? RechnungId { get; set; }

    public decimal? Menge { get; set; }

    public List<Aufmaßzeile> Aufmaßzeilen { get; set; }

    public List<Guid> MerkmalIds { get; set; }

    public bool? ErzeugtInKorrektur { get; set; }
}

public enum MengenArt
{
    Lv = 0,
    Abrechnung = 1,
    Umlagemenge = 2,
    Bautagebuch = 3,
    AbrechnungKorrigiert = 4,
    Rechnung = 5,
    Rechnungkorrigiert = 6,
    Prognose1 = 10,
    Prognose2 = 11,
    Prognose3 = 12,
    Prognose4 = 13,
    Prognose5 = 14,
    Prognose6 = 15,
    Prognose7 = 16,
    Prognose8 = 17,
    Prognose9 = 18,
    Prognose10 = 19
}

public class Aufmaßzeile : BaseObject
{
    public Guid Id { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public AufmaßzeilenArt Art { get; set; }
    public AufmaßzeilenArt? ArtKorrigiert { get; set; }
    public string InternerKommentar { get; set; }
    public string Inhalt { get; set; }
    public string InhaltKorrigiert { get; set; }
    public string Variable { get; set; }
    public string Erläuterung { get; set; }
    public string AdresseVon { get; set; }
    public string AdresseBis { get; set; }
    public decimal? Faktor { get; set; }
    public decimal? FaktorKorrigiert { get; set; }
    public Formel Formel { get; set; }
    public Formel FormelKorrigiert { get; set; }
    public bool? ErzeugtInKorrektur { get; set; }
    public decimal? Menge { get; set; }
}

public enum AufmaßzeilenArt
{
    Ansatz = 0,
    Formel,
    Kommentar
}

public class Formel
{
    public int Id { get; set; }
    public List<FormelParameter> Params { get; set; }
}

public class FormelParameter
{
    public string Name { get; set; }
    public decimal? Value { get; set; }
    public string Variable { get; set; }
}

#endregion Mengenermittlung

/// <summary>
/// Enthält den Wert einer Individualeigenschaft (Custom Property). Beim Auslesen ist neben dem Feld
/// 'ValueType' immer genau eines der übrigen '...Value'-Felder befüllt (abhängig vom Datentyp), sofern ein
/// Wert vorhanden ist, ansonsten sind alle Felder (außer 'ValueType') null.
/// </summary>
public class CustomPropertyValue : BaseObject
{
    /// <summary>
    /// Der Typ der Individualeigenschaft als Enum-Wert. Ist beim Auslesen immer befüllt. Wird beim
    /// Befüllen ignoriert.
    /// </summary>
    public CustomPropertyType? ValueType { get; set; }
    
    /// <summary>
    /// Der Wert, falls dieser als String darstellbar ist.
    /// </summary>
    public string StringValue { get; set; }
    
    /// <summary>
    /// Der Wert, falls dieser als Decimal darstellbar ist.
    /// </summary>
    public decimal? DecimalValue { get; set; }
    
    /// <summary>
    /// Der Wert, falls dieser als Ganzzahl (Typ long) darstellbar ist.
    /// </summary>
    public long? IntegerValue { get; set; }
    
    /// <summary>
    /// Der Wert, falls dieser als Wahrheitswert (true/false) darstellbar ist.
    /// </summary>
    public bool? BooleanValue { get; set; }
    
    /// <summary>
    /// Der Wert, falls dieser als Datumswert (ohne Zeitzone) darstellbar ist.
    /// </summary>
    public DateTime? DateTimeValue { get; set; }
    
    /// <summary>
    /// Der Wert, falls dieser als Datumswert mit Zeitzone darstellbar ist.
    /// </summary>
    public DateTimeOffset? DateTimeWithTimeZoneValue { get; set; }

    /// <summary>
    /// Der Wert, falls dieser als Zeitspanne darstellbar ist.
    /// </summary>
    public TimeSpan? TimeSpanValue { get; set; }

    /// <summary>
    /// Der Wert, falls dieser als Geo-Koordinate darstellbar ist.
    /// </summary>
    public GeoCoordinate GeoCoordinateValue { get; set; }
}

/// <summary>
/// Der Typ einer Individualeigenschaft (Custom Property).
/// </summary>
public enum CustomPropertyType
{
    Text,
    Date,
    Decimal,
    Boolean,
    GeoCoordinate,
    Url,
    DirectoryPath,
    FilePath,
    DateAndTime,
    DateAndTimeWithTimeZone,
    Integer,
    Memo,
    EMailAddress,
    Time,
    Duration,
    PhoneNumber
}

/// <summary>
/// Ein einfacher Geldbetrag (inklusive Währung).
/// </summary>
public class SimpleMoney
{
    public SimpleMoney(string currency, decimal? value)
    {
        Currency = currency;
        Value = value;
    }

    [JsonProperty("cur")]
    public string Currency { get; private set; }

    [JsonProperty("val")]
    public decimal? Value { get; private set; }

    public override string ToString()
    {
        return Currency + " " + Value;
    }
}

/// <summary>
/// Ein mehrfacher Geldbetrag (für unterschiedliche Währungen).
/// </summary>
public class Money : Collection<SimpleMoney>
{
    public Money()
    {
    }

    public Money(string currency, decimal? value)
    {
        Add(currency, value);
    }

    public static Money FromValues(IEnumerable<(string Currency, decimal? Value)> values)
    {
        if (values == null)
        {
            return null;
        }

        Money result = null;

        foreach (var betrag in values)
        {
            if (result == null)
            {
                result = new Money();
            }

            result.Add(betrag.Currency, betrag.Value);
        }

        return result;
    }

    public void Add(string currency, decimal? value)
    {
        Add(new SimpleMoney(currency, value));
    }

    public decimal? FirstValue
    {
        get => this.FirstOrDefault()?.Value;
        set => this[0] = new SimpleMoney(this[0].Currency, value);
    }

    public override string ToString()
    {
        return string.Join("; ", this);
    }
}

/// <summary>
/// Ein berechneter Wert (z.B. Nettobetrag einer Leistungsgruppe). Es sind nur die für den jeweiligen Wertetyp
/// relevanten Felder befüllt.
/// </summary>
public class Rechenwert
{
    /// <summary>
    /// Identifiziert den Wertetyp (z.B. "Lv_Listenpreis_Brutto").
    /// </summary>
    public string Id { get; set; }

    public decimal? Wert { get; set; }

    public decimal? Basiswert { get; set; }

    public string Währung { get; set; }

    public string PreisanteilCode { get; set; }

    public string UmsatzsteuerCode { get; set; }

    public int? ZuschlagIndex { get; set; }

    public ZuschlagsTyp? ZuschlagsTyp { get; set; }

    public decimal? PreisanteilUmrechnungssatz { get; set; }

    public Guid? PreisperiodeId { get; set; }

    public int? PreisperiodeNummer { get; set; }

    public Guid? PositionId { get; set; }

    public string Kostenart { get; set; }

    public Guid? WarengruppeId { get; set; }

    public DBKostenart? DBKostenart { get; set; }
}

/// <summary>
/// Enthält alle Rechenwerte des Projekts einschließlich der Leistungsverzeichnisse.
/// </summary>
public class ProjektRechenwerte
{
    /// <summary>
    /// Die Rechenwerte, gegliedert nach LV-Art.
    /// </summary>
    public Dictionary<LvArt, List<Rechenwert>> Werte { get; set; }

    /// <summary>
    /// Rechenwerte aller Leistungsverzeichnisse.
    /// </summary>
    public List<LvRechenwerte> LvWerte { get; set; }
}

/// <summary>
/// Enthält die Rechenwerte eines Leistungsverzeichnisses.
/// </summary>
public class LvRechenwerte
{
    /// <summary>
    /// ID des Leistungsverzeichnisses.
    /// </summary>
    public Guid LvId { get; set; }

    /// <summary>
    /// Die Rechenwerte, gegliedert nach LV-Art.
    /// </summary>
    public Dictionary<LvArt, List<Rechenwert>> Werte { get; set; }
}

#region Bautagebuch

public class NewBautagesberichtInfo : BaseObject
{
}

/// <summary>
/// Objekt für einen Bautagesbericht
/// </summary>
public class Bautagesbericht : BaseObject
{
    public Guid Id { get; set; }
    public string Nummer { get; set; }
    public DateTime? Datum { get; set; }
    public DateTime? ErfasstAm { get; set; }
    public string ErstelltVon { get; set; }
    public string Bauaufsicht { get; set; }
    public bool? Geprueft { get; set; }
    public string Notiz { get; set; }

    public BautagesberichtArbeitszeit ArbeitsZeit { get; set; }

    public BautagesberichtPausenzeit PausenZeit1 { get; set; }
    public BautagesberichtPausenzeit PausenZeit2 { get; set; }

    public BautagesberichtWetter WetterGanztags { get; set; }
    public BautagesberichtWetter WetterMorgens { get; set; }
    public BautagesberichtWetter WetterMittags { get; set; }
    public BautagesberichtWetter WetterAbends { get; set; }
    public decimal? Luftfeuchtigkeit { get; set; }

    public BautagesberichtSchlechtwetter SchlechtWetter1 { get; set; }
    public BautagesberichtSchlechtwetter SchlechtWetter2 { get; set; }

    public List<BautagesberichtSchnellerfassung> Arbeiter { get; set; }
    public List<BautagesberichtSchnellerfassung> Angestellte { get; set; }
    public List<BautagesberichtSchnellerfassung> Geraete { get; set; }

    public List<BautagesberichtBauarbeitsschluessel> Bauarbeitsschluessel { get; set; }

    public List<BautagesberichtBaustoffe> Baustoffe { get; set; }

    public string Leistungstext { get; set; }
    public List<BautagesberichtLeistungsabrechnung> Leistungsabrechnung { get; set; }

    public string Regietext { get; set; }
    public List<BautagesberichtLeistungsabrechnung> Regieabrechnung { get; set; }
}

public class BautagesberichtArbeitszeit : BaseObject
{
    public DateTime? ArbeitszeitBeginn { get; set; }
    public DateTime? ArbeitszeitEnde { get; set; }
}

public class BautagesberichtPausenzeit : BaseObject
{
    public DateTime? PausenzeitBeginn { get; set; }
    public DateTime? PausenzeitEnde { get; set; }
}

public class BautagesberichtWetter : BaseObject
{
    public string Bezeichnung { get; set; }
    public decimal? TemperaturMin { get; set; }
    public decimal? TemperaturMax { get; set; }
}

public class BautagesberichtSchlechtwetter : BaseObject
{
    public DateTime? SchlechtwetterBeginn { get; set; }
    public DateTime? SchlechtwetterEnde { get; set; }
}

public class BautagesberichtSchnellerfassung : BaseObject
{
    public Guid? Id { get; set; }
    public Guid? DataId { get; set; }
    public string Bezeichnung { get; set; }
    public decimal? Anzahl { get; set; }
}

public class BautagesberichtBauarbeitsschluessel : BaseObject
{
    public Guid? Id { get; set; }
    public Guid? BauarbeitsschluesselId { get; set; }
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
    public decimal? Stunden { get; set; }
}

public class BautagesberichtBaustoffe : BaseObject
{
    public Guid? Id { get; set; }
    public Guid? BaustoffId { get; set; }
    public string Bezeichnung { get; set; }
    public BaustoffArt? Art { get; set; }
    public string Ansatz { get; set; }
    public decimal? Menge { get; set; }
    public string Einheit { get; set; }
    public string Kommentar { get; set; }
}

public enum BaustoffArt
{
    Verbrauch = 0,
    Zugang = 1,
}

public class BautagesberichtLeistungsabrechnung : BaseObject
{
    public Guid? Id { get; set; }
    public Guid? PosBlockid { get; set; }
    public Guid? PositionId { get; set; }
    public string Nummer { get; set; }
    public string Bezeichnung { get; set; }
    public decimal? LvMenge { get; set; }
    public string Einheit { get; set; }
    public string Inhalt { get; set; }
    public decimal? Menge { get; set; }
    public string InternerKommentar { get; set; }
}

/// <summary>
/// Objekt für die Projektdaten zu einem Projekt für die Bautagesberichte
/// </summary>
public class BautagebuchProjektdaten : BaseObject
{
    public List<BautagebuchWetterProjektdaten> WetterProjektdaten { get; set; }
    public List<BautagebuchArbeiterProjektdaten> ArbeiterProjektdaten { get; set; }
    public List<BautagebuchAngestellteProjektdaten> AngestellteProjektdaten { get; set; }
    public List<BautagebuchGeraeteProjektdaten> GeraeteProjektdaten { get; set; }
}

/// <summary>
/// Objekt für die Wetter Projektdaten zu einem Projekt für die Bautagesberichte
/// </summary>
public class BautagebuchWetterProjektdaten : BaseObject
{
    public Guid? Id { get; set; }
    public string Bezeichnung { get; set; }
}

/// <summary>
/// Objekt für die Arbeiter Projektdaten zu einem Projekt für die Bautagesberichte
/// </summary>
public class BautagebuchArbeiterProjektdaten : BaseObject
{
    public Guid? Id { get; set; }
    public string Bezeichnung { get; set; }
}

/// <summary>
/// Objekt für die Angestellte Projektdaten zu einem Projekt für die Bautagesberichte
/// </summary>
public class BautagebuchAngestellteProjektdaten : BaseObject
{
    public Guid? Id { get; set; }
    public string Bezeichnung { get; set; }
}

/// <summary>
/// Objekt für die Geräte Projektdaten zu einem Projekt für die Bautagesberichte
/// </summary>
public class BautagebuchGeraeteProjektdaten : BaseObject
{
    public Guid? Id { get; set; }
    public string Bezeichnung { get; set; }
}

#endregion Bautagebuch
