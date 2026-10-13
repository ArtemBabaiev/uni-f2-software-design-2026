namespace BaseDotNetApp.Lecture13_1;

public class KissGoodExample
{
    
}

public class UserAccessValidator
{
    public bool CanGrantSystemAccess(UserProfile? userProfile, SystemResource? targetResource)
    {
        // Guard Clauses: Відсіювання всіх негативних та невалідних сценаріїв на самому початку
        if (userProfile is null || !userProfile.IsActiveStatus)
        {
            return false;
        }

        if (targetResource is null || targetResource.CurrentStatus != ResourceState.Available)
        {
            return false;
        }

        // Центральна, лінійна та лаконічна перевірка бізнес-правила за допомогою Pattern Matching
        return userProfile.AssignedRole is SecurityRole.Administrator or SecurityRole.SystemManager;
    }
}