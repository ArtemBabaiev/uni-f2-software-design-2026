namespace BaseDotNetApp.Lecture15;

public static class SingletonThreadSafe
{

    public static void Exec()
    {
        var gameConfiguration = GameConfiguration.Instance;
    }
    /// <summary>
    /// Реалізація Singleton за допомогою Double-Checked Locking з бар'єром пам'яті.
    /// </summary>
    public sealed class GameConfiguration
    {
        // Використання volatile генерує бар'єр пам'яті, забороняючи оптимізації переупорядкування.
        private static volatile GameConfiguration? _instance;
        
        // Виділений об'єкт виключно для синхронізації потоків (м'ютекс).
        private static readonly object _syncLock = new object();

        public string DifficultyLevel { get; private set; }

        private GameConfiguration()
        {
            Console.WriteLine("Потокобезпечна ініціалізація налаштувань...");
            Thread.Sleep(1000);
            DifficultyLevel = "Hardcore";
        }

        public static GameConfiguration Instance
        {
            get
            {
                // Перша перевірка: уникаємо блокування, якщо об'єкт вже створено
                if (_instance == null)
                {
                    lock (_syncLock) 
                    {
                        // Друга перевірка: гарантуємо, що інший потік не встиг 
                        // створити об'єкт, поки поточний потік чекав на блокування
                        if (_instance == null)
                        {
                            _instance = new GameConfiguration();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}