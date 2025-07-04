using System;
using System.ComponentModel.DataAnnotations;

namespace EmergencyAssist.Models
{
    public class EmotionData
    {
        [Key]
        public int Id { get; set; }

        public string Emotion { get; set; }

        public float Confidence { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public string CapturedBy { get; set; }
    }
}
