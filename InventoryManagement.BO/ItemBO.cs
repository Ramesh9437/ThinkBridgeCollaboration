using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BO
{
    public class ItemBO
    {
        public long itemId { get; set; }
        public string itemName { get; set; }
        public string itemDescription { get; set; }
        public decimal price { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
