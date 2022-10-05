namespace HangmanAPI.Model.Test {
    using HangmanAPI.Model.Entity;
    public class GameModelTest {
        [Fact]
        public void TestBaseGameModel() {
            var gameModel = new GameModel() {
                GameId = Guid.NewGuid(),
                Word = new WordModel() {
                    WordId = Guid.NewGuid(),
                    WordString = "test",
                },
                Completed = false,
                TotalIncorrectGuesses = 0,
            };
            gameModel.WordId = gameModel.Word.WordId;

            gameModel.Guesses = new List<GuessModel>() {
                new GuessModel() {
                    GameId = gameModel.GameId,
                    GuessId = Guid.NewGuid(),
                    CorrectGuess = true,
                    Letter = 't',
                },
                new GuessModel() {
                    GameId = gameModel.GameId,
                    GuessId = Guid.NewGuid(),
                    CorrectGuess = true,
                    Letter = 'e',
                },
                new GuessModel() {
                    GameId = gameModel.GameId,
                    GuessId = Guid.NewGuid(),
                    CorrectGuess = true,
                    Letter = 's',
                },
            };

            Assert.Equal(gameModel.Word.WordString, gameModel.GuessedWord);
        }
    }
}