using System;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TrainingSessionDto
    {
        [Required]
        [Display(Name = "Data i czas rozpoczêcia")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Data i czas zakoñczenia")]
        public DateTime EndTime { get; set; }
    }
}
