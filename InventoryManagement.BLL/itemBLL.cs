using InventoryManagement.BO;
using InventoryManagement.DAL;
using InventoryManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL
{
    public class ItemBLL
    {
        ItemDAL objItemDAL = new ItemDAL();

        #region PublicMethods    
        //Mapping done from BO object to model object to avoid circular reference
        public async Task<string> AddItemInInventory(ItemBO itemBO)
        {
            string result = string.Empty;
            Item objItem = new Item();
            objItem.ItemName = itemBO.itemName;
            objItem.ItemDescription = itemBO.itemDescription;
            objItem.Price = itemBO.price;
            objItem.IsDeleted = itemBO.IsDeleted;
            objItem.AddedOn = DateTime.UtcNow;
            result = await objItemDAL.AddItemInInventory(objItem);
            return result;
        }

        //Mapping done from BO object to model object to avoid circular reference
        public async Task<string> UpdateItemInInventory(ItemBO itemBO)
        {
            string result = string.Empty;
            Item objItem = new Item();
            objItem.ItemId = itemBO.itemId;
            objItem.ItemName = itemBO.itemName;
            objItem.ItemDescription = itemBO.itemDescription;
            objItem.Price = itemBO.price;
            objItem.IsDeleted = itemBO.IsDeleted;
            result = await objItemDAL.UpdateItemInInventory(objItem);
            return result;
        }

        //Mapping done from Model object to BO object to avoid circular reference
        public async Task<List<ItemBO>> GetAllItems()
        {
            List<ItemBO> listItemsBO = new List<ItemBO>();
            List<Item> listItemsDb = await objItemDAL.GetAllItems();
            listItemsDb?.ForEach(a => {
                ItemBO itemBO = new ItemBO()
                {
                    itemId = a.ItemId,
                    itemName = a.ItemName,
                    itemDescription = a.ItemDescription,
                    price = a.Price
                };
                listItemsBO.Add(itemBO);
            });
            return listItemsBO;
        }
        public async Task<string> DeleteItemFromInventory(long itemId)
        {
            string result = string.Empty;
            int count = await objItemDAL.DeleteItemFromInventory(itemId);
            if (count > 0)
            {
                result = "Success";
            }
            else
            {
                result = "Failed";
            }
            return result;
        }
        #endregion
    }
}
