using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nazwa æwiczenia")]
        public string Name { get; set; }
    }
}
