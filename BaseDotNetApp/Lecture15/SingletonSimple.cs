namespace BaseDotNetApp.Lecture15;

public static class SingletonSimple
{

    public static void Exec()
    {
        GameConfiguration config = GameConfiguration.Instance;
    }
    
    /// <summary>
    /// Проста реалізація Singleton. Абсолютно НЕ БЕЗПЕЧНА для конкурентного середовища.
    /// </summary>
    public sealed class GameConfiguration
    {
        // 1. Приватне статичне поле (- instance: Singleton)
        private static GameConfiguration? _instance;

        public string DifficultyLevel { get; private set; }

        // 2. Приватний конструктор (- Singleton())
        private GameConfiguration()
        {
            Console.WriteLine("Ініціалізація важких налаштувань ігрового світу...");
            // Симуляція ресурсомісткої операції
            Thread.Sleep(1000); 
            DifficultyLevel = "Hardcore";
        }

        // 3. Глобальна точка доступу (+ getInstance(): Singleton)
        public static GameConfiguration Instance
        {
            get
            {
                // КРИТИЧНА ВРАЗЛИВІСТЬ: Стан перегонів (Race Condition)
                if (_instance == null)
                {
                    _instance = new GameConfiguration();
                }
                return _instance;
            }
        }
    }
}