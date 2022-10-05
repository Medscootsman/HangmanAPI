using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Model.Entity {
    public class GameExportModel {
        public Guid GameId { get; set; }

        public bool Completed { get; set; }

        public int TotalIncorrectGuesses { get; set; }

        public string GuessedWord { get; set; }
    }
}
