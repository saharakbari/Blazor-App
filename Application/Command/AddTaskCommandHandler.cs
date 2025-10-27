using Application.Commands.AddTask;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using global::Application.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


 

    namespace Application.Commands
    {
        public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, bool>
        {
        private readonly IRepository<TaskItem> _repository;
        private readonly IMapper _mapper;
            private readonly ILogger<AddTaskCommandHandler> _logger;

            public AddTaskCommandHandler(IRepository<TaskItem> repository, IMapper mapper, ILogger<AddTaskCommandHandler> logger)
            {
                _repository = repository;
                _mapper = mapper;
                _logger = logger;
            }

        public async Task<bool> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TaskItem>(request.Task);
            await _repository.AddAsync(entity);
            _logger.LogInformation("🟢 Task added: {@Task}", request.Task);
            return true;
        }
    }
    }


