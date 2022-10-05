using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Model.Entity {
    public class GuessModel {
        public Guid GuessId { get; set; }
        public Guid GameId { get; set; }

        public char? Letter { get; set; }

        public bool CorrectGuess { get; set; }
    }
}
