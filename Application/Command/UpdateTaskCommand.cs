using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shared.DTO;


 

    namespace Application.Commands
    {
        public class UpdateTaskCommand : IRequest<bool>
        {
            public TaskItemDTO Task { get; }

            public UpdateTaskCommand(TaskItemDTO task)
            {
                Task = task;
            }
        }
    }

