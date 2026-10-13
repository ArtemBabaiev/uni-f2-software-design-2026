namespace BaseDotNetApp.Lecture13_1;

public class KissBadExample
{
    
}

// public class UserAccessValidator
// {
//     // Антипатерн Arrow: Глибока вкладеність, що викликає вибух когнітивної складності
//     public bool CanGrantSystemAccess(UserProfile? userProfile, SystemResource? targetResource)
//     {
//         bool isAccessGranted = false;
//
//         if (userProfile != null)
//         {
//             if (userProfile.IsActiveStatus)
//             {
//                 if (targetResource != null)
//                 {
//                     if (targetResource.CurrentStatus == ResourceState.Available)
//                     {
//                         if (userProfile.AssignedRole == SecurityRole.Administrator || 
//                             userProfile.AssignedRole == SecurityRole.SystemManager)
//                         {
//                             isAccessGranted = true;
//                         }
//                         else
//                         {
//                             isAccessGranted = false;
//                         }
//                     }
//                 }
//             }
//         }
//         
//         return isAccessGranted;
//     }
// }

public class UserProfile
{
    public bool IsActiveStatus {get; set;}
    public SecurityRole AssignedRole {get; set;}
    
}
public class SystemResource
{
    public ResourceState CurrentStatus  {get; set;}
    
}

public enum ResourceState
{
    Available,
    Unavailable
}

public enum SecurityRole
{
    Administrator,
    SystemManager
}