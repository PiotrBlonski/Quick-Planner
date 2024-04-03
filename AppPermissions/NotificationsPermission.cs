
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace Quick_Planner.AppPermissions
{
    internal class NotificationsPermission : BasePlatformPermission
    {
#if ANDROID
    public override (string androidPermission, bool isRuntime)[] RequiredPermissions =>
        new List<(string permission, bool isRuntime)>
        {
        ("android.permission.POST_NOTIFICATIONS", true),
        ("android.permission.SCHEDULE_EXACT_ALARM", true)
    }.ToArray();
#endif
    }
}
