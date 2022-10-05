using AutoMapper;
using HangmanAPI.Common.Constants;
using HangmanAPI.Data.Entity;
using HangmanAPI.Model.Entity;
using HangmanAPI.Repository.Interface;
using HangmanAPI.Service.Helper;
using HangmanAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Service.Service {
    public class GuessService : IGuessService {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        public GuessService(IUnitOfWork unitOfWork, IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<GuessModel> GetGuess(Guid id) {
            var guess = await unitOfWork.Repository<Guess>().Query().Where(x => x.GuessId == id).AsNoTracking().SingleOrDefaultAsync();
            return mapper.Map<GuessModel>(guess);
        }

        public async Task<bool> MakeGuess(Guid gameId, char guess) {
            var guessEntity = new Guess {
                GuessId = Guid.NewGuid(),
                GameId = gameId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Letter = guess
            };
            var existingGuess = await unitOfWork.Repository<Guess>().Query().Where(x => x.GameId == gameId && x.Letter.Equals(guess)).AsNoTracking().FirstOrDefaultAsync();

            if (existingGuess != null) {
                return false;
            }

            //check if the guess is correct
            var game = await unitOfWork.Repository<Game>().Query().Where(x => x.GameId == gameId).Include(x => x.Word).SingleOrDefaultAsync();

            if (game.Word.WordString.Contains(guess)) {
                guessEntity.CorrectGuess = true;
            } else {
                guessEntity.CorrectGuess = false;
            }
            if (!guessEntity.CorrectGuess) {
                game.TotalIncorrectGuesses++;
            }

            var gameModel = mapper.Map<GameModel>(game);

            if(guessEntity.CorrectGuess) {
                gameModel.Guesses.Add(mapper.Map<GuessModel>(guessEntity));
            }

            if (gameModel.TotalIncorrectGuesses == HangmanNumberConstants.HANGMAN_MAX_GUESSES || ServiceHelper.DetermineIfWordIsGuessed(gameModel.Guesses.Select(x => x.Letter).ToList(), gameModel.Word.WordString)) {
                game.Completed = true;
            }

            await unitOfWork.Repository<Guess>().CreateAsync(guessEntity);
            unitOfWork.Repository<Game>().Update(game);
            await unitOfWork.SaveChangesAsync();

            return guessEntity.CorrectGuess;
        }
    }
}
