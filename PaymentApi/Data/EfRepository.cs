using Microsoft.EntityFrameworkCore;
using PaymentApi.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PaymentApi.Data
{
    public class EfRepository<T> : IRepository<T> where T: BaseEntity
    {
        DbSet<T> _dbSet;
        private PaymentsDbContext _dbContext;

        public EfRepository(PaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }
    }
}
