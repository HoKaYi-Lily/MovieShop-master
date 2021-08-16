using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    // T is a class type, generic constraints, 
    //if I put struct, then T can be replace by a value type
    public interface IAsyncRepository<T> where T: class
    {
        //repository classes number depends on the number of entities
        //common CRUD operations

        Task<T> GetByIdAsync(int id);
        //T can be an entity, if I replace T with movie , then it can be movie entity 
        //return id get the id key for the entity
        //we are going to use asyncous method to create this code, and return task properties

        //return a list
        Task<IEnumerable<T>> ListAllAsync();

        //add
        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);

        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter);
        //similar to the linq query where to filter question,
        //this parameter will pass the expression

        Task<int> GetCountAsync(Expression<Func<T, bool>> filter=null);
        //nuallable default value, returning number of something with certain constraint
        //just the count of it 

        //create just to make sure we have the common operator

        Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null);
        //give them condition, and see if it exist, true or false, only check one to be true
        //when we are implementing we are replacing the T with the actual classes 

        //strucutre for our application, simple to use later

    }
}
