using Api.Application.ExtentionMethods;
using Api.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Sql Connections
builder.Services.AddDbContext<ApplicationDbContext>(option => 
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region MediatorEntry Point
builder.Services.AddApplication();
#endregion

#region UnitOfWork

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();