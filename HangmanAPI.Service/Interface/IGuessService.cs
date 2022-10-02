using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Service.Interface {
    public interface IGuessService {
        public Task CreateGuess(Guid gameId);
        public Task GetGuess(Guid id);
        public Task DeleteGuess(Guid id);
    }
}
