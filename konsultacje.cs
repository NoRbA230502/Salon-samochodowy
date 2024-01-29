using System;
using System.IO;

namespace Salon_samochodowy
{
    public class Konsultacja
    {
        public int Id { get; set; }
        public int IdKlienta { get; set; }
        public string ImieKlienta { get; set; }
        public string OpisPytania { get; set; }
        public string Kategoria { get; set; }

        public static void DodajKonsultacje()
        {
            Console.WriteLine("Wybierz kategorię konsultacji:");
            Console.WriteLine("1. Finansowanie kredytowe/leasingowe");
            Console.WriteLine("2. Zamówienie auta");
            Console.WriteLine("3. Oferta biznesowa");

            int wyborKategorii;
            while (!int.TryParse(Console.ReadLine(), out wyborKategorii) || wyborKategorii < 1 || wyborKategorii > 3)
            {
                Console.WriteLine("Niepoprawny wybór. Wybierz numer kategorii od 1 do 3.");
            }

            string kategoria = MapujNumerNaKategorie(wyborKategorii);

            Console.WriteLine("Podaj opis konsultacji:");
            string opis = Console.ReadLine();


            Klient zalogowanyKlient = Authentication.GetLoggedInCustomer();
            int idKlienta = zalogowanyKlient.Id;
            string imieZalogowanego = zalogowanyKlient.Name;

            ZapiszKonsultacjeDoPliku(idKlienta, imieZalogowanego, kategoria, opis);

            Console.Clear();
            Console.WriteLine($"Dodano nową konsultację w kategorii: {kategoria}");
            Console.WriteLine("Nastąpił powrót do menu wyboru");
            Program.WyborKlienta();
        }

        static string MapujNumerNaKategorie(int numer)
        {
            switch (numer)
            {
                case 1:
                    return "Finansowanie kredytowe/leasingowe";
                case 2:
                    return "Zamówienie auta";
                case 3:
                    return "Oferta biznesowa";
                default:
                    return "";
            }
        }

        static void ZapiszKonsultacjeDoPliku(int idKlienta, string imie, string kategoria, string opis)
        {
            try
            {
                int idKonsultacji = PobierzNastepneIdKonsultacji("konsultacje.txt");

                using (StreamWriter writer = new StreamWriter("konsultacje.txt", true))
                {
                    writer.WriteLine($"{idKonsultacji},{idKlienta},{imie},{kategoria},{opis}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu do pliku konsultacje.txt: {ex.Message}");
            }
        }

        static int PobierzNastepneIdKonsultacji(string nazwaPliku)
        {
            List<int> istniejaceId = new List<int>();

            try
            {
                if (File.Exists(nazwaPliku))
                {
                    using (StreamReader reader = new StreamReader(nazwaPliku))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] konsultacjaData = line.Split(',');

                            if (konsultacjaData.Length > 0)
                            {
                                int id = int.Parse(konsultacjaData[0].Trim());
                                istniejaceId.Add(id);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odczytu pliku {nazwaPliku}: {ex.Message}");
            }

            int noweId = 1;
            while (istniejaceId.Contains(noweId))
            {
                noweId++;
            }

            return noweId;
        }

        public static void WyswietlKonsultacje()
        {
            List<Konsultacja> konsultacje = WczytajKonsultacjeZPliku("konsultacje.txt");

            Console.WriteLine("Lista konsultacji:");
            Console.WriteLine();
            Console.WriteLine();

            foreach (Konsultacja konsultacja in konsultacje)
            {
                Console.WriteLine($"ID konsultacji: {konsultacja.Id}");
                Console.WriteLine($"ID klienta: {konsultacja.IdKlienta}");
                Console.WriteLine($"Imię klienta: {konsultacja.ImieKlienta}");
                Console.WriteLine($"Kategoria: {konsultacja.Kategoria}");
                Console.WriteLine($"Opis pytania: {konsultacja.OpisPytania}");
                Console.WriteLine();
            }
        }

        public static void UsunKonsultacje()
        {
            Console.WriteLine("Podaj ID konsultacji do usunięcia: ");
            int id = int.Parse(Console.ReadLine());

            List<Konsultacja> konsultacje = WczytajKonsultacjeZPliku("konsultacje.txt");

            Konsultacja konsultacja = konsultacje.Find(k => k.Id == id);

            if (konsultacja != null)
            {
                konsultacje.Remove(konsultacja);
                ZapiszKonsultacjeDoPliku(konsultacje, "konsultacje.txt");
                Console.Clear();
                Console.WriteLine("Usunięto zrealizowaną konsultację.");
            }
            else
            {
                Console.WriteLine("Nie znaleziono konsultacji o podanym ID.");
            }
        }

        private static void ZapiszKonsultacjeDoPliku(List<Konsultacja> konsultacje, string nazwaPliku)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(nazwaPliku))
                {
                    foreach (Konsultacja konsultacja in konsultacje)
                    {
                        writer.WriteLine($"{konsultacja.Id},{konsultacja.IdKlienta},{konsultacja.ImieKlienta},{konsultacja.Kategoria},{konsultacja.OpisPytania}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu do pliku {nazwaPliku}: {ex.Message}");
            }
        }

        private static List<Konsultacja> WczytajKonsultacjeZPliku(string nazwaPliku)
        {
            List<Konsultacja> konsultacje = new List<Konsultacja>();

            try
            {
                using (StreamReader reader = new StreamReader(nazwaPliku))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] konsultacjaData = line.Split(',');

                        if (konsultacjaData.Length == 5)
                        {
                            int id = int.Parse(konsultacjaData[0].Trim());
                            int idKlienta = int.Parse(konsultacjaData[1].Trim());
                            string imieKlienta = konsultacjaData[2].Trim();
                            string opisPytania = konsultacjaData[3].Trim();
                            string kategoria = konsultacjaData[4].Trim();

                            Konsultacja konsultacja = new Konsultacja
                            {
                                Id = id,
                                IdKlienta = idKlienta,
                                ImieKlienta = imieKlienta,
                                OpisPytania = opisPytania,
                                Kategoria = kategoria
                            };

                            konsultacje.Add(konsultacja);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odczytu pliku: {ex.Message}");
            }

            return konsultacje;
        }
    }
}

