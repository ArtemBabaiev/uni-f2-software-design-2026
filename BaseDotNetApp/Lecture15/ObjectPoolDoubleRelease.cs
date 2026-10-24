namespace BaseDotNetApp.Lecture15;

using Microsoft.Extensions.ObjectPool;

public static class ObjectPoolDoubleRelease
{
    public static void Exec()
    {
        var provider = new DefaultObjectPoolProvider { MaximumRetained = 4 };
        ObjectPool<NetworkConnection> pool =
            provider.Create(new NetworkConnectionPolicy());
        
        using (var lease = new PooledConnection(pool))
        {
            lease.Connection.ExecuteQuery();
        } // автоматичний Return, повторний Dispose безпечний
    }
}

public sealed class PooledConnection : IDisposable
{
    private readonly ObjectPool<NetworkConnection> _pool;
    private NetworkConnection? _connection;

    public PooledConnection(ObjectPool<NetworkConnection> pool)
    {
        _pool = pool;
        _connection = pool.Get();
    }

    public NetworkConnection Connection =>
        _connection ?? throw new ObjectDisposedException(nameof(PooledConnection));

    public void Dispose()
    {
        // Interlocked: повернення відбудеться лише один раз,
        // навіть при повторному Dispose() або з різних потоків
        var conn = Interlocked.Exchange(ref _connection, null);
        if (conn is not null)
            _pool.Return(conn);
    }
}