using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFuel_API_Testing.TestModels
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
