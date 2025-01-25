using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess.Enums;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.Interfaces;
using ToDo.Dto.Dtos.TaskDto;
using ToDto.Services.Interfaces;

namespace ToDo.Controllers
{
	[Route("api/task")]
	public class TaskController : ControllerBase
	{
		private readonly ITaskService _taskService;

		
		public TaskController(ITaskService taskService)
		{
			_taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
		}
		[Authorize(Policy = "UserPolicy")]
		
		[HttpPost("start/{taskId}")]
		public async Task<IActionResult> StartTask(Guid taskId)
		{
			var check = await _taskService.CheckTaskWithUser(taskId);
			if (check)
			{
				await _taskService.StartTask(taskId);
				
				return Ok("Начато");
			}
			return Unauthorized("Не авторизован");
		}
		[Authorize(Policy = "UserPolicy")]
	
		[HttpPost("end/{taskId}")]
		public async Task<IActionResult> EndTask(Guid taskId)
		{
			var check = await _taskService.CheckTaskWithUser(taskId);
			if (check)
			{
				await _taskService.EndTask(taskId);
				return Ok("Отправлено на проверку");
			}
			return Unauthorized("Не авторизован");
		}
		[Authorize(Policy = "AdminPolicy")]
		[HttpGet("all")]
		public async Task<IActionResult> GetAllTasks()
		{
			var tasks = await _taskService.GetAllTasks();
			return Ok(tasks);
		}
		
		[Authorize]
		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetUserTasks(Guid userId)
		{
			var tasks = await _taskService.GetUserTasks(userId);
			return Ok(tasks);
		}
		[Authorize(Policy = "AdminPolicy")]
		[HttpPut("status/{taskId}")]
		public async Task<IActionResult> SetTaskStatus(Guid taskId, TaskStatusMain taskStatus)
		{
			await _taskService.SetTaskStatus(taskId, taskStatus);
			return Ok("Обновлено");
		}
		[Authorize(Policy = "AdminPolicy")]
		[HttpPut("update")]
		public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDto entity)
		{
			if (entity == null) return BadRequest("Invalid task data");
			await _taskService.TaskUpdate(entity);
			return Ok("обновлено");
		}
		[Authorize(Policy = "AdminPolicy")]
		[HttpPost]
		public async Task<IActionResult> AddTask([FromBody] TaskEntity entity)
		{
			if (entity == null)
			{
				return BadRequest("Задача не может быть null");
			}

			await _taskService.AddTask(entity);

			return CreatedAtAction(nameof(GetTaskById), new { id = entity.Id }, entity);
		}
		[Authorize]
		
		[HttpGet("{id}")]
		public async Task<IActionResult> GetTaskById(Guid id)
		{
			var task = await _taskService.GetTaskInfo(id);
			if (task == null)
			{
				return NotFound($"Задача с id {id} нет");
			}
			return Ok(task);
		}
		[Authorize(Policy = "AdminPolicy")]
		[HttpDelete("{taskId}")]
		public async Task<IActionResult> DeleteTask(Guid taskId)
		{
			var taskExists = await _taskService.GetTaskInfo(taskId);
			if (taskExists == null)
			{
				return NotFound($"Задача с id {taskId} нет");
			}

			await _taskService.DeleteTask(taskId);

			return NoContent(); 
		}
	}
}
