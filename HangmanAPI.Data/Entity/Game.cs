using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HangmanAPI.Common.Constants;

namespace HangmanAPI.Data.Entity {
    public class Game : BaseEntity {
        [Key]
        public Guid GameId { get; set; }

        [ForeignKey("Word")]
        [Required]
        public Guid WordId { get; set; }

        public Word Word { get; set; }

        public bool Completed { get; set; }

        public IEnumerable<Guess>? Guesses { get; set; }

        public int TotalIncorrectGuesses { get; set; }

    }
}
