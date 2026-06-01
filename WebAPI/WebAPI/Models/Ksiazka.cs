namespace WebAPI.Models
{
    public class Ksiazka
    {
        public int Id { get; set; }
        public required string Tytul { get; set; }
        public decimal Cena { get; set; }
        public DateTime DataWydania { get; set; }
    }
}
