﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Model.Entity {
    public class GuessUpdateModel {
        public bool CorrectGuess { get; set; }
        public string Word { get; set; }
    }
}
