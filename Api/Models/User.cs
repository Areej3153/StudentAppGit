using Api.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;


        [Required]
        //[Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Required]
        public string photoPath { get; set; }

        [Required]
        public int photoId { get; set; }

        [ForeignKey("photoId")]
        public virtual FileDetails Photo { get; set; }

        //[Required]
       // public Graduate Graduate { get; set; }
    }
}
