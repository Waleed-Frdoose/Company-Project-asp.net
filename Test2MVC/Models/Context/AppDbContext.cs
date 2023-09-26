using Microsoft.EntityFrameworkCore;

namespace Test2MVC.Models.Context
{
    public class AppDbContext : DbContext
    {
        //public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var conn = config.GetSection("constr").Value;

            optionsBuilder.UseSqlServer(conn);
        }
    }
}
