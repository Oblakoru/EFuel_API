namespace EFuel_API
{
    public partial class Program
    {
        public static void Novice(WebApplication app)
        {

            app.MapGet("/pozdrav", () => Results.Ok("hey"));

        }
    }
}
