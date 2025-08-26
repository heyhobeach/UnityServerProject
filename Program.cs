using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using UnityServerProject.Data;
using UnityServerProject.Model;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//useinmemoryDatabase�� ���߿� �����ÿ��� ����ص� �Ǵ��� �޸𸮿� ����Ǵ°Ŷ�� �� �ɰŰ��� ������
//builder.Services.AddDbContext<PlaytestDb>(opt => opt.UseInMemoryDatabase("TodoList"));
//builder.Services.AddMvc();//?
var serverVersion = new MySqlServerVersion(new Version(8, 0, 21)); // MySQL version 8.0.21 or higher is recommended
builder.Services.AddDbContext<PlaytestDb>(opt =>
{
    var connectionstring = builder.Configuration.GetConnectionString("UserDb");//mysql 설정에 대해서 appsettings.json에 작성되어 있음  현재 나는 JackTheReaperDB라는 데이터베이스를 사용하지만 appsettings에서는 UserDb로 정리 되어있음
    if (connectionstring == null)
    {
        //만약 데이터 베이스로 들어가는 connectonstring이 비어있다면 예외처리 발생 시킴
        throw new InvalidOperationException("Connection string 'UserDb' not found. Please add it to your appsettings.json or environment variables.");
    }
    //Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerVersion.AutoDetect(connectionstring) 를 사용하면 MySQL 서버 버전을 감지하고 입력함
    //UseMySql 함수를 사용 했으며 해당 함수의 인자로는 데이터베이스 접속을 위한 설정과, 서버 버전을 입력하게됨
    opt.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring));
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//�ڽ������� ���� �� ���µ�?
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//
//minimal api사용중이며 각 요청별로 내용을 정리한것임
//minimal api에서 의존성 주입을 자체적으로 진행해서 이렇게 사용 가능함
var userplaydata = app.MapGroup("/userplaydata");
app.MapGet("/userplaydata", async (PlaytestDb db) => await db.userplaydata.ToListAsync());
//app.MapGet("/userplaydata/complete", async (PlaytestDb db) => await db.Todos.Where(t => t.IsComplete).ToListAsync());
app.MapGet("/userplaydata/{id}", async (int id, PlaytestDb db) => await db.userplaydata.FindAsync(id) is Playresult todo ? Results.Ok(todo) : Results.NotFound());
app.MapPost("/userplaydata", async (Playresult data, PlaytestDb db) =>
{
    db.userplaydata.Add(data);
    await db.SaveChangesAsync();
    return Results.Created($"/userplaydata/{data.Id}", data);
});

app.MapPut("/userplaydata/{id}", async (int id, Playresult inputData, PlaytestDb db) =>
{
    var data = await db.userplaydata.FindAsync(id);
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
app.MapDelete("/userplaydata/{id}", async (int id, PlaytestDb db) =>
{
    // if (id == 100)
    // {
    //     id = await db.userplaydata.OrderByDescending(t => t.Id).Select(t => t.Id).FirstOrDefaultAsync();
    // }
    if (await db.userplaydata.FindAsync(id) is Playresult todo)
    {
        db.userplaydata.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});
app.MapPatch("userplaydata/{id}/EndTime", async (int id, EndTimeUpdateRequest _EndTime, PlaytestDb db) =>
{
    var data = await db.userplaydata.FindAsync(id);
    if (data is null)
    {
        return Results.NotFound();
    }
    data.EndTime = _EndTime.EndTime;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapPatch("userplaydata/{id}/Death", async (int id, PlaytestDb db) =>
{
    var data = await db.userplaydata.FindAsync(id);
    if (data is null)
    {
        return Results.NotFound();
    }
    data.DeadCount++;
    await db.SaveChangesAsync();
    return Results.NoContent();
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

//도커 테스트용 해당 내용들어가면 괜찮다던데
// app.UseHttpsRedirection();
//
//app.UseAuthorization();
//
//app.MapControllers();
//
//app.MapGet("/", () => "hello world!");
app.Run();
