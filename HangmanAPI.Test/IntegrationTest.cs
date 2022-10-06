using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using HangmanAPI;
using Newtonsoft.Json;
using HangmanAPI.Model.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace HangmanAPI.Test {
    public class IntegrationTest {
        private WebApplicationFactory<Program> application;
        private HttpClient client;
        JsonSerializer serializer;
        public IntegrationTest() {
            application = new WebApplicationFactory<Program>();
            client = application.CreateClient();
            serializer = new JsonSerializer();
        }

        [Fact]
        public async Task CanCreateSingleGame() {
            var response = await client.PostAsync("/api/game", null);
            CheckContentType(response);
            Assert.NotNull(response);

            Assert.True(response.IsSuccessStatusCode, $"response code was actually {response.StatusCode}");

            using var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            using var jsonReader = new JsonTextReader(streamReader);

            var content = serializer.Deserialize<Guid>(jsonReader);

            Assert.True(content != Guid.Empty);
        }

        [Fact]
        public async Task CanGetSingleGameFromId() {
            var gameId = await getGameForTesting();
            var response = await client.GetAsync($"api/game/{gameId}");
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode, $"response code was actually {response.StatusCode}");
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
            var gameId = await getGameForTesting();

            const char letterToTest = 'a';

            var response = await client.PutAsync($"api/game/{gameId}/{letterToTest}", null);

            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode, $"response code was actually {response.StatusCode}");
            CheckContentType(response);

            var content = await DeserializeObject<GuessUpdateModel>(response);

            if (content.CorrectGuess) {
                Assert.True(content.Word.Contains(letterToTest));
            }
        }
        [Fact]
        public async Task ShouldNotBeAbleToSubmitStrings() {
            var gameId = await getGameForTesting();
            var response = await client.PutAsync($"api/game/{gameId}/fergwer", null);
            Assert.NotNull(response);
            Assert.True(!response.IsSuccessStatusCode, $"response code was actually {response.StatusCode}");
        }

        [Fact]
        public async Task CanGetAllWords() {
            var response = await client.GetAsync("api/word/words");
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode, $"response code was actually {response.StatusCode}");
            CheckContentType(response);

            var content = await DeserializeObject<List<WordModel>>(response);

            Assert.NotNull(content);
        }

        [Fact]
        public async Task TestWordCRUD() {
            const string wordToCreate = "testword";
            //Create
            var response = await client.PostAsync($"api/word/{wordToCreate}", null);
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode, $"response code was actually {response.StatusCode}");
            CheckContentType(response);

            var content = await DeserializeObject<WordModel>(response);

            Assert.NotNull(content);
            Assert.Equal(wordToCreate, content.WordString);

            //Read
            var readResponse = await client.GetAsync($"api/word/{content.WordId}");
            Assert.NotNull(readResponse);
            Assert.True(readResponse.IsSuccessStatusCode, $"response code was actually {readResponse.StatusCode}");
            CheckContentType(response);

            var readContent = await DeserializeObject<WordModel>(readResponse);

            Assert.NotNull(readContent);

            Assert.Equal(wordToCreate, readContent.WordString);

            //Update
            content.WordString = "testwordthatisupdated";

            var updateResponse = await client.PutAsync("api/word", GetStringContent(content));
            Assert.NotNull(updateResponse);
            Assert.True(updateResponse.IsSuccessStatusCode, $"response code was actually {updateResponse.StatusCode}");
            CheckContentType(updateResponse);

            var updateContent = await DeserializeObject<WordModel>(updateResponse);

            Assert.NotNull(updateContent);
            Assert.True(updateContent.WordString.Equals(content.WordString));

            //Delete
            var deleteResponse = await client.DeleteAsync($"api/word/{updateContent.WordId}");
            Assert.NotNull(deleteResponse);
            Assert.True(deleteResponse.IsSuccessStatusCode, $"response code was actually {deleteResponse.StatusCode}");
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

        private async Task<Guid> getGameForTesting() {
            var gameIdResponse = await client.PostAsync("/api/game", null);

            using var streamReader = new StreamReader(await gameIdResponse.Content.ReadAsStreamAsync());
            using var jsonReader = new JsonTextReader(streamReader);

            var gameId = serializer.Deserialize<Guid>(jsonReader);
            return gameId;
        }

        private static StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
    }
}