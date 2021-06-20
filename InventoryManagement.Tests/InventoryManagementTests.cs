using System;
using System.Collections.Generic;
using System.Web.Http;
using InventoryManagement.BO;
using InventoryManagement.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace InventoryManagement.Tests
{
    [TestClass]
    public class InventoryManagementTests
    {
        //Test case when all parameters are passed.
        [TestMethod]
        public void AddItemInInventory()
        {
            var inventorySystem = new InventoryManagementController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var objItem = new ItemBO() { itemName = "BatteryTest", itemDescription = "For Car", price = 1000, IsDeleted = false, AddedOn = DateTime.UtcNow };

            var result = inventorySystem.AddItemInInventory(objItem).Result;
            Assert.AreEqual(result.IsSuccessStatusCode, true);
        }
        //Test case when numbers are used in item names.
        [TestMethod]
        public void AddItemInInventory_ItemNameWithNumbers()
        {
            var inventorySystem = new InventoryManagementController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var objItem = new ItemBO() { itemName = "BatteryTest12", itemDescription = "For Car", price = 1000, IsDeleted = false, AddedOn = DateTime.UtcNow };

            var result = inventorySystem.AddItemInInventory(objItem).Result;
            Assert.AreEqual(result.IsSuccessStatusCode, true);
        }
        //Test case when hyphen is used in item names.
        [TestMethod]
        public void AddItemInInventory_ItemNameWithHyphen()
        {
            var inventorySystem = new InventoryManagementController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var objItem = new ItemBO() { itemName = "Battery-Test", itemDescription = "For Car", price = 1000, IsDeleted = false, AddedOn = DateTime.UtcNow };

            var result = inventorySystem.AddItemInInventory(objItem).Result;
            Assert.AreEqual(result.IsSuccessStatusCode, true);
        }
        //Test case when underscore is used in item names.
        [TestMethod]
        public void AddItemInInventory_ItemNameWithUnderscore()
        {
            var inventorySystem = new InventoryManagementController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var objItem = new ItemBO() { itemName = "Battery_Test", itemDescription = "For Car", price = 1000, IsDeleted = false, AddedOn = DateTime.UtcNow };

            var result = inventorySystem.AddItemInInventory(objItem).Result;
            Assert.AreEqual(result.IsSuccessStatusCode, true);
        }

        //Negative test case which includes special characters in the item name
        [TestMethod]
        public void AddItemInInventory_ItemNameWithOtherSpecialCharacter()
        {
            var inventorySystem = new InventoryManagementController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var objItem = new ItemBO() { itemName = "Battery$Test", itemDescription = "For Car", price = 1000, IsDeleted = false, AddedOn = DateTime.UtcNow };

            var result = inventorySystem.AddItemInInventory(objItem).Result;
            Assert.AreEqual(result.IsSuccessStatusCode, false);
        }

        //ItemId which is present in records is sent
        [TestMethod]
        public void UpdateItemInInventory()
        {
            var inventorySystem = new InventoryManagementController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var objItem = new ItemBO() { itemId = 3, itemName = "Glass", itemDescription = "House", price = 800, IsDeleted = false, AddedOn = DateTime.UtcNow };
            var result = inventorySystem.UpdateItemInInventory(objItem).Result;
            Assert.AreEqual(result.IsSuccessStatusCode, true);
        }
        //Negative test case to check for updating item which is not present in database records
        [TestMethod]
        public void UpdateItemInInventory_InvalidItemId()
        {
            var inventorySystem = new InventoryManagementController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var objItem = new ItemBO() { itemId = 250, itemName = "Glass", itemDescription = "House", price = 800, IsDeleted = false, AddedOn = DateTime.UtcNow };
            var result = inventorySystem.UpdateItemInInventory(objItem).Result;
            Assert.AreEqual(result.IsSuccessStatusCode, false);
        }
        //Test case to check whether items are present in list.
        [TestMethod]
        public void GetAllItems()
        {
            var inventorySystem = new InventoryManagementController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var result = inventorySystem.GetAllItems().Result.Content.ReadAsStringAsync().Result;
            List<ItemBO> itemsList = JsonConvert.DeserializeObject<List<ItemBO>>(result);
            Assert.AreEqual(itemsList.Count > 0 ? true : false, true);
        }
        //Test case to check whether only item type elements are sent in the list.
        [TestMethod]
        public void GetAllItems_CheckInstanceType()
        {
            var inventorySystem = new InventoryManagementController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var result = inventorySystem.GetAllItems().Result.Content.ReadAsStringAsync().Result;
            List<ItemBO> itemsList = JsonConvert.DeserializeObject<List<ItemBO>>(result);
            if (itemsList.Count > 0)
            {
                bool isValid = true;
                foreach (var item in itemsList)
                {
                    if (!(item is ItemBO))
                        isValid = false;
                }
                Assert.AreEqual(isValid, true);
            }
        }
        //Negative test case if no records are present
        [TestMethod]
        public void GetAllItems_NoRecordsPresent()
        {
            var inventorySystem = new InventoryManagementController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var result = inventorySystem.GetAllItems().Result;
            Assert.AreEqual(result.IsSuccessStatusCode, false);
        }

        [TestMethod]
        public void DeleteItemFromInventory()
        {
            var inventorySystem = new InventoryManagementController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            long itemId = 1;
            var result = inventorySystem.DeleteItemFromInventory(itemId).Result;
            Assert.AreEqual(result.IsSuccessStatusCode, true);
        }
        //Test case when trying to delete item which is not present in database.
        [TestMethod]
        public void DeleteItemFromInventory_InvalidItemId()
        {
            var inventorySystem = new InventoryManagementController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            long itemId = 250;
            var result = inventorySystem.DeleteItemFromInventory(itemId).Result;
            Assert.AreEqual(result.IsSuccessStatusCode, false);
        }
    }
}
