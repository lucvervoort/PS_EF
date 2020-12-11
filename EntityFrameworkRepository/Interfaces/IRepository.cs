using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EntityFrameworkRepository.Interfaces
{
    // Generics: deze code is geschikt voor eender welk ander type (dwz class); in onze context is T: Customer, Product, Order, ...
    // Interface: contract dat we opleggen aan classes
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// CRUD: Create, Read, Update, Delete
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        T GetById(long id);
        long Insert(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }
}
