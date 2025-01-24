using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DataAccess.Models
{
	public class AccountEntity
	{
		public Guid Id { get; set; }
		public string UserName { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
	}
}
