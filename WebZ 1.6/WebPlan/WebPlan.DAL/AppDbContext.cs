using Microsoft.EntityFrameworkCore;
using WebPlan.Domain.Entity;

namespace WebPlan.DAL
{
    public class AppDbContext : DbContext
    {       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        public DbSet<DataRecord> DataRecords { get; set; }
    }
}