using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using Nevaris.Build.ClientApi;
using Refit;

namespace Lv_Viewer
{
    public class ViewModel : NotifyPropertyChangedBase
    {
        private readonly MainWindow _mainWindow;

        private NevarisBuildClient? Client { get; set; }

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
            SpeicherOrteOrdners.Clear();

            if (Client == null) return;

            try
            {
                if (Client != null)
                {
                    var projektServers = (await Client.StammApi.GetSpeicherorte()).Where(_ => _.DatenbankInfo != null).ToList();

                    // Iteriere über alle Speicherorte und befülle 'SpeicherOrteOrdners' mit den enthaltenen Ordnern
                    foreach (var projektServer in projektServers)
                    {
                        var projektServerDetailed = await Client.StammApi.GetSpeicherort(projektServer.Id);

                        // Wurzelebene
                        SpeicherOrteOrdners.Add(
                            new SpeicherortOrdnerViewModel(
                                projektServerDetailed,
                                ordner: null,
                                pfad: projektServerDetailed.Bezeichnung));

                        // Durch enthaltene Ordner rekursiv iterieren 
                        AddOrdners(
                            projektServerDetailed.RootOrdnerList,
                            parentPfad: projektServerDetailed.Bezeichnung);

                        void AddOrdners(IEnumerable<SpeicherortOrdner> ordnerList, string parentPfad)
                        {
                            foreach (var ordner in ordnerList)
                            {
                                string pfad = parentPfad + "/" + ordner.Bezeichnung;

                                SpeicherOrteOrdners.Add(
                                    new SpeicherortOrdnerViewModel(
                                        projektServerDetailed,
                                        ordner: ordner,
                                        pfad: pfad));

                                AddOrdners(ordner.OrdnerList, parentPfad: pfad);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public void Dispose()
        {
            Client?.Dispose();
        }

        /// <summary>
        /// Liste mit Speicherort-Ordnern (inkl. Wurzelebene).
        /// </summary>
        public ObservableCollection<SpeicherortOrdnerViewModel> SpeicherOrteOrdners { get; } = new();

        private SpeicherortOrdnerViewModel? _selectedSpeicherortOrdner;

        public SpeicherortOrdnerViewModel? SelectedSpeicherOrt
        {
            get { return _selectedSpeicherortOrdner; }
            set
            {
                _selectedSpeicherortOrdner = value;
                UpdateProjektListe();
                OnPropertyChanged(nameof(SelectedSpeicherOrt));
            }
        }

        private void UpdateProjektListe()
        {
            Projekte.Clear();
            Lvs.Clear();

            if (SelectedSpeicherOrt == null || Client == null) return;

            var projektInfos = SelectedSpeicherOrt.Ordner?.ProjektInfos ?? SelectedSpeicherOrt.Speicherort.RootProjektInfos;
            foreach (var p in projektInfos.OrderBy(_ => _.Nummer).ThenBy(_ => _.Bezeichnung))
            {
                Projekte.Add(p);
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
                LoadLvs();
                OnPropertyChanged(nameof(SelectedProjekt));
            }
        }

        private MengenArtViewItem? _selectedMenge;

        public MengenArtViewItem? SelectedMenge
        {
            get { return _selectedMenge; }
            set
            {
                _selectedMenge = value;
                OnPropertyChanged(nameof(SelectedMenge));
                //LV mit neuer Menge neu laden.
                LoadLv(SelectedLv);
            }
        }

        public ObservableCollection<MengenArtViewItem> Mengen { get; } = new();

        private void LoadMengen()
        {
            if (Mengen.Count > 0) { return; }
            Mengen.Clear();
            foreach (var art in MengenArtViewItem.Arten)
            {
                Mengen.Add(new MengenArtViewItem(art));
            }
            _selectedMenge = Mengen.First(_ => _.Art == MengenArt.Lv);
            OnPropertyChanged(nameof(SelectedMenge));
        }

        private async void LoadLvs()
        {
            _mainWindow.SetWaitSpinner(false);
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
                    MessageBox.Show(ex.Message);

                }

                if (projekt != null)
                {
                    foreach (var lv in projekt.Leistungsverzeichnisse.OrderBy(_ => _.Nummer).ThenBy(_ => _.Bezeichnung))
                    {
                        Lvs.Add(lv);
                    }
                }
            }
        }

        public ObservableCollection<Leistungsverzeichnis> Lvs { get; } = new();

        private Leistungsverzeichnis? _selectedLv;

        public Leistungsverzeichnis? SelectedLv
        {
            get { return _selectedLv; }
            set
            {
                _selectedLv = value;
                OnPropertyChanged(nameof(SelectedLv));
                LoadLv(value);
                LoadMengen();
            }
        }
        private LeistungsverzeichnisWrapper? _lvDetails;

        public LeistungsverzeichnisWrapper? LvDetails
        {
            get { return _lvDetails; }
            set { _lvDetails = value; OnPropertyChanged(nameof(LvDetails)); }
        }

        private async void LoadLv(Leistungsverzeichnis? lv)
        {
            LvDetails?.Dispose();
            _mainWindow.ClearFormattedText();
            if (lv != null && SelectedProjekt != null && Client != null)
            {
                _mainWindow.SetWaitSpinner(true);
                Leistungsverzeichnis? newLv = null;
                try
                {
                    newLv = await Client.ProjektApi.GetLeistungsverzeichnis
                        (SelectedProjekt.Id, lv.Id, SelectedMenge?.Art ?? MengenArt.Lv);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    _mainWindow.SetWaitSpinner(false);
                }

                if (newLv != null)
                {
                    LvDetails = new LeistungsverzeichnisWrapper(newLv, SelectedMenge);
                    _mainWindow.SetTreeViewSource();
                }
            }
        }

        #region IsLvAuswaehlen

        /// <summary>
        /// Backingfield für die Property IsLvAuswaehlen
        /// </summary>
        private bool _isLvAuswaehlen = true;

        /// <summary>
        /// IsLvAuswaehlen
        /// </summary>
        public bool IsLvAuswaehlen
        {
            get { return _isLvAuswaehlen; }
            set { _isLvAuswaehlen = value; OnPropertyChanged(nameof(IsLvAuswaehlen)); }
        }

        #endregion IsLvAuswaehlen

        #region IsLvImportieren

        /// <summary>
        /// Backingfield für die Property IsLvImportieren
        /// </summary>
        private bool _isLvImportieren;

        /// <summary>
        /// IsLvImportieren
        /// </summary>
        public bool IsLvImportieren
        {
            get { return _isLvImportieren; }
            set { _isLvImportieren = value; OnPropertyChanged(nameof(IsLvImportieren)); }
        }

        #endregion IsLvImportieren

        /// <summary>
        /// Methode zum Importieren eines Leistungsvezeichnis Datenträger und Anzeige des daraus erzeugten
        /// Leistungsverzeichnis.
        /// </summary>
        internal async void LeistungsverzeichnisImportieren()
        {
            _mainWindow.SetWaitSpinner(false);
            if (SelectedProjekt == null || Client == null)
            {
                MessageBox.Show("Bitte wählen Sie ein Projekt aus.");
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.AddExtension = true;
            openFileDialog.Filter = "ÖNORM A2063 LV (*.onlv)|*.onlv";
            if (openFileDialog.ShowDialog() == true)
            {
                _mainWindow.SetWaitSpinner(true);
                var fileInfo = new FileInfo(openFileDialog.FileName);
                FileInfoPart fileInfoPart = new FileInfoPart(fileInfo, fileInfo.Name);
                var importResult = await Client.ProjektApi.CreateLeistungsverzeichnisAusDatentraegerClientDatei(SelectedProjekt.Id, fileInfoPart);

                /* Das Resultat des Importvorgangs ist ein Leistungsverzeichnis und Meldungen die 
                 * während des Importvorganges entstanden sind. */
                var importieresLv = importResult.ImportieresLeistungsverzeichnis;
                var meldungenVonImporter = importResult.ImporterMeldungen;

                if (importieresLv == null)
                {
                    MessageBox.Show("Es wurde kein Leistungsverzeichnis importiert.");
                }
                else
                {
                    // Das erzeugte LV anzeigen.
                    SelectedLv = importieresLv;
                }

                _mainWindow.SetWaitSpinner(false);
                if (meldungenVonImporter?.Any() == true)
                {
                    StringBuilder sb = new();
                    sb.AppendLine("Meldungen aus Importer: ");
                    foreach (var meldung in meldungenVonImporter)
                    {
                        sb.AppendLine(meldung.Severity.ToString() + ": " + meldung.Text + " " + meldung.Wert);
                        sb.AppendLine();
                    }
                    MessageBox.Show(sb.ToString(), "Importer Meldungen", MessageBoxButton.OK);
                }

                _mainWindow.SetWaitSpinner(false);
            }
        }
    }
}
