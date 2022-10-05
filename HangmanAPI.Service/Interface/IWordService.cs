using HangmanAPI.Data.Entity;
using HangmanAPI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Service.Interface {
    public interface IWordService {
        public Task<Guid> GetRandomWordId();
        public Task<List<WordModel>> GetAllWords();
        public Task<WordModel> CreateWord(string word);

        public Task<WordModel> GetSingleWord(Guid id);

    }
}
