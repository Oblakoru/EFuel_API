namespace EFuel_API.Models
{
    public class Servis
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public double ZemljepisnaSirina { get; set; }
        public double ZemljepisnaDolzina { get; set; }
        public double Razdalja { get; set; }

        public Servis(int id, string ime, double zemljepisnaSirina, double zemljepisnaDolzina)
        {
            Id = id;
            Ime = ime;
            ZemljepisnaSirina = zemljepisnaSirina;
            ZemljepisnaDolzina = zemljepisnaDolzina;
        }
    }
}
