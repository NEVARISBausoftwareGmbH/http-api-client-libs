#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace Nevaris.Build.ClientApi;

/// <summary>
/// Interface zum Zugriff auf Stammdaten-Operationen über die NEVARIS Build API.
/// </summary>
public interface IStammApi
{
    /// <summary>
    /// Liefert Versionsinformationen.
    /// </summary>
    /// <returns>Ein Objekt, das die Build-Programmversion und die Versionsnummer der HTTP API enthält</returns>
    [Get("/build/global/version")]
    Task<VersionInfo> GetVersion();

    /// <summary>
    /// Liefert alle globalen Adressen.
    /// </summary>
    [Get("/build/global/adressen")]
    Task<List<Adresse>> GetAdressen();

    /// <summary>
    /// Liefert die Adresse mit dem angegebenen Code.
    /// </summary>
    /// <param name="code">Adresscode</param>
    [Get("/build/global/adressen/{code}")]
    Task<Adresse> GetAdresse(string code);

    /// <summary>
    /// Erstellt eine neue Adresse.
    /// </summary>
    /// <param name="newAdresseInfo">Informationen zur neuen Adresse</param>
    [Post("/build/global/adressen")]
    Task<Adresse> CreateAdresse([Body] NewAdresseInfo newAdresseInfo);

    /// <summary>
    /// Aktualisiert eine Adresse.
    /// </summary>
    /// <param name="code">Adresscode</param>
    /// <param name="adresse">Adresse mit den neuen Werten</param>
    [Put("/build/global/adressen/{code}")]
    Task UpdateAdresse(string code, [Body] Adresse adresse);

    /// <summary>
    /// Löscht die Adresse mit dem angegebenen Code.
    /// </summary>
    /// <param name="code">Adresscode</param>
    [Delete("/build/global/adressen/{code}")]
    Task DeleteAdresse(string code);

    /// <summary>
    /// Liefert alle Speicherorte.
    /// </summary>
    /// <returns>Liste von Speicherorten (ohne ProjektInfos)</returns>
    [Get("/build/global/speicherorte")]
    Task<List<Speicherort>> GetSpeicherorte();

    /// <summary>
    /// Liefert den Speicherort mit der angegebenen ID.
    /// </summary>
    /// <param name="id">Speicherort-ID</param>
    [Get("/build/global/speicherorte/{id}")]
    Task<Speicherort> GetSpeicherort(Guid id);

    /// <summary>
    /// Erstellt ein neues Projekte an dem angegebenen Speicherort.
    /// </summary>
    /// <param name="speicherortId">Speicherort-ID</param>
    /// <param name="newProjekt">Projektinformationen</param>
    [Post("/build/global/speicherorte/{speicherortId}/projekte")]
    Task<Projekt> CreateProjekt(Guid speicherortId, [Body] NewProjektInfo newProjekt);

    /// <summary>
    /// Liefert alle Betriebsmittelstämme.
    /// </summary>
    [Get("/build/global/betriebsmittelstaemme")]
    Task<List<BetriebsmittelStamm>> GetBetriebsmittelStämme();

    /// <summary>
    /// Liefert den Betriebsmittelstamm mit der angegebenen ID.
    /// </summary>
    /// <param name="betriebsmittelStammId">Betriebsmittelstamm-ID</param>
    [Get("/build/global/betriebsmittelstaemme/{betriebsmittelStammId}")]
    Task<BetriebsmittelStamm> GetBetriebsmittelStamm(Guid betriebsmittelStammId);

    /// <summary>
    /// Erzeugt einen neuen Betriebsmittelstamm.
    /// </summary>
    /// <param name="newBetriebsmittelstammInfo">Informationen zum neuen Betriebsmittelstamm</param>
    [Post("/build/global/betriebsmittelstaemme")]
    Task<BetriebsmittelStamm> CreateBetriebsmittelStamm([Body] NewBetriebsmittelStammInfo newBetriebsmittelstammInfo);

    /// <summary>
    /// Aktualisiert einen Betriebsmittelstamm (ohne Berücksichtigung der Betriebsmittel).
    /// </summary>
    /// <param name="betriebsmittelStammId">Betriebsmittelstamm-ID</param>
    /// <param name="betriebsmittelstamm">Betriebsmittelstamm mit den neuen Werten</param>
    [Put("/build/global/betriebsmittelstaemme/{betriebsmittelStammId}")]
    Task UpdateBetriebsmittelStamm(Guid betriebsmittelStammId, [Body] BetriebsmittelStamm betriebsmittelstamm);

    /// <summary>
    /// Löscht einen Betriebsmittelstamm.
    /// </summary>
    /// <param name="betriebsmittelStammId"></param>
    [Delete("/build/global/betriebsmittelstaemme/{betriebsmittelStammId}")]
    Task DeleteBetriebsmittelStamm(Guid betriebsmittelStammId);

    /// <summary>
    /// Liefert alle Betriebsmittel eines Betriebsmittelstammes.
    /// </summary>
    /// <param name="betriebsmittelStammId">Betriebsmittelstamm-ID</param>
    /// <param name="art">Falls != null, werden nur Betriebsmittel dieses Typs ermittelt. Falls 'art' eine Gruppenart ist, werden nur die passenden Gruppen,
    /// und zwar immer in hierarchischer Form und ohne Kosten, geliefert.</param>
    /// <param name="mitGruppen">Falls true (= Default), werden auch Gruppenelemente ermittelt, und das Ergebnis wird in hierarchischer Form aufbereitet.</param>
    /// <param name="mitKosten">Falls true (Default = false), werden auch die Kosten ermittelt (Property 'kosten').</param>
    /// <param name="mitWeiterenKosten">Falls true (Default = false), werden auch die weiteren Kosten ermittelt (Property 'weitereKosten').</param>
    /// <param name="mitZuschlaegen">Falls true (Default = false), werden auch die Zuschläge ermittelt (Property 'zuschläge').</param>
    /// <param name="mitDetails">Falls true (= Default), werden auch die Detailinformationen ermittelt (Properties 'details', 'lohnDetails' usw.).</param>
    /// <param name="kostenebeneId">Die ID der Kostenebene, das heißt des Kostenkatalogs. Falls null (= Default), wird der Standardkostenkatalog verwendet.</param>
    /// <param name="zuschlagsebeneId">Die ID der Zuschlagsebene, das heißt des Zuschlagskatalogs. Falls null (= Default), wird der Standardzuschlagskatalog verwendet.</param>
    [Get(
        "/build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel?art={art}&mitGruppen={mitGruppen}&mitKosten={mitKosten}&mitWeiterenKosten={mitWeiterenKosten}&mitZuschlaegen={mitZuschlaegen}&mitDetails={mitDetails}&kostenebeneId={kostenebeneId}&zuschlagsebeneId={zuschlagsebeneId}")]
    Task<List<Betriebsmittel>> GetAllBetriebsmittel(
        Guid betriebsmittelStammId,
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
    /// <param name="betriebsmittelId">Betriebsmittel-ID</param>
    /// <param name="art">Betriebsmittelart (optional: Angabe wird für performanteres Laden genutzt)</param>
    /// <param name="kostenebeneId">Die ID der Kostenebene, das heißt des Kostenkatalogs. Falls null (= Default), wird der Standardkostenkatalog verwendet.</param>
    /// <param name="zuschlagsebeneId">Die ID der Zuschlagsebene, das heißt des Zuschlagskatalogs. Falls null (= Default), wird der Standardzuschlagskatalog verwendet.</param>
    [Get(
        "/build/global/betriebsmittel/{betriebsmittelId}?art={art}&kostenebeneId={kostenebeneId}&zuschlagsebeneId={zuschlagsebeneId}")]
    Task<Betriebsmittel> GetBetriebsmittel(
        Guid betriebsmittelId,
        BetriebsmittelArt? art = null,
        Guid? kostenebeneId = null,
        Guid? zuschlagsebeneId = null);

    /// <summary>
    /// Fügt einem Betriebsmittelstamm ein neues Betriebsmittel hinzu.
    /// </summary>
    /// <param name="betriebsmittelStammId">Betriebsmittelstamm-ID</param>
    /// <param name="newBetriebsmittelInfo">Informationen zum neuen Betriebsmittel</param>
    [Post("/build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel")]
    Task<Betriebsmittel> CreateBetriebsmittel(Guid betriebsmittelStammId, [Body] NewBetriebsmittelInfo newBetriebsmittelInfo);

    /// <summary>
    /// Erzeugt mehrere vollständig initialisierte Betriebsmittel (einschließlich Gruppen).
    /// </summary>
    /// <param name="betriebsmittelStammId">Betriebsmittelstamm-ID</param>
    /// <param name="betriebsmittelListe">Liste von zu erzeugenden Betriebsmitteln. Dabei kann es sich auch um Gruppen
    /// handeln, die selbst wiederum Betriebsmittel enthalten.</param>
    /// <returns>Objekt, das die IDs der erzeugten Betriebsmittel enthält (ohne untergeordnete Betriebsmittel)</returns>
    [Post("/build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel_collection")]
    Task<BetriebsmittelCollectionResult> CreateBetriebsmittelCollection(Guid betriebsmittelStammId, [Body] IReadOnlyCollection<Betriebsmittel> betriebsmittelListe);

    /// <summary>
    /// Aktualisiert ein Betriebsmittel.
    /// </summary>
    /// <param name="betriebsmittelId">Betriebsmittel-ID</param>
    /// <param name="betriebsmittel">Betriebsmittel mit den neuen Werten</param>
    [Put("/build/global/betriebsmittel/{betriebsmittelId}")]
    Task UpdateBetriebsmittel(Guid betriebsmittelId, [Body] Betriebsmittel betriebsmittel);

    /// <summary>
    /// Aktualisiert die Kosten von mehreren Betriebsmitteln. Ist wesentlich performanter als
    /// eine Aktualisierung per PUT /build/global/betriebsmittel für jedes einzelnde Betriebsmittel.
    /// </summary>
    /// <param name="updateInfos">Liste von Objekten mit den neuen Kosten. Siehe BetriebsmittelKostenUpdateInfo für genauere Informationen zur Verwendung.</param>
    [Post("/build/global/betriebsmittel/kosten_collection_update")]
    Task UpdateBetriebsmittelKostenCollection([Body] IReadOnlyCollection<BetriebsmittelKostenUpdateInfo> updateInfos);

    /// <summary>
    /// Löscht das Betriebsmittel mit der angegebenen ID.
    /// </summary>
    /// <param name="betriebsmittelId">Betriebsmittel-ID</param>
    [Delete("/build/global/betriebsmittel/{betriebsmittelId}")]
    Task DeleteBetriebsmittel(Guid betriebsmittelId);

    /// <summary>
    /// Liefert alle Mandaten, einschließlich Niederlassungen.
    /// </summary>
    [Get("/build/global/mandanten")]
    Task<List<Mandant>> GetMandaten();

    /// <summary>
    /// Liefert alle Kostenarten einschließlich eingebettete Kostenarten.
    /// </summary>
    [Get("/build/global/kostenarten")]
    Task<List<Kostenart>> GetKostenarten();

    /// <summary>
    /// Liefert eine Kostenart.
    /// </summary>
    /// <param name="kostenartNummer">Nummer der Kostenart</param>
    [Get("/build/global/kostenarten/{kostenartNummer}")]
    Task<Kostenart> GetKostenart(string kostenartNummer);

    /// <summary>
    /// Erstellt eine neue Kostenart.
    /// </summary>
    /// <param name="newKostenartInfo">Informationen zur neuen Kostenart</param>
    [Post("/build/global/kostenarten")]
    Task<Kostenart> CreateKostenart([Body] NewKostenartInfo newKostenartInfo);

    /// <summary>
    /// Aktualisiert eine Kostenart.
    /// </summary>
    /// <param name="kostenartNummer">Nummer der Kostenart</param>
    /// <param name="kostenart">Kostenart mit den neuen Werten</param>
    [Put("/build/global/kostenarten/{kostenartNummer}")]
    Task CreateKostenart(string kostenartNummer, [Body] Kostenart kostenart);

    /// <summary>
    /// Löscht eine Kostenart.
    /// </summary>
    /// <param name="kostenartNummer">Nummer der Kostenart</param>
    [Delete("/build/global/kostenarten/{kostenartNummer}")]
    Task DeleteKostenart(string kostenartNummer);
}