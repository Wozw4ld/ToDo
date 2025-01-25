using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Repositories.Interfaces;
using ToDo.DataAccess.Repositories.Repositories;

namespace ToDo.DataAccess.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ToDoDbContext _dbContext;
		public IAccountRepository AccountRepository { get; }
		public IRoleRepository RoleRepository { get; }
		public ITaskRepository TaskRepository { get; }
		public UnitOfWork(ToDoDbContext dbContext)
		{
			_dbContext = dbContext;
			RoleRepository = new RoleRepository(dbContext);
			AccountRepository = new AccountRepository(dbContext);
			TaskRepository = new TaskRepository(dbContext);
		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}

		public async Task<int> SaveChangeAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}
	}
}
