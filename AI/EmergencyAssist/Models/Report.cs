using System;
using System.ComponentModel.DataAnnotations;

namespace EmergencyAssist.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public string SubmittedBy { get; set; }

        public bool IsAnonymous { get; set; }
    }
}
