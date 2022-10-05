using HangmanAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Repository.Interface {
    public interface IRepository<T> where T : class {
        public Task CreateAsync(T entity);
        public void Update(T entity);

        public IQueryable<T> Query();
        public Task<T> GetByIdAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task DeleteAsync(Guid id);
    }
}
