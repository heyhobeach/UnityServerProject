using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.ent
using UnityServerProject.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//useinmemoryDatabase가 나중에 배포시에도 사용해도 되는지 메모리에 저장되는거라면 안 될거같은 느낌임
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//자습서에서 현재 안 쓰는듯?
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

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

app.MapGet("/", () => "hello world!");
app.Run();
