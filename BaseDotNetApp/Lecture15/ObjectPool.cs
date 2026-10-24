using System.Collections.Concurrent;

namespace BaseDotNetApp.Lecture15;

public class ObjectPool
{
}

/// <summary>
/// Об'єкт, що буде перевикористовуватись. Реалізує IDisposable для коректного 
/// звільнення неуправльованих ресурсів при фінальному руйнуванні пулу.
/// </summary>
public class NetworkConnection : IDisposable
{
    public Guid Id { get; } = Guid.NewGuid();
    public bool IsActive { get; private set; }

    // Масивний буфер, який ми не хочемо постійно виділяти в LOH
    public byte[] DataBuffer { get; } = new byte[90000];

    public void ExecuteQuery()
    {
        IsActive = true;
        Console.WriteLine($"[Conn {Id}] Виконання важкого запиту...");
    }

    /// <summary>
    /// КРИТИЧНИЙ ЕТАП: Очищення стану перед поверненням до пулу (Reset Pattern).
    /// </summary>
    public void ResetForReuse()
    {
        IsActive = false;
        // Очищуємо буфер, щоб наступний клієнт не прочитав чужі дані
        Array.Clear(DataBuffer, 0, DataBuffer.Length);
        Console.WriteLine($"[Conn {Id}] Стан успішно скинуто.");
    }

    public void Dispose()
    {
        Console.WriteLine($"[Conn {Id}] Ресурс остаточно знищено.");
        // Тут закриваються реальні сокети чи файлові дескриптори
    }
}

/// <summary>
/// Контролер пулу об'єктів з підтримкою потокобезпеки.
/// Самий пул реалізує IDisposable для каскадного очищення ресурсів.
/// </summary>
public class ConnectionPool : IDisposable
{
    private readonly ConcurrentBag<NetworkConnection> _pool = new();
    private readonly int _maxCapacity;
    private int _currentCount = 0;
    private readonly object _lock = new object();

    public ConnectionPool(int maxCapacity = 10)
    {
        _maxCapacity = maxCapacity;
    }

    // Оренда об'єкта (Acquire)
    public NetworkConnection Acquire()
    {
        // Спроба взяти готовий об'єкт з кешу пулу
        if (_pool.TryTake(out var connection))
        {
            return connection;
        }

        // Якщо пул порожній, намагаємося створити новий об'єкт, якщо ліміт дозволяє
        lock (_lock)
        {
            if (_currentCount < _maxCapacity)
            {
                _currentCount++;
                return new NetworkConnection();
            }
        }

        // У синхронній архітектурі, якщо пул вичерпано, ми генеруємо виняток, 
        // або можна було б реалізувати блокування через Semaphore
        throw new InvalidOperationException("Пул вичерпано. Доступних з'єднань немає.");
    }

    // Повернення об'єкта (Release)
    public void Release(NetworkConnection connection)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));

        // ПРАВИЛО: Об'єкт ПОВИНЕН очистити свій стан перед тим, як стати доступним іншим
        connection.ResetForReuse();

        _pool.Add(connection);
    }

    public void Dispose()
    {
        // Коли пул завершує своє існування, ми каскадно звільняємо всі ресурси
        foreach (var connection in _pool)
        {
            connection.Dispose();
        }

        _pool.Clear();
    }
}

// Клієнтський код (симуляція логіки гри)
public class GameManager
{
    public void ProcessNetworkEvents()
    {
        using var pool = new ConnectionPool(maxCapacity: 2);

        // Клієнт орендує об'єкт
        var conn1 = pool.Acquire();
        conn1.ExecuteQuery();

        // Клієнт зобов'язаний повернути об'єкт у пул
        pool.Release(conn1);

        // При наступному запиті система перевикористає вже існуючий об'єкт
        var conn2 = pool.Acquire();
        Console.WriteLine($"Чи це той самий об'єкт у пам'яті? {ReferenceEquals(conn1, conn2)}"); // Виведе True
    }
}