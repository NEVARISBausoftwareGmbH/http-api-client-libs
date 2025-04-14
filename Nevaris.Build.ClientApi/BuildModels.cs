using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
// ReSharper disable CollectionNeverUpdated.Global
#pragma warning disable CS1591

namespace Nevaris.Build.ClientApi;

/// <summary>
/// Objekt, das von GET /build/global/version geliefert wird und Versionsinformationen zu Build und der API enthält.
/// </summary>
public class VersionInfo : BaseObject
{
    /// <summary>
    /// Die NEVARIS Build-Programmversion.
    /// </summary>
    public string ProgramVersion { get; set; } = "";

    /// <summary>
    /// Die Versionsnummer der HTTP API. Diese folgt den Regeln der semantischen Versionierung.
    /// </summary>
    public string ApiVersion { get; set; } = "";
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
/// Eine globale oder projektbezogene Währung.
/// </summary>
public class Währung : BaseObject
{
    /// <summary>
    /// Der Code (= "Nummer") der Währung. Dient im Fall von globalen Währungen als Schlüssel. 
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Für Projektwährungen: Die GUID, die die Währung identifiziert. Ist für globale Währungen null.
    /// </summary>
    public Guid? Id { get; set; }

    public string? Bezeichnung { get; set; }

    public string? Beschreibung { get; set; }
}

/// <summary>
/// Eine globale oder projektbezogene Umsatzsteuer.
/// </summary>
public class Umsatzsteuer : BaseObject
{
    /// <summary>
    /// Der Code (= "Nummer") der Umsatzsteuer. Dient im Fall von globalen Umsatzsteuern als Schlüssel. 
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Für ProjektUmsatzsteuern: Die GUID, die die Umsatzsteuer identifiziert. Ist für globale Umsatzsteuern null.
    /// </summary>
    public Guid? Id { get; set; }

    public string? Bezeichnung { get; set; }

    public string? Beschreibung { get; set; }

    /// <summary>
    /// Der Wert (= Prozentsatz).
    /// </summary>
    public decimal? Wert { get; set; }

    public string? Zusatztext { get; set; }

    public string? CodeUntdid5305 { get; set; }
}

/// <summary>
/// Eine globale oder projektbezogene Einheit.
/// </summary>
public class Einheit : BaseObject
{
    /// <summary>
    /// Der Code (= "Nummer") der Einheit. Dient im Fall von globalen Einheiten als Schlüssel. 
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Für Projekteinheite: Die GUID, die die Einheit identifiziert. Ist für globale Einheiten null.
    /// </summary>
    public Guid? Id { get; set; }

    public string? Bezeichnung { get; set; }

    public string? Beschreibung { get; set; }

    public int? AnzahlNachkommestellen { get; set; }

    public string? BezeichnungOeNORMA { get; set; }

    public string? BezeichnungOeNORMB { get; set; }

    public string? BezeichnungGAEB90 { get; set; }

    public string? BezeichnungGAEB2000 { get; set; }

    public string? BezeichnungGAEBXML { get; set; }

    public string? Synonym1 { get; set; }

    public string? Synonym2 { get; set; }

    public string? Synonym3 { get; set; }

    public string? Synonym4 { get; set; }

    public string? Synonym5 { get; set; }

    public string? Synonym6 { get; set; }

    public string? Synonym7 { get; set; }

    public string? Synonym8 { get; set; }

    public string? Synonym9 { get; set; }

    public string? Synonym10 { get; set; }

    public decimal? Umrechnungsfaktor { get; set; }

    public int? Rundung { get; set; }

    public decimal? Aufschlag { get; set; }

    public string? CodeUnEce { get; set; }

    public string? FinanceMengeneinheit { get; set; }

    public override string? ToString() => Code;
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
    public string? Id { get; set; }

    /// <summary>
    /// Mandantenbezeichnung, wie sie in der Benutzeroberfläche angezeigt wird.
    /// </summary>
    public string? AnzeigeText { get; set; }

    /// <summary>
    /// Die Mandatenadresse, falls definiert.
    /// </summary>
    public AdresseKurzInfo? Adresse { get; set; }

    /// <summary>
    /// Liste von zugeordneten Niederlassungen.
    /// </summary>
    public List<Niederlassung>? Niederlassungen;

    public override string? ToString() => AnzeigeText;
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
    public string? Id { get; set; }

    public string? Bezeichnung { get; set; }

    public string? Suchbegriff { get; set; }

    /// <summary>
    /// Niederlassungsbezeichnung, wie sie in der Benutzeroberfläche angezeigt wird.
    /// </summary>
    public string? AnzeigeText { get; set; }

    /// <summary>
    /// Die Niederlassungsadresse, falls definiert.
    /// </summary>
    public AdresseKurzInfo? Adresse { get; set; }

    public override string? ToString() => AnzeigeText;
}

/// <summary>
/// Eine Adresse, wie sie in <see cref="Mandant"/> und <see cref="Niederlassung"/> verwendet wird.
/// Enthält nicht alle Felder des vollständigen Adresstyps <see cref="Adresse"/>.
/// </summary>
public class AdresseKurzInfo : BaseObject
{
    public string? Code { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public AdressArt AdressArt { get; set; }

    public string? Name { get; set; }

    public string? Vorname { get; set; }

    public string? Zusatz1 { get; set; }

    public string? Zusatz2 { get; set; }

    public string? Zusatz3 { get; set; }

    public string? LandCode { get; set; }

    public string? Plz { get; set; }

    public string? Ort { get; set; }

    public string? Straße { get; set; }

    public GesperrtArt GesperrtArt { get; set; }

    public override string ToString() => $"{Code}: {Name}";
}

public class GeoCoordinate : BaseObject
{
    public double Latitude { get; set; }

    public double Longitude { get; set; }
}

/// <summary>
/// Infos zu einer neu anzulegenden globalen Adresse.
/// </summary>
public class NewAdresseInfo : BaseObject
{
    /// <summary>
    /// Der Adresscode. Das ist der Wert, der anschließend in <see cref="Adresse.Code"/> und
    /// <see cref="Adresse.Nummer"/> verfügbar ist. Falls null, wird ein Code automatisch generiert.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Die Adressart: Organisation oder Person.
    /// </summary>
    public AdressArt AdressArt { get; set; }

    /// <summary>
    /// Der Name. Ist für Organisationen der Name der Organisation, für Personen der Nachname, sofern kein solcher
    /// über das Feld <see cref="Nachname"/> spezifiziert wird.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Für Personenadressen: Der Vorname.
    /// </summary>
    public string? Vorname { get; set; }

    /// <summary>
    /// Für Personenadressen: Der Nachname.
    /// </summary>
    public string? Nachname { get; set; }
}

/// <summary>
/// Infos zu einer neu anzulegenden Projektadresse.
/// </summary>
public class NewProjektAdresseInfo : BaseObject
{
    /// <summary>
    /// Falls befüllt, wird die Projektadresse durch Übernahme der globalen Adresse mit diesem Code. In diesem
    /// Fall werden die restlichen Felder in diesem Objekt ignoriert.
    /// </summary>
    public string? GlobaleAdresseCode { get; set; }

    /// <summary>
    /// Die Adressart: Organisation oder Person.
    /// </summary>
    public AdressArt AdressArt { get; set; }

    /// <summary>
    /// Der Name. Ist für Organisationen der Name der Organisation, für Personen der Nachname, sofern kein solcher
    /// über das Feld <see cref="Nachname"/> spezifiziert wird.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Für Personenadressen: Der Vorname.
    /// </summary>
    public string? Vorname { get; set; }

    /// <summary>
    /// Für Personenadressen: Der Nachname.
    /// </summary>
    public string? Nachname { get; set; }

    /// <summary>
    /// Die optionale Nummer der Adresse (<see cref="Adresse.Code"/>).
    /// </summary>
    public string? Nummer { get; set; }
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

/// <summary>
/// Beschreibt die Zuordnung einer Adresse zu einem LV.
/// </summary>
public class LvZugeordneteAdresse : BaseObject
{
    /// <summary>
    /// Die Adressrolle (z.B. "Auftraggeber").
    /// </summary>
    public ZugeordneteAdresseRolle Rolle { get; set; }

    /// <summary>
    /// Die ID der zugeordneten Adresse.
    /// </summary>
    public Guid? AdressId { get; set; }

    /// <summary>
    /// Optionale ID des Ansprechpartners (wenn die Adresse eine Organisation ist).
    /// </summary>
    public Guid? AnsprechpartnerId { get; set; }
}

/// <summary>
/// Eine globale oder projektbezogene Adresse.
/// </summary>
public class Adresse : BaseObject
{
    /// <summary>
    /// Für globale Adressen: Der interne Code, der die Adresse identifiziert.
    /// Für Projektadressen: Der Code der globalen Adresse (sofern es eine gibt), aus der diese Projektadresse
    /// erzeugt wurde (nicht änderbar).
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Für Projektadressen: Die GUID, die die Adresse identifiziert. Ist für globale Adressen null.
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Die in der Oberfläche sichtbare Nummer er Adresse. Ist für manuell angelegte globale Adressen identisch mit dem
    /// Code und kann nicht nachträglich geändert werden.
    /// Für Projektadressen ist dieser Wert optional und änderbar. 
    /// </summary>
    public string? Nummer { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public AdressArt AdressArt { get; set; }

    public string? Name { get; set; }
    public string? Vorname { get; set; }
    public string? Nachname { get; set; }
    public string? Kürzel { get; set; }

    public string? LandCode { get; set; }
    public bool IstPostfachInVerwendung { get; set; }
    public string? Titel { get; set; }
    public string? UstId { get; set; }
    public string? Telefon { get; set; }
    public string? Fax { get; set; }
    public string? EMail { get; set; }
    public string? Name2 { get; set; }
    public string? Name3 { get; set; }
    public string? Name4 { get; set; }
    public string? Ort { get; set; }
    public string? Straße { get; set; }
    public string? Plz { get; set; }
    public string? Postfach { get; set; }
    public string? PostfachPlz { get; set; }
    public string? PostfachOrt { get; set; }
    public string? MobilFirma { get; set; }
    public string? MobilPrivat { get; set; }
    public string? Internet { get; set; }
    public bool IstBauleistender { get; set; }
    public string? Skype { get; set; }
    public string? LoginName { get; set; }
    public string? Suchbegriff { get; set; }
    public string? Briefanschrift { get; set; }
    public string? SozialesNetzwerk1 { get; set; }
    public string? SozialesNetzwerk1Name { get; set; }
    public string? SozialesNetzwerk2 { get; set; }
    public string? SozialesNetzwerk2Name { get; set; }
    public string? Adresszusatz { get; set; }

    /// <summary>
    /// Bundesland-Code. Wird nur für globale Adressen genutzt.
    /// </summary>
    public string? BundeslandCode { get; set; }

    /// <summary>
    /// Landkreis/Bezirks-Code. Wird nur für globale Adressen genutzt.
    /// </summary>
    public string? LandkreisCode { get; set; }

    /// <summary>
    /// Anrede-Code ("FR", "HR"). Wird nur für globale Adressen genutzt.
    /// </summary>
    public string? AnredeCode { get; set; }

    /// <summary>
    /// Sperrhinweis-Code. Wird nur für globale Adressen genutzt.
    /// </summary>
    public string? SperrhinweisCode { get; set; }

    /// <summary>
    /// Sprach-Code ("DE", "EN" usw.). Wird nur für globale Adressen genutzt.
    /// </summary>
    public string? SpracheCode { get; set; }

    /// <summary>
    /// Code für die Adressquelle. Wird nur für globale Adressen genutzt.
    /// </summary>
    public string? AdressQuelleCode { get; set; }

    /// <summary>
    /// Falls ein Verweis auf eine andere Adresse gesetzt ist, ist das der Code. Nur für globale Adressen relevant.
    /// </summary>
    public string? VerweisAufAdresseCode { get; set; }

    /// <summary>
    /// Nur für globale Adressen relevant.
    /// </summary>
    public bool IstDuplikat { get; set; }

    /// <summary>
    /// Nur für globale Adressen: Der Adresscode des zugeordneten Konzerns.
    /// </summary>
    public string? KonzernCode { get; set; }

    /// <summary>
    /// Nur für globale Adressen: Der Adresscode der zugeordneten Zentrale.
    /// </summary>
    public string? ZentraleCode { get; set; }

    /// <summary>
    /// Nur für Projektadressen: Die Adress-ID des zugeordneten Konzerns.
    /// </summary>
    public Guid? KonzernId { get; set; }

    /// <summary>
    /// Nur für Projektadressen: Die Adress-ID der zugeordneten Zentrale.
    /// </summary>
    public Guid? ZentraleId { get; set; }

    public bool IstDebitorVorhanden { get; set; }
    public bool IstKreditorVorhanden { get; set; }
    public string? Handelsregister { get; set; }
    public string? UnsereKundenNummerDort { get; set; }
    public string? SteuernummerGesellschaft { get; set; }
    public string? UrsprungsCode { get; set; }
    public string? TitelImAnschreiben { get; set; }
    public DateTime? Geburtsdatum { get; set; }
    public GesperrtArt GesperrtArt { get; set; }
    public DateTime? GültigAb { get; set; }
    public DateTime? GültigBis { get; set; }
    public int? VollständigkeitInProzent { get; set; }
    public decimal? Saldo { get; set; }
    public string? ExternerCode { get; set; }
    public string? Auslandsvorwahl { get; set; }
    public string? DurchwahlFax { get; set; }
    public string? DurchwahlZentrale { get; set; }

    /// <summary>
    /// Nur für globale Adressen: Eine ID in GUID-Format, die derzeit nur für interne Zwecke genutzt wird.
    /// </summary>
    public Guid? Guid { get; set; }

    public string? Hauptanschlussnummer { get; set; }
    public bool? IsReadOnlyNumber { get; set; }
    public string? Ortskennzahl { get; set; }
    public string? OutlookEntryId { get; set; }
    public int? Ähnlichkeit { get; set; }
    public GeoCoordinate? GeoPosition { get; set; }

    /// <summary>
    /// Notiz. Für globale Adressen ist dies ein unformatierter Text, für Projektadressen ist dies
    /// ein formatierter Text (XHTML).
    /// </summary>
    public string? Notiz { get; set; }

    /// <summary>
    /// Detailinfo (nur für globale Adressen): Optionales Objekt, das das Adress-Bild enthält.
    /// Diese Property wird nur gesetzt, wenn beim Abrufen der Adresse mitBildDetails = true übergeben wurde. 
    /// </summary>
    public AdressBildDetails? BildDetails { get; set; }

    /// <summary>
    /// Feld "Beschreibung". Nur für globale Adressen relevant.
    /// </summary>
    public string? Beschreibung { get; set; }

    public List<Adressat>? Adressaten { get; set; }
    public List<Bankverbindung>? Bankverbindungen { get; set; }
    public List<AdressBranche>? Branchen { get; set; }
    public List<AdressGewerk>? Gewerke { get; set; }

    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die dieser Adresse zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue?>? CustomPropertyValues { get; set; }
}

/// <summary>
/// Enthält Binärdaten zu Grafiken, die einer globalen Adresse zugeordnet sind.
/// </summary>
public class AdressBildDetails : BaseObject
{
    /// <summary>
    /// Die Bild-Daten des Adress-Bildes, falls vorhanden (sonst null).
    /// </summary>
    public Bild? Bild { get; set; }
}

public class Adressat : BaseObject
{
    /// <summary>
    /// Für globale Adressen: Der Code, der den Adressaten identifiziert.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Für Projektadressen: Die GUID, die den Adressaten identifiziert.
    /// </summary>
    public Guid? Id { get; set; }

    public string? AnredeCode { get; set; }

    public string? PrivatadresseCode { get; set; }

    public Guid? PrivatadresseId { get; set; }

    public string? Titel { get; set; }

    public string? TitelImAnschreiben { get; set; }

    public string? Vorname { get; set; }
    public string? Nachname { get; set; }
    public string? Telefon { get; set; }
    public string? Fax { get; set; }
    public string? Mobil { get; set; }
    public string? EMail { get; set; }
    public string? EMailAbteilung { get; set; }
    public string? Url { get; set; }
    public string? Skype { get; set; }
    public string? AbteilungCode { get; set; }
    public string? FunktionCode { get; set; }
    public bool IstInaktiv { get; set; }
    public DateTime? Austrittsdatum { get; set; }
    public string? Raum { get; set; }
    public string? Info { get; set; }
    public string? Beschreibung { get; set; }
    public string? Notiz { get; set; }
    public string? SpracheCode { get; set; }
    public string? Briefanschrift { get; set; }
    public string? Durchwahl { get; set; }
    public string? DurchwahlFax { get; set; }
    public Guid? Guid { get; internal set; }

    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die diesem Adressaten zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue?>? CustomPropertyValues { get; set; }
}

public class Bankverbindung : BaseObject
{
    public string? Iban { get; set; }
    public string? Bic { get; set; }
    public string? Bankname { get; set; }
}

public class AdressBranche : BaseObject
{
    public string? BrancheCode { get; set; }
    public string? Bezeichnung { get; set; }
    public string? Beschreibung { get; set; }
}

public class AdressGewerk : BaseObject
{
    public string? GewerkCode { get; set; }
    public string? Bezeichnung { get; set; }
    public string? Beschreibung { get; set; }
}

#endregion Adressen

#region Projekte

/// <summary>
/// Ein Speicherort (Ordner oder Datenbank).
/// </summary>
public class Speicherort : BaseObject
{
    public Guid Id { get; set; }

    public string? Bezeichnung { get; set; }

    /// <summary>
    /// Falls der Speicherort ein Dateiordner ist, enthält dieses Objekt die passenden Informationen.
    /// Der Begriff 'Ordner' sollte nicht mit Ordnern in Speicherorten (<see cref="RootOrdnerList"/>).
    /// </summary>
    public OrdnerInfo? OrdnerInfo { get; set; }

    /// <summary>
    /// Falls der Speicherort eine Datenbank ist, enthält dieses Objekt die passenden Informationen.
    /// </summary>
    public DatenbankInfo? DatenbankInfo { get; set; }

    /// <summary>
    /// (Detailinfo) Liste aller Projekten an diesem Speicherort (unabhängig davon, ob sich diese in einem
    /// Server-Ordner oder auf der Wurzelebene befinden).
    /// </summary>
    public List<ProjektInfo>? ProjektInfos { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Projekten an diesem Speicherort auf der Wurzelebene.
    /// Diese Property ist nur befüllt, wenn beim Auslesen des Speicherorts mitOrdnern = true
    /// übergeben wurde.
    /// </summary>
    public List<ProjektInfo>? RootProjektInfos { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Ordnern innerhalb dieses Speicherorts (auf Wurzelebene).
    /// Diese Property ist nur befüllt, wenn beim Auslesen des Speicherorts mitOrdnern = true
    /// übergeben wurde.
    /// </summary>
    public List<SpeicherortOrdner>? RootOrdnerList { get; set; }
}

/// <summary>
/// Ein Ordner innerhalb eines Speicherorts (nur für Datenbank-Speicherorte).
/// </summary>
public class SpeicherortOrdner : BaseObject
{
    /// <summary>
    /// Die ID des Ordners (nicht sichtbar on der Oberfläche).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Der Ordnername, wie er in der Navigationsleiste in der Projektverwaltung zu sehen ist.
    /// </summary>
    public string? Bezeichnung { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Projekten in diesem Ordner.
    /// </summary>
    public List<ProjektInfo>? ProjektInfos { get; set; }

    /// <summary>
    /// Liste mit untergeordneten Ordnern.
    /// </summary>
    public List<SpeicherortOrdner>? OrdnerList { get; set; }
}

public class OrdnerInfo : BaseObject
{
    public string? Pfad { get; set; }
}

public class DatenbankInfo : BaseObject
{
    public string? Server { get; set; }
    public string? Datenbank { get; set; }
    public string? Benutzername { get; set; }
    public string? Passwort { get; set; }
    public bool IntegratedSecurity { get; set; }
}

/// <summary>
/// Beschreibt ein Projekt. Im Gegensatz zu <see cref="Projekt"/> enthält dieser Typ nur ID,
/// Nummer und Bezeichnung des Projekts und sonst keine Projektinhalte.
/// </summary>
public class ProjektInfo : BaseObject
{
    public string? Id { get; set; }

    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }
}

/// <summary>
/// Ein Projekt.
/// </summary>
public class Projekt : BaseObject
{
    /// <summary>
    /// Die Projekt-ID im Format "[Speicherort-Guid].[Projekt-Guid]", wie sie in den URLs für Projekt-Endpunkte
    /// (/build/projekte/{projektId}/...) vorkommt. Identifiziert ein Projekt Speicherort-übergreifend.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Die ID des Projekts als Guid (ohne Speicherort-Information). Diese ID kommt zum Einsatz, wenn ein Projekt
    /// als Kostenebene oder Zuschlagsebene in Betriebsmitteln auftaucht, z.B. in der Property
    /// <see cref="BetriebsmittelKosten.KostenebeneId"/>.
    /// </summary>
    public Guid Guid { get; set; }
    
    /// <summary>
    /// Das Datum der letzten Änderung am Projekt.
    /// </summary>
    public DateTimeOffset? ModificationDate { get; set; }

    /// <summary>
    /// Der Benutzer, der die letzte Änderung am Projekt gemacht hat.
    /// </summary>
    public string? ModificationUser { get; set; }

    /// <summary>
    /// Für Projekte, die auf Server-Speicherorten liegen: Die ID des Ordners, in dem das Projekt abgelegt ist
    /// (innerhalb seines Speicherorts). Falls null, liegt das Projekt auf der Wurzelebene.
    /// </summary>
    public Guid? SpeicherortOrdnerId { get; set; }

    /// <summary>
    /// Die ID des Mandanten, dem das Projekt zugeordnet ist (optional).
    /// </summary>
    public string? MandantId { get; set; }

    /// <summary>
    /// Identifiziert in Kombination mit MandantId die Niederlassung, der das Projekt zugeordnet ist (optional).
    /// </summary>
    public string? NiederlassungId { get; set; }

    public bool IstVorlageprojekt { get; set; }

    /// <summary>
    /// Die Nummer des Projekts (optional).
    /// </summary>
    public string? Nummer { get; set; }

    /// <summary>
    /// Die Bezeichnung des Projekts (optional).
    /// </summary>
    public string? Bezeichnung { get; set; }

    public DateTime? Baubeginn { get; set; }
    public DateTime? Bauende { get; set; }
    public string? Ampel { get; set; }
    public string? Art { get; set; }
    public string? Ausschreibungsart { get; set; }
    public string? Status { get; set; }
    public string? Sparte { get; set; }
    public string? Typ { get; set; }

    /// <summary>
    /// Die Nummer der Kostenstelle, die dem Projekt zugeordnet ist. Im Falle aktiver Finance-Integration ist das
    /// eine Finance-Kostenstelle, ansonsten eine Control-Kostenstelle.
    /// </summary>
    public string? KostenstelleNummer { get; set; }

    /// <summary>
    /// Liste von Leistungsverzeichnissen, die in diesem Projekt enthalten sind.
    /// Auf dieses Liste kann nur lesend zugegriffen werden. Für Schreiboperationen gibt es die
    /// passenden Endpunkte unter /build/projekte/{projektId}/leistungsverzeichnisse.
    /// </summary>
    public List<Leistungsverzeichnis>? Leistungsverzeichnisse { get; set; }

    /// <summary>
    /// Liste von Gliederungskatalogen, die in diesem Projekt enthalten sind. Nur Lesezugriffe werden unterstützt.
    /// </summary>
    public List<Gliederungskatalog>? Gliederungskataloge { get; set; }

    /// <summary>
    /// Liste von Leistungszeiträumen, die in diesem Projekt enthalten sind.
    /// </summary>
    public List<Leistungszeitraum>? Leistungszeiträume { get; set; }

    /// <summary>
    /// Liste von Einheiten, die in diesem Projekt enthalten sind.
    /// </summary>
    public List<Einheit>? Einheiten { get; set; }

    /// <summary>
    /// Liste von Währungen, die in diesem Projekt enthalten sind.
    /// </summary>
    public List<Währung>? Währungen { get; set; }

    /// <summary>
    /// Liste von Umsatzsteuern, die in diesem Projekt enthalten sind.
    /// </summary>
    public List<Umsatzsteuer>? Umsatzsteuern { get; set; }

    /// <summary>
    /// (Detailinfo) Verweist auf die Adressen, die dem Projekt zugeordnet sind (inklusive Adressrollen).
    /// </summary>
    public List<ProjektZugeordneteAdresse>? ZugeordneteAdressen { get; set; }

    /// <summary>
    /// Die ID des Betriebsmittelstamms, der für dieses Projekt hinerlegt ist. Kann nicht direkt gesetzt werden,
    /// sondern wird beim erstmaligen Anlegen einer Kalkulation befüllt.
    /// Der Betriebsmittelstamm kann per GET /build/global/betriebsmittelstaemme/{betriebsmittelstammId}
    /// abgefragt werden.
    /// Es ist möglich, dass es zu der hier hinterlegten ID keinen Betriebsmittelstamm gibt, da z.B.
    /// der Betriebsmittelstamm nachträglich gelöscht worden sein kann, wovon das Projekt jedoch nichts mitbekommt.
    /// </summary>
    public Guid? BetriebsmittelStammId { get; set; }

    /// <summary>
    /// Die Nummer des zugewiesenen Betriebsmittelstamms. Ist befüllt, wenn <see cref="BetriebsmittelStammId"/>
    /// befüllt ist und wenn die Betriebsmittelstamm-ID auf einen existierenden Betriebsmittelstamm verweist.
    /// </summary>
    public string? BetriebsmittelStammNummer { get; set; }

    /// <summary>
    /// Die Bezeichnung des zugewiesenen Betriebsmittelstamms. Ist befüllt, wenn <see cref="BetriebsmittelStammId"/>
    /// befüllt ist und wenn die Betriebsmittelstamm-ID auf einen existierenden Betriebsmittelstamm verweist.
    /// </summary>
    public string? BetriebsmittelStammBezeichnung { get; set; }

    /// <summary>
    /// Die Art des zugewiesenen Betriebsmittelstamms. Ist befüllt, wenn <see cref="BetriebsmittelStammId"/>
    /// befüllt ist.
    /// </summary>
    public BetriebsmittelStammArt? BetriebsmittelStammArt { get; set; }

    /// <summary>
    /// Für ÖNORM-Stämme: Die Kalkulationsversion des zugewiesenen Betriebsmittelstamms
    /// Ist befüllt, wenn <see cref="BetriebsmittelStammId"/> befüllt ist.
    /// </summary>
    public BetriebsmittelStammKalkulationsVersion? KalkulationsVersion { get; set; }

    /// <summary>
    /// Die Anzahl der Nachkommastellen für die Berechnung der Mengen. Wird beim Anlegen einer Kalkulation
    /// aus dem Betriebsmittelstamm übernommen.
    /// </summary>
    public int? RechengenauigkeitMengen { get; set; }

    /// <summary>
    /// Die Anzahl der Nachkommastellen für die Berechnung der Beträge. Wird beim Anlegen einer Kalkulation
    /// aus dem Betriebsmittelstamm übernommen.
    /// </summary>
    public int? RechengenauigkeitBeträge { get; set; }

    /// <summary>
    /// Die Anzahl der Nachkommastellen für die Darstellung der Mengen. Wird beim Anlegen einer Kalkulation
    /// aus dem Betriebsmittelstamm übernommen.
    /// </summary>
    public int? DarstellungsgenauigkeitMengen { get; set; }

    /// <summary>
    /// Die Anzahl der Nachkommastellen für die Darstellung der Beträge. Wird beim Anlegen einer Kalkulation
    /// aus dem Betriebsmittelstamm übernommen.
    /// </summary>
    public int? DarstellungsgenauigkeitBeträge { get; set; }

    /// <summary>
    /// Die ID der Wurzel-Lohngruppe des Projekts. Das zugehörige Objekt ist per
    /// /build/projekte/{projektId}/betriebsmittel/{betriebsmittelId} abrufbar.
    /// </summary>
    public Guid LohnRootGruppeId { get; set; }

    /// <summary>
    /// Die ID der Wurzel-Materialgruppe des Projekts. Das zugehörige Objekt ist per
    /// /build/projekte/{projektId}/betriebsmittel/{betriebsmittelId} abrufbar.
    /// </summary>
    public Guid MaterialRootGruppeId { get; set; }

    /// <summary>
    /// Die ID der Wurzel-Gerätegruppe des Projekts. Das zugehörige Objekt ist per
    /// /build/projekte/{projektId}/betriebsmittel/{betriebsmittelId} abrufbar.
    /// </summary>
    public Guid GerätRootGruppeId { get; set; }

    /// <summary>
    /// Die ID der Wurzel-Sonstige-Kosten-Gruppe des Projekts. Das zugehörige Objekt ist per
    /// /build/projekte/{projektId}/betriebsmittel/{betriebsmittelId} abrufbar.
    /// </summary>
    public Guid SonstigeKostenRootGruppeId { get; set; }

    /// <summary>
    /// Die ID der Wurzel-Nachunternehmergruppe des Projekts. Das zugehörige Objekt ist per
    /// /build/projekte/{projektId}/betriebsmittel/{betriebsmittelId} abrufbar.
    /// </summary>
    public Guid NachunternehmerRootGruppeId { get; set; }

    /// <summary>
    /// Die ID der Wurzel-Bausteingruppe des Projekts. Das zugehörige Objekt ist per
    /// /build/projekte/{projektId}/betriebsmittel/{betriebsmittelId} abrufbar.
    /// </summary>
    public Guid BausteinRootGruppeId { get; set; }

    /// <summary>
    /// (Detailinfo) Enthält Kostenanteilbezeichnungen.
    /// </summary>
    public BetriebsmittelStammBezeichnungen? Bezeichnungen { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Zuschlagsarten (entspricht dem Reiter "Zuschläge" in Build).
    /// Über dieses Feld wird bestimmt, wie viele Zuschlagsspalten in den Kosten- und Zuschlagskatalogen angeboten
    /// werden und wie diese heißen.
    /// Für Betriebsmittelstämme mit der Kalkulationsversion B2061_2020 ist hier immer genau eine vordefinierte
    /// Zuschlagsart enthalten (es darf nicht mehr geben).
    /// </summary>
    public List<Zuschlagsart>? Zuschlagsarten { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Zuschlagsgruppen. Legt fest, welche Zuschläge zur Verfügung stehen
    /// (nur für ÖNORM relevant).
    /// </summary>
    public List<Zuschlagsgruppe>? Zuschlagsgruppen { get; set; }

    /// <summary>
    /// (Detailinfo) Für GAEB: Die Einträge im Reiter "Zuschlagsberechnung". Legt die Berechnungsmethode
    /// für die einzelnen Zuschlagstypen (z.B. AGK) fest.
    /// </summary>
    public List<ZuschlagsartGruppe>? ZuschlagsartGruppen { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Gerätefaktoren.
    /// </summary>
    public List<Gerätefaktor>? Gerätefaktoren { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von globalen Variablen.
    /// </summary>
    public List<GlobaleVariable>? GlobaleVariablen { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Warengruppen.
    /// </summary>
    public List<Warengruppe>? Warengruppen { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von DbBetriebsmittelGruppen.
    /// </summary>
    public List<DbBetriebsmittelGruppe>? DbBetriebsmittelGruppen { get; set; }

    /// <summary>
    /// Die Individualeigenschaften, die diesem Projekt zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue?>? CustomPropertyValues { get; set; }
}

/// <summary>
/// Beschreibt ein neu anzulegendes Projekt.
/// </summary>
public class NewProjektInfo : BaseObject
{
    /// <summary>
    /// Die ID des Mandanten, dem das Projekt zugeordnet ist (optional).
    /// </summary>
    public string? MandantId { get; set; }

    /// <summary>
    /// Für Server-Speicherorte: Die ID des Ordners, in dem das Projekt abgelegt wird. Falls null,
    /// wird das Projekt auf der Wurzelebene angelegt.
    /// </summary>
    public Guid? SpeicherortOrdnerId { get; set; }

    /// <summary>
    /// Identifiziert in Kombination mit MandantId die Niederlassung, der das Projekt zugeordnet ist (optional).
    /// </summary>
    public string? NiederlassungId { get; set; }

    /// <summary>
    /// Die Nummer des Projekts (optional).
    /// </summary>
    public string? Nummer { get; set; }

    /// <summary>
    /// Die Bezeichnung des Projekts (optional).
    /// </summary>
    public string? Bezeichnung { get; set; }

    public bool IstVorlageprojekt { get; set; }
}

/// <summary>
/// Beschreibt die Zuordnung einer Adresse zu einem Projekt.
/// </summary>
public class ProjektZugeordneteAdresse : BaseObject
{
    /// <summary>
    /// Die Adressrolle (z.B. "Auftraggeber").
    /// </summary>
    public ZugeordneteAdresseRolle Rolle { get; set; }

    /// <summary>
    /// Die ID der zugeordneten Adresse.
    /// </summary>
    public Guid? AdressId { get; set; }

    /// <summary>
    /// Optionale ID des Ansprechpartners (wenn die Adresse eine Organisation ist).
    /// </summary>
    public Guid? AnsprechpartnerId { get; set; }
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
    public string? MandantId { get; set; }

    /// <summary>
    /// Identifiziert in Kombination mit MandantId die Niederlassung, dem dieser Betriebsmittelstamm zugeordnet ist (optional).
    /// </summary>
    public string? NiederlassungId { get; set; }

    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }

    public string? Beschreibung { get; set; }

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
    public BetriebsmittelStammBezeichnungen? Bezeichnungen { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Kostenkatalogen.
    /// </summary>
    public List<Kostenkatalog>? Kostenkataloge { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Zuschlagskatalogen.
    /// </summary>
    public List<Zuschlagskatalog>? Zuschlagskataloge { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Zuschlagsarten (entspricht dem Reiter "Zuschläge" in Build).
    /// Über dieses Feld wird bestimmt, wie viele Zuschlagsspalten in den Kosten- und Zuschlagskatalogen angeboten
    /// werden und wie diese heißen.
    /// Für Betriebsmittelstämme mit der Kalkulationsversion B2061_2020 ist hier immer genau eine vordefinierte
    /// Zuschlagsart enthalten (es darf nicht mehr geben).
    /// </summary>
    public List<Zuschlagsart>? Zuschlagsarten { get; set; }

    /// <summary>
    /// (Detailinfo) ÖNORM: Liste von Zuschlagsgruppen. Legt fest, welche Zuschläge zur Verfügung stehen.
    /// </summary>
    public List<Zuschlagsgruppe>? Zuschlagsgruppen { get; set; }

    /// <summary>
    /// (Detailinfo) Die Einträge im Grid "Zuschlagsberechnung" (nur für GAEB relevant).
    /// </summary>
    public List<ZuschlagsartGruppe>? ZuschlagsartGruppen { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Gerätefaktoren.
    /// </summary>
    public List<Gerätefaktor>? Gerätefaktoren { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von globalen Variablen.
    /// </summary>
    public List<GlobaleVariable>? GlobaleVariablen { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Warengruppen.
    /// </summary>
    public List<Warengruppe>? Warengruppen { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von DbBetriebsmittelGruppen.
    /// </summary>
    public List<DbBetriebsmittelGruppe>? DbBetriebsmittelGruppen { get; set; }

    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die diesem Betriebsmittelstamm zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue?>? CustomPropertyValues { get; set; }
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
    public string? LohnKostenanteil1 { get; set; }
    public string? LohnKostenanteil2 { get; set; }

    public string? ListenpreisGeraetKostenanteil1 { get; set; }
    public string? ListenpreisGeraetKostenanteil2 { get; set; }

    public string? SonstigeKostenKostenanteil1 { get; set; }
    public string? SonstigeKostenKostenanteil2 { get; set; }
    public string? SonstigeKostenKostenanteil3 { get; set; }
    public string? SonstigeKostenKostenanteil4 { get; set; }
    public string? SonstigeKostenKostenanteil5 { get; set; }
    public string? SonstigeKostenKostenanteil6 { get; set; }

    public string? NachunternehmerKostenanteil1 { get; set; }
    public string? NachunternehmerKostenanteil2 { get; set; }
    public string? NachunternehmerKostenanteil3 { get; set; }
    public string? NachunternehmerKostenanteil4 { get; set; }
    public string? NachunternehmerKostenanteil5 { get; set; }
    public string? NachunternehmerKostenanteil6 { get; set; }
}

/// <summary>
/// Informationen zu einem neu zu erzeugenden Betriebsmittelstamm.
/// </summary>
public class NewBetriebsmittelStammInfo : BaseObject
{
    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }

    public BetriebsmittelStammArt? Art { get; set; }

    /// <summary>
    /// Die Kalkulationsversion (nur für Betriebsmittelstämme der Art "Aut" relevant).
    /// </summary>
    public BetriebsmittelStammKalkulationsVersion? KalkulationsVersion { get; set; }
}

/// <summary>
/// Eine Kostenartstruktur, d.h. das Wurzelelement, das die Kostenarten (und Kostenartengruppen) enthält.
/// Im Fall von aktivem Mandatenbezug muss jede Kostenartstruktur einem Mandanten zugeordnet sein und es gibt
/// pro Mandant oder Niederlassung maximal eine Kostenartstruktur. Im Fall von nicht aktivem Mandantebezug
/// gibt es genau eine mandantenübergreifende Kostenartstruktur.
/// </summary>
public class KostenartStruktur : BaseObject
{
    /// <summary>
    /// Die Bezeichung der Kostenartstruktur.
    /// </summary>
    public string? Bezeichnung { get; set; }

    /// <summary>
    /// Die Nummer der Kostenartstruktur.
    /// </summary>
    public string? Nummer { get; set; }

    /// <summary>
    /// Bei aktivem Mandantenbezug: Die Mandanten-ID.
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
    public string? MandantenId { get; set; }

    /// <summary>
    /// Bei aktivem Mandantenbezug: Die Niederlassung-ID (kann null sein).
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
    public string? NiederlassungId { get; set; }
}

/// <summary>
/// Eine Kostenart. Kann auch eine Kostenartengruppe sein.
/// </summary>
public class Kostenart : BaseObject
{
    /// <summary>
    /// Die Bezeichung der Kostenart.
    /// </summary>
    public string? Bezeichnung { get; set; }

    /// <summary>
    /// Die Nummer der Kostenart innerhalb der enthaltenden Gruppe (z.B. "2").
    /// </summary>
    public string? NummerLokal { get; set; }

    /// <summary>
    /// Die vollständige Nummer (z.B. "3.1.2").
    /// </summary>
    public string? Nummer { get; set; }

    /// <summary>
    /// Die Nummer des Sachkontos.
    /// </summary>
    public string? SachkontoNummer { get; set; }

    /// <summary>
    /// Falls true, ist dies eine Kostenartenguppe.
    /// </summary>
    public bool IstGruppe { get; set; }

    /// <summary>
    /// Befüllt im Fall IstGruppe == true. Enthält die Child-Kostenarten.
    /// </summary>
    public List<Kostenart>? Kostenarten { get; set; }
}

/// <summary>
/// Informationen zu einer neu zu erzeugenden Kostenart (oder Kostenartengruppe).
/// </summary>
public class NewKostenartInfo : BaseObject
{
    /// <summary>
    /// Die Bezeichnung der neuen Kostenart.
    /// </summary>
    public string? Bezeichnung { get; set; }

    /// <summary>
    /// Die vollständige Nummer der neuen Kostenart (oder Kostenartengruppe), z.B. "7.2.". Ein Punkt (".") wird
    /// immer als Trennzeichen zwischen zwei Ebenen interpretiert.
    /// </summary>
    public string? Nummer { get; set; }

    /// <summary>
    /// Die Nummer des Sachkontos, welches referenziert werden soll.
    /// </summary>
    public string? SachkontoNummer { get; set; }

    /// <summary>
    /// Falls true, wird einen Kostenartengruppe erzeugt, ansonsten eine Kostenart.
    /// </summary>
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
    public string? Nummer { get; set; }

    /// <summary>
    /// Die Bezeichnung des Bauarbeitsschluessels (BAS).
    /// </summary>
    public string? Bezeichnung { get; set; }

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

/// <summary>
/// Ein Gerätefaktor zur Verwendung in einem Betriebsmittelstamm oder in den Betriebsmitteln eines Projekts.
/// </summary>
public class Gerätefaktor : BaseObject
{
    public string? Nummer { get; set; }

    public GeräteArt? Art { get; set; }

    public string? Bezeichnung { get; set; }

    /// <summary>
    /// ist nur für Betriebsmittelstämme befüllt. Im Projektfall ist dieser Wert kostenebenenabhängig und
    /// steht in <see cref="Werte"/>.
    /// </summary>
    public decimal? AbminderungsfaktorAV { get; set; }

    /// <summary>
    /// ist nur für Betriebsmittelstämme befüllt. Im Projektfall ist dieser Wert kostenebenenabhängig und
    /// steht in <see cref="Werte"/>.
    /// </summary>
    public decimal? AbminderungsfaktorRepLohn { get; set; }

    /// <summary>
    /// ist nur für Betriebsmittelstämme befüllt. Im Projektfall ist dieser Wert kostenebenenabhängig und
    /// steht in <see cref="Werte"/>.
    /// </summary>
    public decimal? AbminderungsfaktorRepMaterial { get; set; }

    /// <summary>
    /// ist nur für Betriebsmittelstämme befüllt. Im Projektfall ist dieser Wert kostenebenenabhängig und
    /// steht in <see cref="Werte"/>.
    /// </summary>
    public decimal? StundenProMonat { get; set; }

    /// <summary>
    /// Nur für Projekte: Die (kostenebenenabhängigen) Werte
    /// </summary>
    public List<GerätefaktorWert>? Werte { get; set; }
}

public class GerätefaktorWert : BaseObject
{
    /// <summary>
    /// Nur für Projekte: Die Kostenebene (z.B. Kalkulation), für die der Wert definiert ist. 
    /// </summary>
    public Guid KostenebeneId { get; set; }

    public decimal? AbminderungsfaktorAV { get; set; }

    public decimal? AbminderungsfaktorRepLohn { get; set; }

    public decimal? AbminderungsfaktorRepMaterial { get; set; }

    public decimal? StundenProMonat { get; set; }
}

/// <summary>
/// Eine globale Variable zur Verwendung in einem Betriebsmittelstamm oder in den Betriebsmitteln eines Projekts.
/// </summary>
public class GlobaleVariable : BaseObject
{
    /// <summary>
    /// Variablenname
    /// </summary>
    public string? Variable { get; set; }

    [Obsolete("Dieses Flag war nur von interner Bedeutung und wird nicht mehr unterstützt.")]
    public bool? IstKalkulationsVariable { get; set; }

    /// <summary>
    /// Nur für Betriebsmittelstämme: Enthält den Ansatz (= Berechnungsformel). Im Projektfall steht der
    /// Ansatz kostenebenenabhängig in <see cref="Ansätze"/>.
    /// </summary>
    public string? Ansatz { get; set; }

    /// <summary>
    /// Nur für Betriebsmittelstämme: Der berechnete Wert (d.h. das Ergebnis des Ansatzes).
    /// Im Projektfall steht der Wert kostenebenenabhängig in <see cref="Ansätze"/>.
    /// Wird für Schreibzugriffe ignoriert.
    /// </summary>
    public decimal? Wert { get; set; }

    /// <summary>
    /// Nur für Betriebsmittelstämme: Enthält einen optionalen Kommentar. Im Projektfall steht der
    /// Kommentar kostenebenenabhängig in <see cref="Ansätze"/>.
    /// </summary>
    public string? Kommentar { get; set; }

    /// <summary>
    /// Nur für Projekte: Die (kostenebenenabhängigen) Ansätze
    /// </summary>
    public List<GlobaleVariableAnsatz>? Ansätze { get; set; }
}

/// <summary>
/// Ein kostenebenenabhängiger Variablenansatz einer Projektvariablen (<see cref="GlobaleVariable"/>).
/// </summary>
public class GlobaleVariableAnsatz : BaseObject
{
    /// <summary>
    /// Identifiziert die Kostenebene, auf der dieser Ansatz definiert ist. Dies ist
    /// die ID des Projekts oder eine der Kalkulationen. 
    /// </summary>
    public Guid KostenebeneId { get; set; }

    /// <summary>
    /// Der Ansatz (= Berechnungsformel)
    /// </summary>
    public string? Ansatz { get; set; }

    /// <summary>
    /// Der berechnete Wert (d.h. das Ergebnis des Ansatzes). Wird für Schreibzugriffe
    /// ignoriert.
    /// </summary>
    public decimal? Wert { get; set; }

    /// <summary>
    /// Optionaler Kommentar
    /// </summary>
    public string? Kommentar { get; set; }
}

public class Warengruppe : BaseObject
{
    public int Nummer { get; set; }
    public string? Bezeichnung { get; set; }
}

public class NewKostenkatalogInfo : BaseObject
{
    public string? Nummer { get; set; }
    public string? Bezeichnung { get; set; }
}

public class NewZuschlagskatalogInfo : BaseObject
{
    public string? Nummer { get; set; }
    public string? Bezeichnung { get; set; }
}

public class NewZuschlagsartInfo : BaseObject
{
    public int Index { get; set; }
    public string? Bezeichnung { get; set; }
    public ZuschlagsTyp? Zuschlagstyp { get; set; }
}

public class DbBetriebsmittelGruppe : BaseObject
{
    public string? Bezeichnung { get; set; }

    public BetriebsmittelArt? Art { get; set; }
}

/// <summary>
/// (Detailinfo) Für GAEB: Ein Eintrag im Reiter "Zuschlagsberechnung". Legt die Berechnungsmethode
/// für einen Zuschlagstypen (z.B. AGK) fest.
/// </summary>
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
/// Für ÖNORM: Beschreibt einen Zuschlag, der in einem Betriebsmittelstamm zur Verfügung steht.
/// Wird per <see cref="BetriebsmittelZuschlag.ZuschlagsgruppenNummer"/> referenziert.
/// Die Liste der verfügbaren Zuschlagsgruppen erhält man über <see cref="BetriebsmittelStamm.Zuschlagsgruppen"/>.
/// </summary>
public class Zuschlagsgruppe : BaseObject
{
    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }

    /// <summary>
    /// Wird derzeit nicht genutzt.
    /// </summary>
    public int? Stufe { get; set; }

    /// <summary>
    /// Die eigentlichen Zuschlagssätze für diese Zuschlagsgruppe. Ist eine Liste und nicht einzelner Wert,
    /// da jeder Zuschlagssatz individuell pro Kostenebene festgelegt werden kann.
    /// </summary>
    public List<ZuschlagsgruppenWert>? Werte { get; set; }

    /// <summary>
    /// Liefert den Zuschlagswert, der für eine Zuschlagsebene relevant ist. Übergeben wird eine Liste von
    /// Zuschlagsebenen (üblicherweise <see cref="BetriebsmittelResult.Zuschlagsebenen"/>). Zurückgegeben wird der
    /// Zuschlagswert der ersten darin enthaltenen Zuschagsebenen, auf der ein Zuschlagswert festgelegt ist. 
    /// </summary>
    public ZuschlagsgruppenWert? FindWert(IEnumerable<Kostenebene> zuschlagsebenen)
        => (from e in zuschlagsebenen
            from w in Werte ?? []
            where w.ZuschlagsebeneId == e.Id
            select w).FirstOrDefault();
}

/// <summary>
/// ÖNORM: Der Wert (= Zuschlagssatz) einer <see cref="Zuschlagsgruppe"/> innerhalb eines <see cref="Zuschlagskatalog"/>s.
/// </summary>
public class ZuschlagsgruppenWert : BaseObject
{
    /// <summary>
    /// Die ID der Kostenebene (= Zuschlagskatalog im Fall eines Betriebsmittelstamms),
    /// für die dieser Zuschlagssatz festgelegt ist.
    /// </summary>
    public Guid ZuschlagsebeneId { get; set; }

    /// <summary>
    /// Die Nummer der <see cref="Zuschlagsgruppe"/>, zu der dieser Wert gehört.
    /// Wurde nur für <see cref="Zuschlagskatalog.ZuschlagsgruppenWerte"/> benötigt, das mittelerweile aber
    /// abgekündigt ist. 
    /// </summary>
    [Obsolete("Wurde nur für Zuschlagskatalog.ZuschlagsgruppenWerte benötigt, das aber abgekündigt ist.")]
    public string? ZuschlagsgruppenNummer { get; set; }

    /// <summary>
    /// Der Zuschlagssatz (= Zuschlag in Prozent).
    /// </summary>
    public decimal? Wert { get; set; }

    // TODO Weitere Properties wie ZuschlagGewinn implementieren
}

/// <summary>
/// Eine Zuschlagsart. Beschreibt eine Zuschlagsspalte in den Kosten- und Zuschlagskatalogen.
/// Ein konkreter Zuschlag auf einem Betriebsmittel für eine Zuschlagsart wird per <see cref="BetriebsmittelZuschlag"/>
/// spezifiziert.
/// </summary>
public class Zuschlagsart : BaseObject
{
    /// <summary>
    /// Der Index der Zuschlagart. Identifiziert die Zuschlagsart innerhalb des Betriebsmittelstamms
    /// oder Projekts. Per <see cref="BetriebsmittelZuschlag.ArtIndex"/> wird darauf verweisen.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public int Index { get; set; }

    public string? Bezeichnung { get; set; }

    /// <summary>
    /// Für GAEB: Der Zuschlagstyp (z.B. AGK).
    /// </summary>
    public ZuschlagsTyp? Typ { get; set; }
}

public enum ZuschlagsTyp
{
    Agk,
    Bgk,
    Gewinn,
    Wagnis
}

/// <summary>
/// Ein Kostenkatalog eines <see cref="BetriebsmittelStamm"/>s. Ein Kostenkatalog dient als Kostenebene, d.h.
/// seine ID kann an alle Operationen übergeben werden, die einen "kostenebeneId"-Paramater entgegennehmen.
/// </summary>
public class Kostenkatalog : BaseObject
{
    /// <summary>
    /// Die ID des Kostenkatalogs (= kostenebeneId). Für Kosten, die an diesem Kostenkatalog festgemacht sind,
    /// enthält <see cref="BetriebsmittelKosten.KostenebeneId"/> diese ID.
    /// </summary>
    public Guid Id { get; set; }

    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }

    public string? Beschreibung { get; set; }

    public bool IstStandard { get; set; }

    public Guid? ParentKostenkatalogId { get; set; }

    /// <summary>
    /// Im Fall eines Abgleichs mit dem kaufmännischen System NEVARIS Finance
    /// gibt diese Eigenschaft an, mit welcher Preiskomponente aus Finance dieser Kostenkatalog
    /// abgeglichen wird. In Finance hat nämlich jeder Artikel drei Preise, und diesen entsprechen
    /// Build-seitig drei Kostenkataloge. 
    /// </summary>
    public FinancePreisIndex? FinancePreisIndex { get; set; }
}

/// <summary>
/// Gibt um an, um welchen der drei möglichen Preise eines Finance-Artikel es sich handelt.
/// </summary>
public enum FinancePreisIndex
{
    ArbeitsmittelPreis1,
    ArbeitsmittelPreis2,
    ArbeitsmittelPreis3
}

/// <summary>
/// Ein Zuschlagskatalog eines <see cref="BetriebsmittelStamm"/>s. Ein Zuschlagskatalog dient als Zuschlagsebene, d.h.
/// seine ID kann an alle Operationen übergeben werden, die einen "zuschlagsebeneId"-Paramater entgegennehmen.
/// </summary>
public class Zuschlagskatalog : BaseObject
{
    /// <summary>
    /// Die ID des Zuschlagskatalogs (= zuschlagsebeneId). Für Zuschläge, die an diesem Zuschlagskatalog
    /// festgemacht sind, enthält <see cref="BetriebsmittelZuschlag.ZuschlagsebeneId"/> diese ID.
    /// </summary>
    public Guid Id { get; set; }

    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }

    public string? Beschreibung { get; set; }

    public bool IstStandard { get; set; }

    /// <summary>
    /// Sollte nicht mehr verwendet werden. Stattdessen sind für ÖNORM-Stämme die Zuschlägssätze über
    /// <see cref="Zuschlagsgruppe.Werte"/> ansprechbar.
    /// Diese Liste wird nur noch bei Lesezugriffen befüllt. Bei Schreibzugriffen wird der Inhalt ignoriert,
    /// da sonst die per <see cref="Zuschlagsgruppe.Werte"/> spezifizierten Werte überschrieben werden könnten.
    /// </summary>
    [Obsolete("Sollte nicht mehr verwendet werden. Stattdessen sind für ÖNORM-Stämme die "
              + "Zuschlägssätze über Zuschlagsgruppe.Werte ansprechbar.")]
    public List<ZuschlagsgruppenWert>? ZuschlagsgruppenWerte { get; set; }
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
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public BetriebsmittelArt Art { get; set; }

    /// <summary>
    /// Die lokale Nummer (ohne Präfix und ohne Nummer der enthaltenden Gruppe) des Betriebsmittels.
    /// </summary>
    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }
}

/// <summary>
/// Rückgabeobjekt der Operation POST /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel_collection.
/// </summary>
public class BetriebsmittelCollectionResult : BaseObject
{
    /// <summary>
    /// Die IDs der erzeugten Betriebsmittel (einschließlich Grupper), ohne untergeordnete Betriebsmittel.
    /// </summary>
    public IReadOnlyList<Guid> NewRootBetriebsmittelIds { get; set; } = [];
}

/// <summary>
/// Objekt, das an die Operation POST /build/projekte/{projektId}/betriebsmittel/transfer_collection_from_stamm
/// (CreateBetriebsmittelCollectionFromStamm) übergeben wird. Die Property <see cref="QuellBetriebsmittelIds"/>
/// muss befüllt sein.
/// </summary>
public class BetriebsmittelCollectionTransferFromStammInfo : BaseObject
{
    /// <summary>
    /// Liste mit den IDs der zu übernehmenen Betriebsmittel. Die Betriebsmittel müssen alle zum selben
    /// Betriebsmittelstamm gehören.
    /// </summary>
    public IReadOnlyCollection<Guid>? QuellBetriebsmittelIds { get; set; }

    /// <summary>
    /// Optionale ID des Quell-Kostenkatalogs. Falls null, wird der Standardkostenkatalog herangezogen.
    /// </summary>
    public Guid? QuellKostenkatalogId { get; set; }

    /// <summary>
    /// Optionale ID des Quell-Zuschlagskatlogs. Falls null, wird der Standardzuschlagskatalog herangezogen.
    /// </summary>
    public Guid? QuellZuschlagskatalogId { get; set; }

    /// <summary>
    /// Die Ziel-Kostenebene. Falls null, wird für Build das Projekt und für Success X die Kalkulation verwendet.
    /// </summary>
    public Guid? ZielKostenebeneId { get; set; }

    /// <summary>
    /// Die Ziel-Zuschlagsebene. Falls null, wird für Build das Projekt und für Success X die Kalkulation verwendet.
    /// </summary>
    public Guid? ZielZuschlagsebeneId { get; set; }
}

/// <summary>
/// Das Ergebnisobjekt der Operation POST /build/projekte/{projektId}/betriebsmittel/transfer_collection_from_stamm
/// (CreateBetriebsmittelCollectionFromStamm). Enthält die IDs der erzeugten Betriebsmittel.
/// </summary>
public class BetriebsmittelCollectionTransferResult : BaseObject
{
    [JsonConstructor]
    internal BetriebsmittelCollectionTransferResult(IReadOnlyCollection<BetriebsmittelTransferResult> results)
    {
        Results = results;
    }

    /// <summary>
    /// Liste von Einzelergebnisobjekten. Es gibt für jedes erzeugte Betriebsmittel einen Eintrag.
    /// </summary>
    public IReadOnlyCollection<BetriebsmittelTransferResult> Results { get; }
}

/// <summary>
/// Enthält Informationen über ein Projekt-Betriebsmittel, das aus einem Betriebsmittelstamm übernommen wurde.
/// </summary>
public class BetriebsmittelTransferResult : BaseObject
{
    [JsonConstructor]
    internal BetriebsmittelTransferResult(Guid quellBetriebsmittelId, Guid zielBetriebsmittelId)
    {
        QuellBetriebsmittelId = quellBetriebsmittelId;
        ZielBetriebsmittelId = zielBetriebsmittelId;
    }

    /// <summary>
    /// Die ID des Quellbetriebsmitel (aus dem Betriebsmittelstamm).
    /// </summary>
    public Guid QuellBetriebsmittelId { get; }

    /// <summary>
    /// Die ID des Zielbetriebsmittel (im Projekt).
    /// </summary>
    public Guid ZielBetriebsmittelId { get; }
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
    public List<BetriebsmittelKosten>? Kosten { get; set; }

    /// <summary>
    /// Liste mit weiteren Kosten (Ansatzzeilen) für das Betriebsmittel. Falls ungleich null, werden die
    /// bestehenden Ansatzzeilen aktualsiert. Bestehende Ansatzzeilen auf dem Betriebsmittel, die keine
    /// Entsprechung in dieser Liste haben, werden gelöscht, neu in dieser Liste hinzugekommene
    /// werden neu erzeugt. Eine leere Liste führt zur Löschung aller Ansatzzeilen.
    /// Falls null, passiert dagegen nichts.
    /// </summary>
    public List<KalkulationsZeile>? WeitereKosten { get; set; }
}

/// <summary>
/// Identifiziert eine Kosten- oder Zuschlagsebene.
/// </summary>
public class Kostenebene
{
    /// <summary>
    /// Die ID der Kostenebene (z.B. ein Kostenkatalog), auf der die Kosten für das Betriebsmittel definiert sind.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Der Typ der Kostenebene. Wird nur bei Leseoperationen befüllt und bei Schreiboperationen ignoriert.
    /// </summary>
    public KostenebeneTyp? Typ { get; set; }
}

/// <summary>
/// Das Ergebnisobjekt der Endpunkte /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel_ex
/// und /build/projekte/{projektId}/betriebsmittel_ex.
/// </summary>
public class BetriebsmittelResult
{
    /// <summary>
    /// Liste der angeforderten Betriebsmittel (hierarchisch aufgebaut, falls mitGruppen == true).
    /// </summary>
    public IReadOnlyList<Betriebsmittel> BetriebsmittelList { get; set; } = [];

    /// <summary>
    /// Die Kostenebene, die für Berechnungen herangezogen wurde.
    /// </summary>
    public Guid KostenebeneId { get; set; }

    /// <summary>
    /// Alle relevanten Kostenebenen (von innen nach außen), beginnend mit <see cref="KostenebeneId"/>.
    /// Ermöglicht das Auffinden von Kostenebenen-relevanten Daten unter Berücksichtigung der Vererbungsstruktur,
    /// z.B. per <see cref="Betriebsmittel.FindKosten"/>.
    /// </summary>
    public IReadOnlyList<Kostenebene> Kostenebenen { get; set; } = [];

    /// <summary>
    /// Die Zuschlagsebene, die für Berechnungen herangezogen wurde.
    /// </summary>
    public Guid ZuschlagsebeneId { get; set; }

    /// <summary>
    /// Alle relevanten Zuschlagsebenen (von innen nach außen), beginnend mit <see cref="ZuschlagsebeneId"/>.
    /// Ermöglicht das Auffinden von Zuschlagsebenen-relevanten Daten unter Berücksichtigung der Vererbungsstruktur,
    /// z.B. per <see cref="Betriebsmittel.FindZuschlag"/>.
    /// </summary>
    public IReadOnlyList<Kostenebene> Zuschlagsebenen { get; set; } = [];
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

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public BetriebsmittelArt Art { get; set; }

    /// <summary>
    /// Die lokale Nummer (ohne Präfix und Gruppennummer), z.B. "211" (für das Material "M24.211").
    /// </summary>
    public string? Nummer { get; set; }

    /// <summary>
    /// Vollständige Nummer des Betriebsmittels (einschließlich Präfix), z.B. "M24.211".
    /// </summary>
    public string? NummerKomplett { get; set; }

    public string? Bezeichnung { get; set; }

    public bool? Leistungsfähig { get; set; }

    public string? Einheit { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Kosten (eine pro Kostenebene, auf der die Kosten für dieses Betriebsmittel festgelegt
    /// sind). Ist normalerweise eine Detailinfo, das heißt, dieses Feld ist nur im Fall von Einzelabfragen befüllt.
    /// Allerdings erlaubt der Aufruf /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel
    /// über den "mitKosten"-Parameter das Auslesen mehrerer Betriebsmittel einschließlich Kosten.
    /// </summary>
    public List<BetriebsmittelKosten>? Kosten { get; set; }

    /// <summary>
    /// Ist befüllt, wenn <see cref="Kosten"/> befüllt ist. Enthält den Eintrag aus <see cref="Kosten"/>, der für
    /// die angeforderte Kostenebene das passende Kostenobjekt enthält, wobei die Kostenebenen-Hierarchie
    /// berücksichtigt wird (d.h. wenn für die angeforderte Kostenenbene kein Kostenwert hinterlegt ist, wird
    /// die übergeordnete Kostenebene herangezogen usw.).
    /// </summary>
    public BetriebsmittelKosten? KostenEffektiv { get; set; }

    /// <summary>
    /// Liefert die Kosten, die für eine Kostenebene relevant sind. Übergeben wird eine Liste von Kostenebenen
    /// (üblicherweise <see cref="BetriebsmittelResult.Kostenebenen"/>). Zurückgegeben wird das Kostenobjekt der
    /// ersten darin enthaltenen Kostenebene, auf der Kosten festgelegt sind. 
    /// </summary>
    public BetriebsmittelKosten? FindKosten(IEnumerable<Kostenebene> kostenebenen)
        => (from e in kostenebenen
            from k in Kosten ?? []
            where k.KostenebeneId == e.Id
            select k).FirstOrDefault();

    /// <summary>
    /// (Detailinfo) Enthält berechnete Kosten und Preise. Diese sind abhängig von der gewählten Kosten- und
    /// Zuschlagsebene. Ist normalerweise eine Detailinfo, das heißt, dieses Feld ist nur im Fall von Einzelabfragen
    /// befüllt. Allerdings erlaubt der Aufruf
    /// /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel
    /// über den "mitKosten"-Parameter das Auslesen mehrerer Betriebsmittel einschließlich berechneter Kosten und Preise.
    /// Wird bei Schreiboperationen ignoriert.
    /// </summary>
    public BetriebsmittelKostenDetails? KostenDetails { get; set; }

    /// <summary>
    /// (Detailinfo) Liste mit weiteren Kosten.
    /// </summary>
    public List<KalkulationsZeile>? WeitereKosten { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Zuschlägen (einer pro Zuschlagsebene, auf der die Zuschläge für dieses Betriebsmittel
    /// festgelegt sind).
    /// Ist normalerweise eine Detailinfo, das heißt, dieses Feld ist nur im Fall von Einzelabfragen befüllt.
    /// Allerdings erlaubt der Aufruf /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel
    /// über den "mitZuschlägen"-Parameter das Auslesen mehrerer Betriebsmittel einschließlich Kosten.
    /// </summary>
    public List<BetriebsmittelZuschlag>? Zuschläge { get; set; }
    
    /// <summary>
    /// Ist befüllt, wenn <see cref="Zuschläge"/> befüllt ist. Enthält die für die angeforderte Zuschlagsebene
    /// passenden Zuschlagsobjekte, wobei gegebenenfalls übergeordnete Gruppen und Kostenebenen herangezogen
    /// werden, um das passende Zuschlagsobjekt zu ermitteln.
    /// </summary>
    public List<BetriebsmittelZuschlag>? ZuschlägeEffektiv { get; set; }

    /// <summary>
    /// Liefert den Zuschlag, der für eine Zuschlagsebene relevant ist. Übergeben wird eine Liste von Zuschlagsebenen
    /// (üblicherweise <see cref="BetriebsmittelResult.Zuschlagsebenen"/>). Zurückgegeben wird das Zuschlagsobjekt der
    /// ersten darin enthaltenen Zuschagsebenen, auf der ein Zuschlag festgelegt sind. 
    /// </summary>
    public BetriebsmittelZuschlag? FindZuschlag(int index, IEnumerable<Kostenebene> zuschlagsebenen)
        => (from e in zuschlagsebenen
            from z in Zuschläge ?? []
            where z.ZuschlagsebeneId == e.Id && z.ArtIndex == index
            select z).FirstOrDefault();

    /// <summary>
    /// (Detailinfo) Enthält spezielle Eigenschaften, die nur bei Einzelabfragen geladen werden sowie beim
    /// Aufruf von /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel mit
    /// mitDetails = true. 
    /// </summary>
    public BetriebsmittelDetails? Details { get; set; }

    /// <summary>
    /// (Detailinfo) Falls das Betriebsmittel eine Gruppe ist, enthält dieses Objekt die passenden Eigenschaften.
    /// Beim Abrufen von allen Betriebsmitteln per
    /// /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel ist dieses Feld nur
    /// im Fall von mitDetails = true oder mitGruppen = true befüllt.
    /// </summary>
    public BetriebsmittelGruppeDetails? GruppeDetails { get; set; }

    /// <summary>
    /// (Detailinfo) Falls das Betriebsmittel ein Lohn ist, enthält dieses Objekt die passenden Eigenschaften.
    /// Beim Abrufen von allen Betriebsmitteln per
    /// /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel ist dieses Feld nur
    /// im Fall von mitDetails = true befüllt.
    /// </summary>
    public BetriebsmittelLohnDetails? LohnDetails { get; set; }

    /// <summary>
    /// (Detailinfo) Falls das Betriebsmittel ein Material ist, enthält dieses Objekt die passenden Eigenschaften.
    /// Beim Abrufen von allen Betriebsmitteln per
    /// /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel ist dieses Feld nur
    /// im Fall von mitDetails = true befüllt.
    /// </summary>
    public BetriebsmittelMaterialDetails? MaterialDetails { get; set; }

    /// <summary>
    /// (Detailinfo) Falls das Betriebsmittel eine Gerät ist, enthält dieses Objekt die passenden Eigenschaften.
    /// Beim Abrufen von allen Betriebsmitteln per
    /// /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel ist dieses Feld nur
    /// im Fall von mitDetails = true befüllt.
    /// </summary>
    public BetriebsmittelGerätDetails? GerätDetails { get; set; }

    /// <summary>
    /// (Detailinfo) Falls das Betriebsmittel ein Sonstige-Kosten-Objekt ist, enthält dieses Objekt die passenden
    /// Eigenschaften. Beim Abrufen von allen Betriebsmitteln per
    /// /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel ist dieses Feld nur
    /// im Fall von mitDetails = true befüllt.
    /// </summary>
    public BetriebsmittelSonstigeKostenDetails? SonstigeKostenDetails { get; set; }

    /// <summary>
    /// (Detailinfo) Falls das Betriebsmittel ein Nachunternehmer ist, enthält dieses Objekt die passenden
    /// Eigenschaften. Beim Abrufen von allen Betriebsmitteln per
    /// /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel ist dieses Feld nur
    /// im Fall von mitDetails = true befüllt.
    /// </summary>
    public BetriebsmittelNachunternehmerDetails? NachunternehmerDetails { get; set; }

    /// <summary>
    /// (Detailinfo) Falls das Betriebsmittel ein Baustein ist, enthält dieses Objekt die passenden Eigenschaften.
    /// Beim Abrufen von allen Betriebsmitteln per
    /// /build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel ist dieses Feld nur
    /// im Fall von mitDetails = true befüllt.
    /// </summary>
    public BetriebsmittelBausteinDetails? BausteinDetails { get; set; }

    /// <summary>
    /// Zeigt an ob es sich um ein Freies Betriebmsittel handelt.
    /// </summary>
    public bool IsFreiesBetriebsmittel { get; set; }

    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die diesem Betriebsmittel zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue?>? CustomPropertyValues { get; set; }
}

public class BetriebsmittelDetails : BaseObject
{
    /// <summary>
    /// Die Beschreibung als Text (ohne Formatierungen).
    /// </summary>
    public string? Beschreibung { get; set; }

    public string? Stichwörter { get; set; }

    public string? Markierungskennzeichen { get; set; }

    public string? StandardAnsatz { get; set; }

    public string? DbBetriebsmittelGruppeBezeichnung; // = DBBetriebsmittelgruppe

    /// <summary>
    /// Der Zeitpunkt der letzten Änderung. Wird bei Schreiboperationen ignoriert.
    /// </summary>
    public DateTimeOffset? ModificationDate { get; set; }
}

/// <summary>
/// Ein Zuschlag, der auf einem Betriebsmittel definiert ist.
/// </summary>
public class BetriebsmittelZuschlag : BaseObject
{
    /// <summary>
    /// Für ÖNORM: Verweist auf eine <see cref="Zuschlagsgruppe"/>.
    /// Die Liste der verfügbaren Zuschlagsgruppen erhält man über <see cref="BetriebsmittelStamm.Zuschlagsgruppen"/>.
    /// </summary>
    public string? ZuschlagsgruppenNummer { get; set; }

    /// <summary>
    /// Für GAEB: Der Zuschlag als Prozentwert.<br/>
    /// Für ÖNORM: Wird beim Auslesen aller Betriebsmittel befüllt und enthält den in der referenzierten
    /// Zuschlagsgruppe hinterlegten Prozentsatz. Wird bei Schreiboperationen ignoriert.
    /// </summary>
    public decimal? Wert { get; set; }

    /// <summary>
    /// Verweist auf eine Zuschlagsart (<see cref="Zuschlagsart.Index"/>).
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public int ArtIndex { get; set; }

    /// <summary>
    /// Die ID der Kostenebene, auf der der Zuschlag festgelegt ist. Für Stammdaten kann das ein Zuschlagskatalog
    /// sein, für Projektdaten eine Kalkulation oder das Projekt selbst.
    /// </summary>
    public Guid ZuschlagsebeneId { get; set; }
}

public class BetriebsmittelGruppeDetails : BaseObject
{
    /// <summary>
    /// Die in dieser Gruppe enthaltenen Child-Betriebsmittel. Diese Liste ist nur für Lesezugriffe
    /// sowie für Massenupdates per POST /global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel_collection
    /// nutzbar.
    /// </summary>
    public List<Betriebsmittel>? BetriebsmittelList { get; set; }
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
    public string? BasNummer { get; set; }

    public string? KostenartKostenanteil1 { get; set; }

    public string? KostenartKostenanteil2 { get; set; }

    public string? KostenartGemeinkosten { get; set; }

    public string? KostenartUmlagekosten { get; set; }

    public int? WarengruppeKostenanteil1 { get; set; }

    public int? WarengruppeKostenanteil2 { get; set; }

    public int? WarengruppeGemeinkosten { get; set; }

    public int? WarengruppeUmlagekosten { get; set; }

    public string? AlternativeNummer { get; set; }

    /// <summary>
    /// Das Feld "Externe Preiswartung".
    /// </summary>
    public bool? ExternePreiswartung { get; set; }
}

public class BetriebsmittelMaterialDetails : BaseObject
{
    public decimal? Ladezeit { get; set; }

    /// <summary>
    /// Für ÖNorm-Betriebsmittelstämme der Kalkulationsversion B2061_1999 ist dies die einzige Kostenart.
    /// Für B2061_2020 werden die anderen Kostenarten-Eigenschaften genutzt.
    /// </summary>
    public string? Kostenart { get; set; }

    public string? KostenartPreisAbLieferer { get; set; }

    public string? KostenartTransport { get; set; }

    public string? KostenartGemeinkosten { get; set; }

    public string? KostenartManipulation { get; set; }

    public string? KostenartNebenmaterial { get; set; }

    public int? WarengruppePreisAbLieferer { get; set; }

    public int? WarengruppeTransport { get; set; }

    public int? WarengruppeManipulation { get; set; }

    public int? WarengruppeGemeinkosten { get; set; }

    public int? WarengruppeNebenmaterial { get; set; }

    /// <summary>
    /// Im Fall eines Abgleichs mit dem kaufmännischen System NEVARIS Finance
    /// enthält diese Eigenschaft die Artikel-Nummer von Finance.
    /// </summary>
    public string? FinanceArtikelNummer { get; set; }

    /// <summary>
    /// Enthält zusätzliche Material-Eigenschaften (jene, die im Formular unter "Sonstiges" zusammengefasst sind).
    /// </summary>
    public BetriebsmittelMaterialDetailsSonstiges? Sonstiges { get; set; }
}

public class BetriebsmittelMaterialDetailsSonstiges : BaseObject
{
    public decimal? GewichtJeEinheitInT { get; set; }

    public decimal? TransportvolumenJeEinheitInM3 { get; set; }

    public string? Rabattgruppe { get; set; }

    public string? Lieferant { get; set; }

    public string? AlternativeNummer { get; set; }

    public bool? Markierung { get; set; }

    /// <summary>
    /// Das Feld "Externe Preiswartung".
    /// </summary>
    public bool? ExternePreiswartung { get; set; }
}

public class BetriebsmittelGerätDetails : BaseObject
{
    public BglArt? BasisBgl { get; set; } // = BGLArt

    public string? GerätefaktorNummer { get; set; }

    public string? KostenartAV { get; set; }

    public string? KostenartRepLohn { get; set; }

    public string? KostenartRepMaterial { get; set; }


    public string? KostenartGemeinkostenAV { get; set; }

    public string? KostenartGemeinkostenLohn { get; set; }

    public string? KostenartGemeinkostenSonstiges { get; set; }

    public string? KostenartAndereKostenLohn { get; set; }

    public string? KostenartAndereKostenSonstiges { get; set; }

    public string? KostenartListenpreisgerätLohn { get; set; }

    public string? KostenartListenpreisgerätSonstiges { get; set; }

    public string? KostenartListenpreisgerätGemeinkosten { get; set; }

    public string? KostenartListenpreisGeraetKostenanteil1 { get; set; }

    public string? KostenartListenpreisGeraetKostenanteil2 { get; set; }

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

    public string? AlternativeNummer { get; set; }

    /// <summary>
    /// Das Feld "Externe Preiswartung".
    /// </summary>
    public bool? ExternePreiswartung { get; set; }

    /// <summary>
    /// Enthält zusätzliche Geräte-Eigenschaften (jene, die im Formular unter "Sonstiges" zusammengefasst sind).
    /// </summary>
    public BetriebsmittelGerätDetailsSonstiges? Sonstiges { get; set; }
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
    public string? BglNummer { get; set; } // = BGLNummer
    public string? EdvKurztext { get; set; } // = BGLBezeichnung

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
    public string? Fabrikat { get; set; }
    public int? Mindestradius { get; set; }
    public int? BeherrschbareSteigung { get; set; }
    public string? Schallemissionspegel { get; set; }
    public int? Arbeitsgeschwindigkeit { get; set; }
    public int? GegenseitigeUeberhoehung { get; set; }
}

public class BetriebsmittelSonstigeKostenDetails : BaseObject
{
    public string? Kostenart1 { get; set; }

    public string? Kostenart2 { get; set; }

    public string? Kostenart3 { get; set; }

    public string? Kostenart4 { get; set; }

    public string? Kostenart5 { get; set; }

    public string? Kostenart6 { get; set; }

    public string? Kostenart7 { get; set; }

    public string? Kostenart8 { get; set; }

    public string? KostenartGemeinkosten { get; set; }

    public int? WarengruppeKostenanteil1 { get; set; }

    public int? WarengruppeKostenanteil2 { get; set; }

    public int? WarengruppeKostenanteil3 { get; set; }

    public int? WarengruppeKostenanteil4 { get; set; }

    public int? WarengruppeKostenanteil5 { get; set; }

    public int? WarengruppeKostenanteil6 { get; set; }

    public int? WarengruppeKostenanteil7 { get; set; }

    public int? WarengruppeKostenanteil8 { get; set; }

    public int? WarengruppeGemeinkosten { get; set; }

    public string? AlternativeNummer { get; set; }

    /// <summary>
    /// Das Feld "Externe Preiswartung".
    /// </summary>
    public bool? ExternePreiswartung { get; set; }
}

public class BetriebsmittelNachunternehmerDetails : BaseObject
{
    public string? Kostenart1 { get; set; }

    public string? Kostenart2 { get; set; }

    public string? Kostenart3 { get; set; }

    public string? Kostenart4 { get; set; }

    public string? Kostenart5 { get; set; }

    public string? Kostenart6 { get; set; }

    public string? Kostenart7 { get; set; }

    public string? Kostenart8 { get; set; }

    public string? KostenartGemeinkosten { get; set; }

    public int? WarengruppeKostenanteil1 { get; set; }

    public int? WarengruppeKostenanteil2 { get; set; }

    public int? WarengruppeKostenanteil3 { get; set; }

    public int? WarengruppeKostenanteil4 { get; set; }

    public int? WarengruppeKostenanteil5 { get; set; }

    public int? WarengruppeKostenanteil6 { get; set; }

    public int? WarengruppeKostenanteil7 { get; set; }

    public int? WarengruppeKostenanteil8 { get; set; }

    public int? WarengruppeGemeinkosten { get; set; }

    public string? AlternativeNummer { get; set; }

    /// <summary>
    /// Das Feld "Externe Preiswartung".
    /// </summary>
    public bool? ExternePreiswartung { get; set; }
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
    public Money? Kosten { get; set; }

    /// <summary>
    /// Der Betriebsmittelpreis (ohne Berücksichtigung der weiteren Kosten).
    /// </summary>
    public Money? Preis { get; set; }

    /// <summary>
    /// Die Gesamtbetrag der weiteren Kosten.
    /// </summary>
    public Money? WeitereKosten { get; set; }

    /// <summary>
    /// Die Gesamtkosten (= Kosten + weitere Kosten).
    /// </summary>
    public Money? KostenGesamt { get; set; }

    /// <summary>
    /// Der Gesamtpreis.
    /// </summary>
    public Money? PreisGesamt { get; set; }

    /// <summary>
    /// Der Warenkorb dieses Betriebsmittel, d.h. eine Auflistung aller in den weiteren Kosten vorkommenen
    /// Betriebsmittel (rekursiv aufgelöst) jeweils inklusive der kumulierten Menge und der kumulierten Kosten.
    /// Diese Property wird beim Abrufen eines einzelnen Betriebsmittels per IStammApi/IProjektApi.GetBetriebsmittel
    /// immer befüllt (und ist eine leere Liste im Fall eines Betriebsmittels ohne weitere Kosten).
    /// Beim Abholen aller Betriebsmittel mittels IStammApi/IProjektApi.GetAllBetriebsmittel ist die Property
    /// nur dann befüllt, wenn die Argumente mitKosten = true, mitWeiterenKosten = true übergeben werden.
    /// </summary>
    public List<BetriebsmittelWarenkorbItem>? WarenkorbItems { get; set; }
}

/// <summary>
/// Ein Eintrag im Warenkorb eines Betriebsmittels. Siehe <see cref="BetriebsmittelKostenDetails.WarenkorbItems"/>.
/// </summary>
public class BetriebsmittelWarenkorbItem : BaseObject
{
    /// <summary>
    /// Die ID des verwendeten Betriebsmittels.
    /// </summary>
    public Guid BetriebsmittelId { get; set; }

    /// <summary>
    /// Die Art des Betriebsmittels, auf das <see cref="BetriebsmittelId"/> verweist.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public BetriebsmittelArt BetriebsmittelArt { get; set; }

    /// <summary>
    /// Die Menge, mit der das verwendete Betriebsmittel in den Warenkorb einfließt.
    /// </summary>
    public decimal? Menge { get; set; }

    /// <summary>
    /// Die Kosten, mit der das verwendete Betriebsmittel in den Warenkorb einfließt.
    /// </summary>
    public Money? Kosten { get; set; }
}

/// <summary>
/// Enthält die Kostenebenen-spezifischen Kosten eines Betriebsmittels.
/// </summary>
public class BetriebsmittelKosten : BaseObject
{
    /// <summary>
    /// Die ID der Kostenebene, auf der die Kosten für das Betriebsmittel definiert sind.
    /// Für Projekt-Betriebsmittel ist das die ID des Projekts oder des Leistungsverzeichnisses oder einer Kalkulation.
    /// Für Stamm-Betriebsmittel ist das die ID einer Kostenebene.
    /// </summary>
    public Guid KostenebeneId { get; set; }

    /// <summary>
    /// Der Typ der Kostenebene. Wird nur bei Leseoperationen befüllt und bei Schreiboperationen ignoriert.
    /// </summary>
    public KostenebeneTyp? KostenebeneTyp { get; set; }

    /// <summary>
    /// Befüllt, wenn das Betriebsmittel ein Lohn ist.
    /// </summary>
    public BetriebsmittelKostenLohnDetails? LohnDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn das Betriebsmittel ein Material ist.
    /// </summary>
    public BetriebsmittelKostenMaterialDetails? MaterialDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn das Betriebsmittel ein Gerät ist.
    /// </summary>
    public BetriebsmittelKostenGerätDetails? GerätDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn das Betriebsmittel ein Sonstige-Kosten-Objekt ist.
    /// </summary>
    public BetriebsmittelKostenSonstigeKostenDetails? SonstigeKostenDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn das Betriebsmittel ein Nachunternehmer ist.
    /// </summary>
    public BetriebsmittelKostenNachunternehmerDetails? NachunternehmerDetails { get; set; }
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
    public Money? Kostenanteil1 { get; set; }

    /// <summary>
    /// Kostenanteil 2: Nur für B2061_1999 verfügbar.
    /// </summary>
    public Money? Kostenanteil2 { get; set; }

    public Money? Umlagekosten { get; set; }

    public decimal? Gemeinkosten { get; set; }
}

public class BetriebsmittelKostenMaterialDetails : BaseObject
{
    public Money? Listenpreis { get; set; }

    public decimal? Rabatt { get; set; }

    public decimal? Verlust { get; set; }

    public Money? Manipulation { get; set; }

    public Money? Transportkosten { get; set; }

    public decimal? Gemeinkosten { get; set; }

    public decimal? Nebenmaterial { get; set; }
}

public class BetriebsmittelKostenGerätDetails : BaseObject
{
    public Money? Neuwert { get; set; } // = Mittlerer Neuwert

    public Money? Kostenanteil1 { get; set; }

    public Money? Kostenanteil2 { get; set; }

    public decimal? AbschreibungVerzinsung { get; set; } // = A + V

    public decimal? Reparaturkosten { get; set; }

    public decimal? GemeinkostenListenpreisGeraet { get; set; }

    public decimal? GemeinkostenAV { get; set; }

    public decimal? GemeinkostenRepLohn { get; set; }

    public decimal? GemeinkostenRepSonstiges { get; set; }

    public Money? AndereKostenLohn { get; set; }

    public Money? AndereKostenSonstiges { get; set; }
}

public class BetriebsmittelKostenSonstigeKostenDetails : BaseObject
{
    public Money? Kostenanteil1 { get; set; }

    public Money? Kostenanteil2 { get; set; }

    public Money? Kostenanteil3 { get; set; }

    public Money? Kostenanteil4 { get; set; }

    public Money? Kostenanteil5 { get; set; }

    public Money? Kostenanteil6 { get; set; }

    public decimal? Gemeinkosten { get; set; }
}

public class BetriebsmittelKostenNachunternehmerDetails : BaseObject
{
    public Money? Kostenanteil1 { get; set; }

    public Money? Kostenanteil2 { get; set; }

    public Money? Kostenanteil3 { get; set; }

    public Money? Kostenanteil4 { get; set; }

    public Money? Kostenanteil5 { get; set; }

    public Money? Kostenanteil6 { get; set; }

    public decimal? Gemeinkosten { get; set; }
}

#endregion Betriebsmittel

#region Kalkulation

public class KalkulationsZeileDetails : BaseObject
{
    public decimal? Ergebnis { get; set; }

    public decimal? MengeGesamt { get; set; }

    public decimal? LeistungsMenge { get; set; }

    public Money? KostenProEinheit { get; set; }

    public Money? Kosten { get; set; }

    public Money? Preis { get; set; }

    public decimal? StundenProduktiv { get; set; }

    public Money? ZuschlagGesamt { get; set; }
}

/// <summary>
/// Eine Zeile in den weiteren Kosten oder in einem Kalkulationsblatt der Detailkalkulation.
/// </summary>
public class KalkulationsZeile : BaseObject
{
    /// <summary>
    /// Die ID ist bei GET-Zugriffen immer befüllt. Für PUT-Operationen, d.h. für
    /// PUT /build/projekte/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter/{positionId} und
    /// PUT /build/global/betriebsmittel/{betriebsmittelId}
    /// kann sie fehlen. In diesem Fall wird die Zeile neu angelegt.
    /// </summary>
    public Guid? Id { get; set; }

    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }

    public string? Kommentar { get; set; }

    public string? Markierungskennzeichen { get; set; }

    public string? Einheit { get; set; }

    public bool IstInaktiv { get; set; }

    /// <summary>
    /// Befüllt, wenn die Zeile einen Verweis auf ein Betriebsmittel enthält.
    /// </summary>
    public KalkulationsZeileBetriebsmittelDetails? BetriebsmittelDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn die Zeile einen Variablenansatz enthält.
    /// </summary>
    public KalkulationsZeileVariablenDetails? VariablenDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn es sich um eine Kommentarzeile handelt.
    /// </summary>
    public KalkulationsZeileKommentarDetails? KommentarDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn es sich um eine Unterposition handelt. Diese kann mehrere Unterzeilen enthalten.
    /// </summary>
    public KalkulationsZeileUnterpositionDetails? UnterpositionDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn es sich um einen Rückgriff handelt.
    /// </summary>
    public RückgriffZeileDetails? RückgriffDetails { get; set; }

    /// <summary>
    /// Befüllt, wenn es sich um eine Summenzeile handelt.
    /// </summary>
    public SummenKalkulationsZeileDetails? SummenDetails { get; set; }
    
    /// <summary>
    /// (Detailinfo) Enthält weitere Eigenschaften der Kalkulationszeile, insbesondere berechnete Werte.
    /// </summary>
    public KalkulationsZeileDetails? Details { get; set; }
}

/// <summary>
/// Wenn eine Zeile eines Kalkulationsblatts auf ein Betriebsmittel verweist, enthält dieses Objekt die passenden
/// Informationen (z.B. Betriebsmittel-ID).
/// </summary>
public class KalkulationsZeileBetriebsmittelDetails : BaseObject
{
    /// <summary>
    /// Ist true im Fall eines dynamischen Betriebsmittel-Verweises, d.h. wenn das Betriebsmittel nicht fest
    /// verdrahtet ist, sondern über einen Parameter, der in der Nummer der Zeile enthalten ist
    /// (z.B. "M25_10_00_00{X@PL}500"), aufgelöst wird. Dieses Flag muss auch bei Schreibzugriffen mitgegeben werden:
    /// Ist es true, wird die <see cref="BetriebsmittelId"/> ignoriert.
    /// </summary>
    public bool IstDynamisch { get; set; }

    /// <summary>
    /// Die ID des referenzierten Betriebsmittels. Im Fall eines dynamischen Verweises ist dies die ID des
    /// dynamisch aufgelösten Betriebsmittels (und wird dann bei Schreibzugriffen ignoriert) oder eine leere GUID,
    /// wenn das Betriebsmittel nicht aufgelöst werden konnte.
    /// </summary>
    public Guid BetriebsmittelId { get; set; }

    /// <summary>
    /// Die Betriebsmittelart. Wird bei Schreibzugriffen ignoriert.
    /// </summary>
    public BetriebsmittelArt? BetriebsmittelArt { get; set; }

    /// <summary>
    /// Die vollständige Betriebsmittelnummer. Wird bei Schreibzugriffen ignoriert.
    /// </summary>
    public string? BetriebsmittelNummerKomplett { get; set; }

    public string? Ansatz { get; set; }

    public string? Variable { get; set; }

    public string? BasNummer { get; set; }
}

public class KalkulationsZeileVariablenDetails : BaseObject
{
    public string? Variable { get; set; }
    public string? Ansatz { get; set; }
}

public class KalkulationsZeileKommentarDetails : BaseObject
{
}

public class RückgriffZeileDetails : BaseObject
{
    public Guid PositionId { get; set; }

    public Guid? UnterpositionsZeileId { get; set; }

    public string? Ansatz { get; set; }
    public string? Variable { get; set; }
}

public enum SummenKalkulationsZeileArt
{
    Relativ = 0,
    Absolut = 1,
}

public class SummenKalkulationsZeileDetails : BaseObject
{
    public SummenKalkulationsZeileArt? Art { get; set; }
    public string? Modifikator { get; set; }
    public Money? Kosten { get; set; }
    public Money? Preis { get; set; }
    public decimal? StundenProduktiv { get; set; }
}

public class KalkulationsZeileUnterpositionDetails : BaseObject
{
    public string? Ansatz { get; set; }
    public string? Variable { get; set; }
    public string? BasNummer { get; set; }

    /// <summary>
    /// Zeilen, die in dieser Zeile enthalten sind.
    /// </summary>
    public List<KalkulationsZeile>? Zeilen { get; set; }
}

/// <summary>
/// Optionen-Objekt für die Funktion "Kalkulation generieren".
/// </summary>
public class KalkulationGenerierenOptionen
{
    /// <summary>
    /// Die Projekt-ID (im API-Format) des Projekts, das die Quellkalkulation enthält. Muss befüllt sein.
    /// </summary>
    public string? QuellProjektId { get; set; }

    /// <summary>
    /// Die ID der Quellkalkulation. Muss befüllt sein.
    /// </summary>
    public Guid QuellKalkulationId { get; set; }
    
    /// <summary>
    /// Option "Vorhandene Kalkulationen von Positionen überschreiben".
    /// </summary>
    public bool VorhandeneKalkulationenÜberschreiben { get; set; }

    /// <summary>
    /// Option "Betriebsmittel direkt aus ausgewählter Kalkulationsvorlage verwenden".
    /// </summary>
    public bool BetriebsmittelAusQuellProjektVerwenden { get; set; }
    
    /// <summary>
    /// ÖNORM: Option "HG/OG nicht berücksichtigen".
    /// GAEB: Option "Los nicht berücksichtigen".
    /// </summary>
    public bool StrukturenNichtBerücksichtigen { get; set; }
    
    /// <summary>
    /// Option "Nur Standardpositionen berücksichtigen".
    /// </summary>
    public bool NurStandardPositionenBerücksichtigen { get; set; }
    
    /// <summary>
    /// Option "Positionen mit Vorbemerkungskennzeichen generieren".
    /// </summary>
    public bool PositionenMitVorbemerkungskennzeichenGenerieren { get; set; }
    
    /// <summary>
    /// Option "Teilleistungsnummer statt OZ/Positionsnummer vergleichen".
    /// </summary>
    public bool VergleicheTeilleistungsnummerStattOZNummer { get; set; }

    /// <summary>
    /// Option "Übereinstimmung des LB-Bezugs prüfen".
    /// </summary>
    public bool LBBezugPrüfen { get; set; }

    /// <summary>
    /// IDs der LV-Positionen, für die die Kalkulationsdaten übernommen werden sollen. Falls null, werden alle
    /// Positionen genommen.
    /// </summary>
    public List<Guid>? PositionIds { get; set; }

    /// <summary>
    /// Option "Interne Anmerkung mit Verweis auf Quelle einfügen".
    /// </summary>
    public bool VerweisAufQuelleEinfügen { get; set; }

    /// <summary>
    /// Option "Unterschiedlichen Kurztext ignorieren".
    /// </summary>
    public bool UnterschiedlicheKurztexteIgnorieren { get; set; }

    /// <summary>
    /// Option "Unterschiedliche Einheiten ignorieren".
    /// </summary>
    public bool UnterschiedlicheEinheitenIgnorieren { get; set; }
    
    /// <summary>
    /// Option "Bieterlücken bei Standardpositionen übernehmen".
    /// </summary>
    public bool BieterLückenBeiStandardPositionenÜbernehmen { get; set; }

    /// <summary>
    /// Option "Textvergleich" (für GAEB).
    /// </summary>
    public bool Textvergleich { get; set; }

    /// <summary>
    /// Falls <see cref="Textvergleich"/> == true: Textvergleich-Optionen.
    /// </summary>
    public KalkulationGenerierenTextvergleichOptionen? TextvergleichOptionen { get; set; }
}

/// <summary>
/// Enthält Textvergleich-Optionen für die Funktion "Kalkulation generieren".
/// </summary>
public class KalkulationGenerierenTextvergleichOptionen
{
    public double KurztextSchwellwertInProzent { get;set; }

    public bool KurztexteBerücksichtigen { get; set; }

    public double LangtextSchwellwertInProzent { get;set; }

    public bool LangtexteBerücksichtigen { get; set; }

    public bool LückenMiteinbeziehen { get; set; }

    public TextvergleichAlgorithmus TextvergleichAlgorithmus { get; set; }
}

/// <summary>
/// Ergebnisobjekt für die Funktion "Kalkulation generieren".
/// </summary>
public class KalkulationGenerierenResult
{
    /// <summary>
    /// Liste mit Einzelergebnissen (ein Objekt pro Zielposition).
    /// </summary>
    public IReadOnlyList<KalkulationGenerierenResultItem> Items { get; set; } 
        = Array.Empty<KalkulationGenerierenResultItem>();
}

/// <summary>
/// Detailergebnis für die Funktion "Kalkulation generieren".
/// </summary>
public class KalkulationGenerierenResultItem
{
    /// <summary>
    /// ID der Position im Ziel-LV, für die ein Kalkulationsblatt erzeugt/aktualisiert wurde.
    /// </summary>
    public Guid ZielPositionId { get; set; }

    /// <summary>
    /// Die ID der Position im Quell-LV, dessen Kalkulationsdaten übernommen wurden.
    /// </summary>
    public Guid QuellPositionId { get; set; }
}

public enum TextvergleichAlgorithmus
{
    Levenshtein,
    JaroWinkler,
    CharacterOffset90,
    CharacterOffset95,
    CharacterOffset99
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

    /// <summary>
    /// LV für Success X. Wenn ein Projekt ein LV mit dieser Art besitzt, wird das Projekt als
    /// Success X-Projekt betrachtet.
    /// </summary>
    VereinfachterModus
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
    /// Ohne Gliederung (für GAEB). Wenn bei der Erzeugung eines ÖNORM-LVs dieser Wert angegeben wird
    /// (via <see cref="LvDetails.GliederungsArt"/>), wird ein LV mit LG-Gliederung erzeugt.
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
    public string? Nummer { get; set; }
    public string? Bezeichnung { get; set; }
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
    /// Vollständige Nummer des Betriebsmittels (einschließlich Präfix), z.B. "M24.211".
    /// </summary>
    public string? NummerKomplett { get; set; }

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
    /// Id der Position. Kann zusammen mit der Kalkulations-ID verwendet werden, um das Kalkulationsblatt
    /// abzurufen (über den Endpunkt
    /// /build/projekte/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter/{positionId}). 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Vollständige Nummer der dazugehörigen Position
    /// </summary>
    public string? Positionsnummer { get; set; }

    /// <summary>
    /// Die Ergebnisse der im Kalkulationsblatt verwendeten Betriebsmittel
    /// </summary>
    public IReadOnlyList<BetriebsmittelErgebnis> BetriebsmittelErgebnisse { get; set; } = [];
}

public class KalkulationErgebnisse : BaseObject
{
    /// <summary>
    /// Die kalkulierte Angebotssumme für die das gesamte LV (ohne Nachträge).
    /// </summary>
    public decimal? Angebotssumme { get; set; }

    /// <summary>
    /// Die Ergebnisse der verwendeten Betriebsmittel (über das gesamte LV)
    /// </summary>
    public IReadOnlyList<BetriebsmittelErgebnis> BetriebsmittelErgebnisse { get; set; } = [];

    /// <summary>
    /// Die Ergebnisse der Kalkulationsblätter (pro Position)
    /// </summary>
    public IReadOnlyList<KalkulationsBlattErgebnis> KalkulationsblattErgebnisse { get; set; } = [];
}

/// <summary>
/// Eine zu einem Leistungsverzeichnis gehörende Kalkulation.
/// </summary>
public class Kalkulation : BaseObject
{
    public Guid Id { get; set; }

    public Guid LvId { get; set; }

    /// <summary>
    /// Handelt es sich um die aktive Kalkulation?
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public bool IstAktiv { get; set; }

    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }

    public KalkulationsArt? Art { get; set; }

    /// <summary>
    /// Rechenergebnisse (ist befüllt, wenn die Kalkultion per /build/{projektId}/kalkulationen/{kalkulationId}?mitErgebnissen=true
    /// abgerufen wurde).
    /// </summary>
    public KalkulationErgebnisse? Ergebnisse { get; set; }

    /// <summary>
    /// Die Notiz (als XHTML), die der Kalkulation zugeordnet ist.
    /// </summary>
    public string? Notiz { get; set; }

    /// <summary>
    /// Liste von untergeordneten Kalkulationen. Ist nur befüllt, wenn die Kalkulationen
    /// als Teil eines Leistungsverzeichnisses, d.h. per /build/{projektId}/leistungsverzeichnisse/{lvId} geladen wurden.
    /// </summary>
    public List<Kalkulation>? Kalkulationen { get; set; }

    /// <summary>
    /// Enthält Informationen über die Zahl der Nachkommastellen verschiedener Feldtypen.
    /// </summary>
    [Obsolete("Sollte nicht mehr verwendet werden. Die enthaltenen Properties stehen auf dem Projekt zur Verfügung: "
              + "RechengenauigkeitMengen, RechengenauigkeitBeträge, "
              + "DarstellungsgenauigkeitMengen, DarstellungsgenauigkeitBeträge")]
    public KalkulationNachkommastellen? Nachkommastellen { get; set; }
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

    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }

    /// <summary>
    /// Die Notiz (als XHTML), die diesem Kalkulationsblatt zugeordnet ist.
    /// </summary>
    public string? Notiz { get; set; }

    /// <summary>
    /// Objekt mit weiteren Eigenschaften, insbesondere berechnete Werte (z.B. Einheitspreis). Diese Property
    /// wird auch beim Abrufen sämtlicher Kalkulationsblätter per
    /// /build/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter befüllt.
    /// </summary>
    public KalkulationsBlattDetails? Details { get; set; }

    /// <summary>
    /// (Detailinfo) Liste von Kalkulationszeilen (hierarchisch aufgebaut).
    /// </summary>
    public List<KalkulationsZeile>? Zeilen { get; set; }
}

/// <summary>
/// Detailinformationen (insbesondere berechnete Werte) eines Kalkulationsblattes
/// </summary>
public class KalkulationsBlattDetails : BaseObject
{
    public string? PositionsNummerKomplett { get; set; }

    public decimal? Menge { get; set; }
    public decimal? StundenProduktiv { get; set; }
    public decimal? StundenProduktivGesamt { get; set; }
    public Money? Kosten { get; set; }
    public Money? KostenGesamt { get; set; }
    public Money? Preis { get; set; }
    public Money? PreisGesamt { get; set; }
    public Money? Zuschlag { get; set; }
    public Money? ZuschlagGesamt { get; set; }
    public Money? Einheitspreis { get; set; }
    public Money? EinheitspreisGesamt { get; set; }

    public Dictionary<string, Money>? Preisanteile { get; set; }
}

#endregion Kalkulation

#region Leistungsverzeichnis

/// <summary>
/// Definition eines Preisanteils.
/// </summary>
public class PreisanteilInfo : BaseObject
{
    /// <summary>
    /// Der Code, der den Preisanteil definiert (z.B. "L" für Lohn").
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Optionale Bezeichnung (z.B. "Lohn").
    /// </summary>
    public string? Bezeichnung { get; set; }
}

/// <summary>
/// Ein Gliederungskatalog.
/// </summary>
public class Gliederungskatalog : BaseObject
{
    public Guid Id { get; set; }

    public string? Kennung { get; set; }

    public int? Versionsnummer { get; set; }

    public DateTime? Versionsdatum { get; set; }

    public string? Bezeichnung { get; set; }

    /// <summary>
    /// (Detailinfo) Die Wurzelknoten des Katalogs.
    /// </summary>
    public List<Gliederungsknoten>? Knoten { get; set; }
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
    public string? NummerKomplett { get; set; }

    public string? Bezeichnung { get; set; }

    /// <summary>
    /// Die untergeordneten Knoten.
    /// </summary>
    public List<Gliederungsknoten>? Knoten { get; set; }
}

/// <summary>
/// Objekt, das beim Anleigen eines neuen LV übergeben wird und alle grundlegenden Informationen
/// zum LV enthält (nicht jedoch den eigentlichen Inhalt wie Positionen und Gruppierungselemente).
/// </summary>
public class NewLvInfo : BaseObject
{
    public NewLvInfo()
    {
    }

    /// <summary>
    /// Konstruktor, der die Erzeugung eines LVs auf Basis eines bestehenden LVs ermöglicht.
    /// </summary>
    public NewLvInfo(Leistungsverzeichnis sourceLV)
    {
        Nummer = sourceLV.Nummer;
        Bezeichnung = sourceLV.Bezeichnung;
        Art = sourceLV.Art;
        NormExakt = sourceLV.NormExakt;
        Status = sourceLV.Status;
        LvDetails = sourceLV.LvDetails;
        OenormLvDetails = sourceLV.OenormLvDetails;
        GaebLvDetails = sourceLV.GaebLvDetails;
        BildDetails = sourceLV.BildDetails;
    }

    /// <summary>
    /// LV-Nummer
    /// </summary>
    public string? Nummer { get; set; }

    /// <summary>
    /// LV-Bezeichnung
    /// </summary>
    public string? Bezeichnung { get; set; }

    /// <summary>
    /// Die LV-Art, z.B. Ausschreibung. Muss befüllt sein.
    /// Für Success X-Projekte sollte hier <see cref="LvArt.VereinfachterModus"/>
    /// übergeben werden. (Sobald ein LV mit dieser Art existiert, wird das Projekt als Success X-Projekt aufgefasst.)
    /// </summary>
    public LvArt? Art { get; set; }

    /// <summary>
    /// Die exakte Norm, z.B. A2063:2021.
    /// </summary>
    public NormExakt NormExakt { get; set; }

    /// <summary>
    /// Der LV-Status. Welche Status erlaubt sind, hängt von der LV-Art ab. Der Standardwert ist
    /// <see cref="LvStatus.Erstellt"/>.
    /// </summary>
    public LvStatus? Status { get; set; }

    /// <summary>
    /// (Optional) Objekt mit Detailinformationen zum neuen LV.
    /// </summary>
    public LvDetails? LvDetails { get; set; }

    /// <summary>
    /// Falls es sich um ein ÖNorm-LV handelt: Normspezifische Detailinformationen (optional).
    /// </summary>
    public OenormLvDetails? OenormLvDetails { get; set; }

    /// <summary>
    /// Falls es sich um ein GAEB-LV handelt: Normspezifische Detailinformationen (optional).
    /// </summary>
    public GaebLvDetails? GaebLvDetails { get; set; }

    /// <summary>
    /// Optionales Objekt mit Binärdaten, die dem LV zugeordnet sind.
    /// </summary>
    public LvBildDetails? BildDetails { get; set; }

    /// <summary>
    /// Die gewünschte Mengenart. Ist nur relevant für den Fall, dass per
    /// <see cref="LvDetails.GlobaleHilfsberechungen"/> globale Hilfsberechnungen mitgegeben werden.
    /// </summary>
    public MengenArt MengenArt { get; set; } = MengenArt.Lv;
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
    public string? Nummer { get; set; }

    /// <summary>
    /// Die Bezeichnung des LV.
    /// </summary>
    public string? Bezeichnung { get; set; }

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
    public LvDetails? LvDetails { get; set; }

    /// <summary>
    /// Nur für ÖNorm-LVs: Normspezifische Informationen.
    /// </summary>
    public OenormLvDetails? OenormLvDetails { get; set; }

    /// <summary>
    /// Nur für GAEB-LVs: Normspezifische Informationen.
    /// </summary>
    public GaebLvDetails? GaebLvDetails { get; set; }

    /// <summary>
    /// Enthält Binärdaten für Grafiken.
    /// </summary>
    public LvBildDetails? BildDetails { get; set; }

    /// <summary>
    /// (Detailinfo) Die Knoten der obersten Ebene (z.B. Leistungsgruppen) einschließlich untergeordneter Knoten und Positionen.
    /// </summary>
    public List<LvKnoten>? RootKnotenListe { get; set; }

    /// <summary>
    /// Nur für GAEB: Positionen auf der obersten Ebene (z.B. Hinweistexte).
    /// </summary>
    public List<LvPosition>? RootPositionen { get; set; }

    /// <summary>
    /// (Detailinfo) Die Wurzelkalkulationen einschließlich untergeordneter Kalkulationen.
    /// </summary>
    public List<Kalkulation>? RootKalkulationen { get; set; }

    /// <summary>
    /// Die Rechenergebnisse auf oberster Ebene. 
    /// </summary>
    public LvItemErgebnisse? Ergebnisse { get; set; }
}

/// <summary>
/// Nur für ÖNORM-LVs ab Version A 2063:2021: Definition einer Garantierte-Angebotssumme-Gruppe.
/// Positionen des LVs können eine Zuordnung zu einer solchen Gruppe haben
/// (über <see cref="LvPosition.GarantierteAngebotssummeNummer"/>).
/// Die Gruppen selbst werden über <see cref="LvDetails.GarantierteAngebotssummeGruppen"/> dem LV zugeordnet.
/// </summary>
public class GarantierteAngebotssummeGruppe
{
    /// <summary>
    /// Die Nummer (einstelliger Code) der Gruppe. Muss für das LV eindeutig sein und identifiziert die Gruppe.
    /// </summary>
    public string? Nummer { get; set; }

    /// <summary>
    /// Beschreibende Bezeichnung der Gruppe.
    /// </summary>
    public string? Bezeichnung { get; set; }
}

/// <summary>
/// Enthält Detailinformationen zu einem LV.
/// </summary>
public class LvDetails : BaseObject
{
    public bool IstNummerSchreibgeschützt { get; set; }

    public DateTime? Auftragsdatum { get; set; }

    public string? Auftragsnummer { get; set; }

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

    public GewaehrleistungEinheit? GewährleistungEinheit { get; set; }

    public DateTime? Angebotsfrist { get; set; }

    public DateTime? Preisbasis { get; set; }

    public DateTime? Bearbeitungsstand { get; set; }

    /// <summary>
    /// Die Zahl der Nachkommastellen für LV-Mengen. Für ÖNORM-LVs ist dies immer 2, für GAEB-LVs immer 3.
    /// Dieser Wert kann nur ausgelesen, nicht verändert werden.
    /// </summary>
    public int? NachkommastellenMengen { get; set; }

    /// <summary>
    /// Die Zahl der Nachkommastellen für Preisanteile. Für ÖNORM-LVs ist dieser Wert immer 2 und kann nicht geändert
    /// werden. Für GAEB-LVs sind beim Erstellen eines neuen LVs die Werte 2 und 3 erlaubt. Ein nachträgliches
    /// Ändern ist nicht möglich.
    /// </summary>
    public int? NachkommastellenPreisanteile { get; set; }

    public GliederungsArt GliederungsArt { get; set; } = GliederungsArt.OhneGliederung;

    /// <summary>
    /// Die Standardwährung des LVs (wenn noch nicht im Projekt vorhanden, wird zuerst die Währung aus den Stammdaten
    /// ins Projekt kopiert).
    /// </summary>
    public string? Währung { get; set; }

    /// <summary>
    /// Die Standard-USt des LVs (wenn noch nicht im Projekt vorhanden, wird zuerst die USt aus den Stammdaten
    /// ins Projekt kopiert).  
    /// </summary>
    public string? Umsatzsteuer { get; set; }

    public string? Ausschreibungsart { get; set; }

    public string? Sparte { get; set; }

    [Obsolete("Wird künftig nicht mehr unterstützt." +
              " Bitte stattdessen die gleichnamige Eigenschaft in OenormLvDetails verwenden.")]
    public string? Vorhaben { get; set; }

    [Obsolete("Wird künftig nicht mehr unterstützt." +
              " Bitte stattdessen die gleichnamige Eigenschaft in OenormLvDetails verwenden.")]
    public int? Alternativangebotsnummer { get; set; }

    [Obsolete("Wird künftig nicht mehr unterstützt." +
              " Bitte stattdessen die gleichnamige Eigenschaft in OenormLvDetails verwenden.")]
    public int? Abänderungsangebotsnummer { get; set; }

    /// <summary>
    /// Die IDs der Gliederungskataloge, die dem LV zugeordnet sind.
    /// </summary>
    public List<Guid>? GliederungskatalogIds { get; set; }

    /// <summary>
    /// Die Preisanteil-Arten, die in diesem LV unterstützt werden.
    /// </summary>
    public List<PreisanteilInfo>? PreisanteilInfos { get; set; }

    /// <summary>
    /// Die möglichen Variantenzusammenstellungen (ohne die implizit
    /// definierte Standard-Variantenzusammenstellung).
    /// </summary>
    public List<Variantenzusammenstellung>? Variantenzusammenstellungen { get; set; }

    /// <summary>
    /// Die möglichen Zuordnungskennzeichen.
    /// </summary>
    public List<Zuordnungskennzeichen>? ZuordnungskennzeichenList { get; set; }

    /// <summary>
    /// Die Nummer der aktiven Variantenzusammenstellung (oder null im Fall der
    /// Standard-Variantenzusammenstellung).
    /// </summary>
    public string? AktiveVariantenzusammenstellungNummer { get; set; }

    /// <summary>
    /// Aufschläge/Nachlässe, die auf der LV-Ebene definiert sind.
    /// </summary>
    public NachlassInfo? NachlassInfo { get; set; }

    /// <summary>
    /// (Detailinfo) Nur für ÖNORM-LVs ab Version A 2063:2021: Liste von <see cref="GarantierteAngebotssummeGruppe"/>-Objekten.
    /// </summary>
    public List<GarantierteAngebotssummeGruppe>? GarantierteAngebotssummeGruppen { get; set; }

    /// <summary>
    /// (Detailinfo) Verweist auf die Adressen, die dem LV zugeordnet sind (inklusive Adressrollen).
    /// </summary>
    public List<LvZugeordneteAdresse>? ZugeordneteAdressen { get; set; }

    /// <summary>
    /// Globale Hilfsberechnungen für die Mengenermittlung. Diese Liste wird immer nur mit den zu der angeforderten
    /// Mengenart passenden Hilfsberechnungen befüllt. 
    /// </summary>
    public List<Hilfsberechnung>? GlobaleHilfsberechungen { get; set; }

    /// <summary>
    /// Die Zahlungsbedingung für das LV.
    /// </summary>
    public Zahlungsbedingung? ZahlungsbedingungLV { get; set; }

    /// <summary>
    /// Die Zahlungsbedingung für die Abschlagrechnung.
    /// </summary>
    public Zahlungsbedingung? ZahlungsbedingungAbschlagsrechnung { get; set; }

    /// <summary>
    /// Die Zahlungsbedingung für die Schlussrechnung.
    /// </summary>
    public Zahlungsbedingung? ZahlungsbedingungSchlussrechnung { get; set; }

    /// <summary>
    /// Die formatierten Text (z.B. Notiz), die auf der LV-Wurzelebene festgelegt sind.
    /// </summary>
    public LvItemFormatierteTexte? RootFormatierteTexte { get; set; }

    /// <summary>
    /// Notiz (als formatierter Text), die dem LV zugeordnet ist.
    /// </summary>
    public string? LvNotiz { get; set; }

    /// <summary>
    /// Die Nummer der Kostenstelle, die dem LV zugeordnet ist. Im Falle aktiver Finance-Integration ist das
    /// eine Finance-Kostenstelle, ansonsten eine Control-Kostenstelle.
    /// </summary>
    public string? KostenstelleNummer { get; set; }

    /// <summary>
    /// Die Individualeigenschaften, die diesem Leistungsverzeichnis zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue?>? CustomPropertyValues { get; set; }
}

/// <summary>
/// Eine Zahlungsbedingung für ein Leistungsverzeichnis oder eine Rechnung. Besteht aus einer Fälligkeit
/// und maximal 3 Skonti.
/// </summary>
public class Zahlungsbedingung : BaseObject
{
    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }

    public string? Beschreibung { get; set; }

    public Fälligkeit? Fälligkeit { get; set; }

    public int? Postlaufzeit { get; set; }

    public Skonto? Skonto1 { get; set; }

    public Skonto? Skonto2 { get; set; }

    public Skonto? Skonto3 { get; set; }
}

/// <summary>
/// Eine Fälligkeit, bestehend aus einer Laufzeit und einer Einheit (Tage, Werktage, Wochen, Monate).
/// </summary>
public class Fälligkeit : BaseObject
{
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public FälligkeitEinheit Einheit { get; set; } = FälligkeitEinheit.Tage;

    public int? Laufzeit { get; set; }
}

/// <summary>
/// Ein Skonto, bestehend aus einer Fälligkeit und einem Prozentsatz.
/// </summary>
public class Skonto : BaseObject
{
    public Fälligkeit? Fälligkeit { get; set; }

    public decimal? Prozentsatz { get; set; }
}

/// <summary>
/// Die Einheit einer Fälligkeit (Tage, Werktage, Wochen, Monate).
/// Default ist Tage.
/// </summary>
public enum FälligkeitEinheit
{
    Tage = 0,
    Werktage = 1,
    Wochen = 2,
    Monate = 3
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
/// Enthält Binärdaten zu Grafiken, die dem LV zugeordnet sind.
/// </summary>
public class LvBildDetails : BaseObject
{
    /// <summary>
    /// Die Bild-Daten des LV-Bildes, falls vorhanden (sonst null).
    /// </summary>
    public Bild? Bild { get; set; }
}

/// <summary>
/// Ein Bild im Format png, jpeg, svg oder gif.
/// </summary>
public class Bild : BaseObject
{
    /// <summary>
    /// Name des Bilds (üblicherweise Dateiname ohne Verzeichnispfad)
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Das Bildformat (png, jpeg, svg oder gif)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public BildFormat Format { get; set; }

    /// <summary>
    /// Die Bilddaten
    /// </summary>
    public byte[]? Daten { get; set; }
}

public enum BildFormat
{
    Png,
    Jpeg,
    Gif,
    Svg
}

public class Variantenzusammenstellung : BaseObject
{
    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }

    public string? Beschreibung { get; set; }
}

public class Zuordnungskennzeichen : BaseObject
{
    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }

    /// <summary>
    /// Liste mit Varianten.
    /// </summary>
    public List<Variante>? Varianten { get; set; }

    /// <summary>
    /// Ordnet einer Variantenzusammenstellung (identifiziert über die Nummer der Variantenzusammenstellung
    /// eine Variante zu (identifiziert über die Nummer der Variante).  
    /// </summary>
    public Dictionary<string, int>? VariantenzusammenstellungToVariante { get; set; }
}

public class Variante : BaseObject
{
    /// <summary>
    /// Nummer der Variante (identifiziert die Variante innerhalb ihres Zuordnungskennzeichen).
    /// </summary>
    public int Nummer { get; set; }

    public string? Bezeichnung { get; set; }
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

/// <summary>
/// Enthält normspezifische Detailinformationen (für ÖNORM-LVs).
/// </summary>
public class OenormLvDetails : BaseObject
{
    /// <summary>
    /// Die LV-Art. Diese Eigenschaft ist nur für Success X-Projekte relevant. Für Build-Projekt sollte dieser
    /// Wert nicht geändert werden.
    /// </summary>
    public OenormLvArt? Art { get; set; }

    public bool NachlässeAufEinheitspreis { get; set; }

    public bool NachlässeAufPreisanteilen { get; set; }

    public bool NachlässeAufHauptgruppen { get; set; }

    public bool NachlässeAufObergruppen { get; set; }

    public bool NachlässeAufLeistungsgruppen { get; set; }

    public bool NachlässeAufUnterleistungsgruppen { get; set; }

    public bool NachlässeAufLeistungsverzeichnis { get; set; }

    public string? Vorhaben { get; set; }

    public int? Alternativangebotsnummer { get; set; }

    public int? Abänderungsangebotsnummer { get; set; }
}

/// <summary>
/// Enthält normspezifische Detailinformationen (für GAEB-LVs).
/// </summary>
public class GaebLvDetails : BaseObject
{
    public List<GaebGliederungsebene>? Gliederungsebenen { get; set; }

    public string? Füllzeichen { get; set; }

    public GaebVergabeart? GaebVergabeArt { get; set; }

    /// <summary>
    /// Vergabenummer des Auftraggebers
    /// </summary>
    public string? VergabenummerAG { get; set; }

    /// <summary>
    /// Vergabenummer des Auftragnehmers
    /// </summary>
    public string? VergabenummerAN { get; set; }

    public string? DVNummerAG { get; set; }

    public string? Abgabeort { get; set; }

    public string? Zeiteinheit { get; set; }

    public bool BieterkommentareErlaubt { get; set; }

    public string? LeitwegId { get; set; }
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
    public GaebGliederungsebeneTyp? Typ { get; set; }

    public string? Bezeichnung { get; set; }

    public int? Stellenzahl { get; set; }

    public bool IstNumerisch { get; set; }

    public string? Startwert { get; set; }

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

    protected NewLvItemInfo(LvItemBase sourceItem)
    {
        ItemTyp = sourceItem.ItemTyp;
        Nummer = sourceItem.Nummer;
        Stichwort = sourceItem.Stichwort;
        FormatierteTexte = sourceItem.FormatierteTexte;
        Teilleistungsnummer = sourceItem.Teilleistungsnummer;
        Markierungskennzeichen = sourceItem.Markierungskennzeichen;
        Herkunftskennzeichen = sourceItem.Herkunftskennzeichen;
        IstFixpreis = sourceItem.IstFixpreis;
        IstIntern = sourceItem.IstIntern;
        Schreibgeschützt = sourceItem.Schreibgeschützt;
        LbInfo = sourceItem.LbInfo;
        Variante = sourceItem.Variante;
        Zuordnungskennzeichen = sourceItem.Zuordnungskennzeichen;
        NachlassInfo = sourceItem.NachlassInfo;
        Entfällt = sourceItem.Entfällt;
    }

    /// <summary>
    /// Der Typ des neuen Knoten (z.B. Leistungsgruppe) oder Position (z.B. Leistungsposition). Muss befüllt sein.
    /// </summary>
    public LvItemTyp ItemTyp { get; set; }

    public string? Nummer { get; set; }

    public string? Stichwort { get; set; }

    public LvItemFormatierteTexte? FormatierteTexte { get; set; }

    public string? Teilleistungsnummer { get; set; }

    public string? Markierungskennzeichen { get; set; }

    public Herkunftskennzeichen? Herkunftskennzeichen { get; set; }

    public bool IstFixpreis { get; set; }

    public bool IstIntern { get; set; }

    public bool Schreibgeschützt { get; set; }

    public LvItemLbInfo? LbInfo { get; set; }

    public int? Variante { get; set; }

    public string? Zuordnungskennzeichen { get; set; }

    public NachlassInfo? NachlassInfo { get; set; }

    /// <summary>
    /// Entfällt-Flag. Nur für GAEB.
    /// </summary>
    public bool Entfällt { get; set; }

    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die diesem LV-Item zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue?>? CustomPropertyValues { get; set; }
}

/// <summary>
/// Enthält Informationen zu einem neu anzulegenden LV-Knoten.
/// </summary>
public class NewLvKnotenInfo : NewLvItemInfo
{
    public NewLvKnotenInfo()
    {
    }

    /// <summary>
    /// Konstruktor, der die Erzeugung eines neuen Knotens auf Basis eines bestehenden Knotens ermöglicht.
    /// Wichtig: Die <see cref="ParentKnotenId"/> wird nicht automtisch befüllt, sondern muss vom Anwendungscode
    /// befüllt werden.
    /// </summary>
    public NewLvKnotenInfo(LvKnoten sourceKnoten) : base(sourceKnoten)
    {
    }

    /// <summary>
    /// ID des Parent-Knoten, unter dem der neue Knoten eingehängt werden soll.
    /// Falls null, wird der neue Knoten (z.B. Leistungsgruppe) auf oberster Ebene erzeugt.
    /// </summary>
    public Guid? ParentKnotenId { get; set; }
}

/// <summary>
/// Enthält Informationen zu einer neu zu erzeugenden LV-Position.
/// </summary>
public class NewLvPositionInfo : NewLvItemInfo
{
    public NewLvPositionInfo()
    {
    }

    /// <summary>
    /// Konstruktor, der die Erzeugung einer neuen Position auf Basis einer bestehenden Position ermöglicht.
    /// Wichtig: Die <see cref="ParentKnotenId"/> wird nicht automatisch befüllt, sondern muss vom Anwendungscode
    /// befüllt werden.
    /// </summary>
    public NewLvPositionInfo(LvPosition sourcePosition) : base(sourcePosition)
    {
        Einheit = sourcePosition.Einheit;
        Umsatzsteuer = sourcePosition.Umsatzsteuer;
        Positionsart = sourcePosition.Positionsart;
        LvMenge = sourcePosition.LvMenge;
        Prognosemenge1 = sourcePosition.Prognosemenge1;
        Prognosemenge2 = sourcePosition.Prognosemenge2;
        Prognosemenge3 = sourcePosition.Prognosemenge3;
        Prognosemenge4 = sourcePosition.Prognosemenge4;
        Prognosemenge5 = sourcePosition.Prognosemenge5;
        Prognosemenge6 = sourcePosition.Prognosemenge6;
        Prognosemenge7 = sourcePosition.Prognosemenge7;
        Prognosemenge8 = sourcePosition.Prognosemenge8;
        Prognosemenge9 = sourcePosition.Prognosemenge9;
        Prognosemenge10 = sourcePosition.Prognosemenge10;
        Umlagemenge = sourcePosition.Umlagemenge;
        EinheitSchreibgeschützt = sourcePosition.EinheitSchreibgeschützt;
        EinheitspreisSchreibgeschützt = sourcePosition.EinheitspreisSchreibgeschützt;
        TexteSchreibgeschützt = sourcePosition.TexteSchreibgeschützt;
        LvMengeSchreibgeschützt = sourcePosition.LvMengeSchreibgeschützt;
        Mehrfachverwendung = sourcePosition.Mehrfachverwendung;
        Stichwortluecke = sourcePosition.Stichwortluecke;
        Preisanteile = sourcePosition.Preisanteile;
        GliederungsKnotenList = sourcePosition.GliederungsKnotenList;
        IstWesentlichePosition = sourcePosition.IstWesentlichePosition;
        IstNichtAngeboten = sourcePosition.IstNichtAngeboten;
        IstBeauftragt = sourcePosition.IstBeauftragt;
        IstSchwerpunktposition = sourcePosition.IstSchwerpunktposition;
        IstStundenlohnarbeiten = sourcePosition.IstStundenlohnarbeiten;
        IstFreieBietermenge = sourcePosition.IstFreieBietermenge;
        BedarfspositionArt = sourcePosition.BedarfspositionArt;
        GarantierteAngebotssummeNummer = sourcePosition.GarantierteAngebotssummeNummer;
        HatGarantierteAngebotssumme = sourcePosition.HatGarantierteAngebotssumme;
        Zuschlagsprozentsatz = sourcePosition.Zuschlagsprozentsatz;
        Zuschlagsart = sourcePosition.Zuschlagsart;
        WirdBezuschlagt = sourcePosition.WirdBezuschlagt;
        ZuBezuschlagendePositionen = sourcePosition.ZuBezuschlagendePositionen;
        Unterpositionen = sourcePosition.Unterpositionen;
    }

    /// <summary>
    /// Die ID des Knoten, unter dem die Position erzeugt wird. Für GAEB-LVs ist auch null erlaubt, um
    /// Positonen (z.B. Zusatztexte) auf oberster Ebene zu erzeugen.
    /// </summary>
    public Guid? ParentKnotenId { get; set; }

    public string? Einheit { get; set; }

    public string? Umsatzsteuer { get; set; }

    public LvPositionsart? Positionsart { get; set; }

    public decimal? LvMenge { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "VA_Menge".
    /// </summary>
    /// <remarks>
    /// Das Ändern dieser Property per API entspricht einer manuellen Änderung über die Funktion
    /// "Prognosemengen bearbeiten". Das UI-seitige Hinzufügen von Aufmaßzeilen zu einem Positionsblock
    /// führt ebenfalls zu einer Änderung dieses Werts, aber ein derartiger Automatismus greift nicht
    /// bei einem programmatischen Hinzufügen von Aufmaßzeilen per 
    /// PUT /build/projekte/{projektId}/positionsbloecke/{positionsblockId}. Stattdessen muss die gewünschte
    /// Menge immer durch explizites Befüllen dieser Property gesetzt werden. Dasselbe gilt für alle
    /// anderen Prognosemenge-Properties. 
    /// </remarks>
    public decimal? Prognosemenge1 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_1".
    /// </summary>
    public decimal? Prognosemenge2 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_2".
    /// </summary>
    public decimal? Prognosemenge3 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_3".
    /// </summary>
    public decimal? Prognosemenge4 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_4".
    /// </summary>
    public decimal? Prognosemenge5 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_5".
    /// </summary>
    public decimal? Prognosemenge6 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_6".
    /// </summary>
    public decimal? Prognosemenge7 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_7".
    /// </summary>
    public decimal? Prognosemenge8 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_8".
    /// </summary>
    public decimal? Prognosemenge9 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_9".
    /// </summary>
    public decimal? Prognosemenge10 { get; set; }
    
    public decimal? Umlagemenge { get; set; }
    
    public bool EinheitSchreibgeschützt { get; set; }

    public bool EinheitspreisSchreibgeschützt { get; set; }

    public bool TexteSchreibgeschützt { get; set; }

    public bool LvMengeSchreibgeschützt { get; set; }

    public string? Mehrfachverwendung { get; set; }

    public string? Stichwortluecke { get; set; }

    /// <summary>
    /// Die auf der Position festgelegten Preisanteilbeträge.
    /// </summary>
    public Dictionary<string, Money>? Preisanteile { get; set; }

    /// <summary>
    /// Liste mit Gliederungsknoten, die der Position zugeordnet sind.
    /// </summary>
    public List<LvPositionGliederungsKnoten>? GliederungsKnotenList { get; set; }

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
    /// Nur für ÖNORM-LVs ab Version A 2063:2021: Nummer der <see cref="GarantierteAngebotssummeGruppe"/>,
    /// der diese Position zugeordnet ist (optional).
    /// </summary>
    public string? GarantierteAngebotssummeNummer { get; set; }

    /// <summary>
    /// Nur für ÖNORM-LVs bis Version A 2063:2015: Die Markierung "Garantierte Angebotssumme". 
    /// </summary>
    public bool HatGarantierteAngebotssumme { get; set; }

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
    public List<ZuBezuschlagendePosition>? ZuBezuschlagendePositionen { get; set; }

    /// <summary>
    /// Nur für GAEB: Liste von Positionen, die dieser Position untergeordnet sind (Unterbeschreibungen unter
    /// einer Leistungsposition oder Ausführungsbeschreibungstexte unter einer Ausführungsbeschreibung).
    /// </summary>
    public List<LvPosition>? Unterpositionen { get; set; }
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
    public string? Nummer { get; set; }

    /// <summary>
    /// Die vollständige Nummer (z.B. "01.04.03").
    /// </summary>
    public string? NummerKomplett { get; set; }

    /// <summary>
    /// Das Stichwort. Für GAEB kann dieser String nur ausgelesen werden.
    /// Zum Manipulieren gibt es FormatierteTexte.Kurztext.
    /// </summary>
    public string? Stichwort { get; set; }

    /// <summary>
    /// (Detailinfo) Objekt mit allen formatierten Texten (z.B. Langtext).
    /// </summary>
    public LvItemFormatierteTexte? FormatierteTexte { get; set; }

    public string? Teilleistungsnummer { get; set; }

    public string? Markierungskennzeichen { get; set; }

    /// <summary>
    /// Nur für ÖNorm: Das Herkunftskennzeichen (gibt an, ob es einen LB-Bezug gibt).
    /// </summary>
    public Herkunftskennzeichen Herkunftskennzeichen { get; set; } = Herkunftskennzeichen.LB;

    public LvItemLbInfo? LbInfo { get; set; }

    /// <summary>
    /// Enthält berechnete Werte. Ist nur befüllt, wenn das gesamte LV abgerufen wird
    /// (per /build/projekte/{projektId}/leistungsverzeichnisse/{lvId}).
    /// </summary>
    public LvItemErgebnisse? Ergebnisse { get; set; }

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
    public string? Zuordnungskennzeichen { get; set; }

    /// <summary>
    /// Aufschläge/Nachlässe, falls vorhanden.
    /// </summary>
    public NachlassInfo? NachlassInfo { get; set; }

    /// <summary>
    /// Entfällt-Flag. Nur für GAEB.
    /// </summary>
    public bool Entfällt { get; set; }

    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die diesem LV-Item zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue?>? CustomPropertyValues { get; set; }
}

public class NachlassInfo : BaseObject
{
    /// <summary>
    /// Aufschläge/Nachlässe pro Preisanteil (idertifiziert über den Preisanteil-Code, z.B. "L" für Lohn).
    /// </summary>
    public Dictionary<string, Nachlass>? PreisanteilNachlässe { get; set; }

    /// <summary>
    /// Falls Aufschläge/Nachlässe auf dem Einheitspreis unterstützt werden, kommt diese
    /// Eigenschaft zum Einsatz.
    /// </summary>
    public Nachlass? EinheitspreisNachlass { get; set; }
}

public class Nachlass : BaseObject
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
    /// Der Langtext (XHTML).
    /// </summary>
    public string? Langtext { get; set; }

    /// <summary>
    /// Die Baubeschreibung (XHTML).
    /// </summary>
    public string? Baubeschreibung { get; set; }

    /// <summary>
    /// Die Notiz (XHTML).
    /// </summary>
    public string? Notiz { get; set; }

    /// <summary>
    /// Nur für GAEB-LVs: Der Kurztext (XHTML).
    /// Für ÖNorm-LVs wird diese Property ignoriert, da dort für Kurztexte
    /// keine Formatierungen unterstützt werden. 
    /// </summary>
    public string? Kurztext { get; set; }
}

public class LvItemLbInfo : BaseObject
{
    public string? LbKennung { get; set; }
    public int? LbVersion { get; set; }
    public DateTime? LbVersionsDatum { get; set; }

    public string? ErgLbKennung { get; set; }
    public int? ErgLbVersion { get; set; }
    public DateTime? ErgLbVersionsDatum { get; set; }

    public string? LbLgNummer { get; set; }
    public string? LbUlgNummer { get; set; }
    public string? LbGtNummer { get; set; }
    public string? LbPosNummer { get; set; }

    public OnÄnderungsumfang? AenderungsUmfang { get; set; }
}

/// <summary>
/// Ein LV-Knoten (z.B. Titel oder Leistungsgruppe).
/// </summary>
public class LvKnoten : LvItemBase
{
    public List<LvKnoten>? Knoten { get; set; }

    public List<LvPosition>? Positionen { get; set; }
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
    public string? Einheit { get; set; }

    public string? Umsatzsteuer { get; set; }

    public LvPositionsart Positionsart { get; set; } = LvPositionsart.Normalposition;

    public decimal? LvMenge { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "VA_Menge".
    /// </summary>
    public decimal? Prognosemenge1 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_1".
    /// </summary>
    public decimal? Prognosemenge2 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_2".
    /// </summary>
    public decimal? Prognosemenge3 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_3".
    /// </summary>
    public decimal? Prognosemenge4 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_4".
    /// </summary>
    public decimal? Prognosemenge5 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_5".
    /// </summary>
    public decimal? Prognosemenge6 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_6".
    /// </summary>
    public decimal? Prognosemenge7 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_7".
    /// </summary>
    public decimal? Prognosemenge8 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_8".
    /// </summary>
    public decimal? Prognosemenge9 { get; set; }

    /// <summary>
    /// Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_9".
    /// </summary>
    public decimal? Prognosemenge10 { get; set; }
    
    public decimal? Umlagemenge { get; set; }

    public bool EinheitSchreibgeschützt { get; set; }

    public bool EinheitspreisSchreibgeschützt { get; set; }

    public bool TexteSchreibgeschützt { get; set; }

    public bool LvMengeSchreibgeschützt { get; set; }

    public string? Mehrfachverwendung { get; set; }

    public string? Stichwortluecke { get; set; }

    /// <summary>
    /// Die auf der Position festgelegten Preisanteilbeträge.
    /// </summary>
    public Dictionary<string, Money>? Preisanteile { get; set; }

    /// <summary>
    /// Liste mit Gliederungsknoten, die der Position zugeordnet sind.
    /// </summary>
    public List<LvPositionGliederungsKnoten>? GliederungsKnotenList { get; set; }

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
    /// Nur für ÖNORM-LVs ab Version A 2063:2021: Nummer der <see cref="GarantierteAngebotssummeGruppe"/>,
    /// der diese Position zugeordnet ist (optional).
    /// </summary>
    public string? GarantierteAngebotssummeNummer { get; set; }

    /// <summary>
    /// Nur für ÖNORM-LVs bis Version A 2063:2015: Die Markierung "Garantierte Angebotssumme". 
    /// </summary>
    public bool HatGarantierteAngebotssumme { get; set; }

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
    public List<ZuBezuschlagendePosition>? ZuBezuschlagendePositionen { get; set; }

    /// <summary>
    /// Nur für GAEB: Liste von Positionen, die dieser Position untergeordnet sind (Unterbeschreibungen unter
    /// einer Leistungsposition oder Ausführungsbeschreibungstexte unter einer Ausführungsbeschreibung).
    /// </summary>
    public List<LvPosition>? Unterpositionen { get; set; }
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

/// <summary>
/// Identifiziert einen Gliederungsknoten eines Gliederungskatalogs.
/// </summary>
public class LvPositionGliederungsKnoten : BaseObject
{
    /// <summary>
    /// Die ID des Gliederungskatalogs.
    /// </summary>
    public Guid KatalogId { get; set; }

    /// <summary>
    /// Die ID des Gliederungsknotens.
    /// </summary>
    public Guid KnotenId { get; set; }
}

public class LvItemErgebnisse : BaseObject
{
    public Money? Einheitspreis { get; set; }

    /// <summary>
    /// Gesamtbetrag (netto) inkl. Aufschläge/Nachlässe.
    /// </summary>
    public Money? Betrag { get; set; }

    /// <summary>
    /// Gesamtbetrag (brutto) inkl. Aufschläge/Nachlässe.
    /// </summary>
    public Money? BetragBrutto { get; set; }

    /// <summary>
    /// Gesamtbetrag (netto) ohne Aufschläge/Nachlässe.
    /// </summary>
    public Money? BetragInklAN { get; set; }

    /// <summary>
    /// Gesamtbetrag (brutto) ohne Aufschläge/Nachlässe.
    /// </summary>
    public Money? BetragBruttoInklAN { get; set; }

    /// <summary>
    /// Die für die Berechnung verwendete Menge.
    /// </summary>
    public decimal? Menge { get; set; }
}

/// <summary>
/// Enthält ein importieres Leistungsverzeichnis mit den vom 
/// Importer erzeugten Meldungen.
/// </summary>
public class LeistungsverzeichnisMitImportMeldungen : BaseObject
{
    public Leistungsverzeichnis? ImportieresLeistungsverzeichnis { get; set; }

    public List<ResultInfo>? ImporterMeldungen { get; set; }
}

/// <summary>
/// Objekt zur Auswertung von diversen Meldung, die z.B. bei Importvorgängen entstehen. 
/// </summary>
public class ResultInfo : BaseObject
{
    /// <summary>
    /// Meldungstext (z.B. Fehlermeldung).
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Schweregrad der Meldung.
    /// </summary>
    public MeldungSeverity Severity { get; set; }

    /// <summary>
    /// Wert der betroffenen Property, sofern relevant.
    /// </summary>
    public object? Wert { get; set; }

    /// <summary>
    /// Zusätzliche meldungsspezifische Informationen.
    /// </summary>
    public object? Tag { get; set; }
}

/// <summary>
/// Gibt den Schweregrad einer Meldung an.
/// </summary>
public enum MeldungSeverity
{
    Error = 0,
    Warning = 100,
    Information = 200
}

/// <summary>
/// Objekt enthält Informationen darüber wo sich die Quelle des zu importierenden 
/// LVs befindet.
/// </summary>
public class ImportLvVonServerpfad : BaseObject
{
    /// <summary>
    /// Pfad der zu importierenden Datei (muss vom Business Host erreichbar sein).
    /// </summary>
    public string? Dateipfad { get; set; }

    /// <summary>
    /// Die gewünschte LV-Art, z.B. Ausschreibung (= Default). Für Success X ist nur
    /// VereinfachterModus möglich. Sobald ein LV der Art
    /// VereinfachterModus angelegt wird, wird das Projekt als Success X-Projekt
    /// aufgefasst.
    /// </summary>
    public LvArt? LvArt { get; set; }
}

/// <summary>
/// Objekt, das informationen zu der zu importierenden Datei (LV-Datenträger) enthält.
/// </summary>
public class ImportLvAusByteArrayInfo : BaseObject
{
    /// <summary>
    /// Der Inhalt der zu importierenden Datei.
    /// </summary>
    public byte[]? Inhalt { get; set; }

    /// <summary>
    /// Der Name der zu importierenden Datei. Wird benötigt, um anhand der Endung die Norm zu bestimmen.
    /// </summary>
    public string? Dateiname { get; set; }

    /// <summary>
    /// Die gewünschte LV-Art, z.B. Ausschreibung (= Default). Für Success X ist nur
    /// VereinfachterModus möglich. Sobald ein LV der Art
    /// VereinfachterModus angelegt wird, wird das Projekt als Success X-Projekt
    /// aufgefasst.
    /// </summary>
    public LvArt? LvArt { get; set; }
}

#endregion Leistungsverzeichnis

#region Mengenermittlung

public class Aufmaßblatt : BaseObject
{
    public Guid Id { get; set; }

    /// <summary>
    /// Die Mengenart, für die dieses Aufmaßblatt relevant ist. Wird nur Leseoperationen befüllt und enthält
    /// immer die Mengenart, die bei der relevanten GET-Operation mitgegeben wird. Wird bei Schreiboperationen
    /// ignoriert.
    /// </summary>
    public MengenArt? MengenArt { get; set; }

    public string? Nummer { get; set; }

    public string? Bezeichnung { get; set; }

    /// <summary>
    /// (Detailinfo) Die in diesem Aufmaßblatt enthaltenen Hilfsberechnungen.
    /// </summary>
    public List<Hilfsberechnung>? Hilfsberechnungen { get; set; }
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
    public string? Nummer { get; set; }
    public string? Bezeichnung { get; set; }
}

public class Rechnung : BaseObject
{
    public Guid Id { get; set; }
    public string? Nummer { get; set; }
    public string? Bezeichnung { get; set; }
    public DateTime? Rechnungsdatum { get; set; }
    public string? ExterneRechnungsnummer { get; set; }
    public DateTime? Eingangsdatum { get; set; }
    public DateTime? GesendetAm { get; set; }
    public RechnungsStatus? Status { get; set; }
    public List<Zahlung>? Zahlungen { get; set; }
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
    /// Die Zahlungsbedingung dieser Rechnung.
    /// </summary>
    public Zahlungsbedingung? Zahlungsbedingung { get; set; }

    /// <summary>
    /// Enthält berechnete Werte dieser Rechnung (ist auch beim Abrufen aller Rechnungen befüllt).
    /// </summary>
    public RechnungErgebnisse? Ergebnisse { get; set; }
    
    /// <summary>
    /// (Detailinfo) Ist nur befüllt, wenn die kaufmännische Integration (Finance-Anbindung) aktiv ist, nicht aber
    /// das Rechnungsausgangsbuch. Entspricht dem Inhalt des Registers "Kontierung" in der Rechnungsverwaltung.
    /// </summary>
    public RechnungKontierung? Kontierung { get; set; }

    /// <summary>
    /// (Detailinfo) Die Individualeigenschaften, die dieser Rechnung zugeordnet sind.
    /// </summary>
    public Dictionary<string, CustomPropertyValue?>? CustomPropertyValues { get; set; }
}

/// <summary>
/// Rechnungsinformationen, die die kaufmännische Integration betreffen. Wird nur verwendet, wenn die kaufmännische
/// Integration (Finance-Anbindung) aktiv ist, nicht aber das Rechnungsausgangsbuch.
/// Entspricht dem Inhalt des Registers "Kontierung" in der Rechnungsverwaltung.
/// </summary>
public class RechnungKontierung
{
    /// <summary>
    /// Projekt-/Auftrags-Kostenstelle (in Finance)
    /// </summary>
    public string? ProjektAuftragsKostenstelleNummer { get; set; }

    /// <summary>
    /// Rechnungskostenstelle (in Finance)
    /// </summary>
    public string? RechnungsKostenstelleNummer { get; set; }

    /// <summary>
    /// Debitor (in Finance)
    /// </summary>
    public string? DebitorNummer { get; set; }

    /// <summary>
    /// Anzahlungsvorgang (in Finance)
    /// </summary>
    public string? AnzahlungsvorgangNummer { get; set; }

    /// <summary>
    /// Erlöskonto (in Finance)
    /// </summary>
    public string? ErlöskontoNummer { get; set; }

    public DateTime? Buchungsdatum { get; set; }
}

/// <summary>
/// Enthält berechnete Werte einer Rechnung.
/// </summary>
public class RechnungErgebnisse
{
    public decimal? RechnungsbetragNetto { get; set; }

    public decimal? NettobetragInklAN { get; set; }
    
    public decimal? RechnungsbetragKumuliertNetto { get; set; }
    
    public decimal? NettobetragInklANKumuliert { get; set; }
    
    public decimal? RechnungsbetragBrutto { get; set; }
    
    public decimal? RechnungsbetragKumuliertBrutto { get; set; }
    
    public decimal? FreigabeNetto { get; set; }
    
    public decimal? NettobetragInklANKorrigiert { get; set; }
    
    public decimal? FreigabeKumuliertNetto { get; set; }
    
    public decimal? NettobetragInklANKumuliertKorrigiert { get; set; }
    
    public decimal? FreigabeBrutto { get; set; }
    
    public decimal? FreigabeKumuliertBrutto { get; set; }
    
    public decimal? Zahlungen { get; set; }
    
    public decimal? Skonto { get; set; }
    
    public decimal? Ausbuchung { get; set; }

    public decimal? Offen { get; set; }

    public decimal? ZahlungenKumuliert { get; set; }
    
    public decimal? SkontoKumuliert { get; set; }
    
    public decimal? AusbuchungKumuliert { get; set; }

    public decimal? OffenKumuliert { get; set; }
}

public class Zahlung : BaseObject
{
    public Guid Id { get; set; }
    public DateTime? Zahlungsdatum { get; set; }
    public decimal? Zahlbetrag { get; set; }
    public decimal? Skontobetrag { get; set; }
    public string? Bemerkung { get; set; }
}

public class AbrechnungsMerkmal : BaseObject
{
    public Guid Id { get; set; }
    public string? Nummer { get; set; }
    public string? Bezeichnung { get; set; }
    public string? BezeichnungKomplett { get; set; }
    public bool IsAuswertungsKennzeichen { get; set; }
    public List<AbrechnungsMerkmal>? Merkmale { get; set; }
}

public class Leistungszeitraum : BaseObject
{
    public Guid Id { get; set; }
    public string? Nummer { get; set; }
    public string? Bezeichnung { get; set; }
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
    public string? Nummer { get; set; }

    public string? NummerKorrigiert { get; set; }
    public string? Kurztext { get; set; }
    public string? Einheit { get; set; }

    public Guid? AufmaßblattId { get; set; }
    public Guid? LeistungszeitraumId { get; set; }
    public Guid? RechnungId { get; set; }

    public decimal? Menge { get; set; }
    public decimal? MengeKorrigiert { get; set; }

    public List<Aufmaßzeile>? Aufmaßzeilen { get; set; }

    public List<Guid>? MerkmalIds { get; set; }

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
    
    /// <summary>
    /// Prognose1: Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "VA_Menge"
    /// </summary>
    Prognose1 = 10,

    /// <summary>
    /// Prognose2: Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_1"
    /// </summary>
    Prognose2 = 11,

    /// <summary>
    /// Prognose2: Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_2"
    /// </summary>
    Prognose3 = 12,

    /// <summary>
    /// Prognose2: Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_3"
    /// </summary>
    Prognose4 = 13,

    /// <summary>
    /// Prognose2: Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_4"
    /// </summary>
    Prognose5 = 14,

    /// <summary>
    /// Prognose2: Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_5"
    /// </summary>
    Prognose6 = 15,

    /// <summary>
    /// Prognose2: Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_6"
    /// </summary>
    Prognose7 = 16,

    /// <summary>
    /// Prognose2: Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_7"
    /// </summary>
    Prognose8 = 17,

    /// <summary>
    /// Prognose2: Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_8"
    /// </summary>
    Prognose9 = 18,

    /// <summary>
    /// Prognose2: Entspricht standardmäßig der Prognosemenge mit der Bezeichnung "Prognosemenge_9"
    /// </summary>
    Prognose10 = 19
}

public class Aufmaßzeile : BaseObject
{
    public Guid Id { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public AufmaßzeilenArt Art { get; set; }

    public AufmaßzeilenArt? ArtKorrigiert { get; set; }
    public string? InternerKommentar { get; set; }
    public string? Inhalt { get; set; }
    public string? InhaltKorrigiert { get; set; }
    public string? Variable { get; set; }
    public string? Erläuterung { get; set; }
    public string? AdresseVon { get; set; }
    public string? AdresseBis { get; set; }
    public decimal? Faktor { get; set; }
    public decimal? FaktorKorrigiert { get; set; }
    public Formel? Formel { get; set; }
    public Formel? FormelKorrigiert { get; set; }
    public bool? ErzeugtInKorrektur { get; set; }
    
    /// <summary>
    /// Die berechnete Menge (wird bei Schreibzugriffen ignoriert)
    /// </summary>
    public decimal? Menge { get; set; }

    /// <summary>
    /// Die berechnete Korrekturmenge (wird bei Schreibzugriffen ignoriert)
    /// </summary>
    public decimal? MengeKorrigiert { get; set; }

    public bool Geprüft { get; set; }

    public bool Schreibgeschützt { get; set; }
}

public enum AufmaßzeilenArt
{
    Ansatz = 0,
    Formel,
    Kommentar,
    Anhang
}

/// <summary>
/// Eine Hilfsberechung (aufmaßblattgebunden oder global) in der Mengenermittlung.
/// </summary>
public class Hilfsberechnung : BaseObject
{
    public Guid Id { get; set; }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
    public HilfszeilenArt Art { get; set; }

    public HilfszeilenArt? ArtKorrigiert { get; set; }

    public string? Inhalt { get; set; }
    public string? InhaltKorrigiert { get; set; }
    public string? Variable { get; set; }
    public Formel? Formel { get; set; }
    public Formel? FormelKorrigiert { get; set; }
    public bool? ErzeugtInKorrektur { get; set; }

    public bool Geprüft { get; set; }

    public bool Schreibgeschützt { get; set; }
}

public enum HilfszeilenArt
{
    Ansatz = 0,
    Formel,
    Kommentar
}

public class Formel : BaseObject
{
    public int Id { get; set; }
    public List<FormelParameter>? Params { get; set; }
}

public class FormelParameter : BaseObject
{
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public string? Variable { get; set; }
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
    public string? StringValue { get; set; }

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
    public GeoCoordinate? GeoCoordinateValue { get; set; }

    /// <summary>
    /// Liefert den Wert der Individualeigenschaft als object-Instanz.
    /// </summary>
    /// <returns>Wert (kann null sein)</returns>
    public object? GetValueAsObject()
        => ValueType switch
        {
            CustomPropertyType.Boolean => BooleanValue,
            CustomPropertyType.Date => DateTimeValue,
            CustomPropertyType.Decimal => DecimalValue,
            CustomPropertyType.Duration => TimeSpanValue,
            CustomPropertyType.Integer => IntegerValue,
            CustomPropertyType.Memo => StringValue,
            CustomPropertyType.Text => StringValue,
            CustomPropertyType.Time => TimeSpanValue,
            CustomPropertyType.Url => StringValue,
            CustomPropertyType.DirectoryPath => StringValue,
            CustomPropertyType.FilePath => StringValue,
            CustomPropertyType.GeoCoordinate => GeoCoordinateValue,
            CustomPropertyType.PhoneNumber => StringValue,
            CustomPropertyType.DateAndTime => DateTimeValue,
            CustomPropertyType.EMailAddress => StringValue,
            CustomPropertyType.DateAndTimeWithTimeZone => DateTimeWithTimeZoneValue,
            _ => null
        };
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
public class SimpleMoney(string? currency, decimal? value) : BaseObject
{
    [JsonProperty("cur")]
    public string? Currency { get; private set; } = currency;

    [JsonProperty("val")]
    public decimal? Value { get; private set; } = value;

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

    public static Money? FromValues(IEnumerable<(string Currency, decimal? Value)>? values)
    {
        if (values == null)
        {
            return null;
        }

        Money? result = null;

        foreach (var betrag in values)
        {
            result ??= [];
            result.Add(betrag.Currency, betrag.Value);
        }

        return result;
    }

    public void Add(string currency, decimal? value)
    {
        Add(new SimpleMoney(currency, value));
    }

    /// <summary>
    /// Liefert den an erster Stelle eingefügten Geldbetrag. Normalerweise gibt es genau einen Geldbetrag, außer
    /// im Mehrwährungsfall. Wenn es keinen Geldbetrag gibt, wird null zurückgegeben.
    /// </summary>
    [JsonIgnore]
    public decimal? FirstValue
    {
        get => this.FirstOrDefault()?.Value;
        set
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Cannot invoke 'FirstValue' setter on an empty Money object");
            }

            this[0] = new SimpleMoney(this[0].Currency, value);
        }
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
public class Rechenwert : BaseObject
{
    /// <summary>
    /// Identifiziert den Wertetyp (z.B. "Lv_Listenpreis_Brutto").
    /// </summary>
    public string? Id { get; set; }

    public decimal? Wert { get; set; }

    public decimal? Basiswert { get; set; }

    public string? Währung { get; set; }

    public string? PreisanteilCode { get; set; }

    public string? UmsatzsteuerCode { get; set; }

    public int? ZuschlagIndex { get; set; }

    public ZuschlagsTyp? ZuschlagsTyp { get; set; }

    public decimal? PreisanteilUmrechnungssatz { get; set; }

    public Guid? PreisperiodeId { get; set; }

    public int? PreisperiodeNummer { get; set; }

    public Guid? PositionId { get; set; }

    public string? Kostenart { get; set; }

    public Guid? WarengruppeId { get; set; }

    public DBKostenart? DBKostenart { get; set; }
}

/// <summary>
/// Enthält alle Rechenwerte des Projekts einschließlich der Leistungsverzeichnisse.
/// </summary>
public class ProjektRechenwerte : BaseObject
{
    /// <summary>
    /// Die Rechenwerte, gegliedert nach LV-Art.
    /// </summary>
    public Dictionary<LvArt, List<Rechenwert>>? Werte { get; set; }

    /// <summary>
    /// Rechenwerte aller Leistungsverzeichnisse.
    /// </summary>
    public List<LvRechenwerte>? LvWerte { get; set; }
}

/// <summary>
/// Enthält die Rechenwerte eines Leistungsverzeichnisses.
/// </summary>
public class LvRechenwerte : BaseObject
{
    /// <summary>
    /// ID des Leistungsverzeichnisses.
    /// </summary>
    public Guid LvId { get; set; }

    /// <summary>
    /// Die Rechenwerte, gegliedert nach LV-Art.
    /// </summary>
    public Dictionary<LvArt, List<Rechenwert>>? Werte { get; set; }
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
    public string? Nummer { get; set; }
    public DateTime? Datum { get; set; }
    public DateTime? ErfasstAm { get; set; }
    public string? ErstelltVon { get; set; }
    public string? Bauaufsicht { get; set; }
    public bool? Geprueft { get; set; }
    public string? Notiz { get; set; }

    public BautagesberichtArbeitszeit? ArbeitsZeit { get; set; }

    public BautagesberichtPausenzeit? PausenZeit1 { get; set; }
    public BautagesberichtPausenzeit? PausenZeit2 { get; set; }

    public BautagesberichtWetter? WetterGanztags { get; set; }
    public BautagesberichtWetter? WetterMorgens { get; set; }
    public BautagesberichtWetter? WetterMittags { get; set; }
    public BautagesberichtWetter? WetterAbends { get; set; }
    public decimal? Luftfeuchtigkeit { get; set; }

    public BautagesberichtSchlechtwetter? SchlechtWetter1 { get; set; }
    public BautagesberichtSchlechtwetter? SchlechtWetter2 { get; set; }

    public List<BautagesberichtSchnellerfassung>? Arbeiter { get; set; }
    public List<BautagesberichtSchnellerfassung>? Angestellte { get; set; }
    public List<BautagesberichtSchnellerfassung>? Geraete { get; set; }

    public List<BautagesberichtBauarbeitsschluessel>? Bauarbeitsschluessel { get; set; }

    public List<BautagesberichtBaustoffe>? Baustoffe { get; set; }

    public string? Leistungstext { get; set; }
    public List<BautagesberichtLeistungsabrechnung>? Leistungsabrechnung { get; set; }

    public string? Regietext { get; set; }
    public List<BautagesberichtLeistungsabrechnung>? Regieabrechnung { get; set; }
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
    public string? Bezeichnung { get; set; }
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
    public string? Bezeichnung { get; set; }
    public decimal? Anzahl { get; set; }
}

public class BautagesberichtBauarbeitsschluessel : BaseObject
{
    public Guid? Id { get; set; }
    public Guid? BauarbeitsschluesselId { get; set; }
    public string? Nummer { get; set; }
    public string? Bezeichnung { get; set; }
    public decimal? Stunden { get; set; }
}

public class BautagesberichtBaustoffe : BaseObject
{
    public Guid? Id { get; set; }
    public Guid? BaustoffId { get; set; }
    public string? Bezeichnung { get; set; }
    public BaustoffArt? Art { get; set; }
    public string? Ansatz { get; set; }
    public decimal? Menge { get; set; }
    public string? Einheit { get; set; }
    public string? Kommentar { get; set; }
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
    public string? Nummer { get; set; }
    public string? Bezeichnung { get; set; }
    public decimal? LvMenge { get; set; }
    public string? Einheit { get; set; }
    public string? Inhalt { get; set; }
    public decimal? Menge { get; set; }
    public string? InternerKommentar { get; set; }
}

/// <summary>
/// Objekt für die Projektdaten zu einem Projekt für die Bautagesberichte
/// </summary>
public class BautagebuchProjektdaten : BaseObject
{
    public List<BautagebuchWetterProjektdaten>? WetterProjektdaten { get; set; }
    public List<BautagebuchArbeiterProjektdaten>? ArbeiterProjektdaten { get; set; }
    public List<BautagebuchAngestellteProjektdaten>? AngestellteProjektdaten { get; set; }
    public List<BautagebuchGeraeteProjektdaten>? GeraeteProjektdaten { get; set; }
}

/// <summary>
/// Objekt für die Wetter Projektdaten zu einem Projekt für die Bautagesberichte
/// </summary>
public class BautagebuchWetterProjektdaten : BaseObject
{
    public Guid? Id { get; set; }
    public string? Bezeichnung { get; set; }
}

/// <summary>
/// Objekt für die Arbeiter Projektdaten zu einem Projekt für die Bautagesberichte
/// </summary>
public class BautagebuchArbeiterProjektdaten : BaseObject
{
    public Guid? Id { get; set; }
    public string? Bezeichnung { get; set; }
}

/// <summary>
/// Objekt für die Angestellte Projektdaten zu einem Projekt für die Bautagesberichte
/// </summary>
public class BautagebuchAngestellteProjektdaten : BaseObject
{
    public Guid? Id { get; set; }
    public string? Bezeichnung { get; set; }
}

/// <summary>
/// Objekt für die Geräte Projektdaten zu einem Projekt für die Bautagesberichte
/// </summary>
public class BautagebuchGeraeteProjektdaten : BaseObject
{
    public Guid? Id { get; set; }
    public string? Bezeichnung { get; set; }
}

#endregion Bautagebuch
