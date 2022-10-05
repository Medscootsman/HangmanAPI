using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanAPI.Service.Helper;

namespace HangmanAPI.Service.Test {
    public class ServiceHelperTest {
        [Fact]
        public void EnsureWordGuessIsCorrect() {
            var guesses = new List<char?>() {
               't',
               'e',
               's'
            };
            var result = ServiceHelper.DetermineIfWordIsGuessed(guesses, "test");
            Assert.True(result);

            var falseResult = ServiceHelper.DetermineIfWordIsGuessed(guesses, "shouldnotbeguessed");
            Assert.False(falseResult);
        }
            
    }
}
