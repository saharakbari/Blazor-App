using Application.Interfaces;
using Domain.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TaskService
    {
        private readonly IRepository<TaskItem> _taskItemRepository;
        public TaskService(IRepository<TaskItem> taskItemRepository)
        {

            _taskItemRepository = taskItemRepository;
        }


        public async Task<List<TaskItem>> GetTask()
        {
            var list = await _taskItemRepository.GetAllAsync();
            return list;

        }

        public async Task<TaskItem> GetTaskById( int id)
        {
            var task = await _taskItemRepository.GetByIdAsync(id);
            return task;

        }
        public async Task updateTaskItem(TaskItem entity)
        {
            var existing = await _taskItemRepository.GetByIdAsync(entity.Id);
            if (existing == null)
            {
                throw new Exception("task is not exist");
            }
            
            existing.Title = entity.Title;
            existing.Description = entity.Description;
            existing.Status = entity.Status;
            existing.DueDate = entity.DueDate; 
            existing.UpdatedAt=DateTime.Now;

            await _taskItemRepository.UpdateAsync(existing);

        }

        public async Task addTaskItem(TaskItem entity)
        {
          
            var taskItem = new TaskItem();
            taskItem.Title = entity.Title;
            taskItem.Description = entity.Description;
            taskItem.Status = entity.Status;
            taskItem.DueDate = entity.DueDate;
            taskItem.UpdatedAt = DateTime.Now;
            taskItem.CreatedAt = DateTime.Now;

            await _taskItemRepository.AddAsync(taskItem);


        }
    }
}
