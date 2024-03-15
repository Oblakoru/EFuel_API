using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EFuel_API.Models
{
    public class Servis
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Ime_postaja")]
        public string Ime { get; set; }

        public double ZemljepisnaSirina { get; set; }

        public double ZemljepisnaDolzina { get; set; }

        public double Razdalja { get; set; }

        [BsonElement("Lokacija")]
        public string Lokacija { get; set; }

        [BsonElement("delavni_cas")]
        public string DelovniCas {  get; set; }

        public Servis(string id, string ime, double zemljepisnaSirina, double zemljepisnaDolzina)
        {
            Id = id;
            Ime = ime;
            ZemljepisnaSirina = zemljepisnaSirina;
            ZemljepisnaDolzina = zemljepisnaDolzina;
        }

        public Servis(string id, string ime, string lokacija, string delovniCas)
        {
            Id = id;
            Ime = ime;
            Lokacija = lokacija;
            DelovniCas = delovniCas;
        }
    }
}
