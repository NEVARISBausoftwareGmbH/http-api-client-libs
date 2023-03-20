using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS1591

namespace Nevaris.Build.ClientApi.Fianance
{
    public interface IFinanceApi
    {
        ///////////////
        // Debitoren //
        ///////////////

        [Get("/finance/debitoren?company={mandantId}&mitDetails={mitDetails}&aenderungsdatumVon={änderungsdatumVon}&aenderungsdatumBis={änderungsdatumBis}")]
        Task<List<Debitor>> GetDebitoren(string mandantId, bool mitDetails = false, DateTime? änderungsdatumVon = null, DateTime? änderungsdatumBis = null);

        [Get("/finance/debitoren/{debitorId}?company={mandantId}")]
        Task<Debitor> GetDebitor(string mandantId, string debitorId);

        [Post("/finance/debitoren?company={mandantId}")]
        Task<Debitor> CreateDebitor(string mandantId, [Body] NewDebitorInfo newDebitorInfo);

        [Put("/finance/debitoren/{debitorId}?company={mandantId}")]
        Task UpdateDebitor(string mandantId, string debitorId, [Body] Debitor debitor);

        [Delete("/finance/debitoren/{debitorId}?company={mandantId}")]
        Task DeleteDebitor(string mandantId, string debitorId);

        ////////////////
        // Kreditoren //
        ////////////////

        [Get("/finance/kreditoren?company={mandantId}&mitDetails={mitDetails}&aenderungsdatumVon={änderungsdatumVon}&aenderungsdatumBis={änderungsdatumBis}")]
        Task<List<Kreditor>> GetKreditoren(string mandantId, bool mitDetails = false, DateTime? änderungsdatumVon = null, DateTime? änderungsdatumBis = null);

        [Get("/finance/kreditoren/{kreditorId}?company={mandantId}")]
        Task<Kreditor> GetKreditor(string mandantId, string kreditorId);

        [Post("/finance/kreditoren?company={mandantId}")]
        Task<Kreditor> CreateKreditor(string mandantId, [Body] NewKreditorInfo newKreditorInfo);

        [Put("/finance/kreditoren/{kreditorId}?company={mandantId}")]
        Task UpdateKreditor(string mandantId, string kreditorId, [Body] Kreditor kreditor);

        [Delete("/finance/kreditoren/{kreditorId}?company={mandantId}")]
        Task DeleteKreditor(string mandantId, string kreditorId);

        ////////////////////////
        // Wartungspositionen //
        ////////////////////////

        [Get("/finance/wartungspositionen?company={mandantId}&aenderungsdatumVon={änderungsdatumVon}&aenderungsdatumBis={änderungsdatumBis}")]
        Task<List<Wartungsposition>> GetWartungspositionen(string mandantId, DateTime? änderungsdatumVon = null, DateTime? änderungsdatumBis = null);
    }
}
