using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(TaskHiveDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity> specifications)
        {
            var res = await SpecificationEvaluator
                      .CreateQuery(_dbContext.Set<TEntity>(), specifications)
                      .AsNoTracking()
                      .ToListAsync();

            return res;  
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity> specifications)
        {
            var res = await SpecificationEvaluator
                      .CreateQuery(_dbContext.Set<TEntity>(), specifications)
                      .AsNoTracking()
                      .FirstOrDefaultAsync();

            return res;
        }
    }
}
