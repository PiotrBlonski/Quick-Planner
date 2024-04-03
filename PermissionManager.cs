
namespace Quick_Planner
{
    static class PermissionManager
    {
        public static async Task<PermissionStatus> CheckPermission<TPermission>() where TPermission : Permissions.BasePermission, new()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<TPermission>();
            return status;
        }

        public static async Task<PermissionStatus> RequestPermission<TPermission>() where TPermission : Permissions.BasePermission, new()
        {
            PermissionStatus status = await Permissions.RequestAsync<TPermission>();
            return status;
        }
     }
}
