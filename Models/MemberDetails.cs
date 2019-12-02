using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeenaKaata.Models
{
    public class MemberDetails
    {
        public int MemberId { get; set; }
        [Required]
        public string FristName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
      
        public Nullable<bool> IsActive { get; set; }
       
        public Nullable<bool> IsDelete { get; set; }

        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        [Required]
        public string Address { get; set; }
    }
}