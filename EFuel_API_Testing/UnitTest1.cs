using EFuel_API_Testing.TestModels;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace EFuel_API_Testing
{
    public class Tests
    {
        private readonly HttpClient _httpClient;
        public Tests()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5247");
        }
        [Fact]
        public async Task PrijavaRegistracija_LoginCorrectEmailPswd_ReturnsOk()
        {
            //Arrange
            var loginModel = new Login()
            {
                E_posta = "correctuser@email.com",
                Geslo = "Correct?123"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("/login", loginModel);
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task PrijavaRegistracija_LoginMissingPswd_ReturnsBadRequest()
        {
            //Arrange
            var loginModel = new Login()
            {
                E_posta = "correctuser@email.com"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("/login", loginModel);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task PrijavaRegistracija_LoginInvalidEmailPswd_ReturnsBadRequest()
        {
            //Arrange
            var loginModel = new Login()
            {
                E_posta = "correctuser@email.com",
                Geslo = "ThisIsWrongPassword"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("/login", loginModel);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PrijavaRegistracija_RegisterCorrect_ReturnsOk()
        {
            //Arrange
            var correctUporabnik = new Uporabnik()
            {
                E_posta = "correctuser@email.com",
                Geslo = "Correct?123",
                Ime = "Correct",
                Priimek = "Uporabnik"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("/register", correctUporabnik);
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task PrijavaRegistracija_RegisterMissing_ReturnsBadRequest()
        {
            //Arrange
            var correctUporabnik = new Uporabnik()
            {
                E_posta = "missingdata@email.com",
                Geslo = "Missing?123",
                Priimek = "Uporabnik",
                Telefon = "0038641222333"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("/register", correctUporabnik);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task PrijavaRegistracija_RegisterNotCorrect_ReturnsBadRequest()
        {
            //Arrange
            var correctUporabnik = new Uporabnik()
            {
                E_posta = "thisisnotemail",
                Geslo = "Missing?123",
                Priimek = "Uporabnik",
                Ime = "NotCorrrect",
                Telefon = "0038641222333"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("/register", correctUporabnik);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task PrijavaRegistracija_RegisterTelefonFormat_ReturnsOk()
        {
            //Arrange
            var correctUporabnik = new Uporabnik()
            {
                E_posta = "telefonformat@email.com",
                Geslo = "Telefon?123",
                Ime = "Correct",
                Priimek = "Uporabnik",
                Telefon = "+386 41 222 333"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("/register", correctUporabnik);
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task PrijavaRegistracija_LoginCorrect_ReturnsOk()
        {
            //Arrange
            var correctUporabnik = new Uporabnik()
            {
                E_posta = "correctuser@email.com",
                Geslo = "Correct?123"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("/login", correctUporabnik);
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task PrijavaRegistracija_LoginMissing_ReturnsBadRequest()
        {
            //Arrange
            var correctUporabnik = new Uporabnik()
            {
                E_posta = "correctuser@email.com",
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("/login", correctUporabnik);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task PrijavaRegistracija_LoginInvalid_Returns()
        {
            //Arrange
            var correctUporabnik = new Uporabnik()
            {
                E_posta = "correctuser@email.com",
                Geslo = "WrongPassword"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("/login", correctUporabnik);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task Servis_NajblizjiServisImeServisaCorrect_ReturnsOk()
        {
            //Arrange
            var lokacija = "Petrol MB Mlinska";
            //Act
            var response = await _httpClient.GetAsync($"/imeServisa?lokacija={lokacija}");//Currently is POST method
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task Servis_NajblizjiServisImeServisaCorrectType_ReturnsOk()
        {
            try
            {
                //Arrange
                var lokacija = "Petrol MB Mlinska";
                //Act
                var response = await _httpClient.GetAsync($"/imeServisa?lokacija={lokacija}");//Currnetly is POST method
                var contentSerialized = JsonSerializer.Deserialize<List<Servis>>(await response.Content.ReadAsStringAsync());
                //Assert
                Assert.IsType<List<Servis>>(contentSerialized);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Can't get List<Servis> from request. Reason: {ex.Message}");
            }
        }
        [Fact]
        public async Task Servis_NajblizjiServisLokacijaUporabnikaCorrect_ReturnsOk()
        {
            //Arrange
            var lokacijaUporabnika = new LokacijaUporabnika(46.539918, 15.657289);
            //Act
            var response = await _httpClient.GetAsync($"/najblizjiServisUporabniku?lokacija={lokacijaUporabnika}");//Currently it is POST method
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task Servis_NajblizjiServisLokacijaUporabnikaCorrectType_ReturnsOk()
        {
            try
            {
                //Arrange
                var lokacijaUporabnika = new LokacijaUporabnika(46.539918, 15.657289);
                //Act
                var response = await _httpClient.GetAsync($"/najblizjiServisUporabniku?lokacija={lokacijaUporabnika}");//Currently is POST method
                var contentSerialized = JsonSerializer.Deserialize<List<Servis>>(await response.Content.ReadAsStringAsync());
                //Assert
                Assert.IsType<List<Servis>>(contentSerialized);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Can't get List<Servis> from request. Reason: {ex.Message}");
            }
        }
        //GPS Tests

        //Get GPS location and other possible methods

        //Pay
        [Fact]
        public async Task Pay_GetRandomQRCode_ReturnsOk()
        {
            //Arrange

            //Act
            var response = await _httpClient.GetAsync("/randomQRCode");
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task Pay_GetRandomQRCodeBase64Type_ReturnsOk()
        {
            try
            {
                //Arrange

                //Act
                var response = await _httpClient.GetAsync("/randomQRCode");
                var contentString = await response.Content.ReadAsStringAsync();
                var QRCodeAsBuffer = Convert.FromBase64String(contentString);
                //Assert
                Assert.IsType<byte[]>(QRCodeAsBuffer);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Can't convert response content to buffer. Reason: {ex.Message}");
            }
        }

    }
}