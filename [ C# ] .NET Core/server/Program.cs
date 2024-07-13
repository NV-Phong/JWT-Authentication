using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using server;
using server.Models;

var builder = WebApplication.CreateBuilder(args);

//------------------------------------------------------------------------------------------------//

builder.Services.AddDbContext<TaskManagerContext>
(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerConnection")));

//------------------------------------------------------------------------------------------------//

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
#pragma warning disable CS8604
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};
});

//------------------------------------------------------------------------------------------------//

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter into field the word 'Bearer' following by space and JWT",
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement {
	 {
		  new OpenApiSecurityScheme
		  {
				Reference = new OpenApiReference
				{
					 Type = ReferenceType.SecurityScheme,
					 Id = "Bearer"
				}
		  },
		  new string[] { }
	 }
	 });
});

//------------------------------------------------------------------------------------------------//

builder.Services.AddLogging();

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddScoped<EmailService>();

//------------------------------------------------------------------------------------------------//

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowClient", builder =>
	{
		// builder.AllowAnyOrigin()
		builder.WithOrigins("http://localhost:4321")
				  .AllowAnyMethod()
				  .AllowAnyHeader();
	});
});

//------------------------------------------------------------------------------------------------//

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowClient");

app.Run();
