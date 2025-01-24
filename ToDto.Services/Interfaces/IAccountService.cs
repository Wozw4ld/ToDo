using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Dto.Dtos;

namespace ToDto.Services.Interfaces
{
	public interface IAccountService
	{
		public Task<bool> IsUserNameExist(string userName);
		public Task<string> RegisterAsync(AccountCreateDto account);
		public Task<string> LoginAsync(AccountCreateDto account);
	}
}
