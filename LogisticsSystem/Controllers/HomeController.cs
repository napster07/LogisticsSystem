using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogisticsSystem.Models;

namespace LogisticsSystem.Controllers
{
    public class HomeController : Controller
    {
        DataAccess db = new DataAccess();
        public ActionResult Index()
        {            
            ViewBag.ItemList = db.GetItemList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Item item)
        {
            ModelState.Remove("ItemID");

            if (!ModelState.IsValid)
            {
                ViewBag.ItemList = db.GetItemList();
                return View(item);
            }
            var calculatedItem = GetCalculatedCost(item);
            if(item.ItemID != 0)
            {
                if (db.EditItem(calculatedItem))
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (db.AddItem(calculatedItem))
                {
                    return RedirectToAction("Index");
                }
            }          
            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(string input)
        {
            var selectedItems = input.Split(',');
            for(int i=0; i < selectedItems.Length; i++)
            {
                db.DeleteItem(selectedItems[i]);
            }

            return RedirectToAction("Index");           
        }

        private Item GetCalculatedCost(Item item)
        {
            if(item.Weight <= 10)
            {
                item.Priority = 5;
                item.Type = "Small";
                item.Cost = item.Weight * 25;
            }
            else if(item.Weight <= 20)
            {
                item.Priority = 4;
                item.Type = "Medium";
                item.Cost = item.Weight * 20;
            }
            else if (item.Weight <= 30)
            {
                item.Priority = 3;
                item.Type = "Large";
                item.Cost = item.Weight * 15;
            }
            else if (item.Weight <= 50)
            {
                item.Priority = 2;
                item.Type = "Heavy";
                item.Cost = item.Weight * 10;
            }
            else
            {
                item.Priority = 1;
                item.Type = "Reject";
                item.Cost = 0;
            }
            return item;
        }
    }
}