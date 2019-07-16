using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace Model.Models
{
    public partial class MsSQLContext : DbContext
    {
        public MsSQLContext(DbContextOptions<MsSQLContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}