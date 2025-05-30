﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Nevaris.Build.ClientApi;

namespace Lv_Viewer
{
    public class LeistungsverzeichnisWrapper : NotifyPropertyChangedBase
    {
        private Leistungsverzeichnis? _lv;
        
        private readonly MengenArtViewItem _mengenArt;
        
        public LeistungsverzeichnisWrapper(Leistungsverzeichnis lv, MengenArtViewItem? mengenArt)
        {
            _lv = lv ?? throw new ArgumentNullException(nameof(lv));
            _mengenArt = mengenArt ?? throw new ArgumentNullException(nameof(mengenArt));

            RefreshUI();

            LoadItemNodes();            
        }        

        private void LoadItemNodes()
        {
            RootNodes.Clear();
            if (_lv == null) { return; }

            var lvRootNode = CreateLvNode();

            LoadNodesRecursive(_lv.RootKnotenListe!, lvRootNode);
        }

        private LvNode CreateLvNode()
        {
            //LV Knoten Element
            LvNode? lvRootNodeItem = null;            
            
            if (_lv != null)
            {
                lvRootNodeItem = new LvNode(null)
                {
                    NummerUndBezeichnung = String.Join(" - ", _lv.Nummer, _lv.Bezeichnung),
                    Betrag = _lv?.Ergebnisse?.Betrag?.FirstValue
                };

                RootNodes.Add(lvRootNodeItem);
                //Texte auf LV Ebene
                if (_lv != null)
                {
                    foreach (var pos in _lv.RootPositionen!)
                    {
                        var position = new LvPosition(pos, _mengenArt);
                        lvRootNodeItem.ItemNodes.Add(position);
                    }
                }
            }
            return lvRootNodeItem!;
        }

        private void LoadNodesRecursive(IEnumerable<LvKnoten> knoten, LvNode rootLvItem)
        {
            foreach (var node in knoten)
            {
                var nodeLvItem = new LvNode(node);
                rootLvItem.ItemNodes.Add(nodeLvItem);
                CreatePositionen(node, nodeLvItem);

                LoadNodesRecursive(node.Knoten!, nodeLvItem);
            }
        }

        private void CreatePositionen(LvKnoten node, LvNode nodeLvItem)
        {
            foreach (var pos in node.Positionen!)
            {
                var position = new LvPosition(pos, _mengenArt);
                nodeLvItem.ItemNodes.Add(position);
            }
        }

        private ObservableCollection<LvNode> _rootNodes = new();

        public ObservableCollection<LvNode> RootNodes
        {
            get => _rootNodes;
            set { _rootNodes = value; OnPropertyChanged(nameof(RootNodes)); }
        }

        private void RefreshUI()
        {
            OnPropertyChanged(nameof(Umsatzsteuer));
            OnPropertyChanged(nameof(Waehrung));
            OnPropertyChanged(nameof(FormattedLangtext));
        }

        public string? Waehrung => _lv?.LvDetails?.Währung;
        public string? Umsatzsteuer => _lv?.LvDetails?.Umsatzsteuer;
        public string? FormattedLangtext => SelectedLvItem?.FormattedLangtext;

        private LvItem? _selectedLvItem;

        public LvItem? SelectedLvItem
        {
            get => _selectedLvItem;
            set 
            {
                _selectedLvItem = value; 
                OnPropertyChanged(nameof(SelectedLvItem));
            }
        }

        internal void Dispose()
        {
            RootNodes.Clear();
            SelectedLvItem = null;
            _lv = null;
            RefreshUI();
        }
    }
}
