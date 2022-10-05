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

            //if the game has been completed, return the full game model.
            if (model.Completed) {
                return Ok(model);
            }

            GameExportModel exportModel = new GameExportModel {
                GameId = model.GameId,
                Completed = model.Completed,
                TotalIncorrectGuesses = model.TotalIncorrectGuesses,
                GuessedWord = model.GuessedWord
            };

            return Ok(exportModel);
        }

        [HttpPut("guess/{id}/{guess}")]
        public async Task<IActionResult> MakeGuess(Guid id, char guess) {
            var gameModel = await gameService.GetGame(id);

            if (gameModel is null) {
                return NotFound();
            }

            var isCorrectGuess = await guessService.MakeGuess(id, Char.ToLower(guess));
            gameModel = await gameService.GetGame(id);

            GuessUpdateModel updateModel = new GuessUpdateModel {
                CorrectGuess = isCorrectGuess,
                Word = gameModel.GuessedWord,
            };


            return Ok(updateModel);
        }
    }
}
