using HangmanAPI.Data.Entity;
using HangmanAPI.Repository.Interface;
using HangmanAPI.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HangmanAPI.Model.Entity;
using AutoMapper;

namespace HangmanAPI.Service.Service {
    public class GameService : IGameService {
        private IUnitOfWork unitOfWork;
        private IWordService wordService;
        private IMapper mapper;
        public GameService(IUnitOfWork unitOfWork, IWordService wordService, IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.wordService = wordService;
            this.mapper = mapper;
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

        public async Task<GameModel> CreateGame() {
            var game = new Game() {
                GameId = Guid.NewGuid(),
                WordId = await wordService.GetRandomWordId(),
                Completed = false,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
            };

            await unitOfWork.Repository<Game>().CreateAsync(game);

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<GameModel>(game);
        }

        public async Task<GameModel> GetGame(Guid id) {
            var game = await unitOfWork.Repository<Game>().Query().Where(x => x.GameId == id).Include(x => x.Guesses).Include(x => x.Word).SingleOrDefaultAsync();
            if (game != null) {
                return mapper.Map<GameModel>(game);
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
