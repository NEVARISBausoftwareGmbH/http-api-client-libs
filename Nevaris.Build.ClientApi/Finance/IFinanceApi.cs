using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#pragma warning disable CS1591

namespace Nevaris.Build.ClientApi.Finance;

public interface IFinanceApi
{
    ///////////////
    // Debitoren //
    ///////////////

    [Get("/finance/debitoren")]
    Task<List<Debitor>> GetDebitoren(
        [AliasAs("company")] string mandantId,
        bool mitDetails = false,
        [AliasAs("aenderungsdatumVon")] DateTime? änderungsdatumVon = null,
        [AliasAs("aenderungsdatumBis")] DateTime? änderungsdatumBis = null);

    [Get("/finance/debitoren/{debitorId}")]
    Task<Debitor> GetDebitor([AliasAs("company")] string mandantId, string debitorId);

    [Post("/finance/debitoren")]
    Task<Debitor> CreateDebitor([AliasAs("company")] string mandantId, [Body] NewDebitorInfo newDebitorInfo);

    [Put("/finance/debitoren/{debitorId}")]
    Task UpdateDebitor([AliasAs("company")] string mandantId, string debitorId, [Body] Debitor debitor);

    [Delete("/finance/debitoren/{debitorId}")]
    Task DeleteDebitor([AliasAs("company")] string mandantId, string debitorId);

    ////////////////
    // Kreditoren //
    ////////////////

    [Get("/finance/kreditoren")]
    Task<List<Kreditor>> GetKreditoren(
        [AliasAs("company")] string mandantId,
        bool mitDetails = false,
        [AliasAs("aenderungsdatumVon")] DateTime? änderungsdatumVon = null,
        [AliasAs("aenderungsdatumBis")] DateTime? änderungsdatumBis = null);

    [Get("/finance/kreditoren/{kreditorId}")]
    Task<Kreditor> GetKreditor([AliasAs("company")] string mandantId, string kreditorId);

    [Post("/finance/kreditoren")]
    Task<Kreditor> CreateKreditor([AliasAs("company")] string mandantId, [Body] NewKreditorInfo newKreditorInfo);

    [Put("/finance/kreditoren/{kreditorId}")]
    Task UpdateKreditor([AliasAs("company")] string mandantId, string kreditorId, [Body] Kreditor kreditor);

    [Delete("/finance/kreditoren/{kreditorId}")]
    Task DeleteKreditor([AliasAs("company")] string mandantId, string kreditorId);

    ////////////////////////
    // Wartungspositionen //
    ////////////////////////

    [Get("/finance/wartungspositionen")]
    Task<List<Wartungsposition>> GetWartungspositionen(
        [AliasAs("company")] string mandantId,
        [AliasAs("aenderungsdatumVon")] DateTime? änderungsdatumVon = null,
        [AliasAs("aenderungsdatumBis")] DateTime? änderungsdatumBis = null);
}