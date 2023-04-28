using Microsoft.Extensions.Primitives;

namespace ARH.Front.Migrations;

internal class SqlMigrationConfiguration : IConfiguration
{
    public string? this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IEnumerable<IConfigurationSection> GetChildren() => throw new NotImplementedException();
    public IChangeToken GetReloadToken() => throw new NotImplementedException();
    public IConfigurationSection GetSection(string key)
    {
        if (key == "ConnectionStrings")
        {
            return new SqlMigrationConfigurationSection();
        }
        throw new NotImplementedException();
    }
}