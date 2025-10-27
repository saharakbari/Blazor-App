using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Application.Interfaces
{
    public interface IRepository<T> where T: class
    {
        Task<T> GetByIdAsync(int id);

       
        Task<List<T>> GetAllAsync();

        Task<T> GetByPropAsync(string prop);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);

        Task Delete(int id);


    }
}
