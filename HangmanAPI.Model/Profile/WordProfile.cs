using HangmanAPI.Data.Entity;
using HangmanAPI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Model.Profile {
    public class WordProfile : AutoMapper.Profile {
        public WordProfile() {
            CreateMap<Word, WordModel>();
        }
    }
}
