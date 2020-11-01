using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_BookStore.Models.Models
{
    public  class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Slug { get; set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
