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

using FuvarClass;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <<nincs>>
    public partial class MainWindow : Window
    {
        List<Fuvar> Fuvarok = new List<Fuvar>();
        public MainWindow()
        {
            InitializeComponent();


            StreamReader sr = new StreamReader("fuvar.csv");

            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split(';');
                Fuvarok.Add(new Fuvar(Convert.ToInt32(sor[0]), sor[1], Convert.ToInt32(sor[2]), Convert.ToDouble(sor[3]), Convert.ToDouble(sor[4]), Convert.ToDouble(sor[5]), sor[6]));
            }
        }

        private void Harmadik(object sender, RoutedEventArgs e)
        {
            lblHarmadik.Content = $"{Fuvarok.Count.ToString()} fuvar";
        }

        private void Negyedik(object sender, RoutedEventArgs e)
        {
            int fuvarokSzama = 0;
            double bevetel = 0;
            try
            {
                int kapottSzam = Convert.ToInt32(txtNegyedik.Text);
                for (int i = 0; i < Fuvarok.Count(); i++)
                {
                    if (Fuvarok[i].TaxiId == kapottSzam)
                    {
                        fuvarokSzama++;
                        bevetel += Fuvarok[i].Viteldij;
                    }
                }
                lblNegyedik.Content = $"{fuvarokSzama} fuvar alatt: {bevetel}$";
            }
            catch(ArgumentNullException)
            {
                MessageBox.Show("Nem számott adott meg!");
                txtNegyedik.Text = "";
                throw;
            }
            catch (Exception)
            {
                MessageBox.Show("Valami egyéb hiba volt a megadásnál!");
                txtNegyedik.Text = "";
                throw;
            }
            
        }


        
        private void Ötödik(object sender, RoutedEventArgs e)
        {
            Dictionary<string, int> FizetesModok = new Dictionary<string, int>();
            FizetesModok.Add("bankkártya", 0);
            FizetesModok.Add("készpénz", 0);
            FizetesModok.Add("vitatott", 0);
            FizetesModok.Add("ingyenes", 0);
            FizetesModok.Add("ismeretlen", 0);

            for (int i = 0; i < Fuvarok.Count(); i++)
            {
                FizetesModok[Fuvarok[i].FizetesModja] += 1;
            }

            
            lbÖtödik.Items.Add($"bankkártya: {FizetesModok["bankkártya"]}");
            lbÖtödik.Items.Add($"keszpenz: {FizetesModok["készpénz"]}");
            lbÖtödik.Items.Add($"vitatott: {FizetesModok["vitatott"]}");
            lbÖtödik.Items.Add($"ingyenes: {FizetesModok["ingyenes"]}");
            lbÖtödik.Items.Add($"ismeretlen: {FizetesModok["ismeretlen"]}");  

        }
        


        private void Hatodik(object sender, RoutedEventArgs e)
        {
            double osszesTavolsag = 0;

            for (int i = 0; i < Fuvarok.Count(); i++)
                osszesTavolsag = osszesTavolsag + Fuvarok[i].Tavolsag;
            osszesTavolsag = osszesTavolsag * 1.6;

            lblHatodik.Content = $"{Math.Round(osszesTavolsag, 2).ToString()} km";
        }

        private void Hetedik(object sender, RoutedEventArgs e)
        {
            int legHosszabbIndexe = 0;
            for (int i = 1; i < Fuvarok.Count(); i++)
            {
                if (Fuvarok[i].Tavolsag > Fuvarok[legHosszabbIndexe].Tavolsag)
                    legHosszabbIndexe = i;
            }

            lbHetedik.Items.Add($"Fuvar hossza: {Fuvarok[legHosszabbIndexe].Idotartam} másodperc");
            lbHetedik.Items.Add($"Taxi azonosító: {Fuvarok[legHosszabbIndexe].TaxiId}");
            lbHetedik.Items.Add($"Megtett Távolság: {Math.Round(Fuvarok[legHosszabbIndexe].Tavolsag * 1.6, 2)} km");
            lbHetedik.Items.Add($"Viteldíj: {Fuvarok[legHosszabbIndexe].Viteldij}$");
        }

        private void Nyolcadik(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter("hibak.txt");
            sw.WriteLine("taxi_id;indulas;idotartam;tavolsag;viteldij;borravalo;fizetes_modja");
            for (int i = 0; i < Fuvarok.Count(); i++)
            {
                if (Fuvarok[i].Idotartam > 0 && Fuvarok[i].Viteldij > 0 && Fuvarok[i].Tavolsag == 0)
                {
                    sw.WriteLine($"{Fuvarok[i].TaxiId};{Fuvarok[i].Indulas};{Fuvarok[i].Idotartam};{Fuvarok[i].Tavolsag};{Fuvarok[i].Viteldij};{Fuvarok[i].Borravalo};{Fuvarok[i].FizetesModja}");
                }
            }
            lblNyolcadik.Content = "hibak.txt";
        }
    }
}
