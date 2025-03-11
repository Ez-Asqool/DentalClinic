using DentalClinic.Core.Consts;
using DentalClinic.Core.Repositories;
using DentalClinic.EF.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.EF.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		protected ApplicationDbContext _context;

		public BaseRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public T Add(T entity)
		{
			_context.Set<T>().Add(entity);
			_context.SaveChanges();

			return entity;
		}

		public IEnumerable<T> AddRange(IEnumerable<T> entities)
		{
			_context.Set<T>().AddRange(entities);
			_context.SaveChanges();

			return entities;
		}

		public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
		{
			IQueryable<T> query = _context.Set<T>();
			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			return query.SingleOrDefault(criteria);
		}

        public T FindWithThenFind(Expression<Func<T, bool>> criteria, string[] includes = null, string[] thenIncludes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    // If thenIncludes is provided for the same index and is not empty, build a dot-separated path.
                    if (thenIncludes != null && thenIncludes.Length > i && !string.IsNullOrWhiteSpace(thenIncludes[i]))
                    {
                        query = query.Include($"{includes[i]}.{thenIncludes[i]}");
                    }
                    else
                    {
                        query = query.Include(includes[i]);
                    }
                }
            }

            return query.SingleOrDefault(criteria);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
		{
			IQueryable<T> query = _context.Set<T>();
			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			return query.Where(criteria).ToList();
		}

		public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip, string[] includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC")
		{
			IQueryable<T> query = _context.Set<T>().Where(criteria);
			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			if (take.HasValue)
				query = query.Take(take.Value);

			if (skip.HasValue)
				query = query.Skip(skip.Value);

			if (orderBy != null)
			{
				if (orderByDirection == OrderBy.Ascending)
					query = query.OrderBy(orderBy);
				else
					query = query.OrderByDescending(orderBy);
			}

			return query.ToList();
		}

		public IEnumerable<T> GetAll() => _context.Set<T>().ToList();


		public T GetById(int id)
		{
			return _context.Set<T>().Find(id);
		}

		public T Update(T entity)
		{
			_context.Update(entity);
			_context.SaveChanges();
			return entity;
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
			_context.SaveChanges();
		}

		public void DeleteRange(IEnumerable<T> entities)
		{
			_context.Set<T>().RemoveRange(entities);
			_context.SaveChanges();
		}

		public void Attach(T entity)
		{
			_context.Set<T>().Attach(entity);
			_context.SaveChanges();
		}

		public int Count()
		{
			return _context.Set<T>().Count();
		}

		public int Count(Expression<Func<T, bool>> criteria)
		{
			return _context.Set<T>().Count(criteria);
		}
	}
}
