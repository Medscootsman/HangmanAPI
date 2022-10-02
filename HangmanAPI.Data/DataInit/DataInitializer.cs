using HangmanAPI.Data.Context;
using HangmanAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

[assembly: InternalsVisibleTo("HangmanAPI.Test")]

namespace HangmanAPI.Data.DataInit {
    public class DataInitializer {

        private DataContext dataContext;

        public DataInitializer(DataContext dataContext) {
            this.dataContext = dataContext;
        }

        public async Task PopulateWordData() {
            if (!dataContext.Words.Any()) {
                foreach (string line in File.ReadLines(@"Resources/wordlist.txt")) {
                    await dataContext.Words.AddAsync(new Word { WordId = Guid.NewGuid(), DateCreated = DateTime.Now, DateModified = DateTime.Now, Deleted = false, WordString = line });
                }
                await dataContext.SaveChangesAsync();
            }
        }
    }
}
