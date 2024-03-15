using MongoDB.Driver;

namespace EFuel_API
{
    public partial class Program
    {
        public static void PrijavaRegistracija(WebApplication app, MongoClient client)
        {
            app.MapGet("/login", () => Results.Ok("login"));
            app.MapGet("/register", () => Results.Ok("register"));
        }
    }
}
