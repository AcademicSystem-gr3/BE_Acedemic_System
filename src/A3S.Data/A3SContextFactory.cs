using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Data
{
    public class TeduBlogContextFactory : IDesignTimeDbContextFactory<A3SContext>
    {
        public A3SContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();
            var buider = new DbContextOptionsBuilder<A3SContext>();
            buider.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new A3SContext(buider.Options);
        }
    }
}
