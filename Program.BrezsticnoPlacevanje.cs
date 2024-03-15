using MongoDB.Driver;

namespace EFuel_API
{
    public partial class Program
    {
        public static void Pay(WebApplication app, MongoClient client)
        {
            app.MapGet("/pay", () => Results.Ok("pay"));
        }
    }
}
