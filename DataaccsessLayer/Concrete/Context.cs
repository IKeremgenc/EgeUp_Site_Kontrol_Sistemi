using DataaccsessLayer.Table;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataaccsessLayer.Concrete
{
    public class Context:IdentityDbContext<AppUser, AppRole, int>
    {
        private readonly IConfiguration _configuration;

        public Context(DbContextOptions<Context> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

              
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<WebsiteUrl> WebsiteUrls { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Company> Companies { get; set; }
     

    }
}
