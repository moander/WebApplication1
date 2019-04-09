using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Db
{
    public class ApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
