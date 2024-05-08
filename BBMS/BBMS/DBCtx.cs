using BBMS.Models;
using Microsoft.EntityFrameworkCore;

namespace BBMS
{
    public class DBCtx : DbContext
    {
        public DBCtx(DbContextOptions<DBCtx> options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
    }
}
