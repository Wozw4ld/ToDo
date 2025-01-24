using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DataAccess.Models
{
	public class LogEntry
	{
		public Guid Id { get; set; }
		public DateTime Timestamp { get; set; } = DateTime.Now;
		public string Message { get; set; } = string.Empty;
		public string Level { get; set; } = "Info"; 
		public string Source { get; set; } = string.Empty;
		public string ExceptionDetails { get; set; } = string.Empty; 
	}
}
