using InventoryManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DAL
{
    public class ItemDAL
    {
        #region PublicMethods
        //Adds item to Items table
        public async Task<string> AddItemInInventory(Item objItem)
        {
            int count = 0;
            string result = string.Empty;
            try
            {
                using (extdbInventorySystemEntities dbEntities = new extdbInventorySystemEntities())
                {
                    dbEntities.Items.Add(objItem);
                    count = await dbEntities.SaveChangesAsync();
                }
                if (count > 0)
                    result = "Success";

            }
            catch (MetadataException ex)
            {
                result = ex.Message;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        result += ("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return result;
        }

        //Updates records in Items table
        public async Task<string> UpdateItemInInventory(Item objItem)
        {
            int rowsAffected = 0;
            string result = string.Empty;
            try
            {
                Item itemDbModel = new Item();
                using (extdbInventorySystemEntities dbEntities = new extdbInventorySystemEntities())
                {
                    itemDbModel = await (from r in dbEntities.Items where r.ItemId == objItem.ItemId select r).FirstOrDefaultAsync();
                    if (itemDbModel != null)
                    {
                        itemDbModel.ItemName = objItem.ItemName;
                        itemDbModel.ItemDescription = objItem.ItemDescription;
                        itemDbModel.Price = objItem.Price;
                        itemDbModel.AddedOn = DateTime.Now;
                        rowsAffected = await dbEntities.SaveChangesAsync();
                    }
                }
                if (rowsAffected > 0)
                    result = "Success";

            }
            catch (MetadataException ex)
            {
                result = ex.Message;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        result += ("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return result;
        }

        //Gets list of Items from Items table
        public async Task<List<Item>> GetAllItems()
        {
            List<Item> listItemsDb = new List<Item>();
            using (extdbInventorySystemEntities dbEntities = new extdbInventorySystemEntities())
            {
                listItemsDb = await (from r in dbEntities.Items where r.IsDeleted == false select r).ToListAsync();
            }
            return listItemsDb;
        }

        //Deletes item in Items table
        public async Task<int> DeleteItemFromInventory(long itemId)
        {
            int rows = 0;
            Item itemDbModel = new Item();
            using (extdbInventorySystemEntities dbEntities = new extdbInventorySystemEntities())
            {
                itemDbModel = await (from r in dbEntities.Items where r.ItemId == itemId select r).FirstOrDefaultAsync();
                if (itemDbModel != null)
                {
                    itemDbModel.IsDeleted = true;
                    rows = await dbEntities.SaveChangesAsync();
                }
            }
            return rows;
        }
        #endregion
    }
}
