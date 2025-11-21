    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    namespace TPVAXWebsite.DAL
    {
        /// <summary>
        /// Generic Repository Interface
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public interface IRepository<TEntity> where TEntity : class
        {
            // Query methods
            TEntity GetById(object id);
            IEnumerable<TEntity> GetAll();
            IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
            TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
            
            // Command methods
            void Add(TEntity entity);
            void AddRange(IEnumerable<TEntity> entities);
            void Update(TEntity entity);
            void Remove(TEntity entity);
            void RemoveRange(IEnumerable<TEntity> entities);
            
            // Advanced query
            IQueryable<TEntity> Query();
            IQueryable<TEntity> QueryInclude(params Expression<Func<TEntity, object>>[] includes);
            int Count(Expression<Func<TEntity, bool>> predicate = null);
            bool Any(Expression<Func<TEntity, bool>> predicate);
        }
    }
