//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace U_Commerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ProductVerified
    {
        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Admin")]
        public int AdminUserId { get; set; }

        public System.DateTime DateTime { get; set; }
        public string Ip { get; set; }
    
        public virtual AdminUser AdminUser { get; set; }
        public virtual Product Product { get; set; }
    }
}
