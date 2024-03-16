using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFuel_API_Testing.TestModels
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
        public string? Telefon { get; set; }

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
