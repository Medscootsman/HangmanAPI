using HangmanAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Service.Interface
{
    public interface IGameService
    {
        public Task<Game> CreateGame();

        public Task<Game> GetGame(Guid id);

        public Task CompleteGame(Guid id);
    }
}
