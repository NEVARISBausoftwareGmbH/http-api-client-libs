﻿using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KalkulationApp
{
    public class ViewModel : NotifyPropertyChangedBase
    {
        private MainWindow _mainWindow;
        private NevarisBuildClient? Client { get; set; }
        
        /// <summary>
        /// Protokoll ausgeben für Kalk.-Blätter, die geändert wurden.        
        /// </summary>
        public List<ProtokollItem> ProtokollItems { get; private set; } = new();
        
        public ViewModel(MainWindow mainView)
        {
            _mainWindow = mainView;
            InitClient();
        }

        private void InitClient()
        {
            Client = new NevarisBuildClient("http://localhost:8500");

            Run();
        }

        private async void Run()
        {
            IEnumerable<Speicherort>? speicherOrte = null;
            try
            {
                if (Client != null)
                {
                    speicherOrte = (await Client.StammApi.GetSpeicherorte())?.Where(_ => _.DatenbankInfo != null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nevaris Build API");
            }

            SpeicherOrte.Clear();
            if (speicherOrte != null)
            {
                foreach (var sp in speicherOrte)
                {
                    SpeicherOrte.Add(sp);
                }
            }
        }        

        public void Dispose()
        {
            Client?.Dispose();
        }

        internal void ReloadKalkulation()
        {
            //Kalkulationszeilen bzw. Blätter neu laden.            
            Kalkulationsblätter.Clear();
            LoadKalkulationsbältter(CurrentLvPosition);
        }

        public ObservableCollection<Speicherort> SpeicherOrte { get; } = new();

        private Speicherort? _selectedSpeicherOrt;

        public Speicherort? SelectedSpeicherOrt
        {
            get { return _selectedSpeicherOrt; }
            set
            {
                _selectedSpeicherOrt = value;
                LoadProjekte();
                OnPropertyChanged(nameof(SelectedSpeicherOrt));
            }
        }

        private async void LoadProjekte()
        {
            Projekte.Clear();
            if (SelectedSpeicherOrt != null && Client != null)
            {
                Speicherort? speicherort = null;
                try
                {
                    speicherort = await Client.StammApi.GetSpeicherort(SelectedSpeicherOrt.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Nevaris Build API");
                }

                if (speicherort != null)
                {
                    foreach (var p in speicherort.ProjektInfos!.OrderBy(_ => _.Nummer).ThenBy(_ => _.Bezeichnung))
                    {
                        Projekte.Add(p);
                    }
                }
            }
        }

        public ObservableCollection<ProjektInfo> Projekte { get; } = new();

        private ProjektInfo? _selectedProjekt;

        public ProjektInfo? SelectedProjekt
        {
            get { return _selectedProjekt; }
            set
            {
                _selectedProjekt = value;
                _mainWindow.ResetTreeViewSource();
                SelectedKalkulationsblatt = null;
                Kalkulationsblätter.Clear();
                _mainWindow.TxtErsetzenOk.Text = null;
                LoadLvs();
                OnPropertyChanged(nameof(SelectedProjekt));
            }
        }

        public ObservableCollection<Leistungsverzeichnis> Lvs { get; } = new();

        private async void LoadLvs()
        {
            _mainWindow.SetWaitSpinner2(false);
            Lvs.Clear();
            if (SelectedProjekt != null && Client != null)
            {
                Projekt? projekt = null;
                try
                {
                    projekt = await Client.ProjektApi.GetProjekt(SelectedProjekt.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Nevaris Build API");
                }

                if (projekt != null)
                {
                    foreach (var lv in projekt.Leistungsverzeichnisse!.OrderBy(_ => _.Nummer).ThenBy(_ => _.Bezeichnung))
                    {
                        Lvs.Add(lv);
                    }
                }
            }
        }

        private Leistungsverzeichnis? _selectedLv;

        public Leistungsverzeichnis? SelectedLv
        {
            get { return _selectedLv; }
            set
            {
                _selectedLv = value;
                OnPropertyChanged(nameof(SelectedLv));
                Kalkulationsblätter.Clear();
                SelectedKalkulationsblatt = null;
                CurrentLvPosition = null;
                _mainWindow.TxtErsetzenOk.Text = null;
                OnPropertyChanged(nameof(SelectedLvPosition));
                LoadLV(value);
            }
        }

        public ObservableCollection<Kalkulation> Kalkulationen { get; } = new();

        private Kalkulation? _selectedKalkulation;

        public Kalkulation? SelectedKalkulation
        {
            get { return _selectedKalkulation; }
            set
            {
                _selectedKalkulation = value;
                OnPropertyChanged(nameof(SelectedKalkulation));

                LoadKalkulationskopfdaten(value);
            }
        }

        private async void LoadLV(Leistungsverzeichnis? lv)
        {
            Kalkulationen.Clear();            
            if (lv != null && SelectedProjekt != null && Client != null)
            {
                _mainWindow.SetWaitSpinner1(true);
                try
                {
                    Leistungsverzeichnis? fullLv = await Client.ProjektApi.
                            GetLeistungsverzeichnis(SelectedProjekt.Id, lv.Id, mitFormatiertenTexten: false);

                    if (fullLv != null)
                    {
                        LoadKalkulationen(fullLv);
                        LoadLVPositionen(fullLv);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Nevaris Build API");
                }
                finally { _mainWindow.SetWaitSpinner1(false); }
            }
        }
        
        private LvWrapper? _lvDetails;

        public LvWrapper? LvDetails
        {
            get { return _lvDetails; }
            set { _lvDetails = value; OnPropertyChanged(nameof(LvDetails)); }
        }

        private void LoadLVPositionen(Leistungsverzeichnis fullLv)
        {
            LvDetails = new LvWrapper(fullLv);
            _mainWindow.SetTreeViewSource();
        }
       
        private void LoadKalkulationen(Leistungsverzeichnis fullLv)
        {
            foreach (var kalkulation in fullLv.RootKalkulationen!.OrderBy(_ => _.Nummer).ThenBy(_ => _.Bezeichnung))
            {
                Kalkulationen.Add(kalkulation);
            }
        }

        public LvPosition? CurrentLvPosition { get; set; }
        public Kalkulation? CurrentKalkulationskopfdaten { get; set; }

        private async void LoadKalkulationskopfdaten(Kalkulation? kalkulation)
        {
            if (kalkulation != null && Client != null && SelectedProjekt != null)
            {
                _mainWindow.SetWaitSpinner2(true);
                _mainWindow.TxtErsetzenOk.Text = null;
                OnPropertyChanged(nameof(SelectedLvPosition));
                try
                {
                    //Kalkulation Kopfdaten
                    CurrentKalkulationskopfdaten = await Client.ProjektApi.GetKalkulation(SelectedProjekt.Id, kalkulation.Id);

                    if (CurrentKalkulationskopfdaten != null)
                    {
                        //Laden aller Betriebsmittel
                        LoadAllBetriebsmittel();
                        //Kalkulationsblätter vom Projekt abrufen
                        LoadKalkulationsbältter(CurrentLvPosition);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Nevaris Build API");
                }
            }            
        }

        public Dictionary<Guid, Betriebsmittel> CurrentProjektBetriebsmittel { get; private set; } = new();
        private void LoadAllBetriebsmittel()
        {
            CurrentProjektBetriebsmittel.Clear();
            if (Client != null && SelectedProjekt != null)
            {
                var betriebsmittelLöhne = Client.ProjektApi.GetAllBetriebsmittel(SelectedProjekt.Id, BetriebsmittelArt.Lohn, mitGruppen: false);
                var betriebsmittelMaterialen = Client.ProjektApi.GetAllBetriebsmittel(SelectedProjekt.Id, BetriebsmittelArt.Material, mitGruppen: false);
                var betriebsmittelGeräte = Client.ProjektApi.GetAllBetriebsmittel(SelectedProjekt.Id, BetriebsmittelArt.Gerät, mitGruppen: false);
                var betriebsmittelNachunternehmer = Client.ProjektApi.GetAllBetriebsmittel(SelectedProjekt.Id, BetriebsmittelArt.Nachunternehmer, mitGruppen: false);
                var betriebsmittelBausteine = Client.ProjektApi.GetAllBetriebsmittel(SelectedProjekt.Id, BetriebsmittelArt.Baustein, mitGruppen: false);
                var betriebsmittelSonstigeKosten = Client.ProjektApi.GetAllBetriebsmittel(SelectedProjekt.Id, BetriebsmittelArt.SonstigeKosten, mitGruppen: false);

                betriebsmittelBausteine.Result.ForEach((_) => CurrentProjektBetriebsmittel.Add(_.Id, _));
                betriebsmittelGeräte.Result.ForEach((_) => CurrentProjektBetriebsmittel.Add(_.Id, _));
                betriebsmittelLöhne.Result.ForEach((_) => CurrentProjektBetriebsmittel.Add(_.Id, _));
                betriebsmittelMaterialen.Result.ForEach((_) => CurrentProjektBetriebsmittel.Add(_.Id, _));
                betriebsmittelNachunternehmer.Result.ForEach((_) => CurrentProjektBetriebsmittel.Add(_.Id, _));
                betriebsmittelSonstigeKosten.Result.ForEach((_) => CurrentProjektBetriebsmittel.Add(_.Id, _));
            }
        }

        private Kalkulationsblatt? kalkulationsblatt;

        public Kalkulationsblatt? SelectedKalkulationsblatt
        {
            get { return kalkulationsblatt; }
            set { kalkulationsblatt = value; OnPropertyChanged(nameof(SelectedKalkulationsblatt)); }
        }

        public string? SelectedLvPosition
            => CurrentLvPosition?.NummerUndBezeichnung;

        private Dictionary<Guid, Kalkulationsblatt> Kalkulationsblätter { get; set; } = new();

        public async void LoadKalkulationsbältter(LvPosition? currentLvPosition = null)
        {
            try
            {
                CurrentLvPosition = currentLvPosition;

                if (CurrentKalkulationskopfdaten != null &&
                    Client != null && SelectedProjekt != null &&
                    LvDetails?.RootNodes != null && Kalkulationsblätter.Count == 0 &&
                    SelectedKalkulation != null)
                {
                    if (CurrentLvPosition == null)
                    {
                        CurrentLvPosition = GetFirstPosition();
                    }

                    if (CurrentLvPosition != null && IsCalcablePosition(CurrentLvPosition))
                    {
                        //Alle Kalkulationsblätter einmalig für das ausgewählte Projekt und LV lesen.
                        var kalkblätter = await Client.ProjektApi.GetKalkulationsBlätter(SelectedProjekt.Id,
                            CurrentKalkulationskopfdaten.Id, mitZeilen: true);

                        if (kalkblätter != null)
                        {
                            foreach (var kalkblatt in kalkblätter)
                            {
                                var newKalkblatt = new Kalkulationsblatt(kalkblatt);
                                newKalkblatt.Model = this;
                                newKalkblatt.GenerateKalkulationsZeilen();
                                if (!Kalkulationsblätter.ContainsKey(newKalkblatt.PositionId!.Value))
                                {
                                    Kalkulationsblätter.Add(newKalkblatt.PositionId!.Value, newKalkblatt);
                                }
                            }

                            //Zur LV-Position die Kalkulation anzeigen.
                            SelectedKalkulationsblatt = GetCurrentKalkulation(CurrentLvPosition.Id);
                        }
                    }
                }
                else if (Kalkulationsblätter.Count > 0 && CurrentLvPosition != null)
                {
                    SelectedKalkulationsblatt = GetCurrentKalkulation(CurrentLvPosition.Id);
                }
                
                OnPropertyChanged(nameof(SelectedLvPosition));
                _mainWindow.SetWaitSpinner2(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nevaris Build API");
            }
        }

        private Kalkulationsblatt? GetCurrentKalkulation(Guid lvPositionId)
       {
            Kalkulationsblätter.TryGetValue(lvPositionId, out var kalkBlatt);
            return kalkBlatt;
        }

        private static bool IsCalcablePosition(LvPosition? position)
        {
            if (position == null) { return false; }
            return position.ItemTyp == LvItemTyp.GaebLeistungsposition ||
                    position.ItemTyp == LvItemTyp.GaebZuschlagsposition ||
                    position.ItemTyp == LvItemTyp.GaebUnterbeschreibung ||
                    position.ItemTyp == LvItemTyp.OnLeistungsposition;
        }

        private LvPosition? GetFirstPosition()
        {
            LvPosition? currentLvPosition = null;
            if (LvDetails != null)
            {
                foreach (var item in LvDetails.RootNodes[0].ItemNodes)
                {
                    if (item is LvNode node)
                    {
                        currentLvPosition = GetFirstPositionRekursiv(node, currentLvPosition);
                        if (currentLvPosition != null) { break; }
                    }
                    else if (item is LvPosition position)
                    {
                        currentLvPosition = position;
                    }
                }
            }
            return currentLvPosition;
        }

        private LvPosition? GetFirstPositionRekursiv(LvNode parent, LvPosition? lvPosition)
        {
            if (lvPosition != null) { return lvPosition; }
            foreach (var item in parent.ItemNodes)
            {
                if (item is LvNode node)
                {
                    lvPosition = GetFirstPositionRekursiv(node, lvPosition);
                    if (lvPosition != null) { return lvPosition; }
                }
                else if (item is LvPosition position && IsCalcablePosition(position))
                {
                    lvPosition = position;
                    break;
                }
            }
            return lvPosition;
        }

        private bool _ersetzeNurGefiltertePositionen;

        public bool ErsetzeNurGefiltertePositionen
        {
            get { return _ersetzeNurGefiltertePositionen; }
            set { _ersetzeNurGefiltertePositionen = value; OnPropertyChanged(nameof(ErsetzeNurGefiltertePositionen)); }
        }

        internal void ErsetzeVariable()
        {            
            ProtokollItems.Clear();

            //Alle Kalkulationsblätter durchforsten nach der gewünschten Variable und den Wert durch jenen der Eigenleistung der SummenZeile erstezen.
            var nurGefiltertePositionenErsetzen = LvDetails?.LvPositionen.Select(_ => _.Id).ToHashSet();
            if (ErsetzeNurGefiltertePositionen)
            {
                nurGefiltertePositionenErsetzen = LvDetails?.LvPositionen.
                    Where(_ => _.IsVisible).
                    Select(_ => _.Id).
                    ToHashSet();
            }

            var kalkblätterToUpdate = Kalkulationsblätter.
                Where(_ => _.Value.MustUpdateKalkblatt == true && 
                        nurGefiltertePositionenErsetzen?.Contains(_.Key) == true).    
                Select(_ => _.Value).
                ToList();

            var lvPositionen = LvDetails?.LvPositionen?.ToDictionary(_ => _.Id) ?? new();
            foreach (var kalkblatt in kalkblätterToUpdate)
            {
                if (kalkblatt.PositionId != null)
                {
                    var kalkBlattToUpdate = GetCurrentKalkulation(kalkblatt.PositionId.Value);
                    if (kalkBlattToUpdate != null)
                    {
                        //Ermitteln der Variable "sk" und den Kosten der T-Zeile oder Z-Zeile
                        //T-Zeile = Teilsumme
                        //Z-Zeile = Zwischensumme
                        //mehrere T-Zeilen ergeben eine Zwischensumme
                        var originalKalkblatt = kalkBlattToUpdate.OrigianlKalkblatt;
                        if (originalKalkblatt != null)
                        {
                            KalkulationsZeile? zeileVariableSk = null;
                            decimal? kosten_Eigenleistung = null;
                            bool hasEigenleistung = originalKalkblatt.Zeilen!.Any(IsEigenleistung);
                            bool kostenFromZZeile = false;
                            foreach (var kalkZeile in originalKalkblatt.Zeilen!)
                            {
                                if (kalkZeile.VariablenDetails != null && kalkZeile.VariablenDetails.Variable == "sk")
                                {
                                    zeileVariableSk = kalkZeile;
                                }
                                else if (hasEigenleistung && IsEigenleistung(kalkZeile))
                                {
                                    kosten_Eigenleistung = ErmittleEigeneKosten(kosten_Eigenleistung, kalkZeile, ref kostenFromZZeile);
                                }
                                else if (!hasEigenleistung && kalkZeile.SummenDetails != null)
                                {
                                    kosten_Eigenleistung = ErmittleEigeneKosten(kosten_Eigenleistung, kalkZeile, ref kostenFromZZeile);
                                }
                            }
                            

                            if (zeileVariableSk != null && kosten_Eigenleistung != null)
                            {
                                UpdateSkVariablenZeile(
                                    lvPositionen, 
                                    kalkblatt, 
                                    originalKalkblatt, 
                                    zeileVariableSk, 
                                    kosten_Eigenleistung, 
                                    hasEigenleistung);                                
                            }
                            else
                            {
                                if (kalkblatt?.PositionId != null &&
                                    lvPositionen.TryGetValue(kalkblatt.PositionId.Value, out var lvPosition))
                                {
                                    AddProtokollItem(kosten_Eigenleistung, lvPosition, hasEigenleistung);
                                }
                            }
                        }
                    }                    
                }
            }
        }

        private void UpdateSkVariablenZeile(Dictionary<Guid, LvPosition> lvPositionen, 
            Kalkulationsblatt kalkblatt, KalkulationsBlatt? originalKalkblatt, 
            KalkulationsZeile? zeileVariableSk, decimal? kosten_Eigenleistung, bool hasEigenleistung)
        {
            if (kosten_Eigenleistung == null || zeileVariableSk == null) { return; }
            //Update vornehmen mit den gemerkten Werten.
            var ansatzSplit = (zeileVariableSk.VariablenDetails!.Ansatz ?? "").Split("*");
            ansatzSplit[0] = kosten_Eigenleistung.Value.ToString("N2");
            if (ansatzSplit.Length > 1)
            {
                zeileVariableSk.VariablenDetails.Ansatz = $"{ansatzSplit[0]}*{ansatzSplit[1]}";
            }
            else
            {
                zeileVariableSk.VariablenDetails.Ansatz = ansatzSplit[0];
            }

            if (kalkblatt?.PositionId != null &&
                lvPositionen.TryGetValue(kalkblatt.PositionId.Value, out var lvPosition))
            {
                AddProtokollItem(kosten_Eigenleistung.Value, lvPosition, hasEigenleistung);
            }
            else
            {
                string bezeichnung = "Position konnte nicht gefunden werden";
                AddProtokollItem(kosten_Eigenleistung.Value, null, hasEigenleistung, bezeichnung);
            }

            //API aktualisiert die Kalkulation.
            if (SelectedProjekt != null &&
                SelectedKalkulation != null &&
                kalkblatt != null &&
                originalKalkblatt != null)
            {
                Client?.ProjektApi.UpdateKalkulationsBlatt(
                    SelectedProjekt.Id,
                    SelectedKalkulation.Id,
                    kalkblatt.PositionId.GetValueOrDefault(),
                    originalKalkblatt);
            }
        }

        private static decimal? ErmittleEigeneKosten(decimal? kosten_Eigenleistung, KalkulationsZeile kalkZeile, ref bool kostenFromZZeile)
        {        
            //Erste Z-Zeile die Kosten verwenden.
            if (!kostenFromZZeile && kalkZeile.SummenDetails!.Art == SummenKalkulationsZeileArt.Absolut)
            {
                //Bei der Z-Zeile muss es sich um Eigenleistung handeln.
                if (IsEigenleistung(kalkZeile))
                {
                    kosten_Eigenleistung = kalkZeile.SummenDetails.Kosten?.FirstValue;
                    kostenFromZZeile = true;
                }
            }
            else if (kosten_Eigenleistung == null)
            {
                //Wenn die Kosten von der Z-Zeile bereits zugewiesen wurden diese verwenden
                //sonst sollen die Kosten der T-Zeile genommen werden.
                kosten_Eigenleistung = kalkZeile.SummenDetails!.Kosten?.FirstValue;
            }

            return kosten_Eigenleistung;
        }

        private void AddProtokollItem(decimal? kosten_Eigenleistung, LvPosition? lvPosition, bool hasEigenleistung, string? bezeichnung = null)
        {
            ProtokollItems.Add(
                     new ProtokollItem(
                         lvPosition?.Nummer,
                         bezeichnung ?? lvPosition?.Bezeichnung,
                         kosten_Eigenleistung?.ToString("N2"),
                         hasEigenleistung));
        }

        public static bool IsEigenleistung(KalkulationsZeile zeile) =>
            zeile.Bezeichnung?.Contains("Eigenleistung") == true &&
            (zeile.SummenDetails?.Art == SummenKalkulationsZeileArt.Absolut ||
            zeile.SummenDetails?.Art == SummenKalkulationsZeileArt.Relativ);
    }
}
