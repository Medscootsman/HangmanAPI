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
using HangmanAPI.Service.Helper;

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
            var game = await unitOfWork.Repository<Game>().Query().Where(x => x.GameId == id && x.Deleted == false).Include(x => x.Guesses).Include(x => x.Word).SingleOrDefaultAsync();
            if (game != null) {
                return mapper.Map<GameModel>(game);
            } else {
                throw new Exception("Game not found");
            }
        }
    }
}
