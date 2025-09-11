using Microsoft.EntityFrameworkCore;
namespace UnityServerProject.Data
{
    public class PlaytestDb:DbContext
    {
        public PlaytestDb(DbContextOptions<PlaytestDb> options):base(options)
        {
        }

        public DbSet<Model.Playresult> userplaydata => Set<Model.Playresult>();//데이터 베이스쪽이랑 동일해야함
    }
}
