using HangmanAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Service.Interface {
    public interface IWordService {
        public Task<Word> GetWord(Guid id);
        public Task<Guid> GetRandomWordId();
    }
}
