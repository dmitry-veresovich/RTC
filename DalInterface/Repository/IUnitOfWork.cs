using System;

namespace Rtc.DalInterface.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
