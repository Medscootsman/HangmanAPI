using AutoMapper;
using HangmanAPI.Data.Entity;
using HangmanAPI.Model.Entity;
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
        private IMapper mapper;
        public WordService(IUnitOfWork unitOfWork, IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Guid> GetRandomWordId() {
            Random rand = new Random();
            var number = rand.Next(1, await unitOfWork.Repository<Word>().Query().CountAsync());

            var randomWord = await unitOfWork.Repository<Word>().Query().OrderBy(x => Guid.NewGuid()).Skip(number).Take(1).FirstOrDefaultAsync();

            return randomWord.WordId;
        }

        public async Task<WordModel> GetSingleWord(Guid id) {
            var word = await unitOfWork.Repository<Word>().Query().Where(x => x.WordId == id).AsNoTracking().SingleOrDefaultAsync();
            return mapper.Map<WordModel>(word);
        }

        public async Task<WordModel> CreateWord(string wordString) {
            var word = new Word() {
                WordId = Guid.NewGuid(),
                WordString = wordString,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Deleted = false
            };
            await unitOfWork.Repository<Word>().CreateAsync(word);
            return mapper.Map<WordModel>(word);
        }

        public async Task<List<WordModel>> GetAllWords() {
            var words = await unitOfWork.Repository<Word>().GetAllAsync();
            return words.Select(x => mapper.Map<WordModel>(x)).ToList<WordModel>();
        }

    }
}
