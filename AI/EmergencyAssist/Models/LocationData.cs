using System;
using System.ComponentModel.DataAnnotations;

namespace EmergencyAssist.Models
{
    public class LocationData
    {
        [Key]
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public string CapturedBy { get; set; }
    }
}
