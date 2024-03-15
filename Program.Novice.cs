using EFuel_API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EFuel_API
{
    public partial class Program
    {
        public static void Novice(WebApplication app, MongoClient client)
        {

            var databaseName = "eFuel";
            var collectionName = "Novice";

            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);

            app.MapGet("/pridobiNovice", async () =>
            {

                var result = await collection.Find(_ => true).ToListAsync();

                var jsonSerializer = new MongoDB.Bson.IO.JsonWriterSettings { OutputMode = MongoDB.Bson.IO.JsonOutputMode.JavaScript };
                var json = result.ToJson(jsonSerializer);

                return Results.Ok(json);
            });

            app.MapGet("/pridobiNoviceTest", async () =>
            {
                var result = await collection.Find(_ => true).ToListAsync();

                var novicaList = result.Select(doc => new NovicaModel
                {
                    Id = doc["_id"].AsObjectId.ToString(),
                    Naslov = doc["naslov"].AsString,
                    Novica = doc["novica"].AsString,
                    DatumObjave = doc["datum_objave"].AsString
                }).ToList();

                foreach (var novica in novicaList)
                {
                    Console.WriteLine(novica);
                }

                return Results.Ok();
            });

            app.MapGet("/pridobiNovice/{date}", async (string date) =>
            {
                DateTime specifiedDate;
                if (!DateTime.TryParse(date, out specifiedDate))
                {
                    return Results.BadRequest("Invalid date format. Please provide the date in yyyy-MM-dd format.");
                }


                var filter = Builders<BsonDocument>.Filter.Eq("datum_objave", specifiedDate.ToString("yyyy-MM-dd"));
                var result = await collection.Find(filter).ToListAsync();

                var jsonSerializer = new MongoDB.Bson.IO.JsonWriterSettings { OutputMode = MongoDB.Bson.IO.JsonOutputMode.JavaScript };
                var json = result.ToJson(jsonSerializer);

                return Results.Ok(json);
            });

            app.MapGet("/pridobiNovice/{fromDate}/{toDate}", async (DateTime fromDate, DateTime toDate) =>
            {


                var filter = Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Gte("datum_objave", fromDate.ToString("yyyy-MM-dd")),
                    Builders<BsonDocument>.Filter.Lte("datum_objave", toDate.ToString("yyyy-MM-dd"))
                );

                var result = await collection.Find(filter).ToListAsync();

                var jsonSerializer = new MongoDB.Bson.IO.JsonWriterSettings { OutputMode = MongoDB.Bson.IO.JsonOutputMode.JavaScript };
                var json = result.ToJson(jsonSerializer);

                return Results.Ok(json);
            });

            /// <summary>
            /// Endpoint za iskanje novice o prometu po njenem ID.
            /// </summary>
            /// <param name="id">ID prometne novice, ki jo želite pridobiti.</param>
            /// <returns>Vrne prometne novice z navedenim ID, če so bile najdene, ali ustrezen odgovor o napaki v nasprotnem primeru..</returns>

            app.MapGet("/pridobiNovico/{id}", async (string id) =>
            {
                try
                {
                    var objectId = ObjectId.Parse(id);

                    var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);

                    var result = await collection.Find(filter).FirstOrDefaultAsync();

                    if (result != null)
                    {
                        var dict = result.ToDictionary();
                        return Results.Ok(dict);
                    }
                    else
                    {
                        return Results.NotFound($"Prometna novia z ID: '{id}' ni bil najden!.");
                    }
                }
                catch (FormatException)
                {
                    return Results.BadRequest("Nepravilna oblika ID. Navedite veljavni ObjectId..");
                }
                catch (Exception ex)
                {
                    return Results.BadRequest($"Napaka: {ex.Message}");
                }
            });


            /// <summary>
            /// Endpoint za dodajanje novih prometnih novic.
            /// </summary>
            /// <param name="novica">Novica o prometu, ki jo je treba dodati.</param>
            /// <returns>Vrne odgovor, ki označuje stanje operacije.</returns>

            app.MapPost("/dodajNovico", async (NovicaModel novica) =>
            {
                try
                {
                    var novicaDocument = new BsonDocument
                        {
                        { "naslov", novica.Naslov },
                        { "novica", novica.Novica },
                        { "datum_objave", novica.DatumObjave }
                    };
                   

                    await collection.InsertOneAsync(novicaDocument);

                    var insertedId = novicaDocument["_id"].AsObjectId.ToString();

                    var insertedNovica = new NovicaModel
                    {
                        Id = insertedId,
                        Naslov = novica.Naslov,
                        Novica = novica.Novica,
                        DatumObjave = novica.DatumObjave
                    };

                    return Results.Created($"/dodajNovico/{insertedId}", insertedNovica);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest($"Napaka: {ex.Message}");
                }
            });


            app.MapDelete("/izbrisiNovico/{id}", async (string id) =>
            {

                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));

                var result = await collection.DeleteOneAsync(filter);

                return Results.NoContent();
            });


        }
    }
}
