using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DataAccess.Repositories.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IAccountRepository AccountRepository { get; }
		IRoleRepository RoleRepository { get; }
		ITaskRepository TaskRepository { get; }
		//  IRoleRepository RoleRepository { get; }

		Task<int> SaveChangeAsync();
	}
}
