using MongoDB.Driver;
using QRCoder;
using ZXing.Common;
using ZXing;
using ZXing.QrCode.Internal;
using System.Drawing;
using ZXing.Rendering;

namespace EFuel_API
{
    public partial class Program
    {
        // Zaradi težavnosti implementacije brezstičnega plačevanja, smo se odločili, da tega ne bomo implementirali - ter ga zamenjali s QR kodo
        public static void Pay(WebApplication app, MongoClient client)
        {
            app.MapGet("/api/randomQRCode", async (HttpContext context) =>
            {
               
            });

            app.Run();
        }
    }


}
