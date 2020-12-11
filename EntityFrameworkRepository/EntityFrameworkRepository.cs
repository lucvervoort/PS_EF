using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkRepository.Models;
using EntityFrameworkRepository.Interfaces;

namespace EntityFrameworkRepository
{
    /// <summary>
    /// Specifically for Entity Framework
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityFrameworkRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context = new BestellingssysteemContext();

        public virtual IEnumerable<T> List()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public virtual IEnumerable<T> List(
           System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>()
                .Where(predicate)
                .AsEnumerable();
        }

        public virtual T GetById(long id)
        {
            return _context.Set<T>().Find(id);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        public long Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}