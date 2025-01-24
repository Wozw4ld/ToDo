using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.Interfaces;
using ToDo.Dto.Dtos;

namespace ToDo.DataAccess.Repositories.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly ToDoDbContext _dbContext;
		public AccountRepository(ToDoDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task AddAsync(AccountEntity entity)
		{
			await _dbContext.Accounts.AddAsync(entity);
		}

		public Task<IQueryable<AccountSummaryDto>> GetAllUsers()
		{
			throw new NotImplementedException();
		}

		public async Task<AccountEntity> IsUserNameExist(string userName)
		{
			return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.UserName == userName);

		}
		public Task RemoveAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(AccountEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
