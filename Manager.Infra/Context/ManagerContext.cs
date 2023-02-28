using Manager.Domain.Entities;
using Manager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Crypto.Tls;

namespace Manager.Infra.Context
{
    public class ManagerContext : DbContext
    {
        public ManagerContext()
        {
        }

        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"Server=localhost;Database=USERMANAGERAPI;Uid=root;Pwd=password;", new MySqlServerVersion(new Version(8, 0, 11)));
            //Server=localhost:3306;Database=USERMANAGERAPI;Uid=root;Pwd=password;
            // optionsBuilder.UseMySql();//Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
        }
    }
}

