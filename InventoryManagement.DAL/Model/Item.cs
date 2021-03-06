//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryManagement.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Item
    {
        public long ItemId { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9]([\w -]*[a-zA-Z0-9])?$", ErrorMessage="Item name should be alphanumeric")]
        public string ItemName { get; set; }

        [MaxLength(250)]
        public string ItemDescription { get; set; }

        [RegularExpression(@"^\d+(\.\d+)*$")]
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime AddedOn { get; set; }
    }
}
