using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using global::Application.Commands;
using MediatR;
using Microsoft.Extensions.Logging;


 

    namespace Application.Commands
    {
        public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
        {
        private readonly IRepository<TaskItem> _repository;
        private readonly IMapper _mapper;
            private readonly ILogger<UpdateTaskCommandHandler> _logger;

            public UpdateTaskCommandHandler(IRepository<TaskItem> repository, IMapper mapper, ILogger<UpdateTaskCommandHandler> logger)
            {
                _repository = repository;
                _mapper = mapper;
                _logger = logger;
            }

        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TaskItem>(request.Task);
            var existing = await _repository.GetByIdAsync(entity.Id);
            if (existing == null)
            {
                throw new Exception("task is not exist");
            }

           
            await _repository.UpdateAsync(entity);
            _logger.LogInformation("✏️ Task updated: {@Task}", request.Task);
            return true;
        }
    }
    }


