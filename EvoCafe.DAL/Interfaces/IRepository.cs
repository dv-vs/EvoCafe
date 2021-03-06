﻿using EvoCafe.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EvoCafe.DAL.Interfaces
{
    public interface IRepository<T> where T: EntityBase
    {
        IQueryable<T> GetAll(params string[] includes);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] includes);
        Task<T> GetSingleAsync(int id, params string[] includes);
        void Create(T item);
        void Update(T item);
        Task Delete(int id);
        void Delete(T item);
        void DeleteRange(IEnumerable<T> items);
        
    }
}
