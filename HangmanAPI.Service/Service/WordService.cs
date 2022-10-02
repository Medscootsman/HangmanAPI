using HangmanAPI.Data.Entity;
using HangmanAPI.Repository.Interface;
using HangmanAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Service.Service {
    public class WordService : IWordService {
        private IUnitOfWork unitOfWork;
        public WordService(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Guid> GetRandomWordId() {
            Random rand = new Random();
            var number = rand.Next(1, await unitOfWork.Repository<Word>().Query().CountAsync());

            var randomWord = await unitOfWork.Repository<Word>().Query().OrderBy(x => Guid.NewGuid()).Skip(number).Take(1).FirstOrDefaultAsync();

            return randomWord.WordId;
        }

        public Task<Word> GetWord(Guid id) {
            return unitOfWork.Repository<Word>().GetByIdAsync(id);
        }
    }
}
