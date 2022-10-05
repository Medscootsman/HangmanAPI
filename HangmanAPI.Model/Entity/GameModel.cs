using HangmanAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Model.Entity {
    public class GameModel {
        public Guid GameId { get; set; }

        public Guid WordId { get; set; }

        public WordModel Word { get; set; }

        public bool Completed { get; set; }

        public List<GuessModel>? Guesses { get; set; }

        public int TotalIncorrectGuesses { get; set; }

        public List<char?> CorrectCharacters => Guesses?.Any() is true ? Guesses.Where(x => x.CorrectGuess == true).Select(x => x.Letter).ToList() : new List<char?>();

        public string GuessedWord {
            get {
                string word = string.Empty;
                foreach (var character in Word.WordString) {
                    if (CorrectCharacters.Contains(character)) {
                        word += character;
                    } else {
                        word += "_";
                    }
                }
                return word;
            }
        }
    }
}
