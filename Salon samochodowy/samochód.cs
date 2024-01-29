using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace Salon_samochodowy
{
    public class Samochod
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string Kolor { get; set; }
        public string Rok_produkcji { get; set; }
        public string Przebieg { get; set; }
        public string Cena { get; set; }
        public string Pojemnosc_silnika { get; set; }
        public string Rodzaj_paliwa { get; set; }
        public string Skrzynia_biegow { get; set; }
        public string VIN { get; set; }


        public Samochod(int id, string marka, string model, 
                        string kolor, string rok_produkcji, 
                        string przebieg, string cena, 
                        string pojemnosc_silnika, string rodzaj_paliwa,
                        string skrzynia_biegow, string vin)
        {
            Id = id;
            Marka = marka;
            Model = model;
            Kolor = kolor;
            Rok_produkcji = rok_produkcji;
            Przebieg = przebieg;
            Cena = cena;
            Pojemnosc_silnika = pojemnosc_silnika;
            Rodzaj_paliwa = rodzaj_paliwa;
            Skrzynia_biegow = skrzynia_biegow;
            VIN = vin;
        }

        public static void DodajSamochod()
        {
            Console.WriteLine("Podaj markę samochodu: ");
            string marka = Console.ReadLine();
            Console.WriteLine("Podaj model samochodu: ");
            string model = Console.ReadLine();
            Console.WriteLine("Podaj kolor samochodu: ");
            string kolor = Console.ReadLine();
            Console.WriteLine("Podaj rok produkcji samochodu: ");
            string rok_produkcji = Console.ReadLine();
            Console.WriteLine("Podaj przebieg samochodu (w km): ");
            string przebieg = Console.ReadLine();
            Console.WriteLine("Podaj cenę samochodu (w PLN): ");
            string cena = Console.ReadLine();
            Console.WriteLine("Podaj pojemność silnika samochodu (w cm^3): ");
            string pojemnosc_silnika = Console.ReadLine();
            Console.WriteLine("Podaj rodzaj paliwa samochodu (Benzyna lub Diesel): ");
            string rodzaj_paliwa = Console.ReadLine();
            Console.WriteLine("Podaj rodzaj skrzyni biegów samochodu (Automatyczna lub Manualna): ");
            string skrzynia_biegow = Console.ReadLine();
            Console.WriteLine("Podaj VIN samochodu: ");
            string VIN = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(marka) || string.IsNullOrWhiteSpace(model) ||
                   string.IsNullOrWhiteSpace(kolor) || string.IsNullOrWhiteSpace(rok_produkcji) ||
                   string.IsNullOrWhiteSpace(przebieg) || string.IsNullOrWhiteSpace(cena) ||
                   string.IsNullOrWhiteSpace(pojemnosc_silnika) || string.IsNullOrWhiteSpace(rodzaj_paliwa) ||
                   string.IsNullOrWhiteSpace(skrzynia_biegow) || string.IsNullOrWhiteSpace(VIN))
            {
                Console.WriteLine("Wszystkie dane muszą być uzupełnione. Podaj brakujące dane ponownie.");

                Console.WriteLine("Podaj markę samochodu: ");
                marka = Console.ReadLine();
                Console.WriteLine("Podaj model samochodu: ");
                model = Console.ReadLine();
                Console.WriteLine("Podaj kolor samochodu: ");
                kolor = Console.ReadLine();
                Console.WriteLine("Podaj rok produkcji samochodu: ");
                rok_produkcji = Console.ReadLine();
                Console.WriteLine("Podaj przebieg samochodu: ");
                przebieg = Console.ReadLine();
                Console.WriteLine("Podaj cenę samochodu: ");
                cena = Console.ReadLine();
                Console.WriteLine("Podaj pojemność silnika samochodu: ");
                pojemnosc_silnika = Console.ReadLine();
                Console.WriteLine("Podaj rodzaj paliwa samochodu: ");
                rodzaj_paliwa = Console.ReadLine();
                Console.WriteLine("Podaj rodzaj skrzyni biegów samochodu: ");
                skrzynia_biegow = Console.ReadLine();
                Console.WriteLine("Podaj VIN samochodu: ");
                VIN = Console.ReadLine();
            }

            List<Samochod> samochody = WczytajSamochodyZPliku("magazyn.txt");

            int noweId = GenerujNoweId(samochody);

            Samochod samochod = new Samochod(noweId, marka, model, kolor, rok_produkcji, przebieg, cena, pojemnosc_silnika, rodzaj_paliwa, skrzynia_biegow, VIN);
            samochody.Add(samochod);

            ZapiszSamochodyDoPliku(samochody, "magazyn.txt");
        }
        public static List<Samochod> WczytajSamochodyZPliku(string nazwaPliku)
        {
            List<Samochod> samochody = new List<Samochod>();

            try
            {
                using (StreamReader reader = new StreamReader(nazwaPliku))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] samochodData = line.Split(',');

                        if (samochodData.Length == 11)
                        {
                            int id = int.Parse(samochodData[0].Trim());
                            string marka = samochodData[1].Trim();
                            string model = samochodData[2].Trim();
                            string kolor = samochodData[3].Trim();
                            string rokProdukcji = samochodData[4].Trim();
                            string przebieg = samochodData[5].Trim();
                            string cena = samochodData[6].Trim();
                            string pojemnoscSilnika = samochodData[7].Trim();
                            string rodzajPaliwa = samochodData[8].Trim();
                            string skrzyniaBiegow = samochodData[9].Trim();
                            string vin = samochodData[10].Trim();

                            Samochod samochod = new Samochod(id, marka, model, kolor, rokProdukcji, 
                                                             przebieg, cena, pojemnoscSilnika, 
                                                             rodzajPaliwa, skrzyniaBiegow, vin);
                            samochody.Add(samochod);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odczytu pliku: {ex.Message}");
            }

            return samochody;
        }

        public static void ZapiszSamochodyDoPliku(List<Samochod> samochody, string nazwaPliku)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(nazwaPliku))
                {
                    foreach (Samochod samochod in samochody)
                    {
                        writer.WriteLine($"{samochod.Id},{samochod.Marka},{samochod.Model},{samochod.Kolor},{samochod.Rok_produkcji},{samochod.Przebieg},{samochod.Cena},{samochod.Pojemnosc_silnika},{samochod.Rodzaj_paliwa},{samochod.Skrzynia_biegow},{samochod.VIN}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu do pliku: {ex.Message}");
            }
        }
        public static int GenerujNoweId(List<Samochod> samochody)
        {
            int noweId = 1;

            if (samochody.Count > 0)
            {
                noweId = samochody[samochody.Count - 1].Id + 1;
            }

            return noweId;
        }


        public static void UsunSamochod()
        {
            Console.WriteLine("Podaj ID samochodu do usunięcia: ");
            int id = int.Parse(Console.ReadLine());

            List<Samochod> samochody = WczytajSamochodyZPliku("magazyn.txt");

            Samochod samochod = samochody.Find(s => s.Id == id);

            if (samochod != null)
            {
                samochody.Remove(samochod);
                ZapiszSamochodyDoPliku(samochody, "magazyn.txt");
            }
            else
            {
                Console.WriteLine("Nie znaleziono samochodu o podanym ID.");
            }
        }

        public static void WyswietlStan()
        {
            List<Samochod> samochody = WczytajSamochodyZPliku("magazyn.txt");

            foreach (Samochod samochod in samochody)
            {
                
                Console.WriteLine($"ID samochodu: {samochod.Id}");
                Console.WriteLine($"Marka: {samochod.Marka}");
                Console.WriteLine($"Model: {samochod.Model}");
                Console.WriteLine($"Kolor: {samochod.Kolor}");
                Console.WriteLine($"Rok produkcji: {samochod.Rok_produkcji}");
                Console.WriteLine($"Przebieg: {samochod.Przebieg}");
                Console.WriteLine($"Cena: {samochod.Cena}");
                Console.WriteLine($"Pojemność silnika: {samochod.Pojemnosc_silnika}");
                Console.WriteLine($"Rodzaj paliwa: {samochod.Rodzaj_paliwa}");
                Console.WriteLine($"Skrzynia biegów: {samochod.Skrzynia_biegow}");
                Console.WriteLine($"VIN: {samochod.VIN}");
                Console.WriteLine();
                Console.WriteLine();
            }

        }
    }
}
