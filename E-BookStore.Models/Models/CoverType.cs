using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_BookStore.Models.Models
{
    public class CoverType:BaseEntity
    {

        [Required]
        [DisplayName("Cover Type")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
