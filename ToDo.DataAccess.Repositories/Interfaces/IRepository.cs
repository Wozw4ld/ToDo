using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DataAccess.Repositories.Interfaces
{
	public interface IRepository<T> where T : class
	{
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task RemoveAsync(Guid id);
	}
}
