using MongoDB.Driver;

namespace EFuel_API
{
    public partial class Program
    {
        public static void GPS(WebApplication app, MongoClient client)
        {
            app.MapGet("/gps", () => Results.Ok("gps"));
        }
    }
}
