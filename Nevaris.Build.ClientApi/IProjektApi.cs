#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace Nevaris.Build.ClientApi;

/// <summary>
/// Interface zum Zugriff auf projektspezifische Operationen über die NEVARIS Build API.
/// </summary>
public interface IProjektApi
{
    /// <summary>
    /// Liefert das Projekt mit der angegebenen projektId.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    [Get("/build/projekte/{projektId}")]
    Task<Projekt> GetProjekt(string projektId);

    /// <summary>
    /// Aktualisiert ein Projekt.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="projekt">Projekt mit den neuen Werten</param>
    [Put("/build/projekte/{projektId}")]
    Task UpdateProjekt(string projektId, [Body] Projekt projekt);

    /// <summary>
    /// Löscht das Projekt mit der angegebenen projektId.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    [Delete("/build/projekte/{projektId}")]
    Task DeleteProjekt(string projektId);

    /// <summary>
    /// Liefert alle Adressen eines Projekts.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    [Get("/build/projekte/{projektId}/adressen")]
    Task<List<Adresse>> GetAdressen(string projektId);

    /// <summary>
    /// Liefert die Projektadresse mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="id">Adress-ID</param>
    [Get("/build/projekte/{projektId}/adressen/{id}")]
    Task<Adresse> GetAdresse(string projektId, Guid id);

    /// <summary>
    /// Erstellt eine neue Adresse.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="newProjektAdresseInfo">Informationen zur neuen Adresse</param>
    [Post("/build/projekte/{projektId}/adressen")]
    Task<Adresse> CreateAdresse(string projektId, [Body] NewProjektAdresseInfo newProjektAdresseInfo);

    /// <summary>
    /// Aktualisiert eine Projektadresse.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="id">Adress-ID</param>
    /// <param name="adresse">Adresse mit den neuen Werten</param>
    [Put("/build/projekte/{projektId}/adressen/{id}")]
    Task UpdateAdresse(string projektId, Guid id, [Body] Adresse adresse);

    /// <summary>
    /// Löscht die Adresse mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="id">Adress-ID</param>
    [Delete("/build/projekte/{projektId}/adressen/{id}")]
    Task DeleteAdresse(string projektId, Guid id);

    /// <summary>
    /// Liefert alle Leistungsverzeichnisse eines Projekts (ohne Inhalte wie Positionen, Titel usw.).
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    [Get("/build/projekte/{projektId}/leistungsverzeichnisse")]
    Task<List<Leistungsverzeichnis>> GetLeistungsverzeichnisse(string projektId);

    /// <summary>
    /// Liefert das Leistungsverzeichnis mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    /// <param name="mengenArt">Die Mengenart, mit der das Leistungsverzeichnis gerechnet werden sollte</param>
    /// <param name="mitKnoten">Falls true (= Default), wird der gesamte LV-Baum (bestehend aus Knoten und
    /// Positionen) mitgeliefert (in der Property 'RootKnotenListe')</param>
    /// <param name="mitKalkulationen">Falls true (= Default), werden die Kalkulationen (ohne Detailinfos)
    /// mitgeliefert (in der Property 'RootKalkulationen')</param>
    /// <param name="mitFormatiertenTexten">Falls true (= Default), werden die formatierten Texte (Langtexte
    /// + Baubeschreibungen) mitgeliefert</param>
    [Get(
        "/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}?mengenArt={mengenArt}&mitKnoten={mitKnoten}&mitKalkulationen={mitKalkulationen}")]
    Task<Leistungsverzeichnis> GetLeistungsverzeichnis(
        string projektId,
        Guid lvId,
        MengenArt mengenArt = MengenArt.Lv,
        bool mitKnoten = true,
        bool mitKalkulationen = true,
        bool mitFormatiertenTexten = true);

    /// <summary>
    /// Erzeugt ein neues Leistungsverzeichnis.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="newLvInfo">Informationen zum neuen Leistungsverzeichnis</param>
    [Post("/build/projekte/{projektId}/leistungsverzeichnisse")]
    Task<Leistungsverzeichnis> CreateLeistungsverzeichnis(string projektId, [Body] NewLvInfo newLvInfo);

    /// <summary>
    /// Nimmt eine Datei vom Server entgegen, prüft, ob es sich um einen LV-Datenträger handelt 
    /// und importiert die Datei. Zurückgegeben wird das erzeugte Leistungsverzeichnis sowie 
    /// Meldungen, die im Zuge des Importvorgangs entstehen.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="importLvInfo">Information über die Datenträger-Quelle</param>  
    [Post("/build/projekte/{projektId}/leistungsverzeichnisse/ErzeugeLvAusDatentraegerServerDateipfad")]
    Task<LeistungsverzeichnisMitImportMeldungen> CreateLeistungsverzeichnisAusDatentraegerServerDateipfad(
        string projektId, [Body] ImportLvVonServerpfad importLvInfo);

    /// <summary>
    /// Nimmt eine Datei vom Client entgegen, prüft, ob es sich um einen LV-Datenträger handelt 
    /// und importiert die Datei. Zurückgegeben wird das erzeugte Leistungsverzeichnis sowie 
    /// Meldungen, die im Zuge des Importvorgangs entstehen.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="clientFile">Die zu importierende Datei als <see cref="FileInfoPart"/></param>
    [Obsolete("Diese Funktion zum Importieren eines LVs wird künftig nicht mehr angeboten werden. " 
              + "Stattdessen sollte CreateLeistungsverzeichnisAusByteArray verwendet werden.")]
    [Multipart]
    [Post("/build/projekte/{projektId}/leistungsverzeichnisse/ErzeugeLvAusDatentraegerClientDatei")]
    Task<LeistungsverzeichnisMitImportMeldungen> CreateLeistungsverzeichnisAusDatentraegerClientDatei(
        string projektId,
        [AliasAs("Datei")] FileInfoPart clientFile // "Datei" bezieht sich auf ImportLvVonClientdatei (serverseitig)
    );

    /// <summary>
    /// Erzeugt ein LV aus einem LV-Datenträger. Der Inhalt des Datenträgers wird als Byte-Array
    /// übergeben.
    /// Zurückgegeben wird das erzeugte Leistungsverzeichnis sowie 
    /// Meldungen, die im Zuge des Importvorgangs entstehen.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="importLvInfo">Information über die Datenträger-Quelle</param>
    [Post("/build/projekte/{projektId}/leistungsverzeichnisse/ErzeugeLvAusByteArray")]
    Task<LeistungsverzeichnisMitImportMeldungen> CreateLeistungsverzeichnisAusByteArray(
        string projektId, [Body] ImportLvAusByteArrayInfo importLvInfo);

    /// <summary>
    /// Aktualisiert ein Leistungsverzeichnis.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    /// <param name="lv">Leistungsverzeichnis mit den neuen Werten</param>
    /// <param name="mengenArt">Die gewünschte Mengenart (nur relevant als Filterkriterium für die Liste der
    /// globalen Hilfsberechnungen, d.h. <see cref="LvDetails.GlobaleHilfsberechungen"/>)</param>
    [Put("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}?mengenArt={mengenArt}")]
    Task UpdateLeistungsverzeichnis(
        string projektId, 
        Guid lvId, 
        [Body] Leistungsverzeichnis lv, 
        MengenArt mengenArt = MengenArt.Lv);

    /// <summary>
    /// Löscht das Leistungsverzeichnis mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    [Delete("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}")]
    Task DeleteLeistungsverzeichnis(string projektId, Guid lvId);

    /// <summary>
    /// Liefert einen LV-Knoten (z.B. Leistungsgruppe) mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="knotenId">Knoten-ID</param>
    [Get("/build/projekte/{projektId}/lvknoten/{knotenId}")]
    Task<LvKnoten> GetLvKnoten(string projektId, Guid knotenId);

    /// <summary>
    /// Aktualisiert einen LV-Knoten.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="knotenId">Knoten-ID</param>
    /// <param name="lvKnoten">Knoten mit den neuen Werten</param>
    [Put("/build/projekte/{projektId}/lvknoten/{knotenId}")]
    Task UpdateLvKnoten(string projektId, Guid knotenId, [Body] LvKnoten lvKnoten);

    /// <summary>
    /// Löscht den LV-Knoten mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="knotenId">Knoten-ID</param>
    [Delete("/build/projekte/{projektId}/lvknoten/{knotenId}")]
    Task DeleteLvKnoten(string projektId, Guid knotenId);

    /// <summary>
    /// Liefert eine LV-Position mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="positionId">Positions-ID</param>
    [Get("/build/projekte/{projektId}/lvpositionen/{positionId}")]
    Task<LvPosition> GetLvPosition(string projektId, Guid positionId);

    /// <summary>
    /// Aktualisiert eine LV-Position.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="positionId">Positions-ID</param>
    /// <param name="lvPosition">Position mit den neuen Werten</param>
    [Put("/build/projekte/{projektId}/lvpositionen/{positionId}")]
    Task UpdateLvPosition(string projektId, Guid positionId, [Body] LvPosition lvPosition);

    /// <summary>
    /// Löscht die LV-Position mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="positionId">Knoten-ID</param>
    [Delete("/build/projekte/{projektId}/lvpositionen/{positionId}")]
    Task DeleteLvPosition(string projektId, Guid positionId);

    /// <summary>
    /// Erzeugt einen neue Knoten (z.B. Leistungsgruppe) innerhalb des Leistungsverzeichnisses.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    /// <param name="newLvKnotenInfo">Informationen zum neuen Knoten</param>
    [Post("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/knoten")]
    Task<LvKnoten> CreateLvKnoten(string projektId, Guid lvId, [Body] NewLvKnotenInfo newLvKnotenInfo);

    /// <summary>
    /// Erzeugt eine neue Position innerhalb des Leistungsverzeichnisses.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    /// <param name="newLvPositionInfo">Informationen zur neuen Position</param>
    [Post("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/positionen")]
    Task<LvPosition> CreateLvPosition(string projektId, Guid lvId, [Body] NewLvPositionInfo newLvPositionInfo);

    /// <summary>
    /// Erzeugt eine neue Kalkulation für dieses Leistungsverzeichnisses.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    /// <param name="newKalkulationInfo">Informationen zur neuen Kalkulation</param>
    [Post("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/kalkulationen")]
    Task<Kalkulation> CreateKalkulation(string projektId, Guid lvId, [Body] NewKalkulationInfo newKalkulationInfo);

    /// <summary>
    /// Liefert die zu einem Leistungsverzeichnis (Auftrag) gehörenden Abrechnungsmerkmale.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    /// <param name="mengenArt">Die gewünschte Mengenart</param>
    [Get("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/abrechnungsMerkmale?mengenArt={mengenArt}")]
    Task<IEnumerable<AbrechnungsMerkmal>> GetAbrechnungsMerkmale(
        string projektId, Guid lvId, MengenArt mengenArt = MengenArt.Abrechnung);

    /// <summary>
    /// Ändert die zu einem Leistungsverzeichnis (Auftrag) gehörenden Abrechnungsmerkmale.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    /// <param name="mengenArt">Die gewünschte Mengenart</param>
    /// <param name="abrechnungsMerkmale">Geänderte Abrechnungsmerkmale (hierarchisch aufgebaute Liste)</param>
    [Put("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/abrechnungsMerkmale?mengenArt={mengenArt}")]
    Task UpdateAbrechnungsMerkmale(
        string projektId,
        Guid lvId,
        [Body] IEnumerable<AbrechnungsMerkmal> abrechnungsMerkmale,
        MengenArt mengenArt = MengenArt.Abrechnung);

    /// <summary>
    /// Liefert die zu einem Leistungsverzeichnis (Auftrag) gehörenden Aufmaßblätter.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    /// <param name="mengenArt">Die gewünschte Mengenart</param>
    /// <param name="mitDetails">Falls true (Default: false), werden Detailinformationen (hier: Hilfsberechnungen) mitgeladen</param>
    [Get("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/aufmassblaetter?mengenArt={mengenArt}&mitDetails={mitDetails}")]
    Task<List<Aufmaßblatt>> GetAufmaßblätter(
        string projektId,
        Guid lvId,
        MengenArt mengenArt = MengenArt.Abrechnung,
        bool mitDetails = false);

    /// <summary>
    /// Ändert die zu einem Leistungsverzeichnis (Auftrag) gehörenden Aufmaßblätter.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    /// <param name="aufmaßblätter">Die Aufmaßblätter mit den neuen Werten. Diese Liste sollte auf Basis der zuvor per GET ermittelten Liste erzeugt werden.</param>
    /// <param name="mengenArt">Die gewünschte Mengenart</param>
    [Put("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/aufmassblaetter?mengenArt={mengenArt}")]
    Task UpdateAufmaßblätter(
        string projektId,
        Guid lvId,
        [Body] IEnumerable<Aufmaßblatt> aufmaßblätter,
        MengenArt mengenArt = MengenArt.Abrechnung);

    /// <summary>
    /// Liefert die zu einem Leistungsverzeichnis (Auftrag) gehörenden Rechnungen.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    [Get("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/rechnungen")]
    Task<IEnumerable<Rechnung>> GetRechnungen(string projektId, Guid lvId);

    /// <summary>
    /// Fügt einem Leistungsverzeichnis eine neue Rechnung hinzu.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    /// <param name="newRechnungInfo">Informationen zur neuen Rechnung</param>
    [Post("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/rechnungen")]
    Task<Rechnung> CreateRechnung(string projektId, Guid lvId, [Body] NewRechnungInfo newRechnungInfo);

    /// <summary>
    /// Liefert eine Rechnung.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="rechnungId">Rechnungs-ID</param>
    [Get("/build/projekte/{projektId}/rechnungen/{rechnungId}")]
    Task<Rechnung> GetRechnung(string projektId, Guid rechnungId);

    /// <summary>
    /// Aktualisiert eine Rechnung.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="rechnungId">Rechnungs-ID</param>
    /// <param name="rechnung">Rechnung mit den neuen Werten</param>
    [Put("/build/projekte/{projektId}/rechnungen/{rechnungId}")]
    Task GetRechnung(string projektId, Guid rechnungId, [Body] Rechnung rechnung);

    /// <summary>
    /// Löscht die Rechnung mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="rechnungId">Rechnungs-ID</param>
    /// <param name="positionsbloeckeLoeschen">Falls true (Default: false), werden zur Rechnung gehörende
    /// Positionsblöcke ebenfalls gelöscht</param>
    [Get("/build/projekte/{projektId}/rechnungen/{rechnungId}?positionsbloeckeLoeschen={positionsbloeckeLoeschen}")]
    Task DeleteRechnung(string projektId, Guid rechnungId, bool positionsbloeckeLoeschen = false);

    /// <summary>
    /// Liefert die zu einem Leistungsverzeichnis (Auftrag) gehörenden Positionsblöcke.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    /// <param name="mengenArt">Die gewünschte Mengenart</param>
    /// <param name="mitZeilen">Falls true (= Default), werden die Aufmaßzeilen mitgeliefert</param>
    /// <param name="aufmassblattId">Falls angegeben, werden nur Positionsblöcke zurückgegeben, die zu diesem
    /// Aufmaßblatt gehören.
    /// Falls "00000000-0000-0000-0000-000000000000", wird nach Positionsblöcken ohne Aufmaßblatt gefiltert.</param>
    /// <param name="leistungszeitraumId">Falls angegeben, werden nur Positionsblöcke zurückgegeben, die zu diesem
    /// Leistungszeitraum gehören.
    /// Falls "00000000-0000-0000-0000-000000000000", wird nach Positionsblöcken ohne Leistungszeitraum
    /// gefiltert.</param>
    /// <param name="rechnungId">Falls angegeben, werden nur Positionsblöcke zurückgegeben, die zu dieser Rechnung
    /// gehören.
    /// Falls "00000000-0000-0000-0000-000000000000", wird nach Positionsblöcken ohne Rechnung gefiltert.</param>
    [Get(
        "/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/positionsbloecke?mengenArt?{mengenArt}&mitZeilen={mitZeilen}&aufmassblattId={aufmassblattId}&leistungszeitraumId={leistungszeitraumId}&rechnungId={rechnungId}")]
    Task<IEnumerable<Positionsblock>> GetPositionsblöcke(
        string projektId,
        Guid lvId,
        MengenArt mengenArt = MengenArt.Abrechnung,
        bool mitZeilen = true,
        Guid? aufmassblattId = null,
        Guid? leistungszeitraumId = null,
        Guid? rechnungId = null);

    /// <summary>
    /// Fügt einem Leistungsverzeichnis einen neuen Positionsblock hinzu.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    /// <param name="newPositionsblockInfo">Informationen zum neuen Positionsblock</param>
    [Post("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/positionsbloecke")]
    Task<Positionsblock> CreatePositionsblock(string projektId, Guid lvId,
        [Body] NewPositionsblockInfo newPositionsblockInfo);

    /// <summary>
    /// Liefert einen Positionsblock.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="positionsblockId">Positionsblock-ID</param>
    [Get("/build/projekte/{projektId}/positionsbloecke/{positionsblockId}")]
    Task<Positionsblock> GetPositionsblock(string projektId, Guid positionsblockId);

    /// <summary>
    /// Aktualisiert einen Positionsblock.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="positionsblockId">Positionsblock-ID</param>
    /// <param name="positionsblock">Positionsblock mit den neuen Werten</param>
    [Put("/build/projekte/{projektId}/positionsbloecke/{positionsblockId}")]
    Task UpdatePositionsblock(string projektId, Guid positionsblockId, [Body] Positionsblock positionsblock);

    /// <summary>
    /// Löscht den Positionsblock mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="positionsblockId">Positionsblock-ID</param>
    [Delete("/build/projekte/{projektId}/positionsbloecke/{positionsblockId}")]
    Task DeletePositionsblock(string projektId, Guid positionsblockId);

    /// <summary>
    /// Liefert die Kalkulation mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="kalkulationId">ID der Kalkulation</param>
    /// <param name="mitErgebnissen">Falls true (Default: false), werden auch berechnete Werte mitgeliefert (im Feld KalkulationErgebnisse)</param>
    /// <param name="mengenArt">Die gewünschte Mengenart (Default: LV-Menge)</param>
    [Get(
        "/build/projekte/{projektId}/kalkulationen/{kalkulationId}?mitErgebnissen={mitErgebnissen}&mengenArt={mengenArt}")]
    Task<Kalkulation> GetKalkulation(
        string projektId, Guid kalkulationId, bool mitErgebnissen = false, MengenArt mengenArt = MengenArt.Lv);

    /// <summary>
    /// Erzeugt eine neue untergeordnete Kalkulation.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="parentKalkulationId">ID der Parent-Kalkulation</param>
    /// <param name="newKalkulationInfo">Informationen zur neuen Kalkulation</param>
    [Post("/build/projekte/{projektId}/kalkulationen/{parentKalkulationId}/kalkulationen")]
    Task<Kalkulation> CreateUntergeordneteKalkulation(string projektId, Guid parentKalkulationId,
        [Body] NewKalkulationInfo newKalkulationInfo);

    /// <summary>
    /// Aktualisiert eine Kalkulation. Untergeordnete Kalkulationen werden nicht berücksichtigt.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="kalkulationId">Kalkulations-ID</param>
    /// <param name="kalkulation">Kalkulation mit den neuen Werten</param>
    [Put("/build/projekte/{projektId}/kalkulationen/{kalkulationId}")]
    Task UpdateKalkulation(string projektId, Guid kalkulationId, [Body] Kalkulation kalkulation);

    /// <summary>
    /// Löscht die Kalkulation mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="kalkulationId">Kalkulations-ID</param>
    [Delete("/build/projekte/{projektId}/kalkulationen/{kalkulationId}")]
    Task DeleteKalkulation(string projektId, Guid kalkulationId);

    /// <summary>
    /// Liefert alle Kalkulationblätter einer Kalkulation. Die Detailinfos (KalkulationsBlatt.Details)
    /// werden grundsätzlich mitgeliefert, die Kalkulationszeilen (KalkulationsBlatt.Details.Zeilen) jedoch nur
    /// falls mitZeilen = true.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="kalkulationId">Die Kalkulations-ID</param>
    /// <param name="mitZeilen">Falls true (Default: false), werden auch die Kalkulationszeilen mitgeliefert</param>
    [Get("/build/projekte/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter?mitZeilen={mitZeilen}")]
    Task<List<KalkulationsBlatt>> GetKalkulationsBlätter(string projektId, Guid kalkulationId, bool mitZeilen = false);

    /// <summary>
    /// Liefert das Kalkulationblatt für die angegebene Position (einschließlich Kalkulationszeilen).
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="kalkulationId">Die Kalkulations-ID</param>
    /// <param name="positionId">Die Positions-ID</param>
    /// <param name="createNewIfNecessary">Falls true (Default: false), wird ein neues Kalkulationsblatt erzeugt,
    /// falls es für die angegebene Position noch keines gibt</param>
    /// <param name="includeParentKalkulationen">Falls true (Default: false), werden bei der Suche auch
    /// Parent-Kalkulationen berücksichtigt</param>
    [Get(
        "/build/projekte/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter/{positionId}?createNewIfNecessary={createNewIfNecessary}&includeParentKalkulationen={includeParentKalkulationen}")]
    Task<KalkulationsBlatt> GetKalkulationsBlatt(string projektId, Guid kalkulationId, Guid positionId,
        bool createNewIfNecessary = false, bool includeParentKalkulationen = false);

    /// <summary>
    /// Aktualisiert das Kalkulationsblatt der angegebenen Position (einschließlich Kalkulationszeilen).
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="kalkulationId">Die Kalkulations-ID</param>
    /// <param name="positionId">Die Positions-ID</param>
    /// <param name="kalkulationsBlatt">Kalkulationsblatt mit den neuen Werten</param>
    [Put("/build/projekte/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter/{positionId}")]
    Task UpdateKalkulationsBlatt(string projektId, Guid kalkulationId, Guid positionId,
        [Body] KalkulationsBlatt kalkulationsBlatt);

    /// <summary>
    /// Löscht das Kalkulationsblatt der angegebenen Position (einschließlich Kalkulationszeilen).
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="kalkulationId">Die Kalkulations-ID</param>
    /// <param name="positionId">Die Positions-ID</param>
    [Delete("/build/projekte/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter/{positionId}")]
    Task DeleteKalkulationsBlatt(string projektId, Guid kalkulationId, Guid positionId);

    /// <summary>
    /// Liefert die Betriebsmittel eines Projekts. Falls zudem Informationen zu den Kostenbenene
    /// benötigt werden, sollte <see cref="GetAllBetriebsmittelEx"/> genutzt werden.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="art">Falls != null, werden nur Betriebsmittel dieses Typs ermittelt. Falls 'art' eine Gruppenart ist, werden nur die passenden Gruppen,
    /// und zwar immer in hierarchischer Form und ohne Kosten, geliefert.</param>
    /// <param name="mitGruppen">Falls true (= Default), werden auch Gruppenelemente ermittelt, und das Ergebnis wird in hierarchischer Form aufbereitet.</param>
    /// <param name="mitKosten">Falls true (Default = false), werden auch die Kosten ermittelt (Property 'kosten').</param>
    /// <param name="mitWeiterenKosten">Falls true (Default = false), werden auch die weiteren Kosten ermittelt (Property 'weitereKosten').</param>
    /// <param name="mitZuschlaegen">Falls true (Default = false), werden auch die Zuschläge ermittelt (Property 'zuschläge').</param>
    /// <param name="mitDetails">Falls true (= Default), werden auch die Detailinformationen ermittelt (Properties 'details', 'lohnDetails' usw.).</param>
    /// <param name="kostenebeneId">Die ID der Kostenebene, z.B. der Kalkulation. Falls null (= Default), wird das Projekt verwendet.</param>
    /// <param name="zuschlagsebeneId">Die ID der Zuschlagsebene, z.B. der Kalkulation. Falls null (= Default), wird das Projekt verwendet.</param>
    [Get(
        "/build/projekte/{projektId}/betriebsmittel?art={art}&mitGruppen={mitGruppen}&mitKosten={mitKosten}&mitWeiterenKosten={mitWeiterenKosten}&mitZuschlaegen={mitZuschlaegen}&mitDetails={mitDetails}&kostenebeneId={kostenebeneId}&zuschlagsebeneId={zuschlagsebeneId}")]
    Task<List<Betriebsmittel>> GetAllBetriebsmittel(
        string projektId,
        BetriebsmittelArt? art,
        bool mitGruppen = true,
        bool mitKosten = false,
        bool mitWeiterenKosten = false,
        bool mitZuschlaegen = false,
        bool mitDetails = true,
        Guid? kostenebeneId = null,
        Guid? zuschlagsebeneId = null);

    /// <summary>
    /// Liefert die Betriebsmittel eines Projekts sowie Informationen zu den Kostenebenen.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="art">Falls != null, werden nur Betriebsmittel dieses Typs ermittelt. Falls 'art' eine Gruppenart ist, werden nur die passenden Gruppen,
    /// und zwar immer in hierarchischer Form und ohne Kosten, geliefert.</param>
    /// <param name="mitGruppen">Falls true (= Default), werden auch Gruppenelemente ermittelt, und das Ergebnis wird in hierarchischer Form aufbereitet.</param>
    /// <param name="mitKosten">Falls true (Default = false), werden auch die Kosten ermittelt (Property 'kosten').</param>
    /// <param name="mitWeiterenKosten">Falls true (Default = false), werden auch die weiteren Kosten ermittelt (Property 'weitereKosten').</param>
    /// <param name="mitZuschlaegen">Falls true (Default = false), werden auch die Zuschläge ermittelt (Property 'zuschläge').</param>
    /// <param name="mitDetails">Falls true (= Default), werden auch die Detailinformationen ermittelt (Properties 'details', 'lohnDetails' usw.).</param>
    /// <param name="kostenebeneId">Die ID der Kostenebene, z.B. der Kalkulation. Falls null (= Default), wird das Projekt verwendet.</param>
    /// <param name="zuschlagsebeneId">Die ID der Zuschlagsebene, z.B. der Kalkulation. Falls null (= Default), wird das Projekt verwendet.</param>
    [Get(
        "/build/projekte/{projektId}/betriebsmittel_ex?art={art}&mitGruppen={mitGruppen}&mitKosten={mitKosten}&mitWeiterenKosten={mitWeiterenKosten}&mitZuschlaegen={mitZuschlaegen}&mitDetails={mitDetails}&kostenebeneId={kostenebeneId}&zuschlagsebeneId={zuschlagsebeneId}")]
    Task<BetriebsmittelResult> GetAllBetriebsmittelEx(
        string projektId,
        BetriebsmittelArt? art,
        bool mitGruppen = true,
        bool mitKosten = false,
        bool mitWeiterenKosten = false,
        bool mitZuschlaegen = false,
        bool mitDetails = true,
        Guid? kostenebeneId = null,
        Guid? zuschlagsebeneId = null);

    /// <summary>
    /// Liefert das Betriebsmittel mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="betriebsmittelId">Betriebsmittel-ID</param>
    /// <param name="art">Betriebsmittelart (optional: Angabe wird für performanteres Laden genutzt)</param>
    /// <param name="kostenebeneId">Die ID der Kostenebene, z.B. der Kalkulation. Falls null (= Default), wird das Projekt verwendet.</param>
    /// <param name="zuschlagsebeneId">Die ID der Zuschlagsebene, z.B. der Kalkulation. Falls null (= Default), wird das Projekt verwendet.</param>
    [Get(
        "/build/projekte/{projektId}/betriebsmittel/{betriebsmittelId}?art={art}&kostenebeneId={kostenebeneId}&zuschlagsebeneId={zuschlagsebeneId}")]
    Task<Betriebsmittel> GetBetriebsmittel(
        string projektId,
        Guid betriebsmittelId,
        BetriebsmittelArt? art = null,
        Guid? kostenebeneId = null,
        Guid? zuschlagsebeneId = null);

    /// <summary>
    /// Liefert ein bestimmtes Betriebsmittel des Projekts über seine Nummer.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="nummer">Vollständige Betriebsmittelnummer (z.B. "M24.211")</param>
    /// <param name="kostenebeneId">Die ID der Kostenebene, z.B. der Kalkulation. Falls null (= Default),
    /// wird für Build das Projekt und für Success X die Kalkulation verwendet.</param>
    /// <param name="zuschlagsebeneId">Die ID der Zuschlagsebene, z.B. der Kalkulation. Falls null (= Default),
    /// wird das für Build das Projekt und für Success X die Kalkulation verwendet.</param>
    [Get("/build/projekte/{projektId}/betriebsmittel_by_nummer/{nummer}")]
    Task<Betriebsmittel> GetBetriebsmittelByNummer(
        string projektId,
        string nummer,
        Guid? kostenebeneId = null,
        Guid? zuschlagsebeneId = null);

    /// <summary>
    /// Fügt einem Projekt ein neues Betriebsmittel hinzu.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="newBetriebsmittelInfo">Informationen zum neuen Betriebsmittel</param>
    [Post("/build/projekte/{projektId}/betriebsmittel")]
    Task<Betriebsmittel> CreateBetriebsmittel(string projektId, [Body] NewBetriebsmittelInfo newBetriebsmittelInfo);

    /// <summary>
    /// Aktualisiert ein Betriebsmittel.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="betriebsmittelId">Betriebsmittel-ID</param>
    /// <param name="betriebsmittel">Betriebsmittel mit den neuen Werten</param>
    [Put("/build/projekte/{projektId}/betriebsmittel/{betriebsmittelId}")]
    Task UpdateBetriebsmittel(string projektId, Guid betriebsmittelId, [Body] Betriebsmittel betriebsmittel);

    /// <summary>
    /// Löscht das Betriebsmittel mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="betriebsmittelId">Betriebsmittel-ID</param>
    /// <param name="loescheKalkulationszeilen">Falls true (Default = false), werden auch
    /// die darauf verweisenden Kalkulationszeilen gelöscht. Ansonsten führt das Vorhandensein
    /// derartiger Kalkulationszeilen zu einem 400-Fehler (Bad Request).</param>
    [Delete(
        "/build/projekte/{projektId}/betriebsmittel/{betriebsmittelId}?loescheKalkulationszeilen={loescheKalkulationszeilen}")]
    Task DeleteBetriebsmittel(string projektId, Guid betriebsmittelId, bool loescheKalkulationszeilen = false);

    /// <summary>
    /// Erzeugt mehrere Betriebsmittel in diesem Projekt durch Übernahme aus einem Betriebsmittelstamm.
    /// Betriebsmittel, die in den weiteren Kosten verwendet werden, werden ebenfalls übernommen.
    /// Es werden keine bestehenden Betriebsmittel überschrieben.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="transferInfo">Enthält die IDs der zu übernehmenden Betriebsmittel</param>
    /// <returns>Ergebnisobjekt, das die Liste der IDs der neu erzeugten Projekt-Betriebsmittel enthält</returns>
    [Post("/build/projekte/{projektId}/betriebsmittel/create_collection_from_stamm")]
    Task<BetriebsmittelCollectionTransferResult> CreateBetriebsmittelCollectionFromStamm(
        string projektId, BetriebsmittelCollectionTransferFromStammInfo transferInfo);

    /// <summary>
    /// Liefert die Projektdaten für die Bautagesberichte des Projekt.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    [Get("/build/projekte/{projektId}/bautagesberichte/projektdaten")]
    Task<BautagebuchProjektdaten> GetBautagesberichtProjektdaten(string projektId);

    /// <summary>
    /// Liefert die zu einem Leistungsverzeichnis (Auftrag) gehörenden Bautagesberichte.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="lvId">LV-ID</param>
    [Get("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/bautagesberichte")]
    Task<List<Bautagesbericht>> GetBautagesberichte(string projektId, Guid lvId);

    /// <summary>
    /// Liefert einen Bautagesbericht.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="bautagesberichtId">Bautagesbericht-ID</param>
    /// <param name="mitAbrechnung">Falls true (= Default), werden die Abrechnungsdaten (Leistungsabrechnung, Regieabrechnung) mitgeliefert (in der Property 'leistungsabrechnung', 'regieabrechnung')</param>
    [Get("/build/projekte/{projektId}/bautagesberichte/{bautagesberichtId}?mitAbrechnung={mitAbrechnung}")]
    Task<Bautagesbericht> GetBautagesbericht(string projektId, Guid bautagesberichtId, bool mitAbrechnung = true);

    /// <summary>
    /// Aktualisiert einen Bautagesbericht.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="bautagesberichtId">Bautagesbericht-ID</param>
    /// <param name="bautagesbericht">Bautagesbericht mit den neuen Werten</param>
    [Put("/build/projekte/{projektId}/bautagesberichte/{bautagesberichtId}")]
    Task UpdateBautagesbericht(string projektId, Guid bautagesberichtId, [Body] Bautagesbericht bautagesbericht);

    /// <summary>
    /// Löscht den Bautagesbericht mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="bautagesberichtId">Bautagesbericht-ID</param>
    [Delete("/build/projekte/{projektId}/bautagesberichte/{bautagesberichtId}")]
    Task DeleteBautagesbericht(string projektId, Guid bautagesberichtId);

    /// <summary>
    /// Liefert die zu einem Projekt gehörenden Bauarbeitsschluessel.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    [Get("/build/projekte/{projektId}/bauarbeitsschluessel")]
    public Task<List<Bauarbeitsschluessel>> GetBauarbeitsschluessel(string projektId);

    /// <summary>
    /// Liefert berechnete Werte des Projekts (einschließlich aller Leistungsverzeichnisse).
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    [Get("/build/projekte/{projektId}/rechenwerte")]
    Task<ProjektRechenwerte> GetRechenwerte(string projektId);

    /// <summary>
    /// Liefert den Gliederungskatalog mit der angegebenen ID.
    /// </summary>
    /// <param name="projektId">Projekt-ID</param>
    /// <param name="gliederungsKatalogId">ID des Gliederungskatalogs</param>
    [Get("/build/projekte/{projektId}/gliederungskataloge/{gliederungsKatalogId}")]
    Task<Bautagesbericht> GetGliederungsKatalog(string projektId, Guid gliederungsKatalogId);
}