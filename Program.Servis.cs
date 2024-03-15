using EFuel_API.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace EFuel_API
{
    public partial class Program
    {
        public static void Servis(WebApplication app, MongoClient client)
        {
            var databaseName = "eFuel";
            var collectionName = "Bencinska_postaja";

            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);

            app.MapGet("/servis", () => Results.Ok("servis"));

            app.MapPost("/najblizjiServisImeServisa", async (string lokacija) =>
            {
                var servisiList =await PridobiSeznamServisev(collection);

                return servisiList.Where(x=> x.Lokacija == lokacija).ToList();
            });

            /* TESTNI PODATEK - lokacija štuka
            {
                "zemljepisnaSirina": 46.5635072,
                "zemljepisnaDolzina": 15.6257464
            }
            */
            app.MapPost("/najblizjiServisLokacijaUporabnika", async (LokacijaUporabnika lokacija) =>
            {
                // klic na bazo: baza vrne seznam servisov
                Servis servis1 = new Servis("1", "Petrol MB Mlinska", 46.55735936254534, 15.654900389190303);
                Servis servis2 = new Servis("2", "Petrol MB Partizanska", 46.56480963033872, 15.659228706081425);
                Servis servis3 = new Servis("3", "Petrol MB Gosposvetska", 46.56499044700252, 15.626309844557982);
                List<Servis> servisiList = new() { servis1, servis2, servis3 };
                //

                foreach (Servis servis in servisiList)
                    servis.Razdalja = IzracunRazdalje(lokacija, servis);

                return servisiList.OrderBy(x => x.Razdalja).ToList().First();

            });
        }

        private static async Task<List<Servis>> PridobiSeznamServisev(IMongoCollection<BsonDocument> collection)
        {
            var result = await collection.Find(_ => true).ToListAsync();

            var servisiList = result.Select(doc =>
            {
                var id = doc["_id"].AsObjectId.ToString();
                var imePostaja = doc.GetValue("Ime_postaja", "").AsString;
                var lokacija = doc.GetValue("Lokacija", "").AsString;
                var delovniCas = doc.GetValue("delavni_cas", "").AsString;
                return new Servis(id, imePostaja, lokacija, delovniCas);
            }).ToList();

            return servisiList;
        }

        private static double IzracunRazdalje(LokacijaUporabnika uporabnik, Servis servis)
        {
            double earthRadiusKm = 6371.0;

            double _sirina = PretvorbaVRadiane(servis.ZemljepisnaSirina - uporabnik.ZemljepisnaSirina);
            double _dolzina = PretvorbaVRadiane(servis.ZemljepisnaDolzina - uporabnik.ZemljepisnaDolzina);

            double a = Math.Sin(_sirina / 2) * Math.Sin(_sirina / 2) +
                       Math.Sin(_dolzina / 2) * Math.Sin(_dolzina / 2) *
                       Math.Cos(PretvorbaVRadiane(uporabnik.ZemljepisnaSirina)) * Math.Cos(PretvorbaVRadiane(servis.ZemljepisnaSirina));
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadiusKm * c;
        }

        private static double PretvorbaVRadiane(double stopinje)
        {
            return stopinje * (Math.PI / 180);
        }
    }
}
