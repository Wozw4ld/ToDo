using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Repositories.Interfaces;
using ToDto.Services.Interfaces;

namespace ToDto.Services.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public AuthService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
		}
		public async Task<bool> CheckUserRole()
		{
			if (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User == null)
			{	
				return false;
			}

			var claims = _httpContextAccessor.HttpContext.User.Claims;

			var userIdClaim = claims.FirstOrDefault(c => c.Type == "userId");
			var roleClaim = claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

			if (userIdClaim == null || roleClaim == null)
			{
				Console.WriteLine("Не все данные в Jwt");
				return false;
			}
			var roleFromTable = await _unitOfWork.RoleRepository.GetById(Guid.Parse(userIdClaim.Value));
			Console.WriteLine(roleFromTable.ToString());
			if (int.TryParse(roleClaim.Value, out var role) && role == roleFromTable)
			{
				return true;
			}

			return false;


		}


		public string GenerateJwtToken(Guid userId, int role)
		{
			string jwtSecr = "asdasdasdasdasdasdasdsssdfdbbnrrr";
			var claims = new[]
			{
			new Claim("userId", userId.ToString()),
			new Claim("role", role.ToString()) 
			 };

			var signingCredentials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecr)),
				SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				claims: claims,
				signingCredentials: signingCredentials,
				expires: DateTime.UtcNow.AddHours(1));
			var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
			return tokenValue;
		}
	}
}
