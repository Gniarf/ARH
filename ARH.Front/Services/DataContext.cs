using ARH.Front.Models;
using Microsoft.EntityFrameworkCore;

namespace ARH.Front.Services;
public class DataContext : DbContext
{
    protected readonly IConfiguration? configuration;

    //public DataContext() : this(new ARH.Front.Migrations.SqlMigrationConfiguration()) { }
    public DataContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(configuration?.GetConnectionString("SqlServerDb"));
    }

    public DbSet<DailyRecord>? DailyRecordCollection { get; set; }
    public DbSet<Comment>? CommentCollection { get; set; }
    public DbSet<Holyday>? HolydayCollection { get; set; }
    public DbSet<HolydayUser>? HolydayUserCollection { get; set; }
}