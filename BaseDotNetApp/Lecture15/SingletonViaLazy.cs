namespace BaseDotNetApp.Lecture15;

public static class SingletonViaLazy
{
    /// <summary>
    /// Сучасний індустріальний стандарт Singleton на базі Lazy<T>.
    /// </summary>
    public sealed class GameConfiguration
    {
        // LazyThreadSafetyMode.ExecutionAndPublication гарантує, що делегат створення 
        // буде виконано лише одним потоком, а всі інші будуть заблоковані до його завершення.
        private static readonly Lazy<GameConfiguration> _lazyInstance = 
            new Lazy<GameConfiguration>(
                () => new GameConfiguration(), 
                LazyThreadSafetyMode.ExecutionAndPublication);

        public string DifficultyLevel { get; private set; }

        private GameConfiguration()
        {
            Console.WriteLine("Модернова лінива ініціалізація налаштувань...");
            DifficultyLevel = "Hardcore";
        }

        // Властивість Value ініціює створення лише під час першого звернення
        public static GameConfiguration Instance => _lazyInstance.Value;
        
        public void RenderUI()
        {
            Console.WriteLine($"UI відображає складність: {DifficultyLevel}");
        }
    }
}