using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EFuel_API.Models
{
  
    public class NovicaModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("naslov")]
        public string Naslov { get; set; }

        [BsonElement("novica")]
        public string Novica { get; set; }

        [BsonElement("datum_objave")]
        public string DatumObjave { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Naslov: {Naslov}, Datum objave: {DatumObjave}, Vsebina: {Novica}";
        }
    }
}
