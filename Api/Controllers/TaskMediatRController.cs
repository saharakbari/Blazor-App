

using Api.Hubs;
using Application.Commands.AddTask;
using Azure.Core;
//using Application.Queries.GetAllTasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shared.DTO;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskMediatRController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<TaskHub> _hubContext;
        private readonly ILogger<TaskController> _logger;

        public TaskMediatRController(IMediator mediator, IHubContext<TaskHub> hubContext,
            ILogger<TaskController> logger)
        {
            _mediator = mediator;
            _hubContext = hubContext;
            _logger = logger;
        }

        //[HttpGet("GetTask")]
        //public async Task<IActionResult> GetTask()
        //{
        //    var result = await _mediator.Send(new GetAllTasksQuery());
        //    return Ok(new { Data = result });
        //}

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] TaskItemDTO dto)
        {
            var result = await _mediator.Send(new AddTaskCommand(dto));

            if (result)
            {
                // ✅ ثبت لاگ با ILogger (که از Serilog استفاده می‌کنه)
                _logger.LogInformation("🟢 New task added: {@Task}", dto);

                await _hubContext.Clients.All.SendAsync("TaskAdded", dto);
                return Ok(new { message = "Task added successfully" });
            }

            return BadRequest(new { message = "Failed to add task" });
        }
    }
}

