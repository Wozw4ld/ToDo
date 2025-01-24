using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Models;
using ToDo.Dto.Dtos;

namespace ToDo.DataAccess.Repositories.Interfaces
{
	public interface IAccountRepository : IRepository<AccountEntity>
	{
		Task<AccountEntity> IsUserNameExist(string userName);
		Task<IQueryable<AccountSummaryDto>> GetAllUsers();
		
	}
}
