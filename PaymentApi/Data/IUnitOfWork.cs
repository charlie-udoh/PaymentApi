using PaymentApi.Entities;
using System;

namespace PaymentApi.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Payment> PaymentRepository { get; }
        IRepository<PaymentLog> PaymentLogRepository { get; }
        void Commit();
    }
}
