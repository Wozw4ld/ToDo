using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DataAccess.Models
{
	public class RoleEntity
	{
		public Guid Id { get; set; }
		public Guid AccountId { get; set; } 
		public int Role { get; set; }  = 1 ;
	}
}
