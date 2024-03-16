using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFuel_API_Testing.TestModels
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
