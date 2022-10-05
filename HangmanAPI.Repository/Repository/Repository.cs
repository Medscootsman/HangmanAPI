using HangmanAPI.Data.Context;
using HangmanAPI.Data.Entity;
using HangmanAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanAPI.Repository.Repository {
    public class Repository<T> : IRepository<T> where T : class {
        private DataContext dataContext;

        public Repository(DataContext dataContext) {
            this.dataContext = dataContext;
        }
        public async Task CreateAsync(T entity) {
            await dataContext.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id) {
            var entity = await dataContext.Set<T>().FindAsync(id);
            if (entity != null) {
                dataContext.Set<T>().Remove(entity);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync() {
            return await dataContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id) {
            return await dataContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> Query() {
            return dataContext.Set<T>().AsQueryable<T>();
        }

        public void Update(T entity) {
            dataContext.Set<T>().Update(entity);
        }
    }
}
