using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheSystem.Persistance
{
    public interface IRepository<TEntity, TKey>
    {
        TEntity Get(TKey key);
        void Create(TEntity entity, TKey key);
        void Update(TEntity entity, TKey key);
        bool Delete(TKey key);
    }
}
