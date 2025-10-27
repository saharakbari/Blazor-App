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
    public class TaskItemRepository : IRepository<TaskItem>
    {
        private readonly BlazorDBContext _Context;
        public TaskItemRepository(BlazorDBContext context)
        {
            _Context = context;
        }
        public async Task AddAsync(TaskItem task)
        {
            _Context.TaskItems.Add(task);
            await _Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var task = await _Context.TaskItems.FindAsync(id);
            if (task != null)
            {
                _Context.TaskItems.Remove(task);
                await _Context.SaveChangesAsync();
            }

        }

        public async Task<List<TaskItem>> GetAllAsync() => await _Context.TaskItems.AsNoTracking().ToListAsync();

       


        public async Task<TaskItem> GetByPropAsync(string title)
        {
            return await _Context.TaskItems.FirstOrDefaultAsync(u => u.Title == title);
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _Context.TaskItems.FindAsync(id);
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _Context.TaskItems.Update(task);
            await _Context.SaveChangesAsync();
        }
    }
}
