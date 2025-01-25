using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Enums;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.Interfaces;
using ToDo.Dto.Dtos.TaskDto;
using ToDto.Services.Interfaces;

namespace ToDto.Services.Services
{
	public class TaskService : ITaskService
	{
		
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly Guid _userId;
		public TaskService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
			_userId = SetUserId();
			_userId = SetUserId();
		}
		public Guid SetUserId()
		{
			var claims = _httpContextAccessor.HttpContext.User.Claims;
			var userIdClaim = claims.FirstOrDefault(c => c.Type == "userId");
			if (userIdClaim == null)
			{
				Console.WriteLine("Не все данные в Jwt");
				return Guid.NewGuid();
			}
			return Guid.Parse(userIdClaim.Value);
		}
		public async Task<bool> CheckTaskWithUser(Guid id)
		{
			return	await _unitOfWork.TaskRepository.CheckTaskWithUser(id, _userId);
		}

		public async Task EndTask(Guid taskId)
		{
			var check = await CheckTaskWithUser(taskId);
			if (check) await _unitOfWork.TaskRepository.EndTask(taskId);
			await _unitOfWork.SaveChangeAsync();
		}

		public async Task<IEnumerable<GetAllTasksDto>> GetAllTasks()
		{
			return await _unitOfWork.TaskRepository.GetAllTasks();
		}

		public Task<TaskEntity> GetTaskById(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<GetTaskInfoDto> GetTaskInfo(Guid taskId)
		{
			return await _unitOfWork.TaskRepository.GetTaskInfo(taskId);
		}

		public async Task<IEnumerable<GetUserTasksDto>> GetUserTasks(Guid userId)
		{
			return await _unitOfWork.TaskRepository.GetUserTasks(userId);
		}

		public async Task SetTaskStatus(Guid taskId, TaskStatusMain taskStatus)
		{
			await _unitOfWork.TaskRepository.SetTaskStatus(taskId, taskStatus);
			await _unitOfWork.SaveChangeAsync();
		}

		public async Task StartTask(Guid taskId)
		{

			var check = await CheckTaskWithUser(taskId);
			if (check) await _unitOfWork.TaskRepository.StartTask(taskId);
			await _unitOfWork.SaveChangeAsync();
		}

		public async Task TaskUpdate(UpdateTaskDto entity)
		{
			await _unitOfWork.TaskRepository.TaskUpdate(entity);
			await _unitOfWork.SaveChangeAsync();
		}
		public async Task AddTask(TaskEntity entity)
		{
			await _unitOfWork.TaskRepository.AddAsync(entity);
			await _unitOfWork.SaveChangeAsync();

		}
		public async Task DeleteTask(Guid taskId)
		{
			await _unitOfWork.TaskRepository.RemoveAsync(taskId);
			await _unitOfWork.SaveChangeAsync();
		}
	}
}
