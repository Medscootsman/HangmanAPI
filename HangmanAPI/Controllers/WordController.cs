﻿using HangmanAPI.Model.Entity;
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

        [HttpGet("/")]
        [ProducesResponseType(200, Type = typeof(List<WordModel>))]
        public async Task<IActionResult> GetAllWords() {
            return Ok(await wordService.GetAllWords());
        }

        [HttpPut("/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateWord(WordModel model) {
            var result = await wordService.UpdateWord(model);
            if (result) {
                return Ok();
            } else {
                return BadRequest();
            }
        }

        [HttpDelete("/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteWord(Guid id) {
            var result = await wordService.DeleteWord(id);
            if (result) {
                return Ok();
            } else {
                return BadRequest();
            }
        }
    }
}
