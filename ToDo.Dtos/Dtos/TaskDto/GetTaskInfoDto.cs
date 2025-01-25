using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ToDo.DataAccess.Enums;

namespace ToDo.Dto.Dtos.TaskDto
{
	public class GetTaskInfoDto
	{
		public Guid TaskId { get; set; }
		public Guid AccountId { get; set; }
		public string AccountName { get; set; }
		public string Title {  get; set; }
		public string Description { get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public TaskStatusMain Status {  get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}
