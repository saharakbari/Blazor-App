using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shared.DTO;




namespace Application.Commands.AddTask
{
    public class AddTaskCommand : IRequest<bool>
    {
        public TaskItemDTO Task { get; }

        public AddTaskCommand(TaskItemDTO task)
        {
            Task = task;
        }
    }
}

