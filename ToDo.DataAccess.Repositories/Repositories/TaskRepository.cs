using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Enums;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.Interfaces;
using ToDo.Dto.Dtos.TaskDto;

namespace ToDo.DataAccess.Repositories.Repositories
{
	internal class TaskRepository : ITaskRepository
	{
		private readonly ToDoDbContext _dbContext;
		public TaskRepository(ToDoDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task AddAsync(TaskEntity entity) 
		{
			entity.Id = new Guid();
			 await _dbContext.Tasks.AddAsync(entity);
		}
		public async Task<bool> CheckTaskWithUser(Guid id, Guid userId)
		{
			
			var task = await _dbContext.Tasks
				.FirstOrDefaultAsync(t => t.Id == id);
			if (task == null)
			{
				return false;
			}

			
			return task.AccountId == userId;
		}
		public async Task EndTask(Guid taskId)
		{
			var task = await _dbContext.Tasks.FindAsync(taskId);

			if (task == null)
			{
				throw new Exception("Task not found");
			}
		

			task.Status = (int)TaskStatusMain.Completed;
			
			task.StartTime = DateTime.UtcNow;
		}

		public async Task<IEnumerable<GetAllTasksDto>> GetAllTasks()
		{
			var tasks = from task in _dbContext.Tasks
						join account in _dbContext.Accounts
						on task.AccountId equals account.Id
						select new GetAllTasksDto
						{
							TaskId = task.Id,
							AccountId = account.Id,
							AccountName = account.UserName,
							Title = task.Title,
							Status = (TaskStatusMain)task.Status
						};

			return await tasks.ToListAsync();
		}


		public async Task<GetTaskInfoDto> GetTaskInfo(Guid taskId)
		{
			var taskInfo = await ( from task in _dbContext.Tasks
						join  account in _dbContext.Accounts
						on task.AccountId equals account.Id
						select new GetTaskInfoDto
						{
							AccountId = account.Id,
							AccountName = account.UserName,
							StartTime = task.StartTime,
							Description = task.Description,
							Title = task.Title,
							Status = (TaskStatusMain)task.Status,
							EndTime = task.EndTime,
							TaskId = task.Id
						}).FirstOrDefaultAsync();
			return taskInfo;
		}

		public async Task<IEnumerable<GetUserTasksDto>> GetUserTasks(Guid userId)
		{
			var userTasks = await(from task in _dbContext.Tasks
								  where task.AccountId == userId
								  select new GetUserTasksDto
								  {
									  TaskId = task.Id,
									  Title = task.Title,
									  Status = (TaskStatusMain)task.Status
								  }).ToListAsync();

			return userTasks;
		}

		public async Task RemoveAsync(Guid id)
		{
			var task = await _dbContext.Tasks.FindAsync(id);
			if (task != null)
			{
				_dbContext.Tasks.Remove(task);
				
			}
			else
			{
				throw new Exception("Task not found");
			}

		}

		public async Task SetTaskStatus(Guid taskId, Enums.TaskStatusMain taskStatus)
		{
			var task = await _dbContext.Tasks.FindAsync(taskId);

			if (task == null)
			{
				throw new Exception("Task not found");
			}
			if (!Enum.IsDefined(typeof(TaskStatusMain), taskStatus))
			{
				throw new InvalidOperationException("Invalid task status.");
			}

			task.Status = (int)taskStatus;

		
			_dbContext.Entry(task).Property(t => t.Status).IsModified = true;

		
		}

		

		public async Task StartTask(Guid taskId)
		{
			var task = await _dbContext.Tasks.FindAsync(taskId);

			if (task == null)
			{
				throw new Exception("Task not found");
			}

			

			task.Status = (int)TaskStatusMain.InProgress;
			task.StartTime = DateTime.UtcNow;
			

		}

		public async Task TaskUpdate(UpdateTaskDto entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var task = new TaskEntity { Id = entity.Id };
			_dbContext.Tasks.Attach(task);

			if (!string.IsNullOrWhiteSpace(entity.Title) && entity.Title != task.Title)
				task.Title = entity.Title;

			if (!string.IsNullOrWhiteSpace(entity.Description) && entity.Description != task.Description)
				task.Description = entity.Description;
		}

		
		public Task UpdateAsync(TaskEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
