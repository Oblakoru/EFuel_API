namespace EFuel_API
{
    public partial class Program
    {
        public static void GPS(WebApplication app)
        {
            app.MapGet("/gps", () => Results.Ok("gps"));
        }
    }
}
