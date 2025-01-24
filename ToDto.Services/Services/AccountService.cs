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
		public AccountService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> IsUserNameExist(string userName)
		{
			var existingAccount = await _unitOfWork.AccountRepository.IsUserNameExist(userName);
			return existingAccount != null;
		}
		public async Task
		public async Task<string> LoginAsync(AccountCreateDto account)
		{
			throw new NotImplementedException();
		}

		public async Task<string> RegisterAsync(AccountCreateDto account)
		{
			if(await IsUserNameExist(account.UserName))
			{
				return "Данный никнейм занят";
			}
			else
			{
				try
				{
				await _unitOfWork.AccountRepository.AddAsync(new ToDo.DataAccess.Models.AccountEntity
				{
					Id = Guid.NewGuid(),
					UserName = account.UserName,
					PasswordHash = HasdPassword(account.Password)
				});
				}
				catch(Exception ex)
				{
					Console.
				}


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
	}
}
