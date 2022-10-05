using HangmanAPI.Data.Context;
using HangmanAPI.Data.Entity;
using HangmanAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HangmanAPI.Repository.Repository {
    public class UnitOfWork : IUnitOfWork {
        private DataContext dataContext;

        private Dictionary<Type, object> repositories;

        private bool disposed;

        public UnitOfWork(DataContext dataContext) {
            this.dataContext = dataContext;
            this.repositories = new Dictionary<Type, object>();

        }

        public async Task SaveChangesAsync() {
            await dataContext.SaveChangesAsync();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class {
            if (dataContext.Set<TEntity>() == null) {
                throw new NotImplementedException($"{typeof(TEntity)} is not a valid dataset");
            }

            if (repositories == null) {
                repositories = new Dictionary<Type, object>();
            }

            Type type = typeof(TEntity);

            if (!repositories.ContainsKey(type)) {
                repositories[type] = new Repository<TEntity>(dataContext);
            }

            return (IRepository<TEntity>)repositories[type];
        }

        public void Dispose() {
            if (disposed) {
                return;
            }
            GC.SuppressFinalize(this);
            dataContext.Dispose();
            disposed = true;
        }
    }
}
