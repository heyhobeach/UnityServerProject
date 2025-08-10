using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using UnityServerProject.Data;
using UnityServerProject.Model;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//useinmemoryDatabase�� ���߿� �����ÿ��� ����ص� �Ǵ��� �޸𸮿� ����Ǵ°Ŷ�� �� �ɰŰ��� ������
//builder.Services.AddDbContext<PlaytestDb>(opt => opt.UseInMemoryDatabase("TodoList"));
//builder.Services.AddMvc();//?
var serverVersion = new MySqlServerVersion(new Version(8, 0, 21)); // MySQL version 8.0.21 or higher is recommended
builder.Services.AddDbContext<PlaytestDb>(opt =>
{
    var connectionstring = builder.Configuration.GetConnectionString("UserDb");
    if (connectionstring == null)
    {
        throw new InvalidOperationException("Connection string 'UserDb' not found. Please add it to your appsettings.json or environment variables.");
    }
    opt.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring));
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//�ڽ������� ���� �� ���µ�?
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var userPlayData = app.MapGroup("/userPlayData");
app.MapGet("/userPlayData", async (PlaytestDb db) => await db.UserPlayData.ToListAsync());
//app.MapGet("/userPlayData/complete", async (PlaytestDb db) => await db.Todos.Where(t => t.IsComplete).ToListAsync());
app.MapGet("/userPlayData/{id}", async (int id, PlaytestDb db) => await db.UserPlayData.FindAsync(id) is Playresult todo ? Results.Ok(todo) : Results.NotFound());
app.MapPost("/userPlayData", async (Playresult data, PlaytestDb db) =>
{
    db.UserPlayData.Add(data);
    await db.SaveChangesAsync();
    return Results.Created($"/userPlayData/{data.Id}", data);
});

app.MapPut("/userPlayData/{id}", async (int id, Playresult inputData, PlaytestDb db) =>
{
    var data = await db.UserPlayData.FindAsync(id);
    if (data is null)
    {
        return Results.NotFound();
    }
    data.IsNormal = inputData.IsNormal;
    data.StartTime = inputData.StartTime;
    data.EndTime = inputData.EndTime;
    data.DeadCount = inputData.DeadCount;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/userPlayData/{id}", async (int id, PlaytestDb db) =>
{
    if (id == 100)
    {
        id = await db.UserPlayData.OrderByDescending(t => t.Id).Select(t => t.Id).FirstOrDefaultAsync();
    }
    if (await db.UserPlayData.FindAsync(id) is Playresult todo)
    {
        db.UserPlayData.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//
//app.UseAuthorization();
//
//app.MapControllers();
//
//app.MapGet("/", () => "hello world!");
app.Run();
