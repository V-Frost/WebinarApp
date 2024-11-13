using System.Data.Entity;

namespace WebinarApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=DefaultConnection") { }

        public DbSet<Webinar> Webinars { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<WebinarRegistration> WebinarRegistrations { get; set; }
        public DbSet<WebinarDirectory> WebinarDirectories { get; set; }

        // Додаємо таблицю Users
        public DbSet<Users> Users { get; set; }
    }
}
