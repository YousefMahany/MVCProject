using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity entity);
        IEnumerable<TEntity> GetAll(bool withtracking = false);
        TEntity? GetById(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
