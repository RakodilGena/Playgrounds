namespace Playgrounds.EfCore.AppSettings;

public sealed class DbConnectionOptions
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Database { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int CommandTimeout { get; set; }
}
