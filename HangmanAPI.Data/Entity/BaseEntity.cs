using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Data.Entity {
    public class BaseEntity {
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }
    }
}
