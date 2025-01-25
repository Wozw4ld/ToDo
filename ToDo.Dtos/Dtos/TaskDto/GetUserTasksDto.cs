using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ToDo.DataAccess.Enums;

namespace ToDo.Dto.Dtos.TaskDto
{
	public class GetUserTasksDto
	{
		public Guid TaskId {  get; set; }
		public string Title {  get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public TaskStatusMain Status { get; set; }
	}
}
