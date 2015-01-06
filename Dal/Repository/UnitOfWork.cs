using System;
using System.Data.Entity;
using Rtc.DalInterface.Repository;

namespace Rtc.Dal.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Ctor

        private readonly DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        #endregion


        public void Commit()
        {
            if (context != null)
            {
                context.SaveChanges();
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            //Debug.WriteLine("Context dispose!");
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (context != null)
                context.Dispose();
        }
    }
}