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
            var word = await unitOfWork.Repository<Word>().Query().Where(x => x.WordId == id && x.Deleted == false).AsNoTracking().SingleOrDefaultAsync();
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

        public async Task<bool> UpdateWord(WordModel model) {
            var wordEntity = await unitOfWork.Repository<Word>().Query().Where(x => x.WordId == model.WordId && x.Deleted == false).SingleOrDefaultAsync();
            if (wordEntity != null) {
                wordEntity.WordString = model.WordString;
                wordEntity.DateCreated = DateTime.Now;
                unitOfWork.Repository<Word>().Update(wordEntity);
                await unitOfWork.SaveChangesAsync();
                return true;
            } else {
                return false;
            }
        }

        public async Task<bool> DeleteWord(Guid id) {
            var wordToLogicDelete = await unitOfWork.Repository<Word>().Query().Where(x => x.WordId == id && x.Deleted == false).SingleOrDefaultAsync();

            if (wordToLogicDelete != null) {
                wordToLogicDelete.Deleted = true;
                wordToLogicDelete.DateModified = DateTime.Now;
                unitOfWork.Repository<Word>().Update(wordToLogicDelete);
                await unitOfWork.SaveChangesAsync();
                return true;
            } else {
                return false;
            }
        }

    }
}
