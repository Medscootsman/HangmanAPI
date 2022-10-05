using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Service.Helper {
    public static class ServiceHelper {
        public static bool DetermineIfWordIsGuessed(List<char?> guesses, string word) {
            foreach (var character in word) {
                if (guesses.Contains(character)) {
                    continue;
                } else {
                    return false;
                }
            }
            return true;
        }
    }
}
