using System;
using WebinarApp.Models;

public class Webinar
{
    public int WebinarId { get; set; }
    public string Topic { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string Link { get; set; }
    public string Status { get; set; }

    // Зовнішній ключ до моделі Speaker
    public int SpeakerId { get; set; }
    public Speaker Speaker { get; set; }

    // Властивість для відображення імені спікера
    public string SpeakerName => Speaker != null ? $"{Speaker.FirstName} {Speaker.LastName}" : "Невідомий";
}
