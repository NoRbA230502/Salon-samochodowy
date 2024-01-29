using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;

namespace Salon_samochodowy
{
    public class Klient : Osoba
    {
        public string Password { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Pesel { get; set; }
                                                            

        static List<Klient> Klienci = new List<Klient>();

        public Klient(int id, string name, string email, string password, string address, string telephone, string pesel)
    : base(id, name, email)
        {
            Password = password;
            Address = address;
            Telephone = telephone;
            Pesel = pesel;
        }


        static public void Logowanie()
        {
            List<Klient> klienci = WczytajKlientowZPliku("klienci.txt");

            Console.WriteLine("Podaj e-mail: ");
            string login = Console.ReadLine();
            Console.WriteLine("Podaj hasło: ");
            string hasło = Console.ReadLine();

            Klient authenticatedCustomer = Authentication.AuthenticateCustomer(login, hasło, klienci);
            if (authenticatedCustomer != null)
            {
                Console.Clear();
                Console.WriteLine($"Zalogowano użytkownika: {authenticatedCustomer.Name}");
                Program.WyborKlienta();
            }
            else
            {     
                PonowneLogowanie();
            }
        }

        static public void PonowneLogowanie()
        {
            Console.Clear();
            Console.WriteLine("Błąd logowania, czy chcesz spróbować ponownie?");
            Console.WriteLine("1. Tak");
            Console.WriteLine("2. Wyjdź");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Program.Login();
                    break;
                case "2":
                    Console.WriteLine("Wyjście z programu");
                    break;
                default:
                    Console.WriteLine("Podano nieprawidłową opcję.");
                    PonowneLogowanie();
                    break;
            }
        }

        static public void rejestracja()
        {
            Console.WriteLine("Podaj imię: ");
            string imie = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko: ");
            string nazwisko = Console.ReadLine();
            Console.WriteLine("Podaj e-mail: ");
            string email = Console.ReadLine();
            Console.WriteLine("Podaj hasło: ");
            string hasło = Console.ReadLine();
            Console.WriteLine("Podaj adres: ");
            string adres = Console.ReadLine();
            Console.WriteLine("Podaj numer telefonu: ");
            string telefon = Console.ReadLine();
            Console.WriteLine("Podaj pesel: ");
            string pesel = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazwisko) ||
                   string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(hasło) ||
                   string.IsNullOrWhiteSpace(adres) || string.IsNullOrWhiteSpace(telefon) ||
                   string.IsNullOrWhiteSpace(pesel))
            {
                Console.Clear();
                Console.WriteLine("Wszystkie dane muszą być uzupełnione. Podaj brakujące dane ponownie.");
                Console.WriteLine("Podaj imię: ");

                imie = Console.ReadLine();
                Console.WriteLine("Podaj nazwisko: ");
                nazwisko = Console.ReadLine();
                Console.WriteLine("Podaj e-mail: ");
                email = Console.ReadLine();
                Console.WriteLine("Podaj hasło: ");
                hasło = Console.ReadLine();
                Console.WriteLine("Podaj adres: ");
                adres = Console.ReadLine();
                Console.WriteLine("Podaj numer telefonu: ");
                telefon = Console.ReadLine();
                Console.WriteLine("Podaj pesel: ");
                pesel = Console.ReadLine();
            }
                

                List<Klient> klienci = WczytajKlientowZPliku("klienci.txt");

            int noweId = klienci.Count + 1;
            Klient nowy = new Klient(noweId, imie + " " + nazwisko, email, hasło, adres, telefon, pesel);
            klienci.Add(nowy);

            ZapiszKlientowDoPliku(klienci, "klienci.txt");
            Console.Clear();
            Console.WriteLine("Zarejestrowano użytkownika: " + nowy.Name);
            Program.LoginPo();
            
        }

        static List<Klient> WczytajKlientowZPliku(string nazwaPliku)
        {
            var klienci = new List<Klient>();

            try
            {
                using (StreamReader reader = new StreamReader(nazwaPliku))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] klientData = line.Split(',');

                        if (klientData.Length == 7)
                        {
                            int id = int.Parse(klientData[0].Trim());
                            string name = klientData[1].Trim();
                            string email = klientData[2].Trim();
                            string password = klientData[3].Trim();
                            string address = klientData[4].Trim();
                            string telephone = klientData[5].Trim();
                            string pesel = klientData[6].Trim();
                            

                            Klient klient = new Klient(id, name, email, password, address, telephone, pesel);
                            klienci.Add(klient);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odczytu pliku: {ex.Message}");
            }

            return klienci;
        }

        static void ZapiszKlientowDoPliku(List<Klient> klienci, string nazwaPliku)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(nazwaPliku))
                {
                    foreach (Klient klient in klienci)
                    {
                        writer.WriteLine($"{klient.Id},{klient.Name},{klient.Email},{klient.Password},{klient.Address},{klient.Telephone},{klient.Pesel}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu do pliku: {ex.Message}");
            }
        }


    }
}
