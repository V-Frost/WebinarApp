using WebinarApp.Models;

namespace WebinarApp.Helpers
{
    public static class Session
    {
        public static string UserRole { get; set; } // "Admin" або "User"
        public static string UserEmail { get; set; }
        public static Users CurrentUser { get; set; }
    }
}
