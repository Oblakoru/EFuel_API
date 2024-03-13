namespace EFuel_API
{
    public partial class Program
    {
        public static void PrijavaRegistracija(WebApplication app)
        {
            app.MapGet("/login", () => Results.Ok("login"));
            app.MapGet("/register", () => Results.Ok("register"));
        }
    }
}
