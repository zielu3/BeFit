using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeFit.Models
{
    [NotMapped]
    public class ExerciseStats
    {
        [Display(Name = "Nazwa æwiczenia")]
        public string ExerciseName { get; set; }

        [Display(Name = "Liczba wykonañ")]
        public int Count { get; set; }

        [Display(Name = "£¹czna liczba powtórzeñ")]
        public int TotalReps { get; set; }

        [Display(Name = "Œrednie obci¹¿enie (kg)")]
        public double AverageWeight { get; set; }

        [Display(Name = "Maksymalne obci¹¿enie (kg)")]
        public double MaxWeight { get; set; }
    }
}
