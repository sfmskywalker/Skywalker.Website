using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Pulumi;
using Sodium;

namespace Skywalker.Website.Infra
{
    public class GitHubClient
    {
        private readonly string _owner;
        private readonly string _repo;
        private readonly string _token;
        private readonly HttpClient _httpClient;

        public GitHubClient(string owner, string repo, string token)
        {
            _owner = owner;
            _repo = repo;
            _token = token;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.github.com")
            };

            //var token = _config.Require("token");

            _httpClient.DefaultRequestHeaders.Accept.Add(
                MediaTypeWithQualityHeaderValue.Parse("application/vnd.github.v3+json"));
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Pulumi", "1"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
        }

        public async Task SetSecretAsync(string name, string value)
        {
            // var owner = _config.Require("owner");
            // var repo = _config.Require("repo");
            var owner = _owner;
            var repo = _repo;
            var publicKeyResponse = await _httpClient.GetAsync($"repos/{owner}/{repo}/actions/secrets/public-key");
            var publicKeyJson = await publicKeyResponse.Content.ReadAsStringAsync();
            var publicKeyModel = JToken.Parse(publicKeyJson);
            var publicKey = Convert.FromBase64String(publicKeyModel.Value<string>("key"));
            var publicKeyId = publicKeyModel.Value<string>("key_id");
            var secretValue = System.Text.Encoding.UTF8.GetBytes(value);
            var sealedPublicKeyBox = SealedPublicKeyBox.Create(secretValue, publicKey);
            var sealedPublicKeyBoxEncoded = Convert.ToBase64String(sealedPublicKeyBox);
            
            var updateContent =
                new StringContent(
                    JToken.FromObject(new {encrypted_value = sealedPublicKeyBoxEncoded, key_id = publicKeyId})
                        .ToString(), System.Text.Encoding.UTF8, "applcation/json");
            
            var updateResponse =
                await _httpClient.PutAsync($"repos/{owner}/{repo}/actions/secrets/{name}",
                    updateContent);
            
            updateResponse.EnsureSuccessStatusCode();
        }
    }
}