using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon_samochodowy
{
    public class Authentication:Osoba
    {
        public Authentication(int id, string name, string email) : base(id, name, email)
        {
        }

        public static Klient AktualnieZalogowanyUzytkownik { get; private set; }

        public static Klient AuthenticateCustomer(string email, string password, List<Klient> Klienci)
        {
            AktualnieZalogowanyUzytkownik = Klienci.FirstOrDefault(c => c.Email == email && c.Password == password);
            return AktualnieZalogowanyUzytkownik;
        }

        public static Administrator AuthenticateAdmin(string username, string password, List<Administrator> Administratorzy)
        {
            return Administratorzy.FirstOrDefault(a => a.Username == username && a.Password == password);
        }
        public static Klient GetLoggedInCustomer()
        {
            return Authentication.AktualnieZalogowanyUzytkownik;
        }
    }
}
