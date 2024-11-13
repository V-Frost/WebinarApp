using System;

namespace WebinarApp.Models
{
    public class WebinarRegistration
    {
        public int WebinarRegistrationId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Status { get; set; }

        public int ParticipantId { get; set; }
        public Participant Participant { get; set; }

        public int WebinarId { get; set; }
        public Webinar Webinar { get; set; }

        // Властивості для відображення імені учасника та теми вебінару
        public string ParticipantName => Participant != null ? $"{Participant.FirstName} {Participant.LastName}" : "Невідомий";
        public string WebinarTopic => Webinar != null ? Webinar.Topic : "Невідомий";
    }
}
