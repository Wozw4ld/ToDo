using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Enums;

namespace ToDo.DataAccess.Models
{
	public class TaskEntity
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public Guid AccountId { get; set; } = Guid.Empty;
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int Status { get; set; } = 0;

	}
}
