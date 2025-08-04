using Microsoft.EntityFrameworkCore;
namespace UnityServerProject.Data
{
    public class TodoDb:DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options):base(options)
        {
        }

        public DbSet<Model.Todo> Todos => Set<Model.Todo>();
    }
}
