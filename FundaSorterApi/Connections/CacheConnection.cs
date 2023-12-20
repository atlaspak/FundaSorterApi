using StackExchange.Redis;

public class RedisConnectorHelper
{
    private static Lazy<ConnectionMultiplexer> lazyConnection;

    static RedisConnectorHelper()
    {
        ConfigurationOptions options = new ConfigurationOptions
        {
            //list of available nodes of the cluster along with the endpoint port.
            EndPoints = {
                { "localhost", 6379 },
            },
        };

        lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("redis:6379");
        });
    }

    public static ConnectionMultiplexer Connection => lazyConnection.Value;
}