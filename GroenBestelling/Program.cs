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
        static void Main(string[] args)
        {
            DateTime peildatum = DateTime.Parse("2018/08/06");
            var artikel1 = new Artikel("198047", peildatum, 42.05);
            var artikel2 = new Artikel("201738", peildatum, 20.40);
            var artikel3 = new Artikel("200068", peildatum, 30.65);
            var artikel4 = new Artikel("201433", peildatum, 21.35);
            Console.WriteLine(artikel1);
            Console.WriteLine(artikel2);
            Console.WriteLine(artikel3);
            Console.WriteLine(artikel4);
        }
    }
}
