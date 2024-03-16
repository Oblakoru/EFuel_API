using EFuel_API.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace EFuel_API
{
    public partial class Program
    {
        public static void PrijavaRegistracija(WebApplication app, MongoClient client)
        {
            var name = "eFuel";
            var collectionName = "Uporabniki";

            var db = client.GetDatabase(name);
            var collection = db.GetCollection<BsonDocument>(collectionName);


            app.MapGet("/login", () => Results.Ok("login"));


            app.MapGet("/register", async (Uporabnik uporabnik) => {

                if (uporabnik != null)
                {
                    if (string.IsNullOrEmpty(uporabnik.Ime) || string.IsNullOrEmpty(uporabnik.Priimek) || string.IsNullOrEmpty(uporabnik.E_posta) || string.IsNullOrEmpty(uporabnik.Geslo) )
                    {
                        return Results.BadRequest("Niso podani vsi zahtevani podatki!");
                    }

                    if (!Regex.IsMatch(uporabnik.E_posta, @"@.*\.com"))
                    {
                        return Results.BadRequest("Napačno podana epošta!");
                    }

                    var userObstaja = await collection.Find(Builders<BsonDocument>.Filter.Eq("E_posta", uporabnik.E_posta)).AnyAsync();

                    if (userObstaja)
                    {
                        return Results.BadRequest("Epošta več obstaja!");
                    }

                    await collection.InsertOneAsync(uporabnik.ToBsonDocument());

                    return Results.Ok("Uporabnik je registriran");
                }
                else 
                {
                    return Results.BadRequest("Napačno podani podatki!");
                }
            
            });
        }
    }
}
