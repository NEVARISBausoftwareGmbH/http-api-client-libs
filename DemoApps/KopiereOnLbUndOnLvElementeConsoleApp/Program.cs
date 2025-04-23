using Nevaris.Build.ClientApi;

internal class Program
{
    private static async Task Main(string[] args)
    {
        try
        {
            // Verbindung zur RESTful API herstellen. Dies setzt voraus, dass auf dem lokalen Rechner
            // der NEVARIS Build Businessdienst läuft und die API hostet. Der erste Zugriff erfolgt
            // erst beim ersten API-Aufruf (hier: client.StammApi.GetSpeicherorte()).
            using var client = new NevarisBuildClient("https://localhost:8500", new NevarisBuildClientOptions
            {
                // Hier müssen Authentifizierungsdaten übergeben werden, außer die API wird per Build
                // gehostet oder DisableBuildApiAuthentication = true gesetzt.
                // Username = "<Username>",
                // Password = "<Password>"
            });

            // API-Version abfragen und auf Kompatibilität mit Client überprüfen
            var versionCheckResult = await client.CheckVersion();
            if (!versionCheckResult.AreVersionsCompatible)
            {
                throw new InvalidOperationException(
                    $"Versionskonflikt: API: {versionCheckResult.ApiVersion}, client: {versionCheckResult.ClientVersion}");
            }

            Console.WriteLine("Vorbereiten der Daten: ");
            Console.WriteLine();


            /* Importiere falls notwendig Leistungsbeschreibung für Demonstrationszwecke */
            Console.WriteLine("Ermittle existierende Leistungsbeschreibungen...");
            var alleLeistungsbeschreibungen = await client.StammApi.GetLeistungsbeschreibungen();
            Console.WriteLine($"Anzahl Leistungsbeschreibungen: {alleLeistungsbeschreibungen.Count}");

            var demoLb = alleLeistungsbeschreibungen.FirstOrDefault(_ => _.Kenndaten!.Kennung == "SCHOKO");

            if (demoLb == null)
            {
                Console.WriteLine("Leistungsbeschreibung nicht gefunden, erstelle importiere Leistungsbeschreibung...");
                using var stream = typeof(Program).Assembly.GetManifestResourceStream("KopiereOnLbUndOnLvElementeConsoleApp.Daten.LB-SCHOKO_V2021.onlb") ?? throw new InvalidOperationException("Resource LB-SCHOKO_V2021.onlb not found.");
                using var binaryReader = new BinaryReader(stream);
                var data = binaryReader.ReadBytes((int)stream.Length);

                var importLbResult = await client.StammApi.ImportiereLeistungsbeschreibung(new ImportiereLeistungsbeschreibungInfo() { Data = data });

                if (importLbResult.LeistungsbeschreibungId != null)
                {
                    demoLb = await client.StammApi.GetLeistungsbeschreibung(importLbResult.LeistungsbeschreibungId.Value);
                    if (demoLb != null)
                    { Console.WriteLine("Leistungsbeschreibung erfolgreich importiert"); }
                    else 
                    { Console.WriteLine("Leistungsbeschreibung konnte nicht importiert werden"); return; }
                }
                else
                {
                    Console.WriteLine("Leistungsbeschreibung konnte nicht importiert werden");
                    return;
                }
            }
            else
            {
                Console.WriteLine($"Leistungsbeschreibung {demoLb.Bezeichnung} existiert bereits.");
            }

            Console.WriteLine();   
            Console.WriteLine("Erstelle neues Projekt...");

            /* Ermittle alle Knoten und Items des Demo LBs */
            var alleLbKnotenUndItems = await ErmittleLbElementeAsync(client, demoLb.Id!.Value);

            var speicherorte = await client.StammApi.GetSpeicherorte();
            if (speicherorte.Count == 0)
            {
                Console.WriteLine("Es existiert kein Speicherort");
                return;
            }

            var speicherort = speicherorte.First();

            /* Lege ein neues Projekt mit einer LG Struktur an */
            (Projekt projekt, Leistungsverzeichnis lv) = await ErzeugeProjektAsync(client, speicherort.Id, "Testprojekt", GliederungsArt.LgGliederung);


            /* Übernimm alle Elemente des LBs in das LV */
            Console.WriteLine("Übernehme alle Elemente des LBs in das LV...");
            await client.ProjektApi.PasteOnLbDataIntoOnLv(projekt.Id, lv.Id, new PasteToLvOptionen()
            {
                QuellLeistungsbeschreibungId = demoLb.Id,
                QuellElemente = [.. alleLbKnotenUndItems.Select(_ => _.GetTypedId())],
                VervollständigeFehlendeKnotenAutomatisch = false,
            });


            /* Ermittle das LV und gib alle Elemente aus */
            Console.WriteLine("Ermittle alle LV-Elemente...");
            lv = await client.ProjektApi.GetLeistungsverzeichnis(projekt.Id, lv.Id, mitKalkulationen: false);
            var lvElemente = ErmittleLvElemente(lv);

            foreach(var e in lvElemente.OrderBy(_=>_.NummerKomplett))
            { Console.WriteLine($@"{e.NummerKomplett} - {e.Stichwort}"); }

            var lv1ElementeLookUp = lvElemente.ToDictionary(_ => _.NummerKomplett!);

            /* Erzeuge ein weiteres Projekt */
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Erstelle ein weiteres Projekt...");
            (Projekt projekt2, Leistungsverzeichnis lv2) = await ErzeugeProjektAsync(client, speicherort.Id, "Testprojekt2", GliederungsArt.LgGliederung);

            /* Übernimm ausgewählte Elemente des ersten LVs in das zweite LV */
            Console.WriteLine("Übernehme ausgewählte Elemente des ersten LVs in das zweite LV...");
            
            var lvPos_010101A = lv1ElementeLookUp["01.01.01.A"];
            var lvPos_010101C = lv1ElementeLookUp["01.01.01.C"];
            var lvPos_010201B = lv1ElementeLookUp["01.02.01.B"];

            var pasteResult = await client.ProjektApi.PasteOnLvDataIntoOnLv(projekt2.Id, lv2.Id, new PasteToLvOptionen()
            {
                QuellProjektId = projekt.Id,
                QuellLeistungsverzeichnisId = lv.Id,
                QuellElemente = TypedId.FromCollection(lvPos_010101A, lvPos_010101C, lvPos_010201B),
                VervollständigeFehlendeKnotenAutomatisch = true,
                PreiseÜbernehmen = true,
                MengenÜbernehmen = true,
                MengenermittlungÜbernehmen = true,
            });

            /* Ermittle das LV und gib alle Elemente aus */
            Console.WriteLine("Ermittle alle LV-Elemente...");
            lv2 = await client.ProjektApi.GetLeistungsverzeichnis(projekt2.Id, lv2.Id, mitKalkulationen: false);
            var lvElemente2 = ErmittleLvElemente(lv2);

            foreach (var e in lvElemente2.OrderBy(_ => _.NummerKomplett))
            { Console.WriteLine($@"{e.NummerKomplett} - {e.Stichwort}"); }

            /* Lösche Projekte */
            Console.WriteLine("Lösche Projekte...");
            await client.ProjektApi.DeleteProjekt(projekt.Id);
            await client.ProjektApi.DeleteProjekt(projekt2.Id);
            Console.WriteLine("Projekte gelöscht");
        }
        catch (Refit.ApiException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Methode erstellt ein leeres Projekt mit einem Leistungsverzeichnis
    /// </summary>
    /// <param name="client">Client für den Zugriff auf die API</param>
    /// <param name="speicherortId">die Id des Speicherorts</param>
    /// <param name="projektBezeichnung">Bezeichnung des neuen Projekts</param>
    /// <param name="gliederungsArt">Die Gliederungsart des LVs im neuen Projekt</param>
    /// <returns>Ein leeres Projekt mit einem leerem LV</returns>
    private static async Task<(Projekt projekt, Leistungsverzeichnis lv)> ErzeugeProjektAsync(NevarisBuildClient client, Guid speicherortId, string projektBezeichnung, GliederungsArt gliederungsArt)
    {
        var targetPrj = await client.StammApi.CreateProjekt(speicherortId, new NewProjektInfo() { Bezeichnung = projektBezeichnung });

        var newLvInfo = new NewLvInfo()
        {
            Art = LvArt.Ausschreibung,
            Bezeichnung = gliederungsArt.ToString(),
            NormExakt = NormExakt.OENORMA2063V2021,
            LvDetails = new LvDetails()
            {
                GliederungsArt = gliederungsArt,
            },
            OenormLvDetails = new OenormLvDetails()
            {
                Art = OenormLvArt.AusschreibungsLV,
            }
        };

        var targetLv = await client.ProjektApi.CreateLeistungsverzeichnis(targetPrj.Id, newLvInfo);

        return (targetPrj, targetLv);
    }

    /// <summary>
    /// Methode ermittelt die Knoten und Items eines Lbs und gibt diese als Liste zurück.
    /// </summary>
    /// <param name="client">Client Objekt für den Zugriff auf die API</param>
    /// <param name="lbId">die ID des auszulesenden LBs</param>
    /// <returns>Liste mit den Knoten und Items des Lbs</returns>
    private static async Task<List<LbItemBase>> ErmittleLbElementeAsync(NevarisBuildClient client, Guid lbId)
    {
        List<LbItemBase> result = [];

        var lbData = await client.StammApi.GetLeistungsbeschreibung(lbId, mitKnoten: true);

        foreach (var k in lbData.RootKnotenListe ?? [])
        { ProcessKnoten(k); }

        void ProcessKnoten(LbKnoten k)
        {
            result.Add(k);

            foreach (var ck in k.Knoten ?? [])
            { ProcessKnoten(ck); }

            foreach (var cp in k.Positionen ?? [])
            { result.Add(cp); }
        }

        return result;
    }

    /// <summary>
    /// Ermittle alle Elemente eines Leistungsverzeichnisses und gib diese als Liste zurück.
    /// </summary>
    /// <param name="lv"></param>
    /// <returns></returns>
    private static IReadOnlyCollection<LvItemBase> ErmittleLvElemente(Leistungsverzeichnis lv)
    {
        var result = new List<LvItemBase>();
        if (lv.RootPositionen != null) { result.AddRange(lv.RootPositionen); }

        void AddKnoten(LvKnoten knoten)
        {
            result.Add(knoten);

            if (knoten.Knoten != null)
            {
                foreach (var k in knoten.Knoten)
                { AddKnoten(k); }
            }

            if (knoten.Positionen != null) { result.AddRange(knoten.Positionen); }
        }

        if (lv.RootKnotenListe != null)
        {
            foreach (var k in lv.RootKnotenListe)
            { AddKnoten(k); }
        }

        return result;
    }
}