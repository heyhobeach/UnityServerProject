using Microsoft.EntityFrameworkCore;
using UnityServerProject.Data;
using UnityServerProject.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//useinmemoryDatabase가 나중에 배포시에도 사용해도 되는지 메모리에 저장되는거라면 안 될거같은 느낌임
//builder.Services.AddDbContext<PlaytestDb>(opt => opt.UseInMemoryDatabase("TodoList"));
//builder.Services.AddMvc();//?
var connectionstring = builder.Configuration.GetConnectionString("UserDb");
//builder.Services.AddDbContext<PlaytestDb>(opt => opt.UseMySQL(connectionstring,serververs));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//자습서에서 현재 안 쓰는듯?
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

var userPlayData = app.MapGroup("/userPlayData");
app.MapGet("/userPlayData", async (PlaytestDb db) => await db.Todos.ToListAsync());
//app.MapGet("/userPlayData/complete", async (PlaytestDb db) => await db.Todos.Where(t => t.IsComplete).ToListAsync());
app.MapGet("/userPlayData/{id}", async (int id, PlaytestDb db) => await db.Todos.FindAsync(id) is Playresult todo ? Results.Ok(todo) : Results.NotFound());
app.MapPost("/userPlayData", async (Playresult todo, PlaytestDb db) => { 
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/userPlayData/{todo.Id}", todo);
});

app.MapPut("/userPlayData/{id}", async (int id, Playresult inputData, PlaytestDb db) => {
    var data = await db.Todos.FindAsync(id);
    if(data is null)
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
        id = await db.Todos.OrderByDescending(t => t.Id).Select(t=>t.Id).FirstOrDefaultAsync();
    }
    if (await db.Todos.FindAsync(id) is Playresult todo)
    {
        db.Todos.Remove(todo);
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
