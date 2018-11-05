using CacheSystem.Persistance;
using System.Collections.Generic;

namespace CacheSystem.PersistanceInMemory
{
    public class GenericRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    {
        Dictionary<TKey, TEntity> _entities;

        public GenericRepository()
        {
            _entities = new Dictionary<TKey, TEntity>();
        }

        public void Create(TEntity entity, TKey key)
        {
            _entities.Add(key, entity);
        }

        public bool Delete(TKey key) 
            => _entities.Remove(key);

        public TEntity Get(TKey key) 
            => _entities[key];

        public void Update(TEntity entity, TKey key)
        {
            var ent = Get(key);
            ent = entity;
        }
    }
}
