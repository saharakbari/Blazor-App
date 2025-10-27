using Application.Mapper;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Shared.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Api.Hubs;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly IMapper _mapper;
        private readonly IHubContext<TaskHub> _hubContext;
        private readonly ILogger<TaskController> _logger;

        public TaskController(TaskService taskService,IMapper mapper , IHubContext<TaskHub> hubContext,
            ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _mapper = mapper;
            _hubContext = hubContext;
            _logger = logger;
        }

        [HttpGet("GetTask")]
        public async Task<IActionResult> GetTask()
        {

            try
            {
                var tasks =await  _taskService.GetTask();
                var taskDtos = _mapper.Map<List<TaskItemDTO>>(tasks);
                return Ok(new { Data = taskDtos });

            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }


        [HttpGet("GetTaskById/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {

            try
            {
                var task = await _taskService.GetTaskById(id);
                var taskDtos = _mapper.Map<TaskItemDTO>(task);
                return Ok(new { Data = taskDtos });

            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }


        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] TaskItemDTO request)
        {

            try
            {
                var taskEntity = _mapper.Map<TaskItem>(request);
                 await _taskService.addTaskItem(taskEntity);


                // ✅ ثبت لاگ با ILogger (که از Serilog استفاده می‌کنه)
                _logger.LogInformation("🟢 New task added: {@Task}", request);

                // حالا به کلاینت‌ها اطلاع بده
                await _hubContext.Clients.All.SendAsync("TaskAdded", request);

                return Ok(new { message = "Task added successfully" });
            }
            catch (Exception ex)
            {
                // return Unauthorized(new { error = ex.Message });
                _logger.LogError(ex, "❌ Error while adding task: {Message}", ex.Message);
                return StatusCode(500, "Error while adding task");
            }
        }


        [HttpPost("UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskItemDTO request)
        {

            try
            {
                var taskEntity = _mapper.Map<TaskItem>(request);

                // فراخوانی سرویس برای آپدیت
                await _taskService.updateTaskItem(taskEntity);
              
                return Ok(new { message = "Task is Updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
