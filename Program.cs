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
//�ڽ������� ���� �� ���µ�? 테스트
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//
//minimal api사용중이며 각 요청별로 내용을 정리한것임
//minimal api에서 의존성 주입을 자체적으로 진행해서 이렇게 사용 가능함
var userplaydata = app.MapGroup("/userplaydata");
// access denied 되어 있는데 왜 그럴까 로컬에서 접속은 문제 없는데 
app.MapGet("/userplaydata", async (PlaytestDb db) =>
{
    await db.userplaydata.ToListAsync();//여기가 이제 문제 같아 보이는데
    Console.WriteLine("데이터 출력");
});
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
    //var stagedata = await db.userStageDeathInfo.FindAsync(id);
    if (data is null)
    {
        return Results.NotFound();
    }
    data.IsNormal = inputData.IsNormal;
    data.StartTime = inputData.StartTime;
    data.EndTime = inputData.EndTime;
    data.DeadCount = inputData.DeadCount;
    // data.QuizResults=inputData.QuizResults;
    // data.stageDeaths=inputData.stageDeaths; //여기 부분은 어떻게 해야하지 
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

//주소는 문제없어 보이는데 아마 전송하는 인자 전달이 모자라서 그런듯
app.MapPatch("userplaydata/stageDeath", async (RootRequest info, PlaytestDb db) =>//StageDeathInfos info//HttpRequest request
// stageDeathInfos
{
    //유니티에서 데이터 전송후 제대로 받는지 확인하고 받는다면 출력을 시키는게 필요함
    foreach(var s in info.stageDeathInfos)
    {
        var stageEntity = new StageDeathInfo
        {
            StageName = s.stageName,
            DeathCount = s.DeathCount
        };
        foreach(var d in s.deathinfos)
        {
            stageEntity.DeathInfos.Add(new DeathInfo
            {
                EnemyName=d.EnemyName,
                DeathPositionX = d.deathPosition.x,
                DeathPositionY=d.deathPosition.y,
                EnemyPositionX=d.EnemyPosition.x,
                EnemyPositionY=d.EnemyPosition.y
            });
        }
        db.userStageDeathInfo.Add(stageEntity);
    }


    // var data = await db.userStageDeathInfo.FindAsync(id);
    // if (data is null)
    // {
    //     return Results.NotFound();
    // }
    // data.StageName = info.StageName;
    // data.DeathInfos=info.DeathInfos;
    // data.DeathCount=info.DeathCount;
    // await db.SaveChangesAsync();
    // Console.WriteLine($"stageDeath info \n {info.StageName}, {info.}");//이게 되려나? json형태로 온다면 string으로 진행오는것 아닌가 싶기도 하고
    for (int i = 0; i < info.stageDeathInfos.Count; i++)
    {
        Console.WriteLine("========================================");
        Console.WriteLine(info.stageDeathInfos.Count);//보내도 자꾸 개수가 0이라고 나오네 클라이언트에서는 0이라고 나오면 안 되긴함

        // using (var reader = new StreamReader(request.Body))
        // {
        //     var body = await reader.ReadToEndAsync();
        //     Console.WriteLine("=== 받은 JSON ===");
        //     Console.WriteLine(body);
        // }
        Console.WriteLine($"[Stage Receipt] 스테이지: {info.stageDeathInfos[i].stageName}");
        Console.WriteLine($"[Stage Receipt] 총 사망 횟수: {info.stageDeathInfos[i].DeathCount}");
        Console.WriteLine($"[Stage Receipt] 연결된 플레이 ID: {info.stageDeathInfos[i].playresultId}");
        // 2. 리스트(DeathInfos) 내부 데이터 상세 출력
        if (info.stageDeathInfos[i].deathinfos != null && info.stageDeathInfos[i].deathinfos.Count > 0)
        {
            Console.WriteLine($"--- 상세 사망 정보 ({info.stageDeathInfos[i].deathinfos.Count}건) ---");
            foreach (var death in info.stageDeathInfos[i].deathinfos)
            {
                Console.WriteLine($"- 원인 적: {death.EnemyName}");
                Console.WriteLine($"  내 위치: ({death.deathPosition.x}, {death.deathPosition.y})");
                Console.WriteLine($"  적 위치: ({death.EnemyPosition.x}, {death.EnemyPosition.y})");
            }
        }
        else
        {
            Console.WriteLine("--- 상세 사망 정보가 없습니다. ---");
        }
        Console.WriteLine("========================================");
    }
    return Results.NoContent();
});

app.MapPatch("userplaydata/{id}/stageDeath", async (int id, StageDeathInfo info, PlaytestDb db) =>
{
    //유니티에서 데이터 전송후 제대로 받는지 확인하고 받는다면 출력을 시키는게 필요함


    // var data = await db.userStageDeathInfo.FindAsync(id);
    // if (data is null)
    // {
    //     return Results.NotFound();
    // }
    // data.StageName = info.StageName;
    // data.DeathInfos=info.DeathInfos;
    // data.DeathCount=info.DeathCount;
    // await db.SaveChangesAsync();
    Console.WriteLine($"{id} 번 stageDeath ");//이게 되려나? json형태로 온다면 string으로 진행오는것 아닌가 싶기도 하고
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
