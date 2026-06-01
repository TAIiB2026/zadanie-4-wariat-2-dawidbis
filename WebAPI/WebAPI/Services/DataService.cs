using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services 
{
    public class DataService : IGetDataInterface, IFormSubmitInterface
    {
        private static readonly List<Ksiazka> _ksiazki = new List<Ksiazka>()
        {
            new Ksiazka { Id = 1, Tytul = "Władca Pierścieni", Cena = 49.99m, DataWydania = new DateTime(1954, 7, 29) },
            new Ksiazka { Id = 2, Tytul = "Diuna", Cena = 39.99m, DataWydania = new DateTime(1965, 8, 1) }
        };

        public IEnumerable<Ksiazka> Get()
        {
            return _ksiazki;
        }

        public Ksiazka GetByID(int id)
        {
            return _ksiazki.FirstOrDefault(k => k.Id == id);
        }

        public void Add(Ksiazka ksiazka)
        {
            ksiazka.Id = _ksiazki.Any() ? _ksiazki.Max(k => k.Id) + 1 : 1;
            _ksiazki.Add(ksiazka);
        }

        public void Edit(Ksiazka ksiazka)
        {
            var istniejaca = _ksiazki.FirstOrDefault(k => k.Id == ksiazka.Id);
            if (istniejaca != null)
            {
                istniejaca.Tytul = ksiazka.Tytul;
                istniejaca.Cena = ksiazka.Cena;
                istniejaca.DataWydania = ksiazka.DataWydania;
            }
        }
    }
}