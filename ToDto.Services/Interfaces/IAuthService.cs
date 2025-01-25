using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDto.Services.Interfaces
{
	public interface IAuthService
	{
		public string GenerateJwtToken(Guid userId, int role);
		public Task<bool> CheckUserRole();
	}
}
