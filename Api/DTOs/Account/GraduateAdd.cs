using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Account
{
    public class GraduateAdd
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string LevelStudy { get; set; }

        [Required]
        public string Program { get; set; }

        [Required]
        public string FaculityDivision { get; set; }
    }
}
