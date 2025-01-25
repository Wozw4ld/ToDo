using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Enums;

namespace ToDo.Dto.Dtos.TaskDto
{
	public class UpdateTaskDto
	{
		public Guid Id {  get; set; }
		public string Title {  get; set; } 
		public string Description { get; set; }
		public Guid AccountId {  get; set; }
		public System.Threading.Tasks.TaskStatus TaskStatus { get; set; }
	}
}
