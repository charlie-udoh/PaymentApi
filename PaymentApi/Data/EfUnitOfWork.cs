using PaymentApi.Entities;
using System;

namespace PaymentApi.Data
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private PaymentsDbContext _dbContext;
        private IRepository<Payment> _paymentRepository;
        private IRepository<PaymentLog> _paymentLogRepository;

        public EfUnitOfWork(PaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public EfRepository<TEntityType> GetRepoInstance<TEntityType>() where TEntityType: BaseEntity
        {
            return new EfRepository<TEntityType>(_dbContext);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<Payment> PaymentRepository
        {
            get
            {

                if (_paymentRepository == null)
                    _paymentRepository = new EfRepository<Payment>(_dbContext);
                return _paymentRepository;
            }
        }

        public IRepository<PaymentLog> PaymentLogRepository
        {
            get
            {

                if (_paymentLogRepository == null)
                    _paymentLogRepository = new EfRepository<PaymentLog>(_dbContext);
                return _paymentLogRepository;
            }
        }
    }
}
