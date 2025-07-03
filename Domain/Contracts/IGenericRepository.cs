using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity> specifications);
        public Task<TEntity?> GetByIdAsync(ISpecifications<TEntity> specifications);
        public void Add(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
    }
}
