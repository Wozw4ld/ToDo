using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Models;

namespace ToDo.DataAccess
{
	public class ToDoDbContext(DbContextOptions<ToDoDbContext> options) : DbContext(options)
	{
		public DbSet<AccountEntity> Accounts { get; set; }
		public DbSet<TaskEntity> Tasks { get; set; }
		public DbSet<RoleEntity> Roles { get; set; }
	
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{	
			base.OnModelCreating(modelBuilder);

		}

	}
}
