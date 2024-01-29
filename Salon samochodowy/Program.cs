using Salon_samochodowy;
using System.ComponentModel.Design;
using System.IO;

namespace Salon_samochodowy
{
   
    internal class Program
    {
        static void Main()
        {
            Menu();
        }

        static void Menu()
        {
            Console.WriteLine("Witaj w salonie samochodowym");
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1. Zaloguj się");
            Console.WriteLine("2. Zarejestruj się");
            Console.WriteLine("3. Wyjdź");
            string wybor = Console.ReadLine();
            if (wybor == "1")
            {
                Login();
            }
            else if (wybor == "2")
            {
                Klient.rejestracja();
            }
            else if (wybor == "3")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Podano nieprawidłową wartość.");
                Menu();
            }

        }
        static public void LoginPo()
        {
            Console.WriteLine("Czy chcesz się zalogować?");
            Console.WriteLine("1. Tak");
            Console.WriteLine("2. Nie");
            string method = Console.ReadLine();

            if (method == "1")
            {
                Console.Clear();
                Klient.Logowanie();
            }
            else if (method == "2")
            {
                Console.Clear();
                Menu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Podano nieprawidłowe dane.");
                Login();
            }
        }

        static public void Login()
        {
            Console.WriteLine("Wybierz metodę logowania:");
            Console.WriteLine("1. Pracownik/Administrator");
            Console.WriteLine("2. Klient/Użytkownik");
            Console.WriteLine("3. Wyjdź");
            string method = Console.ReadLine();

            if (method == "1")
            {
                Console.Clear();
                Administrator.Logowanie();
            }
            else if (method == "2")
            {
                Console.Clear();
                Klient.Logowanie();

            }
            else if (method == "3")
            {
                Console.Clear();
                Menu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Podano nieprawidłowe dane.");
                Login();
            }
        }
        static public void Zarzadzanie()
        {
            Console.WriteLine("Wybierz opcję pracownika:");
            Console.WriteLine("1. Wyświetl stan dostępności samochodów");
            Console.WriteLine("2. Dodaj samochód do stanu");
            Console.WriteLine("3. Usuń samochód ze stanu");
            Console.WriteLine("4. Wyświetl historię transakcji klientów");
            Console.WriteLine("5. Wyświetl konsultacje klientów");
            Console.WriteLine("6. Wyloguj się");
            Console.WriteLine("7. Wyjdź");
            
            string wybor = Console.ReadLine();

            if (wybor == "1")
            {
                Console.Clear();
                Samochod.WyswietlStan();
                PonownyWyborAdmin();    
            }
            else if (wybor == "2")
            {
                Console.Clear();
                Samochod.DodajSamochod();
            }
            else if (wybor == "3")
            {
                Console.Clear();
                Samochod.UsunSamochod();
            }
            
            else if (wybor == "4")
            {
                Console.Clear();
                Transakcje.WyswietlHistorie();
                PonownyWyborAdmin();
            }
            else if (wybor == "5")
            {
                Console.Clear();
                Konsultacja.WyswietlKonsultacje();
                AdminKonsu();
            }
            else if (wybor == "6")
            {
                Console.Clear();
                Menu();
            }
            else if (wybor == "7")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Podano nieprawidłową wartość.");
                Zarzadzanie();  
            }
        }
        static public void PonownyWyborAdmin()
        {
            Console.WriteLine("Czy kontynuować?");
            Console.WriteLine("1. Tak");
            Console.WriteLine("2. Wyjdź");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Zarzadzanie();
                    break;
                case "2":
                    Console.WriteLine("Wyjście z programu");
                    break;
                default:
                    Console.WriteLine("Podano nieprawidłową opcję.");
                    PonownyWyborAdmin();
                    break;
            }
        }
        static public void AdminKonsu()
        {
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1. Usuń zrelizowaną konsultację");
            Console.WriteLine("2. Wróć do menu");
            Console.WriteLine("3. Wyjdź");
            string wybor = Console.ReadLine();
            if (wybor == "1")
            {
                Konsultacja.UsunKonsultacje();

            }
            else if (wybor == "2")
            {
                Console.Clear();
                Zarzadzanie();
            }
            else if (wybor == "3")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Podano nieprawidłową wartość.");
                AdminKonsu();
            }
        }

        static public void WyborKlienta()
        {
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1. Wyświetl aktualnie dostępne samochody");
            Console.WriteLine("2. Wyświetl historię zakupu");
            Console.WriteLine("3. Zapisz się na konsultację");
            Console.WriteLine("4. Wyloguj się");
            Console.WriteLine("5. Wyjdź");
           
            string wybor = Console.ReadLine();

            if (wybor == "1")
            {
                Console.Clear();
                Samochod.WyswietlStan();
                Console.WriteLine("Czy chcesz dokonać zakupu?");
                Console.WriteLine("1. Tak");
                Console.WriteLine("2. Nie");
                string wybor2 = Console.ReadLine();
                if (wybor2 == "1")
                {
                    Transakcje.Zakup();
                }
                else if (wybor2 == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Wróc");
                    WyborKlienta();
                }
                else if (wybor2 == "3")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Podano nieprawidłową wartość.");
                    WyborKlienta();
                }
            }
            else if (wybor == "2")
            {
                int idZalogowanegoKlienta = Authentication.GetLoggedInCustomer().Id;
                Transakcje.WyswietlHistorieZakupow(idZalogowanegoKlienta);

                Console.WriteLine("Czy chcesz wrócić do menu klienta?");
                Console.WriteLine("1. Tak");
                Console.WriteLine("2. Nie");
                string wybor3 = Console.ReadLine();

                if (wybor3 == "1")
                {
                    Console.Clear();
                    WyborKlienta();
                }
                else if (wybor3 == "2")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Podano nieprawidłową wartość.");
                    WyborKlienta();
                }
            }
            else if (wybor == "3")
            {
                Console.Clear();
                Konsultacja.DodajKonsultacje();
            }

            else if (wybor == "4")
            {
                Console.Clear();
                Menu();
            }
            else if (wybor == "5")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Podano nieprawidłową wartość.");
                WyborKlienta();
            }
            
        }

       
    }
}

