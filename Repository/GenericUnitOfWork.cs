using KeenaKaata.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeenaKaata.Repository
{
    public class GenericUnitOfWork:IDisposable
    {
        private dbKeenaKaataOnlineShoppingEntities DBEntity = new dbKeenaKaataOnlineShoppingEntities();

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBEntity.Dispose();
                }
            }
            this.disposed = true;
        }
        private bool disposed = false;

        public IRepository<Tbl_EntityType> GetRepositoryInstance<Tbl_EntityType>() where Tbl_EntityType : class
        {
            return new GenericRepository<Tbl_EntityType>(DBEntity);
        }
        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}