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
        public async Task CanCreateWord() {
            var result = await wordService.CreateWord("test");
            Assert.NotNull(result);
            Assert.IsType<WordModel>(result);
            Assert.NotEmpty(result.WordString);
        }

        [Fact]
        public async Task CanGetAllWords() {
            var result = await wordService.GetAllWords();
            Assert.NotNull(result);
            Assert.IsType<List<WordModel>>(result);
        }
    }
}
