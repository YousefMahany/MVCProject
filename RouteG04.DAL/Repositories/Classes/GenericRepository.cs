using RouteG04.DAL.Data.Contexts;
using RouteG04.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public IEnumerable<TEntity> GetAll(bool IsTracking = false)
        {
            if (IsTracking)
            {
                return _dbContext.Set<TEntity>().ToList();
            }
            else
            {
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
            }
        }

        //Get Department By Id
        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);
       

        //Add

        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }

        //Update

        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }

        //Delete
        public int Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
