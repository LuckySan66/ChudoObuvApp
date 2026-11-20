using ChudoObuvApp.Models;

namespace ChudoObuvApp
{
    public static class CurrentUser
    {
        public static User? User { get; set; }
        public static bool IsGuest => User == null;

        public static string FullName => User == null
            ? "Гость"
            : $"{User.LastName} {User.FirstName} {User.Patronymic}".Trim();

        public static string RoleName => User?.Role?.Name ?? "Неавторизованный пользователь";
    }
}
