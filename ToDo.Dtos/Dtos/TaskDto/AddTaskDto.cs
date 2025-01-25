using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Dto.Dtos.TaskDto
{
	public class AddTaskDto
	{
		public string Title {  get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public Guid AccountId { get; set; }
	}
}
