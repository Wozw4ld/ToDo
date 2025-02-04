﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Enums;
using ToDo.DataAccess.Models;

namespace ToDo.Dto.Dtos
{
	public class AccountCreateDto
	{
		public string UserName { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public UserRole Role { get; set; } = UserRole.User;
	}
}
