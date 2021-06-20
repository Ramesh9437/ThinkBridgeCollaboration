using InventoryManagement.BLL;
using InventoryManagement.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace InventoryManagement.Controllers
{
    public class InventoryManagementController : ApiController
    {
        ItemBLL objItemBLL = new ItemBLL();

        #region PublicMethods
        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="itemBO">Object of instance type ItemBO </param>   
        /// <returns>Success message if item is added in inventory</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> AddItemInInventory(ItemBO itemBO)
        {
            string result = string.Empty;
            try
            {
                if (itemBO != null)
                {
                    result = await objItemBLL.AddItemInInventory(itemBO);
                    if (!string.IsNullOrEmpty(result) && result.ToLower().Contains("success"))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to add item to inventory");
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                result = ex.Message;
            }
            catch (ArgumentNullException ex)
            {
                result = ex.Message;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error occured while adding item to inventory" + result);
        }

        /// <summary>
        ///Updates the item details in the inventory.
        /// </summary>
        /// <param name="itemBO">Object of instance type ItemBO </param> 
        /// <returns>Success message if item details are updated in inventory</returns>
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateItemInInventory(ItemBO itemBO)
        {
            string result = string.Empty;
            try
            {
                if (itemBO != null)
                {
                    result = await objItemBLL.UpdateItemInInventory(itemBO);
                    if (!string.IsNullOrEmpty(result) && result.ToLower().Contains("success"))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to update item in inventory" + result);
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                result = ex.Message;
            }
            catch (ArgumentNullException ex)
            {
                result = ex.Message;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error occured while updating item in inventory due to" + result);

        }

        /// <summary>
        /// Get all items from the inventory.
        /// </summary>
        /// <returns>List of items in inventory</returns>     
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllItems()
        {
            string result = string.Empty;
            List<ItemBO> listItems = new List<ItemBO>();
            try
            {
                listItems = await objItemBLL.GetAllItems();
                if (listItems != null && listItems.Count() > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, listItems);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, "No items found in inventory");
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error occured" + result);

        }

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="itemId">ItemId of item which needs to be deleted</param>   
        /// <returns>Success message if item is deleted from inventory</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> DeleteItemFromInventory([FromUri]long itemId)
        {
            string result = string.Empty;
            try
            {
                if (itemId > 0)
                {
                    result = await objItemBLL.DeleteItemFromInventory(itemId);
                    if (!string.IsNullOrEmpty(result) && result.ToLower().Contains("success"))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Item is not deleted");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, "ItemId should be greater than zero");
                }
            }
            catch (ArgumentNullException ex)
            {
                result = ex.Message;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error occured" + result);
        }
        #endregion
    }
}
