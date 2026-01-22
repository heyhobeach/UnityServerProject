using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using UnityServerProject.Model;
namespace UnityServerProject.Data
{
    public class PlaytestDb:DbContext
    {
        public PlaytestDb(DbContextOptions<PlaytestDb> options):base(options)
        {
        }

        public DbSet<Model.Playresult> userplaydata => Set<Model.Playresult>();//데이터 베이스쪽이랑 동일해야함//왜 인지 여기에 문제가 생기고 있음
        public DbSet<Model.StageDeathInfo> StageDeathInfo =>Set<Model.StageDeathInfo>();//이 라인 사용시 지금 vector2 자료형 관련 버그 발생중


        //안에 자료형을 제회 하더라도 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Playresult 엔티티에 string,int 딕셔너리를 
            //여기 라인 문제인가
            // modelBuilder.Entity<Playresult>()
            //     .Property(e=>e.stageDeaths)
            //     .HasConversion(
            //         v=>JsonSerializer.Serialize(v, (JsonSerializerOptions)null),//C# -> DB로 갈때 그래서 json형태로 바꿔줌
            //         v=>JsonSerializer.Deserialize<Dictionary<string,int>>(v,(JsonSerializerOptions)null)??new Dictionary<string, int>()//DB -> C# 으로 바꿔줌 그래서 json형태를 다시 딕셔너리로 바꿔줌
            //     );

            // modelBuilder.Entity<Playresult>()
            //     .Property(e=>e.QuizResults)
            //     .HasConversion(
            //         v=>JsonSerializer.Serialize(v,(JsonSerializerOptions)null),
            //         v=>JsonSerializer.Deserialize<Dictionary<string,QuizRecord>>(v,(JsonSerializerOptions)null)?? new Dictionary<string, QuizRecord>()
            //     );
        }
    }
}
