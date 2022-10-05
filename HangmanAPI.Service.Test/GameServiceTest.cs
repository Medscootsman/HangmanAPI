
using HangmanAPI.Service.Test.Base;

namespace HangmanAPI.Service.Test {

    public class GameServiceTest : BaseServiceTest, IDisposable {

        public GameServiceTest() : base() {
        }

        public void Dispose() {
            dataContext.Games.Remove(FakeDb.Games[0]);
        }

        [Fact]
        public async Task ShouldBeAbleToCreateGame() {
            var result = await gameService.CreateGame();
            Assert.NotNull(result);
            Assert.IsType<GameModel>(result);
            Assert.NotNull(result.Word);
            Assert.Empty(result.Guesses);
            Assert.Contains("_", result.GuessedWord);
        }

        [Fact]
        public async Task ShouldReturnGame() {
            var createResult = await gameService.CreateGame();
            var result = await gameService.GetGame(createResult.GameId);
            Assert.NotNull(result);
            Assert.IsType<GameModel>(result);
            Assert.Equal(createResult.GameId, result.GameId);
            Assert.NotNull(result.Word);
        }
    }
}