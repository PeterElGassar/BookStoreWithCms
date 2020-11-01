using System;
using System.Collections.Generic;
using System.Text;

namespace E_BookStore.Models.Models
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string StreetAddress { get; set; }

        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsVerifiedCompany { get; set; }

    }
}
