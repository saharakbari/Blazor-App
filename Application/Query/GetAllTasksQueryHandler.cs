using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.TaskQueries
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, List<TaskItemDTO>>
    {
        private readonly IRepository<TaskItem> _repository;
        private readonly IMapper _mapper;

        public GetAllTasksQueryHandler(IRepository<TaskItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TaskItemDTO>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(); // متد Repository که لیست Task ها رو میده
            return _mapper.Map<List<TaskItemDTO>>(entities);
        }
    }
}
