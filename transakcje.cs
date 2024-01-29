using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Salon_samochodowy
{

    public class Transakcje
    {
        
            static public void Zakup()
        {
            Console.WriteLine("Podaj ID samochodu, który chcesz kupić:");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Clear();
                Console.WriteLine("Podano nieprawidłowe ID.");
                Console.WriteLine("Nastąpił powrót do menu wyboru");
                Program.WyborKlienta();
                return;
                
            }

            List<Samochod> samochody = Samochod.WczytajSamochodyZPliku("magazyn.txt");
            Samochod samochod = samochody.Find(s => s.Id == id);

            if (samochod != null)
            {
                Klient zalogowanyKlient = Authentication.GetLoggedInCustomer();
                string imieZalogowanego = zalogowanyKlient.Name;
                int idKlienta = zalogowanyKlient.Id;
                DateTime dataZakupu = DateTime.Now;

                ZapiszTransakcjeDoPliku(idKlienta, imieZalogowanego, dataZakupu, samochod);

                samochody.Remove(samochod);
                Samochod.ZapiszSamochodyDoPliku(samochody, "magazyn.txt");
                Console.Clear();
                Console.WriteLine($"Dokonano zakupu samochodu {samochod.Marka} {samochod.Model}");
                Console.WriteLine($"Data zakupu: {dataZakupu}");
                Console.WriteLine("Nastąpił powrót do menu wyboru");
                Program.WyborKlienta();
            }
            else
            {
                Console.WriteLine("Nie znaleziono samochodu o podanym ID, nastąpił powrót do menu wyboru");
                Program.WyborKlienta();
            }
        }

        static void ZapiszTransakcjeDoPliku(int idKlienta, string imie, DateTime dataZakupu, Samochod samochod)
        {
            try
            {
                int idTransakcji = PobierzNastepneIdTransakcji("transakcje.txt");

                using (StreamWriter writer = new StreamWriter("transakcje.txt", true))
                {
                    writer.WriteLine($"{idTransakcji},{idKlienta},{imie},{dataZakupu},{samochod.Id},{samochod.Marka},{samochod.Model},{samochod.Kolor},{samochod.Rok_produkcji},{samochod.Przebieg},{samochod.Cena},{samochod.Pojemnosc_silnika},{samochod.Rodzaj_paliwa},{samochod.Skrzynia_biegow},{samochod.VIN}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu do pliku transakcje.txt: {ex.Message}");
            }
        }


        static int PobierzNastepneIdTransakcji(string nazwaPliku)
        {
            int idTransakcji = 1;

            try
            {
                if (File.Exists(nazwaPliku))
                {
                    using (StreamReader reader = new StreamReader(nazwaPliku))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] transakcjaData = line.Split(',');

                            if (transakcjaData.Length > 0)
                            {
                                int id = int.Parse(transakcjaData[0].Trim());
                                idTransakcji = Math.Max(id + 1, idTransakcji);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odczytu pliku {nazwaPliku}: {ex.Message}");
            }

            return idTransakcji;
        }
        static public void WyswietlHistorieZakupow(int idKlienta)
        {
            string nazwaPliku = "transakcje.txt";

            try
            {
                using (StreamReader reader = new StreamReader(nazwaPliku))
                {
                    Console.WriteLine($"Historia zakupów klienta o ID {idKlienta}:");

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] transakcjaData = line.Split(',');

                        if (transakcjaData.Length == 15) 
                        {
                            int idTransakcji = int.Parse(transakcjaData[0].Trim());
                            int idKlientaTransakcji = int.Parse(transakcjaData[1].Trim());

                            if (idKlientaTransakcji == idKlienta)
                            {
                                DateTime dataZakupu = DateTime.Parse(transakcjaData[3].Trim());
                                int idSamochodu = int.Parse(transakcjaData[4].Trim());
                                string marka = transakcjaData[5].Trim();
                                string model = transakcjaData[6].Trim();
                                string kolor = transakcjaData[7].Trim();
                                string rokProdukcji = transakcjaData[8].Trim();
                                string przebieg = transakcjaData[9].Trim();
                                string cena = transakcjaData[10].Trim();
                                string pojemnoscSilnika = transakcjaData[11].Trim();
                                string rodzajPaliwa = transakcjaData[12].Trim();
                                string skrzyniaBiegow = transakcjaData[13].Trim();
                                string vin = transakcjaData[14].Trim();

                                
                                Console.WriteLine($"ID transakcji: {idTransakcji}");
                                Console.WriteLine($"Data zakupu: {dataZakupu}");
                                Console.WriteLine($"ID samochodu: {idSamochodu}");
                                Console.WriteLine($"Marka: {marka}");
                                Console.WriteLine($"Model: {model}");
                                Console.WriteLine($"Kolor: {kolor}");
                                Console.WriteLine($"Rok produkcji: {rokProdukcji}");
                                Console.WriteLine($"Przebieg: {przebieg}");
                                Console.WriteLine($"Cena: {cena}");
                                Console.WriteLine($"Pojemność silnika: {pojemnoscSilnika}");
                                Console.WriteLine($"Rodzaj paliwa: {rodzajPaliwa}");
                                Console.WriteLine($"Skrzynia biegów: {skrzyniaBiegow}");
                                Console.WriteLine($"VIN: {vin}");
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odczytu pliku {nazwaPliku}: {ex.Message}");
            }
        }
        static public void WyswietlHistorie()
        {
            string nazwaPliku = "transakcje.txt";

            try
            {
                using (StreamReader reader = new StreamReader(nazwaPliku))
                {
                    Console.WriteLine($"Historia zakupów klientów: ");

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] transakcjaData = line.Split(',');

                            int idTransakcji = int.Parse(transakcjaData[0].Trim());
                            int idKlientaTransakcji = int.Parse(transakcjaData[1].Trim());
                                string imie = transakcjaData[2].Trim();
                                DateTime dataZakupu = DateTime.Parse(transakcjaData[3].Trim());
                                int idSamochodu = int.Parse(transakcjaData[4].Trim());
                                string marka = transakcjaData[5].Trim();
                                string model = transakcjaData[6].Trim();
                                string kolor = transakcjaData[7].Trim();
                                string rokProdukcji = transakcjaData[8].Trim();
                                string przebieg = transakcjaData[9].Trim();
                                string cena = transakcjaData[10].Trim();
                                string pojemnoscSilnika = transakcjaData[11].Trim();
                                string rodzajPaliwa = transakcjaData[12].Trim();
                                string skrzyniaBiegow = transakcjaData[13].Trim();
                                string vin = transakcjaData[14].Trim();

                        
                                Console.WriteLine($"ID transakcji: {idTransakcji}");
                                Console.WriteLine($"ID klienta: {idKlientaTransakcji}");
                                Console.WriteLine($"Imię i nazwisko klienta: {imie}");
                                Console.WriteLine($"Data zakupu: {dataZakupu}");
                                Console.WriteLine($"ID samochodu: {idSamochodu}");
                                Console.WriteLine($"Marka: {marka}");
                                Console.WriteLine($"Model: {model}");
                                Console.WriteLine($"Kolor: {kolor}");
                                Console.WriteLine($"Rok produkcji: {rokProdukcji}");
                                Console.WriteLine($"Przebieg: {przebieg}");
                                Console.WriteLine($"Cena: {cena}");
                                Console.WriteLine($"Pojemność silnika: {pojemnoscSilnika}");
                                Console.WriteLine($"Rodzaj paliwa: {rodzajPaliwa}");
                                Console.WriteLine($"Skrzynia biegów: {skrzyniaBiegow}");
                                Console.WriteLine($"VIN: {vin}");
                                Console.WriteLine();
                            
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odczytu pliku {nazwaPliku}: {ex.Message}");
            }
        }

    }

}

