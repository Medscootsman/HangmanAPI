using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Model.Entity {
    public class WordModel {
        public Guid WordId { get; set; }
        public string WordString { get; set; }
    }
}
