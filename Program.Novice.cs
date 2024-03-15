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
            var collectionName = "Ponudba_goriva";

            app.MapGet("/pridobiPonudbo", async () =>
            {

                var database = client.GetDatabase(databaseName);
                var collection = database.GetCollection<BsonDocument>(collectionName);

                var result = await collection.Find(_ => true).ToListAsync();

                var jsonSerializer = new MongoDB.Bson.IO.JsonWriterSettings { OutputMode = MongoDB.Bson.IO.JsonOutputMode.JavaScript };
                var json = result.ToJson(jsonSerializer);

                return Results.Ok(json);
            });


        }
    }
}
