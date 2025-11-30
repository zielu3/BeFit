using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseDto
    {
        [Required]
        [Display(Name = "Typ æwiczenia")]
        public int ExerciseTypeId { get; set; }

        [Required]
        [Display(Name = "Sesja treningowa")]
        public int TrainingSessionId { get; set; }

        [Required]
        [Display(Name = "Obci¹¿enie (kg)")]
        public double Weight { get; set; }

        [Required]
        [Display(Name = "Liczba serii")]
        public int Sets { get; set; }

        [Required]
        [Display(Name = "Liczba powtórzeñ")]
        public int Reps { get; set; }
    }
}
