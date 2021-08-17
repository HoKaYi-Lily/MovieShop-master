using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T: class
    {
        //its already implementing that 8 method
        protected readonly MovieShopDbContext _dbContext;

        //we can not make any changes unless we are making it in constructor
        //changes the instances in the constructor

        //this is an instance constructor, not an instance, _dbcontext is a field
        // but not an instance, and pass value from dbcontext to _dbContext , save some value call MovieShopDbContext 
        public EfRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
            //passing the connection string?
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            //inserting and commiting to database
            return entity;
        }

        public Task<T> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }
        //make virtual in base class means we can override it, not neccessarily have to 
        public virtual async Task<T> GetByIdAsync(int id)
        {
            //we are using the dbcontext set class suppply by the subclass
            var entity = await _dbContext.Set<T>().FindAsync(id);

            return entity;
            //need to override, because this will only give us movie detail information
            //but we need to get information from other table also
        }
        //we have to make it virtual because sometimes the base class code does not satisfy our needs
        //can override this method and implement our implementation
        //moviedetail UI page, movie info, design info, this is only going to the table, you need more, join table
        //we need more table and dataset join to display> than just getting the data

        public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            return await _dbContext.Set<T>().Where(filter).CountAsync();
        }

        public virtual async Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            //list 1, 5, 7,8
            //list.where(x=> x>3).Any(); any better than count sometimes
            return await _dbContext.Set<T>().Where(filter).AnyAsync();
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            var data = await _dbContext.Set<T>().ToListAsync();
            return data;
        }

        public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            //filter = where method
            var data = await _dbContext.Set<T>().Where(filter).ToListAsync();
            return data; 
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
            //
        }
    }
}
