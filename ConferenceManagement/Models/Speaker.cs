namespace WebinarApp.Models
{
    public class Speaker
    {
        public int SpeakerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Додаємо властивість FullName для відображення ПІБ
        public string FullName => $"{FirstName} {LastName}";
    }

}
