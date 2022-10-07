using HangmanAPI.Model.Entity;
using HangmanAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HangmanAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase {
        private IGameService gameService;
        private IWordService wordService;
        private IGuessService guessService;

        public WordController(IGameService gameService, IGuessService guessService, IWordService wordService) {
            this.gameService = gameService;
            this.guessService = guessService;
            this.wordService = wordService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(WordModel))]
        public async Task<IActionResult> GetWord(Guid id) {
            if (id == Guid.Empty) {
                return StatusCode(400);
            }
            var wordModel = await wordService.GetSingleWord(id);

            if (wordModel == null) {
                return NotFound();
            }

            return Ok(wordModel);
        }

        [HttpGet("words")]
        [ProducesResponseType(200, Type = typeof(List<WordModel>))]
        public async Task<IActionResult> GetAllWords() {
            return Ok(await wordService.GetAllWords());
        }

        [HttpPost("{word}")]
        [ProducesResponseType(200, Type = typeof(WordModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateWord(string word) {
            var result = await wordService.CreateWord(word);
            if (result == null) {
                return BadRequest();
            } else {
                return StatusCode(201, result);
            }
        }

        [HttpPut]
        [ProducesResponseType(204, Type = typeof(WordModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateWord(WordModel model) {
            var result = await wordService.UpdateWord(model);
            if (result != null) {
                return StatusCode(204, result);
            } else {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteWord(Guid id) {
            var result = await wordService.DeleteWord(id);
            if (result) {
                return StatusCode(204);
            } else {
                return BadRequest();
            }
        }
    }
}
