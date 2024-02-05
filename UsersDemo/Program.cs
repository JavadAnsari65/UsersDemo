using Microsoft.EntityFrameworkCore;
using UsersDemo.Application;
using UsersDemo.Infrastructure.Configuration;
using UsersDemo.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add DbContext
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(CustomMap));

//Add Services
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<CRUDService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add app StaticFiles
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
