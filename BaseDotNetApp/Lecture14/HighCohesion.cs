namespace BaseDotNetApp.Lecture14;

public class LowCohesion
{
    // Порушення: Низька згуртованість (Low Cohesion)
    public class UserAccount
    {
        private string _passwordHash = string.Empty;
        private string _email = string.Empty;
        private string _bio = string.Empty;

        // Зв'язний компонент 1: Автентифікація
        public void UpdatePassword(string hash) => _passwordHash = hash;
        public bool VerifyPassword(string hash) => _passwordHash == hash;

        // Зв'язний компонент 2: Представлення (UI)
        public void UpdateBio(string bio) => _bio = bio;
        public string GenerateHtmlProfile() => $"<h1>{_email}</h1><p>{_bio}</p>";
    }
}

public class HighCohesion
{
    // Клас фокусується лише на ідентифікаційних даних
    public class UserCredentials(string email, string passwordHash)
    {
        public string Email { get; } = email;
        public string PasswordHash { get; private set; } = passwordHash;

        public void UpdatePassword(string newHash) => PasswordHash = newHash;
        public bool Verify(string hash) => PasswordHash == hash;
    }

    // Окремий сервіс фокусується виключно на генерації профілю
    public class UserProfileRenderer
    {
        public string GenerateHtmlProfile(string email, string bio) 
            => $"<h1>{email}</h1><p>{bio}</p>";
    }
}