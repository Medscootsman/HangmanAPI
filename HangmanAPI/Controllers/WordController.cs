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

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetWord(Guid id) {

            var wordModel = await wordService.GetSingleWord(id);

            if (wordModel == null) {
                return NotFound();
            }

            return Ok(wordModel);
        }
    }
}
