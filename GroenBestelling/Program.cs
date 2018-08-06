using System;
using System.Collections.Generic;

namespace GroenBestelling
{
    public class Artikel
    {
        public string Artikelnummer { get; set; }
        public DateTime Peildatum { get; set; }
        public DateTime Productiedatum { get; set; }
        public Double Prijs { get; set; }
        public Double Leeftijdskorting { get; set; }
        public Double Extrakorting { get; set; }
        public Double Subtotaal { get; set; }

        public Artikel() { }
        public Artikel(string artikelnummer, DateTime peildatum, Double prijs)
        {
            Artikelnummer = artikelnummer;
            Peildatum = peildatum;
            // In de opdrachtomschrijving staat niets over de precieze productiedatum van een artikel
            // Ik neem aan dat ieder artikel geproduceerd is op 1 januari
            Productiedatum = DateTime.Parse(Artikelnummer.Substring(0, 4) + "/01/01");
            Prijs = prijs;
            // Dit wordt lelijk
            TimeSpan td = Peildatum - Productiedatum;
            int dagenoud = (int)Math.Truncate(td.TotalDays);
            Leeftijdskorting = 0.0;
            if (dagenoud >= (2*365) && (dagenoud < 3*365))
            {
                Leeftijdskorting = 0.05;
            }
            if (dagenoud >= (3*365) && (dagenoud < 5*365))
            {
                Leeftijdskorting = 0.10;
            }
            if (dagenoud >= (5*365))
            {
                Leeftijdskorting = 0.25;
            }
            Extrakorting = (int.Parse(Artikelnummer.Substring(4, 1)) % 2) == 0 ? 0.02 : 0.0;
            Subtotaal = (Prijs * (1 - Leeftijdskorting)) * (1 - Extrakorting);
        }

        public override string ToString()
        {
            return "Art. code: " + Artikelnummer + " Peildat: " + Peildatum + " Proddat: " + Productiedatum + " Prijs: " + Prijs + " Lkorting: " + Leeftijdskorting + " ExKorting: " + Extrakorting + " Subtotaal: " + Subtotaal;
        }
    }


    class Program
    {
        static string vraagArtikelnummer()
        {
            Console.WriteLine("Artikelnummer van zes cijfers opgeven.");
            Console.WriteLine("De eerste vier cijfers van het artikelnummer moet een jaartal vormen groter dan 1979.");
            Console.WriteLine("Het jaartal mag niet groter zijn dan het huidige jaartal.");
            return Console.ReadLine();
        }

        static string vraagPeildatum()
        {
            Console.WriteLine("Geef peildatum op in formaat jjjj/mm/dd: ");
            return Console.ReadLine();
        }

        static Double vraagPrijs()
        {
            Console.WriteLine("Geef de prijs op van het artikel: ");
            return (double.Parse(Console.ReadLine()));
        }

        static void vraagArtikelinfo(ref List<Artikel> tartikelen)
        {
            var artnummer = vraagArtikelnummer();
            var peildatum = DateTime.Parse(vraagPeildatum());
            var prijs = vraagPrijs();
            var nieuwartikel = new Artikel(artnummer, peildatum, prijs);
            tartikelen.Add(nieuwartikel);
        }

        static void Main(string[] args)
        {
            List<Artikel> artikelen = new List<Artikel>();
            //FIXME List datastructuur vervangen door Dictionary
            var meerartikelen = true;

            while (meerartikelen == true)
            {
                vraagArtikelinfo(ref artikelen);
                Console.Write("Wil je meer artikelen invoeren? (ja of nee) ");
                if (Console.ReadLine() == "nee")
                {
                    meerartikelen = false;
                }
            }
            
            foreach(Artikel artk in artikelen)
            {
                Console.WriteLine(artk);
            }

            Double bedrag = 0.0;

            foreach(Artikel artk in artikelen)
            {
                bedrag = bedrag + artk.Subtotaal;
            }

            Console.WriteLine("Het verschuldigde bedrag voor de artikelen bedraag: {0}", bedrag);
        }
    }
}
