﻿using System;
using System.IO;
using System.Text;
using System.Windows;

namespace Lv_Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Model = new(this);
        }

        private ViewModel? _model;

        public ViewModel? Model
        {
            get { return _model; }
            set { DataContext = _model = value; }
        }

        public void SetTreeViewSource()
            => tv.ItemsSource = Model?.LvDetails?.RootNodes;

        public void SetWaitSpinner(bool visible)
            => WaitSpinner.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Model?.Dispose();
            Close();
        }

        private void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //Langetext angezeigen
            if (e.NewValue != null && Model?.LvDetails != null)
            {
                Model.LvDetails.SelectedLvItem = e.NewValue as LvItem;
                LoadHtmlText(Model?.LvDetails?.SelectedLvItem?.FormattedLangtext);
            }
        }

        private void LoadHtmlText(string? xmlText = null)
        {
            var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "GaebLvItemText.htm");
            //Ausschreiberlücken werden gelb dargestellt.
            StringBuilder sb = new(FormattedTextTemplate.GetVorlageAusschreiber());
            if (Model?.SelectedLv?.Norm == Nevaris.Build.ClientApi.Norm.Gaeb &&
                xmlText?.Contains("Kind=\"Bidder\"") == true)
            {
                //Bieterlücken werden grün dargestellt.
                sb = new(FormattedTextTemplate.GetVorlageBieter());
            }

            if (xmlText != null)
            {
                sb.Append(xmlText);
            }

            using (FileStream fs = new(path, FileMode.Create))
            {
                using StreamWriter w = new(fs, Encoding.UTF8);
                w.WriteLine(sb.ToString());
            }

            WebBrowserLt.Navigate(new Uri(path));
        }

        internal void ClearFormattedText()
        {
            LoadHtmlText();
        }

        private async void ButtonLvImportieren_Click(object sender, RoutedEventArgs e)
        {
            if (Model is { } model)
            {
                await model.LeistungsverzeichnisImportieren();
            }
        }
    }
}
