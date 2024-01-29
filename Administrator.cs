using Salon_samochodowy;
using System;
using System.Collections.Generic;
using System.IO;

namespace Salon_samochodowy
{
    public class Administrator : Osoba
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Position { get; set; }   


        public Administrator(int id, string name, string email, string username, string password, string address, string telephone, string position)
            : base(id, name, email)
        {
            Username = username;
            Password = password;
        }

        public static List<Administrator> WczytajAdmin()
        {
            var administratorzy = new List<Administrator>();

            try
            {
                using (StreamReader reader = new StreamReader("administratorzy.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] adminData = line.Split(',');

                        if (adminData.Length == 8)
                        {
                            int id = int.Parse(adminData[0].Trim());
                            string name = adminData[1].Trim();
                            string email = adminData[2].Trim();
                            string username = adminData[3].Trim();
                            string password = adminData[4].Trim();
                            string address = adminData[5].Trim();
                            string telephone = adminData[6].Trim();
                            string position = adminData[7].Trim();


                            Administrator admin = new Administrator(id, name, email, username, password, address, telephone, position);
                            administratorzy.Add(admin);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odczytu pliku: {ex.Message}");
            }

            return administratorzy;
        }

        public static void Logowanie()
        {
            List<Administrator> administratorzy = WczytajAdmin();

            Console.WriteLine("Podaj login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Podaj hasło: ");
            string hasło = Console.ReadLine();

            Administrator authenticatedAdmin = Authentication.AuthenticateAdmin(login, hasło, administratorzy);
            if (authenticatedAdmin != null)
            {
                Console.Clear();
                Console.WriteLine($"Logowanie pomyślne, witaj: {authenticatedAdmin.Name}");
                Program.Zarzadzanie();
            }
            else
            {
                Klient.PonowneLogowanie();
            }
        }
        

    }
}
