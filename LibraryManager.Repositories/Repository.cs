using System.Collections.Generic;
using LibraryManager.Interfaces;
using LibraryManager.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private LibraryManagerContext _dbContext = new LibraryManagerContext();
        private DbSet<T> _dbSet;

        public Repository()
        {
            _dbSet = _dbContext.Set<T>();
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            T? entity = GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
