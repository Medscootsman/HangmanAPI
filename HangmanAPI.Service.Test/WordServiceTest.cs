using HangmanAPI.Service.Test.Base;

namespace HangmanAPI.Service.Test {
    public class WordServiceTest : BaseServiceTest {

        public WordServiceTest() : base() {
        }

        [Fact]
        public async Task ShouldReturnRandomWordId() {
            var result = await wordService.GetRandomWordId();
            Assert.NotNull(result);
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task ShouldGetSingleWord() {
            var guid = await wordService.GetRandomWordId();
            var result = await wordService.GetSingleWord(guid);
            Assert.NotNull(result);
            Assert.IsType<WordModel>(result);
            Assert.NotEmpty(result.WordString);
        }

        [Fact]
        public async Task CanCRUDWord() {
            var result = await wordService.CreateWord("testserviceword");
            Assert.NotNull(result);
            Assert.IsType<WordModel>(result);
            Assert.NotEmpty(result.WordString);

            result.WordString = "updatetestserviceword";
            var updatedWord = await wordService.UpdateWord(result);
            Assert.NotNull(updatedWord);
            Assert.IsType<WordModel>(result);
            Assert.Equal(result.WordString, updatedWord.WordString);

            var deleteResult = await wordService.DeleteWord(updatedWord.WordId);
            Assert.True(deleteResult);
            var wordThatShouldBeNull = await wordService.GetSingleWord(updatedWord.WordId);
            Assert.Null(wordThatShouldBeNull);

        }

        [Fact]
        public async Task CanGetAllWords() {
            var result = await wordService.GetAllWords();
            Assert.NotNull(result);
            Assert.IsType<List<WordModel>>(result);
        }
    }
}
