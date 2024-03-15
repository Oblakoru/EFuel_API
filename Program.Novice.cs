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

            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);

            var filter = new BsonDocument();
            var documents = collection.Find(filter).ToList();

            Console.WriteLine($"Retrieved {documents.Count} documents from the collection '{collectionName}':");
            foreach (var document in documents)
            {
                Console.WriteLine(document.ToJson());
            }

            app.MapGet("/pozdrav", () => Results.Ok("hey"));
            app.MapGet("/test", () => Results.Ok("test"));

           // app.MapGet("/documents", async (IMongoClient client) =>
           // {
           //     try
           //     {
           //         // Get reference to the MongoDB collection
           //         var database = client.GetDatabase(databaseName);
           //         var collection = database.GetCollection<BsonDocument>(collectionName);
           //
           //         // Perform the basic GET query to retrieve all documents from the collection
           //         var filter = new BsonDocument();
           //         var documents = await collection.Find(filter).ToListAsync();
           //
           //         // Serialize documents to JSON and return as response
           //         return Results.Ok(documents);
           //     }
           //     catch (Exception ex)
           //     {
           //         return Results.BadRequest($"Error retrieving documents: {ex.Message}");
           //     }
           // });

        }
    }
}
