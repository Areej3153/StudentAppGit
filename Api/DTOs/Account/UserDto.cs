using Api.Data;
using Api.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.DTOs.Account
{
    public class UserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }


      
        public string BirthDate { get; set; }

        public string JWT { get; set; }

        public string photoPath { get; set; }

        public Graduate Graduate { get; set; }

        /// <summary>
        /// public int photoId { get; set; }
        /// </summary>

        // [ForeignKey("photoId")]
        //public virtual FileDetails Photo { get; set; }

    }
}
