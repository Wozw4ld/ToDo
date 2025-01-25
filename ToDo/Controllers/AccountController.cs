using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Dto.Dtos;
using ToDto.Services.Interfaces;
using ToDto.Services.Services;

namespace ToDo.Controllers
{
	[Route("api/account")]
	public class AccountController : ControllerBase
	{
		private readonly IHttpContextAccessor _httpContext;
		private readonly IAccountService _accountService;
		public AccountController(IAccountService accountService, IHttpContextAccessor httpContext)
		{
			_httpContext = httpContext;
			_accountService = accountService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> AccountRegister(AccountCreateDto account)
		{
			var res = await _accountService.RegisterAsync(account);
			return Ok(res);
		}

	
		[HttpPost("login")]
		public async Task<IActionResult> AccountLogin(AccountLoginDto account)
		{
			var token = await _accountService.LoginAsync(account);
			_httpContext.HttpContext?.Response.Cookies.Append("jwt", token);
			return Ok(token);
		}
		[Authorize(Policy = "UserPolicy")]
		[HttpGet("justForCheck")]
		public async Task<IActionResult> Asd()
		{
			var data = await _accountService.JustForCheck();
			return Ok(data);
		}


	}
}
