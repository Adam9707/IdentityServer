using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class TokenService 
    {
        private readonly IdentityServerSettings _identityServerSettings;
        private readonly DiscoveryDocumentResponse _discoveryDocument;

        public TokenService(IdentityServerSettings identityServerSettings)
        {
            _identityServerSettings = identityServerSettings;

            using var httpClient = new HttpClient();
            _discoveryDocument = httpClient.GetDiscoveryDocumentAsync(identityServerSettings.DiscoveryUrl).Result;
            if (_discoveryDocument.IsError)
            {
                throw new Exception("Unable to get discovery document", _discoveryDocument.Exception);
            }
        }

        public async Task<TokenResponse> GetToken(string scope)
        {
            using var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discoveryDocument.TokenEndpoint,

                ClientId = _identityServerSettings.ClientName,
                ClientSecret = _identityServerSettings.ClientPassword,
                Scope = scope
            });


            if (tokenResponse.IsError)
            {
                throw new Exception("Unable to get token", tokenResponse.Exception);
            }

            return tokenResponse;
        }

    }
}
