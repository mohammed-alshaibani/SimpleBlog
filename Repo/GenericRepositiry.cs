using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;

namespace SimpleBlog.Repo
{
    public class GenericRepositiry<T> : IRepositiry<T> where T : class
    {
        private readonly DbContextBlog context;

        public GenericRepositiry(DbContextBlog context)
        {
            this.context = context;
        }
        public T Add(T entity)
        {
            try
            {
                var res = context.Add(entity);
                var rowcount = context.SaveChanges();
                return res.Entity as T;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public T Delete(int id)
        {
            try
            {
                var entity = GetById(id);
                if (entity == null)
                {
                    return default(T);
                }
                var res = context.Remove(entity);
                context.SaveChanges();
                return res.Entity;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public List<T> GetAll()
        {
            var res = context.Set<T>().ToList();
            return res;
        }

        public T GetById(int id)
        {
            try
            {
                var res = context.Set<T>().Find(id);
                return res;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public T Update(T entity)
        {
            try
            {
                var res = context.Update(entity);
                var rowcount = context.SaveChanges();
                return res.Entity as T;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}

