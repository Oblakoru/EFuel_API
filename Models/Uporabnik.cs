using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using static QRCoder.PayloadGenerator;

namespace EFuel_API.Models
{
    public class Uporabnik
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public string E_posta { get; set; }
        public string Geslo { get; set; }
        public int? Telefon { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Ime: {Ime}, Priimek: {Priimek}, E_posta: {E_posta}, Telefon: {Telefon}";
        }
    }

    public class Login
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string E_posta { get; set; }
        public string Geslo { get; set; }
    }

}
