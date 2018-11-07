using System.Collections.Generic;

namespace CacheSystem.Persistance
{
    public interface IRepository<TEntity, TKey>
    {
        TEntity Get(TKey key);
        void Create(TEntity entity, TKey key);
        void Update(TEntity entity, TKey key);
        bool Delete(TKey key);
        List<KeyValuePair<TKey, TEntity>> GetAll();
    }
}
