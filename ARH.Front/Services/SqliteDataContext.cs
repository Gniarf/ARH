using Microsoft.EntityFrameworkCore;

namespace ARH.Front.Services;

public class SqliteDataContext : DataContext
{
    //public SqliteDataContext() : base(new ARH.Front.Migrations.SqlMigrationConfiguration()) { }

    public SqliteDataContext(IConfiguration configuration) : base(configuration) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(configuration?.GetConnectionString("SqliteDb") ?? "Data Source=sqlite.db");
    }
}