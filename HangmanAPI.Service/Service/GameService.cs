using HangmanAPI.Data.Entity;
using HangmanAPI.Repository.Interface;
using HangmanAPI.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HangmanAPI.Service.Service {
    public class GameService : IGameService {
        private IUnitOfWork unitOfWork;
        private IWordService wordService;
        public GameService(IUnitOfWork unitOfWork, IWordService wordService) {
            this.unitOfWork = unitOfWork;
            this.wordService = wordService;
        }
        public async Task CompleteGame(Guid id) {
            var game = await unitOfWork.Repository<Game>().Query().Where(x => x.GameId == id).Include(x => x.Guesses.Where(x => x.CorrectGuess == true)).Include(x => x.Word).SingleOrDefaultAsync();
            if (game != null) {
                if(DetermineIfWordIsGuessed(game.Guesses.Select(x => x.Letter).ToList(), game.Word.WordString)) {
                    game.Completed = true;
                }
                unitOfWork.Repository<Game>().Update(game);
                await unitOfWork.SaveChangesAsync();
            } else {
                throw new Exception("Game not found");
            }
        }

        public async Task<Game> CreateGame() {
            var game = new Game() {
                GameId = Guid.NewGuid(),
                WordId = await wordService.GetRandomWordId(),
                Completed = false,
            };

            await unitOfWork.Repository<Game>().CreateAsync(game);

            await unitOfWork.SaveChangesAsync();

            return game;
        }

        public async Task<Game> GetGame(Guid id) {
            var game = await unitOfWork.Repository<Game>().Query().Where(x => x.GameId == id).SingleOrDefaultAsync();
            if (game != null) {
                return game;
            } else {
                throw new Exception("Game not found");
            }
        }

        private bool DetermineIfWordIsGuessed(IEnumerable<char?> guesses, string word) {
            foreach (var character in word) {
                if (guesses.Contains(character)) {
                    continue;
                } else {
                    return false;
                }
            }
            return true;
        }
    }
}
