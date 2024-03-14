namespace EFuel_API.Models
{
    public class LokacijaUporabnika
    {
        public double ZemljepisnaSirina { get; set; }
        public double ZemljepisnaDolzina { get; set; }

        public LokacijaUporabnika(double zemljepisnaSirina, double zemljepisnaDolzina)
        {
            ZemljepisnaSirina = zemljepisnaSirina;
            ZemljepisnaDolzina = zemljepisnaDolzina;
        }
    }
}
