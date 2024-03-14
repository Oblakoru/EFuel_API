using EFuel_API.Models;

namespace EFuel_API
{
    public partial class Program
    {
        public static void Servis(WebApplication app)
        {
            app.MapGet("/servis", () => Results.Ok("servis"));

            app.MapPost("/najblizjiServis", (LokacijaUporabnika lokacija) =>
            {
                var servis = NajdiNajblizjiServis(lokacija);
                return Results.Ok(servis);
            });
        }

        /* TESTNI PODATEK - lokacija štuka
            {
                "zemljepisnaSirina": 46.5635072,
                "zemljepisnaDolzina": 15.6257464
            }
        */

        private static object NajdiNajblizjiServis(LokacijaUporabnika lokacija)
        {
            // klic na bazo: baza vrne seznam servisov
            Servis servis1 = new Servis(1, "Petrol MB Mlinska", 46.55735936254534, 15.654900389190303);
            Servis servis2 = new Servis(2, "Petrol MB Partizanska", 46.56480963033872, 15.659228706081425);
            Servis servis3 = new Servis(3, "Petrol MB Gosposvetska", 46.56499044700252, 15.626309844557982);
            List<Servis> ListServisov = new() { servis1, servis2, servis3 };
            //

            foreach (Servis servis in ListServisov)
                servis.Razdalja = IzracunRazdalje(lokacija, servis);

            return ListServisov.OrderBy(x => x.Razdalja).ToList().First();
        }

        private static double IzracunRazdalje(LokacijaUporabnika uporabnik, Servis servis)
        {
            double earthRadiusKm = 6371.0;

            double _sirina = PretvorbaVRadiane(servis.ZemljepisnaSirina - uporabnik.ZemljepisnaSirina);
            double _dolzina = PretvorbaVRadiane(servis.ZemljepisnaDolzina - uporabnik.ZemljepisnaDolzina);

            double a = Math.Sin(_sirina / 2) * Math.Sin(_sirina / 2) +
                       Math.Sin(_dolzina / 2) * Math.Sin(_dolzina / 2) *
                       Math.Cos(PretvorbaVRadiane(uporabnik.ZemljepisnaSirina)) * Math.Cos(PretvorbaVRadiane(servis.ZemljepisnaSirina));
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadiusKm * c;
        }

        private static double PretvorbaVRadiane(double stopinje)
        {
            return stopinje * (Math.PI / 180);
        }
    }
}
