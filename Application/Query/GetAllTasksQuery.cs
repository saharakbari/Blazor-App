using MediatR;
using Shared.DTO;
using System.Collections.Generic;

namespace Application.Queries.TaskQueries
{
    public class GetAllTasksQuery : IRequest<List<TaskItemDTO>>
    {
     
    }
}