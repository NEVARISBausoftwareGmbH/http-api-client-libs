using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#pragma warning disable CS1591

namespace Nevaris.Build.ClientApi.Fianance;

public class Adresse : BaseObject
{
    public string? Code { get; set; }
    public string? Anrede { get; set; }
    public string? Titel { get; set; }
    public string? TitelImAnschreiben { get; set; }
    public string? Name { get; set; }
    public string? Vorname { get; set; }

    public string? Nachname { get; set; }

    public string? Adresszeile1 { get; set; }
    public string? Adresszeile2 { get; set; }
    public string? Adresszeile3 { get; set; }

    public string? Straße { get; set; }
    public string? Plz { get; set; }
    public string? Ort { get; set; }

    public string? LandCode { get; set; }
    public string? Postfach { get; set; }
    public string? PostfachPlz { get; set; }
    public string? PostfachOrt { get; set; }
    public string? Internet { get; set; }
    public string? EMail { get; set; }
    public string? Telefon { get; set; }
    public string? Fax { get; set; }
    public string? UstId { get; set; }
    public string? SteuernummerGesellschaft { get; set; }
    public string? ExterneAdressNummer { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Gruppe { get; set; }
    public bool? DebitorVorhanden { get; set; }
    public bool? KreditorVorhanden { get; set; }
    public DateTime? Änderungsdatum { get; set; }

    //public List<Adressat> Adressaten { get; set; }
}

public class NewAdresseInfo
{
    /// <summary>
    /// Der Code (= Adressnummer) der anzulegenden Adresse. Wenn nicht angegeben, wird der Code
    /// über die passende Finance-Nummernserie ermittelt.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Identifiziert die globale Nummernserie, die herangezogen wird, wenn das Code-Feld nicht befüllt ist.
    /// Falls leer, wird "ADRESSE" verwendet.
    /// </summary>
    public string? NummernserieCode { get; set; }

    public string? Name { get; set; }

    public string? Vorname { get; set; }

    public string? Nachname { get; set; }
}

//public class Adressat : BaseObject
//{
//    public string? Id { get; set; }
//    public string? Anrede { get; set; }
//    public string? Titel { get; set; }
//    public string? TitelImAnschreiben { get; set; }
//    public string? Name { get; set; }
//    public string? Vorname { get; set; }
//    public string? EMail { get; set; }
//    public string? Telefon { get; set; }
//    public string? Mobil { get; set; }
//    public string? Fax { get; set; }
//    public string? Abteilung { get; set; }
//    public string? Funktion { get; set; }
//    public DateTime? Geburtsdatum { get; set; }
//    public bool? Inaktiv { get; set; }
//    public DateTime? Änderungsdatum { get; set; }
//}

public class Land : BaseObject
{
    public string? Code { get; set; }
    public string? Bezeichnung { get; set; }
    public string? LänderCodeEU { get; set; }
    public string? LänderCodeISO3166Alpha { get; set; }
    public string? LänderCodeISO3166Numerisch { get; set; }
}

public class NewLandInfo
{
    public string? Code { get; set; }
    public string? Bezeichnung { get; set; }
    public string? LänderCodeEU { get; set; }
    public string? LänderCodeISO3166Alpha { get; set; }
    public string? LänderCodeISO3166Numerisch { get; set; }
}

public class Kostenstelle : BaseObject
{
    public string? Id { get; set; }
    public string? Bezeichnung { get; set; }

    public string? HauptkostenstellenId { get; set; }

    public bool? IstBaustelle { get; set; }
    public bool? IstBauhof { get; set; }
    public bool? IstBauträger { get; set; }
    public bool? IstGerät { get; set; }
    public bool? IstARGE { get; set; }
    public bool? IstFertiggestellt { get; set; }
    public bool? IstSchlussgerechnet { get; set; }

    public string? DebitorNummer { get; set; }
    public string? DebitorBezeichnung { get; set; }

    public string? AdressCode { get; set; }
    public string? AuftraggeberAdressCode { get; set; }
    public string? BauherrAdressCode { get; set; }
    public string? BauleiterName { get; set; }

    public string? Vertragsart { get; set; }

    public string? Baubeginn { get; set; }
    public string? Bauende { get; set; }
    public string? Bauabnahmedatum { get; set; }


    public decimal? Auftragswert { get; set; }
    public decimal? Nachtragswert { get; set; }
    public decimal? GestellteRechnungen { get; set; }
    public decimal? ErhalteneZahlungen { get; set; }

    public DateTime? Änderungsdatum { get; set; }
}

public class Zahlungsbedingung : BaseObject
{
    public string? Id { get; set; } = string.Empty;
    public string? Beschreibung { get; set; }
    public string? NettoFälligkeitTage { get; set; }
    public string? SkontoTage1 { get; set; }
    public decimal? SkontoProzent1 { get; set; }
    public string? SkontoTage2 { get; set; }
    public decimal? SkontoProzent2 { get; set; }
}

public class NewDebitorInfo
{
    /// <summary>
    /// Die eindeutige Nummer des Debitors. Wenn nicht angegeben, wird die ID
    /// über die passende mandantenspezifische Finance-Nummernserie ermittelt.
    /// </summary>
    public string? DebitorId { get; set; }

    /// <summary>
    /// Identifiziert die mandantenspezifische Nummernserie, die herangezogen wird, wenn das DebitorId-Feld nicht befüllt ist.
    /// Falls leer, wird "DEBITOREN" verwendet.
    /// </summary>
    public string? NummernserieCode { get; set; }

    /// <summary>
    /// Der Code der zugehörigen Adresse.
    /// </summary>
    public string? AdressCode { get; set; }
}

/// <summary>
/// Ein Debitor aus Finance.
/// </summary>
public class Debitor : BaseObject
{
    public string? Id { get; set; }
    public string? AdressCode { get; set; }

    public string? Name { get; set; }
    public string? Suchbegriff { get; set; }
    public string? Name_2 { get; set; }
    public string? Adresse { get; set; }
    public string? Adresse_2 { get; set; }
    public string? Ort { get; set; }
    public string? Kontaktperson { get; set; }
    public string? Telefonnr { get; set; }
    public string? Telexnr { get; set; }
    public string? Unsere_Kontonr { get; set; }
    public string? Gebietscode { get; set; }
    public string? Kostenstelle { get; set; }
    public string? Kostenträger { get; set; }
    public string? Kette { get; set; }
    public decimal? Budgetierter_Betrag { get; set; }
    public decimal? Kreditlimit_MW { get; set; }
    public string? Debitorenbuchungsgruppe { get; set; }
    public string? Währungscode { get; set; }
    public string? Preisgruppencode { get; set; }
    public string? Sprachcode { get; set; }
    public int? Statistikgruppe { get; set; }
    public string? Zlg_Bedingungscode { get; set; }
    public string? Zinskonditionencode { get; set; }
    public string? Verkäufercode { get; set; }
    public string? Lieferbedingungscode { get; set; }
    public string? Spediteurcode { get; set; }
    public string? Transitstelle { get; set; }
    public string? Rechnungsrabattcode { get; set; }
    public string? Deb_Artikelrabattgr { get; set; }
    public string? Ländercode { get; set; }
    public string? Einzugsverfahren { get; set; }
    public decimal? Betrag { get; set; }
    public bool? Bemerkung { get; set; }
    public bool? Gesperrt { get; set; }
    public int? Anzahl_Rechnungskopien { get; set; }
    public int? Letzte_Kontoauszugsnr { get; set; }
    public bool? Kontoauszüge_drucken { get; set; }
    public string? Rech_an_Deb_Nr { get; set; }
    public int? Priorität { get; set; }
    public string? Zahlungsform { get; set; }
    public DateTime? Korrigiert_am { get; set; }
    public decimal? Saldo { get; set; }
    public decimal? Saldo_MW { get; set; }
    public decimal? Bewegung { get; set; }
    public decimal? Bewegung_MW { get; set; }
    public decimal? Verkauf_MW { get; set; }
    public decimal? DB_MW { get; set; }
    public decimal? Rechnungsrabatte_MW { get; set; }
    public decimal? Skonto_MW { get; set; }
    public decimal? Fälliger_Saldo { get; set; }
    public decimal? Fälliger_Saldo_MW { get; set; }
    public decimal? Zahlungen { get; set; }
    public decimal? Fakturierter_Betrag { get; set; }
    public decimal? Gutgeschriebener_Betrag { get; set; }
    public decimal? Zinsrechnungsbetrag { get; set; }
    public decimal? Bezahlt_MW { get; set; }
    public decimal? Fakturierter_Betrag_MW { get; set; }
    public decimal? Gutgeschriebener_Betrag_MW { get; set; }
    public decimal? Zinsrechnungsbetrag_MW { get; set; }
    public decimal? Auftragsbestand { get; set; }
    public decimal? Nicht_fakt_Lieferungen { get; set; }
    public AusgleichsmethodeOption? Ausgleichsmethode { get; set; }
    public bool? Mengenrabatt_zulassen { get; set; }
    public bool? VK_Preise_inkl_MWSt { get; set; }
    public string? Lagerortcode { get; set; }
    public string? Faxnr { get; set; }
    public string? Telex_Namengeber { get; set; }
    public string? USt_IdNr { get; set; }
    public bool? Sammelrechnung { get; set; }
    public string? Geschäftsbuchungsgruppe { get; set; }
    public string? PLZ_Code { get; set; }
    public string? Bundesregion { get; set; }
    public decimal? Sollbetrag { get; set; }
    public decimal? Habenbetrag { get; set; }
    public decimal? Sollbetrag_MW { get; set; }
    public decimal? Habenbetrag_MW { get; set; }
    public string? E_Mail { get; set; }
    public string? Homepage { get; set; }
    public string? Mahnmethodencode { get; set; }
    public decimal? Mahnbeträge { get; set; }
    public decimal? Mahnbeträge_MW { get; set; }
    public string? Nummernserie { get; set; }
    public string? Steuergebietscode { get; set; }
    public bool? Steuerpflichtig { get; set; }
    public string? MWSt_Geschäftsbuchungsgruppe { get; set; }
    public decimal? Auftragsbestand_MW { get; set; }
    public decimal? Nicht_fakt_Lieferungen_MW { get; set; }
    public ReservierenOption? Reservieren { get; set; }
    public string? Aktuelles_Bankkonto { get; set; }
    public string? Versicherung { get; set; }
    public string? Risikonr { get; set; }
    public string? Adressnr_Avise { get; set; }
    public string? Adressnr_Mahnungen { get; set; }
    public string? Adressnr_Mahnungen_Kopie { get; set; }
    public decimal? DR_Prozent { get; set; }
    public string? DR_Fälligkeitsformel { get; set; }
    public bool? Konto_pro_Diverse { get; set; }
    public string? Nummernserie_global { get; set; }
    public string? Kundengruppe { get; set; }
    public bool? Trab_Debitor { get; set; }
    public bool? Notleidend { get; set; }
    public string? Debitorenzuschlagsgruppe { get; set; }
    public string? Zielmandant_Belegübertragung { get; set; }
    public string? Zielkreditor_Belegübertragung { get; set; }
    public bool? Leistungsempfänger { get; set; }
    public string? Rech_an_abw_Adressnr { get; set; }
    public string? Bankname { get; set; }
    public string? BLZ { get; set; }
    public string? Bankkontonr { get; set; }
    public string? SWIFT_Code { get; set; }
    public string? IBAN { get; set; }
    public bool? Konto_mit_Bewegung { get; set; }
    public bool? Konto_mit_offenen_Posten { get; set; }
    public decimal? Saldo_bis_Datum { get; set; }
    public decimal? Saldo_bis_Datum_MW { get; set; }
    public ESR_SystemOption? ESR_System { get; set; }
    public string? Bankkonto { get; set; }
    public bool? Gutschriftsverfahren { get; set; }
    public string? Dtaus_Fülldefinition { get; set; }
    public string? Adressat_Avise { get; set; }
    public string? Adressat_Mahnung { get; set; }
    public string? Adressat_Mahnung_Kopie { get; set; }
    public ZeilenrabattberechnungOption? Zeilenrabattberechnung { get; set; }
    public int? Anzahl_Lieferscheinkopien { get; set; }
    public bool? Öffentlicher_Auftraggeber { get; set; }
    public string? Steuernummer_IT { get; set; }
    public string? Transportzeit { get; set; }
    public bool? Ist_Niederlassung { get; set; }
    public VersicherbarkeitOption? Versicherbarkeit { get; set; }
    public string? Provisionsberechtigter { get; set; }
    public bool? Überarbeiten { get; set; }
    public int? Anzahl_Posten_pro_Zahlsatz { get; set; }
    public string? max_Anzahl_Zahlsätze { get; set; }
    public Datensatz_GesperrtOption? Datensatz_Gesperrt { get; set; }
    public string? Sperrhinweis { get; set; }
    public DateTime? Gültig_ab { get; set; }
    public DateTime? Gültig_bis { get; set; }
    public DateTime? Freistellung_LE_bis { get; set; }
    public string? Bauabzugssteuer_Adressnr { get; set; }
    public DateTime? Freistellung_LE_von { get; set; }
    public string? Verwendungszweck_SEPA { get; set; }
    public string? Mandatreferenz { get; set; }
    public string? USt_IdNr_Betriebsstätte { get; set; }
    public string? Steuernummer { get; set; }
    public string? Neuanlagesystem { get; set; }
    public string? Neuanlagebenutzer { get; set; }
    public DateTime? Neuanlagedatum { get; set; }
    public string? Änderungssystem { get; set; }
    public string? Änderungsbenutzer { get; set; }
    public DateTime? Änderungsdatum { get; set; }
    public DateTime? Datumsfilter { get; set; }
    public string? Kostenstellenfilter { get; set; }
    public string? Kostenträgerfilter { get; set; }
    public string? Währungsfilter { get; set; }
    public bool? Statistischfilter { get; set; }
    public decimal? Betragsfilter { get; set; }
    public string? Niederlassungsfilter { get; set; }
    public string? DebitorenbuchungsgruppenFilter { get; set; }
    public bool? Bonusberechnungsfilter { get; set; }
    public string? Bauleiterfilter { get; set; }
    public EinbehaltsartFilterOption? EinbehaltsartFilter { get; set; }
    public bool? Umsatzfilter { get; set; }

    public List<DebitorBankkonto>? Bankkonten { get; set; }
}

public class DebitorBankkonto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Name_2 { get; set; }
    public string? Adresse { get; set; }
    public string? Adresse_2 { get; set; }
    public string? Ort { get; set; }
    public string? PLZ_Code { get; set; }
    public string? Kontaktperson { get; set; }
    public string? Telefonnr { get; set; }
    public string? Telexnr { get; set; }
    public string? BLZ_alt { get; set; }
    public string? Bankkontonummer { get; set; }
    public string? Transit_Nr { get; set; }
    public string? Währungscode { get; set; }
    public string? Ländercode { get; set; }
    public string? Bundesregion { get; set; }
    public string? Faxnr { get; set; }
    public string? Telex_Namengeber { get; set; }
    public string? Sprachcode { get; set; }
    public string? E_Mail { get; set; }
    public string? Homepage { get; set; }
    public string? BLZ { get; set; }
    public string? Land_BLZ { get; set; }
    public string? SWIFT_Code { get; set; }
    public string? IBAN { get; set; }
    public string? Kontoinhaber { get; set; }
    public Datensatz_GesperrtOption? Datensatz_Gesperrt { get; set; }
    public string? Sperrhinweis { get; set; }
    public DateTime? Gültig_ab { get; set; }
    public DateTime? Gültig_bis { get; set; }
    public string? Mandatreferenz { get; set; }
    public string? Neuanlagesystem { get; set; }
    public string? Neuanlagebenutzer { get; set; }
    public DateTime? Neuanlagedatum { get; set; }
    public string? Änderungssystem { get; set; }
    public string? Änderungsbenutzer { get; set; }
    public DateTime? Änderungsdatum { get; set; }
}

public class NewKreditorInfo
{
    /// <summary>
    /// Die eindeutige Nummer des Kreditors. Wenn nicht angegeben, wird die ID
    /// über die passende mandantenspezifische Finance-Nummernserie ermittelt.
    /// </summary>
    public string? KreditorId { get; set; }

    /// <summary>
    /// Identifiziert die mandantenspezifische Nummernserie, die herangezogen wird, wenn das KreditorId-Feld nicht befüllt ist.
    /// Falls leer, wird "KRDITOREN" verwendet.
    /// </summary>
    public string? NummernserieCode { get; set; }

    /// <summary>
    /// Der Code der zugehörigen Adresse.
    /// </summary>
    public string? AdressCode { get; set; }
}

/// <summary>
/// Ein Kreditor aus Finance.
/// </summary>
public class Kreditor : BaseObject
{
    public string? Id { get; set; }
    public string? AdressCode { get; set; }

    public string? Name { get; set; }
    public string? Suchbegriff { get; set; }
    public string? Name_2 { get; set; }
    public string? Adresse { get; set; }
    public string? Adresse_2 { get; set; }
    public string? Ort { get; set; }
    public string? Kontaktperson { get; set; }
    public string? Telefonnr { get; set; }
    public string? Telexnr { get; set; }
    public string? Unsere_Kontonr { get; set; }
    public string? Gebietscode { get; set; }
    public string? Kostenstelle { get; set; }
    public string? Kostenträger { get; set; }
    public decimal? Budgetierter_Betrag { get; set; }
    public string? Kreditorenbuchungsgruppe { get; set; }
    public string? Währungscode { get; set; }
    public string? Sprachcode { get; set; }
    public int? Statistikgruppe { get; set; }
    public string? Zlg_Bedingungscode { get; set; }
    public string? Zinskonditionencode { get; set; }
    public string? Einkäufercode { get; set; }
    public string? Lieferbedingungscode { get; set; }
    public string? Spediteurcode { get; set; }
    public string? Rechnungsrabattcode { get; set; }
    public string? Ländercode { get; set; }
    public bool? Bemerkung { get; set; }
    public bool? Gesperrt { get; set; }
    public string? Zahlung_an_Kred_Nr { get; set; }
    public int? Priorität { get; set; }
    public string? Zahlungsform { get; set; }
    public DateTime? Korrigiert_am { get; set; }
    public decimal? Saldo { get; set; }
    public decimal? Saldo_MW { get; set; }
    public decimal? Bewegung { get; set; }
    public decimal? Bewegung_MW { get; set; }
    public decimal? Einkauf_MW { get; set; }
    public decimal? Rechnungsrabatte_MW { get; set; }
    public decimal? Skonto_MW { get; set; }
    public decimal? Fälliger_Saldo { get; set; }
    public decimal? Fälliger_Saldo_MW { get; set; }
    public decimal? Zahlungen { get; set; }
    public decimal? Fakturierter_Betrag { get; set; }
    public decimal? Gutgeschriebener_Betrag { get; set; }
    public decimal? Zinsrechnungsbetrag { get; set; }
    public decimal? Bezahlt_MW { get; set; }
    public decimal? Fakturierter_Betrag_MW { get; set; }
    public decimal? Gutgeschriebener_Betrag_MW { get; set; }
    public decimal? Zinsrechnungsbetrag_MW { get; set; }
    public decimal? Bestellungsbestand { get; set; }
    public decimal? Nicht_fakt_Lieferbetrag { get; set; }
    public AusgleichsmethodeOption? Ausgleichsmethode { get; set; }
    public string? Faxnr { get; set; }
    public string? Telex_Namengeber { get; set; }
    public string? USt_IdNr { get; set; }
    public string? Geschäftsbuchungsgruppe { get; set; }
    public string? PLZ_Code { get; set; }
    public string? Bundesregion { get; set; }
    public decimal? Sollbetrag { get; set; }
    public decimal? Habenbetrag { get; set; }
    public decimal? Sollbetrag_MW { get; set; }
    public decimal? Habenbetrag_MW { get; set; }
    public string? E_Mail { get; set; }
    public string? Homepage { get; set; }
    public decimal? Mahnbeträge { get; set; }
    public decimal? Mahnbeträge_MW { get; set; }
    public string? Nummernserie { get; set; }
    public string? Steuergebietscode { get; set; }
    public bool? Steuerpflichtig { get; set; }
    public string? MWSt_Geschäftsbuchungsgruppe { get; set; }
    public decimal? Bestellungsbestand_MW { get; set; }
    public decimal? Nicht_fakt_Lieferbetrag_MW { get; set; }
    public ReservierenOption? Reservieren { get; set; }
    public string? Adressnr_Avise { get; set; }
    public string? Adressnr_Mahnungen { get; set; }
    public decimal? DR_Prozent { get; set; }
    public string? DR_Fälligkeitsformel { get; set; }
    public int? Rahmenvereinbarung { get; set; }
    public string? Übernommen_als_Kreditor { get; set; }
    public DateTime? Datum_Übernahme_als_Kreditor { get; set; }
    public decimal? Mindestbestellbetrag { get; set; }
    public Lieferant_SubunternehmerOption? Lieferant_Subunternehmer { get; set; }
    public bool? GAEB_Preisanfrage { get; set; }
    public string? Aktuelles_Bankkonto { get; set; }
    public bool? Konto_pro_Diverse { get; set; }
    public string? Nummernserie_global { get; set; }
    public bool? Notleidend { get; set; }
    public string? Kundengruppe { get; set; }
    public string? Bankname { get; set; }
    public string? BLZ { get; set; }
    public string? Bankkontonr { get; set; }
    public string? SWIFT_Code { get; set; }
    public string? IBAN { get; set; }
    public string? Kontoinhaber { get; set; }
    public ZahlungsartOption? Zahlungsart { get; set; }
    public ESR_ArtOption? ESR_Art { get; set; }
    public string? Postcheckkonto { get; set; }
    public string? ESR_Konto { get; set; }
    public string? Ausland_BLZ { get; set; }
    public int? Rechnungsnr_Startpos { get; set; }
    public int? Rechnungsnr_Länge { get; set; }
    public string? Verwendungszweck_SEPA { get; set; }
    public string? Dtaus_Fülldefinition { get; set; }
    public string? Gruppe { get; set; }
    public string? Bestelladreßcode { get; set; }
    public SperrkennzeichenOption? Sperrkennzeichen { get; set; }
    public string? Steuernummer { get; set; }
    public string? Adressat_Avise { get; set; }
    public string? Steuernummer_IT { get; set; }
    public DateTime? AGH_Freistellung_ab { get; set; }
    public DateTime? AGH_Freistellung_bis { get; set; }
    public bool? Prüfung_gültige_Kostenarten { get; set; }
    public bool? Gültige_Kostenarten { get; set; }
    public int? Anzahl_Posten_pro_Zahlsatz { get; set; }
    public string? max_Anzahl_Zahlsätze { get; set; }
    public SkontobasisOption? Skontobasis { get; set; }
    public Datensatz_GesperrtOption? Datensatz_Gesperrt { get; set; }
    public string? Sperrhinweis { get; set; }
    public DateTime? Gültig_ab { get; set; }
    public DateTime? Gültig_bis { get; set; }
    public bool? Bauleistender_13b { get; set; }
    public DateTime? Freistellung_bis { get; set; }
    public bool? Grenzunterschreitung { get; set; }
    public string? Bauabzugssteuer_Adressnr { get; set; }
    public DateTime? Freistellung_von { get; set; }
    public decimal? Skontierfähiger_Saldo_MW { get; set; }
    public decimal? Saldo_bis_Datum { get; set; }
    public decimal? Saldo_bis_Datum_MW { get; set; }
    public bool? Konto_mit_Bewegung { get; set; }
    public bool? Konto_mit_offenen_Posten { get; set; }
    public decimal? Bewegung_MW_Plus { get; set; }
    public bool? Eintrag_EPU_in_HFU_Gesamtliste { get; set; }
    public string? USt_IdNr_Betriebsstätte { get; set; }
    public string? Steuernummer_Freistellung { get; set; }
    public string? Finanzamt_Freistellung { get; set; }
    public string? Versicherungsnummer_VSNR { get; set; }
    public string? Dienstleistungszentrum { get; set; }
    public string? DG_Nummer { get; set; }
    public bool? Eintrag_in_HFU_Gesamtliste { get; set; }
    public bool? Haftungsübernahme { get; set; }
    public bool? Ist_Niederlassung { get; set; }
    public string? Neuanlagesystem { get; set; }
    public string? Neuanlagebenutzer { get; set; }
    public DateTime? Neuanlagedatum { get; set; }
    public string? Änderungssystem { get; set; }
    public string? Änderungsbenutzer { get; set; }
    public DateTime? Änderungsdatum { get; set; }
    public DateTime? Datumsfilter { get; set; }
    public string? Kostenstellenfilter { get; set; }
    public string? Kostenträgerfilter { get; set; }
    public string? Währungsfilter { get; set; }
    public bool? Bonusberechnungsfilter { get; set; }
    public EinbehaltsartFilterOption? EinbehaltsartFilter { get; set; }
    public decimal? Betragsfilter { get; set; }
    public bool? Statistischfilter { get; set; }
    public string? Niederlassungsfilter { get; set; }
    public bool? Umsatzfilter { get; set; }
    public string? Kreditorenbuchungsgruppenfilter { get; set; }

    public List<KreditorBankkonto>? Bankkonten { get; set; }
}

public class KreditorBankkonto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Name_2 { get; set; }
    public string? Adresse { get; set; }
    public string? Adresse_2 { get; set; }
    public string? Ort { get; set; }
    public string? PLZ_Code { get; set; }
    public string? Kontaktperson { get; set; }
    public string? Telefonnr { get; set; }
    public string? Telexnr { get; set; }
    public string? BLZ_alt { get; set; }
    public string? Bankkontonummer { get; set; }
    public string? Transit_Nr { get; set; }
    public string? Währungscode { get; set; }
    public string? Ländercode { get; set; }
    public string? Bundesregion { get; set; }
    public string? Faxnr { get; set; }
    public string? Telex_Namengeber { get; set; }
    public string? Sprachcode { get; set; }
    public string? E_Mail { get; set; }
    public string? Homepage { get; set; }
    public string? BLZ { get; set; }
    public string? Land_BLZ { get; set; }
    public ZahlungsartOption? Zahlungsart { get; set; }
    public ESR_ArtOption? ESR_Art { get; set; }
    public string? Postcheckkonto { get; set; }
    public string? ESR_Konto { get; set; }
    public string? Ausland_BLZ { get; set; }
    public string? SWIFT_Code { get; set; }
    public string? Gegenkonto { get; set; }
    public int? Rechnungsnr_Startpos { get; set; }
    public int? Rechnungsnr_Länge { get; set; }
    public string? Kontoinhaber { get; set; }
    public string? IBAN { get; set; }
    public EntgeltreglungOption? Entgeltreglung { get; set; }
    public string? Weisungsschlüssel_1 { get; set; }
    public string? Weisungsschlüssel_2 { get; set; }
    public Datensatz_GesperrtOption? Datensatz_Gesperrt { get; set; }
    public string? Sperrhinweis { get; set; }
    public DateTime? Gültig_ab { get; set; }
    public DateTime? Gültig_bis { get; set; }
    public bool? Aktuelles_Bankkonto { get; set; }
    public string? Neuanlagesystem { get; set; }
    public string? Neuanlagebenutzer { get; set; }
    public DateTime? Neuanlagedatum { get; set; }
    public string? Änderungssystem { get; set; }
    public string? Änderungsbenutzer { get; set; }
    public DateTime? Änderungsdatum { get; set; }
}

public enum Lieferant_SubunternehmerOption
{
    /// <summary>
    /// Lieferant
    /// </summary>
    Lieferant = 0,

    /// <summary>
    /// Subunternehmer
    /// </summary>
    Subunternehmer = 1,

    /// <summary>
    /// Beides
    /// </summary>
    Beides = 2
}

public enum SkontobasisOption
{
    /// <summary>
    /// V
    /// </summary>
    V = 0,

    /// <summary>
    /// W
    /// </summary>
    W = 1
}

public enum SperrkennzeichenOption
{
    _ = 0,

    /// <summary>
    /// Exekution
    /// </summary>
    Exekution = 1,

    /// <summary>
    /// Insolvenz
    /// </summary>
    Insolvenz = 2,

    /// <summary>
    /// Konkurs
    /// </summary>
    Konkurs = 3,

    /// <summary>
    /// Zession
    /// </summary>
    Zession = 4
}

public enum ESR_ArtOption
{
    _ = 0,

    /// <summary>
    /// 5/15
    /// </summary>
    _5_15 = 1,

    /// <summary>
    /// 9/27
    /// </summary>
    _9_27 = 2,

    /// <summary>
    /// 9/16
    /// </summary>
    _9_16 = 3
}

public enum ZahlungsartOption
{
    /// <summary>
    /// ESR
    /// </summary>
    ESR = 0,

    /// <summary>
    /// ESR+
    /// </summary>
    ESR_2 = 1,

    /// <summary>
    /// EZ Post
    /// </summary>
    EZ_Post = 2,

    /// <summary>
    /// EZ Bank
    /// </summary>
    EZ_Bank = 3,

    /// <summary>
    /// ZA Inland
    /// </summary>
    ZA_Inland = 4,

    /// <summary>
    /// PC Ausland
    /// </summary>
    PC_Ausland = 5,

    /// <summary>
    /// Bank Ausland
    /// </summary>
    Bank_Ausland = 6,

    /// <summary>
    /// SWIFT
    /// </summary>
    SWIFT = 7,

    /// <summary>
    /// Postanweisung Ausland
    /// </summary>
    Postanweisung_Ausland = 8
}

public enum AusgleichsmethodeOption
{
    /// <summary>
    /// Offener Posten
    /// </summary>
    Offener_Posten = 0,

    /// <summary>
    /// Saldomethode
    /// </summary>
    Saldomethode = 1
}

public enum ReservierenOption
{
    /// <summary>
    /// Nie
    /// </summary>
    Nie = 0,

    /// <summary>
    /// Optional
    /// </summary>
    Optional = 1,

    /// <summary>
    /// Immer
    /// </summary>
    Immer = 2
}

public enum VersicherbarkeitOption
{
    _ = 0,

    /// <summary>
    /// nicht versicherbar
    /// </summary>
    nicht_versicherbar = 1,

    /// <summary>
    /// bedingt versicherbar
    /// </summary>
    bedingt_versicherbar = 2,

    /// <summary>
    /// versicherbar
    /// </summary>
    versicherbar = 3
}

public enum ESR_SystemOption
{
    _ = 0,

    /// <summary>
    /// ESR
    /// </summary>
    ESR = 1,

    /// <summary>
    /// ESR+
    /// </summary>
    ESR_2 = 2
}

public enum EntgeltreglungOption
{
    _ = 0,
    _00 = 1,
    _01 = 2,
    _02 = 3
}

public enum Datensatz_GesperrtOption
{
    /// <summary>
    /// Nein
    /// </summary>
    Nein = 0,

    /// <summary>
    /// Ja
    /// </summary>
    Ja = 1,

    /// <summary>
    /// Bereich
    /// </summary>
    Bereich = 2,

    /// <summary>
    /// Hinweis
    /// </summary>
    Hinweis = 3
}

public enum ZeilenrabattberechnungOption
{
    _ = 0,

    /// <summary>
    /// Mengenrabatt + Deb.-/Artikelrabatt
    /// </summary>
    Mengenrabatt_Deb_Artikelrabatt = 1,

    /// <summary>
    /// Mengenrabatt * Deb.-/Artikelrabatt
    /// </summary>
    Mengenrabatt_Deb_Artikelrabatt_2 = 2
}

public enum EinbehaltsartFilterOption
{
    _ = 0,

    /// <summary>
    /// Einbehalt
    /// </summary>
    Einbehalt = 1,

    /// <summary>
    /// Deckungsrücklass
    /// </summary>
    Deckungsrücklass = 2,

    /// <summary>
    /// Haftrücklass
    /// </summary>
    Haftrücklass = 3,

    /// <summary>
    /// Sicherheitseinbehalt
    /// </summary>
    Sicherheitseinbehalt = 4,

    /// <summary>
    /// Vertragserfüllungsbürgschaft
    /// </summary>
    Vertragserfüllungsbürgschaft = 5,

    /// <summary>
    /// Gewährleistungsbürgschaft
    /// </summary>
    Gewährleistungsbürgschaft = 6
}

public class Wartungsposition : BaseObject
{
    public int Lfdnr { get; set; }
    public WartungspositionBelegart? Belegart { get; set; }
    public string? Belegnr { get; set; }
    public int? Zeilennr { get; set; }
    public string? Verk_an_Deb_Nr { get; set; }
    public string? Verk_an_Name { get; set; }
    public string? Verk_an_PLZ_Code { get; set; }
    public string? Verk_an_Adressnr { get; set; }
    public string? Verk_an_Ort { get; set; }
    public string? Kostenstelle { get; set; }
    public string? Abweichender_Mandant { get; set; }
    public string? Niederlassung { get; set; }
    public string? Projektnr { get; set; }
    public WartungspositionAuftragBelegart? Auftrag_Belegart { get; set; }
    public string? Auftragsnr { get; set; }
    public int? Auftrag_Zeilennr { get; set; }
    public string? Dokument_ID { get; set; }
    public WartungspositionArt? Art { get; set; }
    public string? Nr { get; set; }
    public string? Beschreibung { get; set; }
    public string? Beschreibung_2 { get; set; }
    public string? Einheit { get; set; }
    public decimal? Menge { get; set; }
    public string? Einheitencode { get; set; }
    public decimal? Menge_Basis { get; set; }
    public string? Gruppenstufe { get; set; }
    public string? Gruppenstufe2 { get; set; }
    public string? Gruppenstufe3 { get; set; }
    public string? Gruppenstufe4 { get; set; }
    public string? Gruppenstufe5 { get; set; }
    public decimal? VK_Preis { get; set; }
    public decimal? Rabatt_Percent { get; set; }
    public decimal? Betrag { get; set; }
    public string? Währungscode_des_Auftrages { get; set; }
    public string? Rech_an_Deb_Nr { get; set; }
    public string? Rech_an_Name { get; set; }
    public string? Rech_an_Adressnr { get; set; }
    public string? Rech_an_Ort { get; set; }
    public string? Rech_an_PLZ_Code { get; set; }
    public string? Lief_an_Code { get; set; }
    public string? Lief_an_Name { get; set; }
    public string? Lief_an_Adressnr { get; set; }
    public string? Lief_an_PLZ_Code { get; set; }
    public string? Lief_an_Ort { get; set; }
    public decimal? Ursprünglicher_Wartungssatz_Percent { get; set; }
    public DateTime? Datum_Wartungserhöhung { get; set; }
    public decimal? Aktueller_Wartungsprozentsatz { get; set; }
    public decimal? Zuschlag_wg_Zahlungsweise { get; set; }
    public decimal? Tatsächlicher_Wartungsbetrag { get; set; }
    public DateTime? Datum_letzte_Wartungsrechnung { get; set; }
    public string? Belegnr_letzte_Wartungsrechn { get; set; }
    public int? Zeilennr_letzte_Wartungsrech { get; set; }
    public DateTime? Datum_nächste_Wartungsrechnung { get; set; }
    public string? Belegnr_akt_Wartungsrechn { get; set; }
    public int? Zeilennr_akt_Wartungsrechn { get; set; }
    public string? Währungscode_der_Wartung { get; set; }
    public DateTime? Letzter_Fakturierzeitraum_von { get; set; }
    public DateTime? Letzter_Fakturierzeitraum_bis { get; set; }
    public int? Gehört_zu_Stückliste { get; set; }
    public bool? Ist_Stückliste { get; set; }
    public WartungspositionKomponentenDrucken? Komponenten_drucken { get; set; }
    public bool? Wartungsvertrag { get; set; }
    public bool? Hotlinevertrag { get; set; }
    public DateTime? Vertragsbeginn { get; set; }
    public DateTime? Vertragsende { get; set; }
    public string? Wartungsvertragsnummer { get; set; }
    public string? Zugang_Supportserver { get; set; }
    public WartungspositionWartungsbasis? Wartungsbasis { get; set; }
    public DateTime? Wartungsbeginn { get; set; }
    public DateTime? Wartungsende { get; set; }
    public int? Wartungsintervall { get; set; }
    public decimal? Wartungsprozentsatz { get; set; }
    public decimal? Wartungsrabattprozentsatz { get; set; }
    public decimal? Wartungsbetrag_Brutto { get; set; }
    public decimal? Wartungsbetrag_Netto { get; set; }
    public DateTime? Fakturierbeginn_der_Wartung { get; set; }
    public DateTime? Einmaliger_Fakturierbeginn { get; set; }
    public DateTime? Einmaliges_Fakturierende { get; set; }
    public int? Einmaliger_Fakturierungszeit { get; set; }
    public bool? Abrechnung_ausgesetzt { get; set; }
    public string? Neuanlagesystem { get; set; }
    public string? Neuanlagebenutzer { get; set; }
    public DateTime? Neuanlagedatum { get; set; }
    public string? Änderungssystem { get; set; }
    public string? Änderungsbenutzer { get; set; }
    public DateTime? Änderungsdatum { get; set; }
    public WartungspositionStatus? Status { get; set; }
}

public enum WartungspositionBelegart
{
    Angebot = 0,
    Auftrag = 1,
    Rechnung = 2,
    Gutschrift = 3,
    Rahmenauftrag = 4
}

public enum WartungspositionAuftragBelegart
{
    Angebot = 0,
    Auftrag = 1,
    Rechnung = 2,
    Gutschrift = 3,
    Rahmenauftrag = 4
}

public enum WartungspositionArt
{
    _ = 0,
    Sachkonto = 1,
    Artikel = 2,
    Ressource = 3,
    WG_Anlage = 4
}

public enum WartungspositionKomponentenDrucken
{
    Komponenten = 0,
    Stücklisten = 1,
    beides_Komponenten_und_Stücklisten = 2
}

public enum WartungspositionWartungsbasis
{
    Listenpreis_gem_Angebot = 0,
    Rabattierter_VK_Preis = 1
}

public enum WartungspositionStatus
{
    _ = 0,
    gekündigt = 1,
    insolvent = 2,
    Rücknahme = 3
}