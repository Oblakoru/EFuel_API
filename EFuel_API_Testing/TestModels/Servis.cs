using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFuel_API_Testing.TestModels
{
    public class Servis
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Ime_postaja")]
        public string Ime { get; set; }

        [BsonElement("ZemljepisnaSirina")]
        public double ZemljepisnaSirina { get; set; }

        [BsonElement("ZemljepisnaDolzina")]
        public double ZemljepisnaDolzina { get; set; }

        public double Razdalja { get; set; }

        [BsonElement("Lokacija")]
        public string Lokacija { get; set; }

        [BsonElement("delavni_cas")]
        public string DelovniCas { get; set; }


        public Servis(string id, string ime)
        {
            Id = id;
            Ime = ime;
        }

        public Servis(string id, string ime, double zemljepisnaSirina, double zemljepisnaDolzina) : this(id, ime)
        {
            ZemljepisnaSirina = zemljepisnaSirina;
            ZemljepisnaDolzina = zemljepisnaDolzina;
        }

        public Servis(string id, string ime, string lokacija, string delovniCas) : this(id, ime)
        {
            Lokacija = lokacija;
            DelovniCas = delovniCas;
        }

        public Servis(string id, string ime, double zemljepisnaSirina, double zemljepisnaDolzina, string lokacija, string delovniCas) : this(id, ime)
        {
            ZemljepisnaSirina = zemljepisnaSirina;
            ZemljepisnaDolzina = zemljepisnaDolzina;
            Lokacija = lokacija;
            DelovniCas = delovniCas;
        }
    }
}
