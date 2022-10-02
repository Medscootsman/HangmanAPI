using HangmanAPI.Model.Entity;
using HangmanAPI.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace HangmanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase {
        private IGameService gameService;
        private IGuessService guessService;

        public GameController(IGameService gameService, IGuessService guessService) {
            this.gameService = gameService;
            this.guessService = guessService;
        }

        [HttpPost]
        public async Task<IActionResult> Game() {
            var model = await gameService.CreateGame();
            if (model is null) {
                return StatusCode(500);
            }
            return Ok(model.GameId);
        }

        [HttpGet]
        public async Task<IActionResult> Game(Guid id) {
            var model = await gameService.GetGame(id);
            if (model is null) {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpPut("guess/{id}/{guess}")]
        public async Task<IActionResult> MakeGuess(Guid id, char guess) {
            var model = await gameService.GetGame(id);
            if (model is null) {
                return NotFound();
            }
            var isCorrectGuess = await guessService.MakeGuess(id, guess);
            GuessUpdateModel updateModel = new GuessUpdateModel {
                CorrectGuess = isCorrectGuess,
                Word = model.GuessedWord,
            };

            return Ok(updateModel);
        }
    }
}
