namespace EFuel_API
{
    public partial class Program
    {
        public static void Servis(WebApplication app)
        {
            app.MapGet("/servis", () => Results.Ok("servis"));
        }
    }
}
