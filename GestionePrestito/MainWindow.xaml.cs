using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionePrestito
{
    /// <summary>
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cmb_citta.Items.Add("Foligno");
            cmb_citta.Items.Add("Spello");
            cmb_citta.Items.Add("Castiglione del Lago");


        }
        private const string file = "File.csv";
        List<string> lst = new List<string>();
        string frase;
       string sesso;
        DateTime date;
        private void btn_calcola_Click(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(txt_cognome.Text)) || string.IsNullOrWhiteSpace(txt_nome.Text) || (cmb_citta.SelectedItem == null) || (string.IsNullOrWhiteSpace(txt_importo.Text)) || (string.IsNullOrWhiteSpace(txt_rate.Text)) || (dtp_data.SelectedDate == null))
            {
                MessageBox.Show("Inserire tutti i campi richiesti!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if ((rdb_F.IsChecked == false) && (rdb_M.IsChecked == false))
            {
                MessageBox.Show("Inserire il sesso", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            date = (DateTime)dtp_data.SelectedDate;
            if (date >= DateTime.Today)
            {
                MessageBox.Show("Inserire una data valida", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            double richiesta = double.Parse(txt_importo.Text);
            int rate = int.Parse(txt_rate.Text);
            int percentule = int.Parse(txt_percentuale.Text);
            double restituire = richiesta + (richiesta * percentule / 100);
            double importorata = restituire / rate;
            txt_restituire.Text = restituire.ToString();
            txt_rata.Text = importorata.ToString();
            string nato = "";
            if (rdb_F.IsChecked == true)
            {
                sesso = "F";
                nato = "nata";

            }
            else if (rdb_M.IsChecked == true)
            {
                sesso = "M";
                nato = "nato";
            }

            frase = $"{txt_cognome.Text} {txt_nome.Text}, residente in {cmb_citta.SelectedItem} {nato} il {date.ToShortDateString()}. Prestito di € {richiesta} ad un tasso del {percentule}% da restituire in {rate} rate da {importorata}€ ciascuna, per un totale di {restituire}€.";
            lst.Add($"{txt_cognome.Text}; {txt_nome.Text} ; {cmb_citta.SelectedItem}; {sesso} ;{date.ToShortDateString()}; {richiesta} ; {percentule};{rate} ;{importorata}; {restituire}");
            lbl_riepilogo.Content += $"{frase} \n";
        }

        private void btn_stampa_Click(object sender, RoutedEventArgs e)
        {
            lst.Sort();
            StreamWriter sw = new StreamWriter(file, false, Encoding.UTF8);
            {
                sw.WriteLine($"Cognome; Nome; Città; Sesso; Data di Nascita; Importo richiesto €; % di interesse; Numero di rate; Importo rata €; Totale € da restituire");
                foreach (string frase in lst)
                {
                    sw.WriteLine(frase);
                }
            }
            sw.Close();
        }

        private void btn_nuovo_Click(object sender, RoutedEventArgs e)
        {
            txt_cognome.Clear();
            txt_nome.Clear();
            txt_importo.Clear();
            txt_percentuale.Clear();
            txt_rata.Clear();
            txt_importo.Clear();
            txt_restituire.Clear();
            txt_rate.Clear();
            dtp_data.SelectedDate = null;
            rdb_F.IsChecked = false;
            rdb_M.IsChecked = false;
            cmb_citta.SelectedItem = null;
        }
    }
}



