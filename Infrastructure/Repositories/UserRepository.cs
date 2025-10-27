using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly BlazorDBContext _Context;
        public UserRepository(BlazorDBContext context)
        {
            _Context = context;
        }
        public async Task AddAsync(User user)
        {
            _Context.Users.Add(user);
            await _Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _Context.Users.FindAsync(id);
            if (user != null)
            {
                _Context.Users.Remove(user);
                await _Context.SaveChangesAsync();
            }

        }

        public async Task<List<User>> GetAllAsync() => await _Context.Users.AsNoTracking().ToListAsync();


        public async Task<User> GetByPropAsync(string email)
        {
            return await _Context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _Context.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            _Context.Users.Update(user);
            await _Context.SaveChangesAsync();
        }
    }
}
