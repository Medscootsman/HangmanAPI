using HangmanAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Service.Test {
    public static class FakeDb {
        public static List<Word> Words = new List<Word>() {
            new Word {
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                WordString = "test",
                WordId = Guid.NewGuid(),
                Deleted = false,
            },
        };

        public static List<Game> Games = new List<Game>() {
            new Game {
                GameId = Guid.NewGuid(),
                Word = Words[0],
                WordId = Words[0].WordId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Deleted = false,
            },
        };
    }
}
