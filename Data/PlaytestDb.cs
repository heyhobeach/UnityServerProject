using Microsoft.EntityFrameworkCore;
namespace UnityServerProject.Data
{
    public class PlaytestDb:DbContext
    {
        public PlaytestDb(DbContextOptions<PlaytestDb> options):base(options)
        {
        }

        public DbSet<Model.Playresult> Todos => Set<Model.Playresult>();
    }
}
