using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApi.Model;

namespace WebApi.DAL
{
    public partial class MsSQLContext : DbContext
    {
        public MsSQLContext(DbContextOptions<MsSQLContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}