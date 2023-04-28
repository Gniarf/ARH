using Microsoft.Extensions.Primitives;

namespace ARH.Front.Migrations;

internal class SqlMigrationConfigurationSection : IConfigurationSection
{
    public string? this[string key]
    {
        get
        {
            switch (key)
            {
                case "SqliteDb":
                    return "Data Source=sqlite.db";
                case "SqlServerDb":
                    return @"Data Source=SRVBDX1\BB8;Initial Catalog=ARH;Integrated Security=true;Trusted_Connection=True;";
                default:
                    throw new NotImplementedException();
            }
        }
        set => throw new NotImplementedException();
    }
    public string Key => throw new NotImplementedException();
    public string Path => throw new NotImplementedException();
    public string? Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IEnumerable<IConfigurationSection> GetChildren() => throw new NotImplementedException();
    public IChangeToken GetReloadToken() => throw new NotImplementedException();
    public IConfigurationSection GetSection(string key) => throw new NotImplementedException();

    IChangeToken IConfiguration.GetReloadToken()
    {
        throw new NotImplementedException();
    }
}