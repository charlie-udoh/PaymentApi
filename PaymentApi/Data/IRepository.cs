using PaymentApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();

        T GetById(object id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
