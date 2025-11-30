using System;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data i czas rozpoczêcia")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Data i czas zakoñczenia")]
        public DateTime EndTime { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
