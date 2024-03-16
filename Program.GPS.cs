using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using EFuel_API.Models;

namespace EFuel_API
{
    public partial class Program
    {
        public static void GPS(WebApplication app, MongoClient client)
        {
            /*"London" 51.5074, -0.1278
             "Los Angeles" 34.0522, -118.2437
             "New York" 40.7128, -74.0060
             */

            app.MapPost("/gps", ([FromBody] LokacijaUporabnika lokacijaUporabnika) => {

                if(string.IsNullOrEmpty(lokacijaUporabnika.ZemljepisnaDolzina.ToString()) || string.IsNullOrEmpty(lokacijaUporabnika.ZemljepisnaSirina.ToString()))
                {
                    return Results.BadRequest("Napačno podana lokacija!");
                }

                var googleMaps = $"https://www.google.com/maps/place/{lokacijaUporabnika.ZemljepisnaSirina},{lokacijaUporabnika.ZemljepisnaDolzina}";

                return Results.Ok(googleMaps);
            });
        }
    }
}
