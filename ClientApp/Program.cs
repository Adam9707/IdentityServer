// See https://aka.ms/new-console-template for more information

using ClientApp;
using IdentityModel.Client;

var identityServerSettings = new IdentityServerSettings
{
    DiscoveryUrl = "https://localhost:5001",
    ClientName = "m2m.client",
    ClientPassword = "511536EF-F270-4058-80CA-1C89C192F69AA",
    UseHttps = true,
};

var scope = "WebApi.read";

var apiUrl = "https://localhost:7006/Test";

var tokenService = new TokenService(identityServerSettings);

HttpClient client = new HttpClient();

var token = await tokenService.GetToken(scope);

client.SetBearerToken(token.AccessToken);


using var response = await client.GetAsync(apiUrl);

Console.WriteLine("WebApi client!");

if (response.IsSuccessStatusCode)
{
    string responseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseBody);
}
else
{
    var statusCode = response.StatusCode;
    Console.WriteLine($"Status code: {statusCode}");
}


Console.ReadKey();

