using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Aqarlist.Core.Models.Database
{
    public class ApiDbContext :DbContext
    {
        private readonly IConfiguration _configuration;
        public ApiDbContext(DbContextOptions options,IConfiguration configuration)
          : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        }
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Roles> Roles { get; set; } = null!;
        public DbSet<Property> Property { get; set; } = null!;
        public DbSet<PropertyType> PropertyType { get; set; } = null!;
        public DbSet<Attachment> Attachments{ get; set; } = null!;
    }
}
