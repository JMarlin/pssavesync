using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PSaveSync.Data {

    public class PSaveSyncDbContext : DbContext {

        public const string CONNECTION_STRING = "server=makaira-mysql.mysql.database.azure.com;uid=makaira_admin;pwd=GEvDyMAqJ72hwgJ;database=psavesync";

        public DbSet<Block> Block { get; set; }
        public DbSet<Save> Save { get; set; }

        public PSaveSyncDbContext(DbContextOptions<PSaveSyncDbContext> options)
            : base(options) { }
    }
}