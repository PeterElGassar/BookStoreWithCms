using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_BookStore.Models.Models
{
   public  class AppUser:IdentityUser
    {
        public AppUser()
        {
            CreatedAt = DateTime.Now;
        }

        public string Slug { get; set; }

        public DateTimeOffset CreatedAt { get; private set; }

        [Required]
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        [NotMapped]
        public string Role { get; set; }

        public int? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
    }
}
