using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class ContextExample : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}