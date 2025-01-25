using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Models;

namespace ToDo.DataAccess.Repositories.Interfaces
{
	public interface IRoleRepository : IRepository<RoleEntity>
	{
		public Task<IQueryable<RoleEntity>> GetByRole(int role);
		public Task<int> GetById(Guid id);
		
		
	}
}
