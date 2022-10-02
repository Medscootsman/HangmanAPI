using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Data.Entity {
    public class Guess : BaseEntity {

        [Key]
        public Guid GuessId { get; set; }

        [ForeignKey("Game")]
        [Required]
        public Guid GameId { get; set; }

        public Game Game { get; set; }

        public char? Letter { get; set; }

        public bool CorrectGuess { get; set; }
    }
}
