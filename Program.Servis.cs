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

			app.MapPost("/imeServisa", async (string lokacija) =>
			{
				var servisiList = await PridobiSeznamServisev(collection);

				return servisiList.Where(x => x.Lokacija == lokacija).ToList();
			});

			/* TESTNI PODATEK - lokacija štuka
			{
				"zemljepisnaSirina": 46.5635072,
				"zemljepisnaDolzina": 15.6257464
			}
			*/
			app.MapPost("/najblizjiServisUporabniku", async (LokacijaUporabnika lokacija) =>
			{
				var servisiList = await PridobiSeznamServisev(collection);

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
				var sirina = doc.GetValue("ZemljepisnaSirina", 0).AsDouble;
				var dolzina = doc.GetValue("ZemljepisnaDolzina", 0).AsDouble;
				var lokacija = doc.GetValue("Lokacija", "").AsString;
				var delovniCas = doc.GetValue("delavni_cas", "").AsString;
				return new Servis(id, imePostaja, sirina, dolzina, lokacija, delovniCas);
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
