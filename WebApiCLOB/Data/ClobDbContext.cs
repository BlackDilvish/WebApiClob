using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCLOB.Models;

namespace WebApiCLOB.Data
{
    public class ClobDbContext : DbContext
    {
        public ClobDbContext(DbContextOptions<ClobDbContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
    }
}
