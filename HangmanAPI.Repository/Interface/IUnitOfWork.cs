using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Repository.Interface {
    public interface IUnitOfWork : IDisposable {
        public Task SaveChangesAsync();
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}
