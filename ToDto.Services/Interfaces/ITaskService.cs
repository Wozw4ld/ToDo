using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Enums;
using ToDo.DataAccess.Models;
using ToDo.Dto.Dtos.TaskDto;

namespace ToDto.Services.Interfaces
{
	public interface ITaskService
	{
		Task<bool> CheckTaskWithUser(Guid id);
		Task<TaskEntity> GetTaskById(Guid id);
		public Task TaskUpdate(UpdateTaskDto entity);
		public Task StartTask(Guid taskId);
		public Task EndTask(Guid taskId);
		public Task<IEnumerable<GetUserTasksDto>> GetUserTasks(Guid userId);
		public Task<IEnumerable<GetAllTasksDto>> GetAllTasks();
		public Task SetTaskStatus(Guid taskId, TaskStatusMain taskStatus);
		public Task<GetTaskInfoDto> GetTaskInfo(Guid taskId);
		public Task AddTask(TaskEntity entity);
		public Task DeleteTask(Guid taskId);
	}
}
