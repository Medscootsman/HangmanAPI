using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HangmanAPI.Data.Entity;

namespace HangmanAPI.Data.Context {
    public class DataContext : DbContext {

        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        public DbSet<Guess> Guesses { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Word> Words { get; set; }
    }
}
