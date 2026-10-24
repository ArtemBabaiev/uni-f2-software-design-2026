using Microsoft.Extensions.ObjectPool;

namespace BaseDotNetApp.Lecture15;

public static class ObjectPoolViaDefaultObjectPool
{
    public static void Exec()
    {
        var provider = new DefaultObjectPoolProvider { MaximumRetained = 4 };
        ObjectPool<NetworkConnection> pool =
            provider.Create(new NetworkConnectionPolicy());

        var conn = pool.Get();
        try
        {
            conn.ExecuteQuery();
        }
        finally
        {
            pool.Return(conn); // завжди повертаємо у finally
        }

        var conn2 = pool.Get();
        Console.WriteLine(ReferenceEquals(conn, conn2)); // True
        pool.Return(conn2);
    }
}

// Політика: створення + скидання стану (Reset)
public class NetworkConnectionPolicy : IPooledObjectPolicy<NetworkConnection>
{
    public NetworkConnection Create() => new();

    public bool Return(NetworkConnection connection)
    {
        connection.ResetForReuse();
        return true; // true = повернути об'єкт у пул
    }
}