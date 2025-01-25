using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Repositories.Interfaces;
using ToDo.Dto.Dtos;
using ToDto.Services.Interfaces;

namespace ToDto.Services.Services
{
	public class AccountService : IAccountService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAuthService _authService;
		public AccountService(IUnitOfWork unitOfWork, IAuthService authService)
		{
			_unitOfWork = unitOfWork;
			_authService = authService;
		}

		public async Task<bool> IsUserNameExist(string userName)
		{
			
			var existingAccount = await _unitOfWork.AccountRepository.GetByName(userName);
			return existingAccount != null;
			
		}
		
		public async Task<string> LoginAsync(AccountLoginDto account)
		{

			var loginAcc = await _unitOfWork.AccountRepository.GetUserWithRoleAsync(account.UserName);
			
			if (loginAcc == null)
			{
				return "Неправильный пароль или ник";
			}
			Console.WriteLine(loginAcc.PasswordHash);
			var checkUser = VerifyPassword(account.Password, loginAcc.PasswordHash);
			if (checkUser)
			{
				var token = _authService.GenerateJwtToken(loginAcc.Id, loginAcc.Role);
				return token;
			}
			else
			{
				return "Неправильный пароль или ник";
			}
		}

		public async Task<string> RegisterAsync(AccountCreateDto account)
		{
			if(await IsUserNameExist(account.UserName))
			{
				return "Данный ник занят";
			}
			else
			{
				var accountId = Guid.NewGuid();
				await _unitOfWork.AccountRepository.AddAsync(new ToDo.DataAccess.Models.AccountEntity
				{
					Id = accountId,
					UserName = account.UserName,
					PasswordHash = HasdPassword(account.Password)
				});
				await _unitOfWork.RoleRepository.AddAsync(new ToDo.DataAccess.Models.RoleEntity
				{
					Id = Guid.NewGuid(),
					AccountId = accountId,
					Role = (int)account.Role
				});
				await _unitOfWork.SaveChangeAsync();
				return "Успешно";
			}
		}







		public string HasdPassword(string password)
		{
			var hashedPass = BCrypt.Net.BCrypt.HashPassword(password);
			return hashedPass;
		}

		public bool VerifyPassword(string password, string hashedPassword)
		{
			var verifyPass = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
			return verifyPass;
		}

		public async Task<bool> JustForCheck()
		{
			var data = await _authService.CheckUserRole();
			return data;
		}
	}
}
