using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Enums;

namespace ToDo.Dto.Dtos.TaskDto
{
	public class SetTaskStatusDto
	{
		public Guid TaskId { get; set; }
		public TaskStatusMain Status { get; set; }
	}
}
