﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Persistance.Identity.Stores
{
    public class EntityStore<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Constructor that takes a Context
        /// </summary>
        /// <param name="context"></param>
        public EntityStore(DbContext context)
        {
            Context = context;
            DbEntitySet = context.Set<TEntity>();
        }

        /// <summary>
        ///     Context for the store
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        ///     Used to query the entities
        /// </summary>
        public IQueryable<TEntity> EntitySet
        {
            get { return DbEntitySet; }
        }

        /// <summary>
        ///     EntitySet for this store
        /// </summary>
        public DbSet<TEntity> DbEntitySet { get; private set; }

        /// <summary>
        ///     FindAsync an entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return DbEntitySet.FindAsync(id);
        }

        /// <summary>
        ///     Insert an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Create(TEntity entity)
        {
            DbEntitySet.Add(entity);
        }

        /// <summary>
        ///     Mark an entity for deletion
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            DbEntitySet.Remove(entity);
        }

        /// <summary>
        ///     Update an entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            if (entity != null)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}