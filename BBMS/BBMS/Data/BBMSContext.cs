using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BBMS.Models;

namespace BBMS.Data
{
    public class BBMSContext : DbContext
    {
        public BBMSContext (DbContextOptions<BBMSContext> options)
            : base(options)
        {
        }

        public DbSet<BBMS.Models.Order> Orders { get; set; } = default!;
        //public object Orders { get; internal set; }
    }
}
