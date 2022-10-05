using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using HangmanAPI;
using Newtonsoft.Json;
using HangmanAPI.Model.Entity;
using Microsoft.Extensions.Configuration;

namespace HangmanAPI.Test {
    public class IntegrationTest {
        private Guid gameId;
        private WebApplicationFactory<Program> application;
        private HttpClient client;
        JsonSerializer serializer;
        public IntegrationTest() {
            var projectDir = AppDomain.CurrentDomain.BaseDirectory;
            var configPath = Path.Combine(projectDir, "appsettings.json");
            application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => {
                builder.ConfigureAppConfiguration((context, conf) => {
                    conf.AddJsonFile(configPath);
                });
            });
            client = application.CreateClient();
            serializer = new JsonSerializer();
        }

        [Fact]
        public async Task CanCreateSingleGame() {
            var response = await client.PostAsync("/api/game", null);
            CheckContentType(response);
            Assert.NotNull(response);

            Assert.True(response.IsSuccessStatusCode);

            using var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            using var jsonReader = new JsonTextReader(streamReader);

            var content = serializer.Deserialize<Guid>(jsonReader);

            Assert.True(content != Guid.Empty);
            gameId = content;
        }

        [Fact]
        public async Task CanGetSingleGameFromId() {
            if (gameId == Guid.Empty) {
                return;
            }

            var response = await client.GetAsync($"api/game/{gameId}");
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);
            CheckContentType(response);

            var content = await DeserializeObject<GameExportModel>(response);

            Assert.NotNull(content);

            Assert.True(content.Completed == false);
            Assert.True(content.TotalIncorrectGuesses == 0);
            Assert.True(content.GuessedWord.Contains("_"));
            Assert.True(content.GuessedWord.Distinct().Count() == 1);
        }

        [Fact]
        public async Task CanMakeGuess() {
            if (gameId == Guid.Empty) {
                return;
            }

            const char letterToTest = 'a';

            var response = await client.PutAsync($"api/game/{gameId}/{letterToTest}", null);

            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);
            CheckContentType(response);

            var content = await DeserializeObject<GuessUpdateModel>(response);

            if (content.CorrectGuess) {
                Assert.True(content.Word.Contains(letterToTest));
            }
        }

        public async Task ShouldNotBeAbleToSubmitStrings() {
            var response = await client.PutAsync($"api/game/{gameId}/fergwer", null);
            Assert.NotNull(response);
        }

        private void CheckContentType(HttpResponseMessage response) {
            Assert.True(response.Content.Headers.ContentType.MediaType == "application/json");
        }

        private async Task<IModel> DeserializeObject<IModel>(HttpResponseMessage response) where IModel : class {
            using var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            using var jsonReader = new JsonTextReader(streamReader);

            var content = serializer.Deserialize<IModel>(jsonReader);

            Assert.NotNull(content);
            return content;
        }
    }
}