namespace EFuel_API
{
    public partial class Program
    {
        public static void Pay(WebApplication app)
        {
            app.MapGet("/pay", () => Results.Ok("pay"));
        }
    }
}
