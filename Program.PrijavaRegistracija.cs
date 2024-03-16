using EFuel_API.Models;
using Microsoft.AspNetCore.Mvc;
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


            app.MapPost("/login", async ([FromBody]Login uporabnik) =>
            {
                if (uporabnik != null)
                {
                    if (string.IsNullOrEmpty(uporabnik.E_posta) || string.IsNullOrEmpty(uporabnik.Geslo))
                    {
                        return Results.BadRequest("Niso podani vsi zahtevani podatki!");
                    }

                    var user = await collection.Find(Builders<BsonDocument>.Filter.And(
                        Builders<BsonDocument>.Filter.Eq("E_posta", uporabnik.E_posta),
                        Builders<BsonDocument>.Filter.Eq("Geslo", uporabnik.Geslo)
                    )).FirstOrDefaultAsync();

                    if (user == null)
                    {
                        return Results.BadRequest("Napačna e-pošta ali geslo!");
                    }

                    return Results.Ok("Uspešna prijava!");
                }
                else
                {
                    return Results.BadRequest("Napačno podani podatki!");
                }
            });


            app.MapPost("/register", async ([FromBody]Uporabnik uporabnik) => {

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
                        return Results.BadRequest("Eposta ze v uporabi");
                    }

                    uporabnik.Id = ObjectId.GenerateNewId().ToString();

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
