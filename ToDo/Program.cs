using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess;
using ToDo.DataAccess.Repositories.Interfaces;
using ToDo.DataAccess.Repositories;
using ToDto.Services.Interfaces;
using ToDto.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
	{
		options.TokenValidationParameters = new()
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdasdasdasdasdasdasdsssdfdbbnrrr"))
		};
		options.Events = new JwtBearerEvents
		{
			OnMessageReceived = context =>
			{
				context.Token = context.Request.Cookies["jwt"];
				return Task.CompletedTask;
			}
		};
	});
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminPolicy", policy =>
		policy.RequireRole("1")); 

	options.AddPolicy("UserPolicy", policy =>
		policy.RequireRole("2"));   
});
builder.Services.AddDbContext<ToDoDbContext>(
	options =>
	{
		options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"));
	}
	);
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo");
		c.RoutePrefix = "swagger";
	});
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
