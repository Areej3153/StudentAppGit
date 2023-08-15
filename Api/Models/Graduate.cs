using Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("Graduate")]
    public class Graduate
    {
        [ForeignKey("User")]
        public string Id { get; set; }

        [Required]
        public string LevelStudy { get; set; }

        [Required]
        public string Program { get; set; }

        [Required]
        public string FaculityDivision { get; set; }

        public User User { get; set; }


    }
}
