using AutoMapper;
using HangmanAPI.Data.Context;
using HangmanAPI.Model.Profile;
using HangmanAPI.Repository.Interface;
using HangmanAPI.Service.Test.Base;

namespace HangmanAPI.Service.Test {
    public class GuessServiceTest : BaseServiceTest {


        public GuessServiceTest() : base() {
        }

        [Fact]
        public async Task ShouldBeAbleToMakeGuessBothSuccessfulAndUnsuccessful() {
            var result = await gameService.CreateGame();
            Assert.NotNull(result);

            char letterToGuess = result.Word.WordString[0];
            var guessResult = await guessService.MakeGuess(result.GameId, letterToGuess);

            Assert.NotNull(guessResult);
            Assert.True(guessResult);

            var gameResult = await gameService.GetGame(result.GameId);
            Assert.Contains(letterToGuess.ToString(), gameResult.GuessedWord);

            var falseResult = await guessService.MakeGuess(result.GameId, '!');
            Assert.NotNull(falseResult);
            Assert.False(falseResult);
        }

        [Fact]
        public async Task RepeatGuessesShouldReturnFalse() {
            var result = await gameService.CreateGame();
            Assert.NotNull(result);

            char firstLetter = result.Word.WordString[0];
            var guessResult = await guessService.MakeGuess(result.GameId, firstLetter);
            Assert.True(guessResult);

            guessResult = await guessService.MakeGuess(result.GameId, firstLetter);
            Assert.False(guessResult);
        }

        [Fact]
        public async Task ShouldBeAbleToCompleteGuess() {
            var result = await gameService.CreateGame();
            Assert.NotNull(result);

            List<char> lettersToGuess = result.Word.WordString.ToList();
            HashSet<char> triedLetters = new HashSet<char>();

            foreach (var letter in lettersToGuess) {

                if (!triedLetters.Contains(letter)) {
                    var guessResult = await guessService.MakeGuess(result.GameId, letter);
                    Assert.True(guessResult);
                }

                triedLetters.Add(letter);
            }
        }
    }
}
