using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.DataAccess.Repositories.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		private readonly ToDoDbContext _dbContext;
		public RoleRepository(ToDoDbContext dbContext)
		{
				_dbContext = dbContext;
		}
		public async Task AddAsync(RoleEntity entity)
		{
			await _dbContext.Roles.AddAsync(entity);
		}

		public async Task<int> GetById(Guid id)
		{
			return  await _dbContext.Roles.Where(x => x.AccountId == id)
				.Select(x => x.Role)
				.FirstOrDefaultAsync();
			
		}

		public async Task<IQueryable<RoleEntity>> GetByRole(int role)
		{
			return _dbContext.Roles.Where(a => a.Role == role);
		}

		public Task RemoveAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(RoleEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
