using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Typ æwiczenia")]
        public int ExerciseTypeId { get; set; }
        
        [Display(Name = "Æwiczenie")]
        public ExerciseType ExerciseType { get; set; }

        [Required]
        [Display(Name = "Sesja treningowa")]
        public int TrainingSessionId { get; set; }
        
        [Display(Name = "Sesja treningowa")]
        public TrainingSession TrainingSession { get; set; }

        [Required]
        [Display(Name = "Obci¹¿enie (kg)")]
        public double Weight { get; set; }

        [Required]
        [Display(Name = "Liczba serii")]
        public int Sets { get; set; }

        [Required]
        [Display(Name = "Liczba powtórzeñ")]
        public int Reps { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
