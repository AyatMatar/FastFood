using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.Mvc;
using MVC_AYAT.Models;
using System.Web;

namespace MVC_AYAT.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        PizzaHatEntities pizzaHatdata = new PizzaHatEntities();
        public ActionResult Index()
        {

            List<Item> items = pizzaHatdata.Items.ToList();
            return View(items);
        }
        [HttpGet]
        public ActionResult Create()
        {

            ItemModel ItemModel = new ItemModel();
            ItemModel.Categories = pizzaHatdata.Categories.ToList();
            return View(ItemModel);
        }

        [HttpPost]
        public ActionResult Create(ItemModel model, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                string fillname = "";
                if (file != null)
                {
                    fillname = file.FileName;
                    string fillnamefullpath = Path.Combine(Server.MapPath("~/content/img"), fillname);
                    file.SaveAs(fillnamefullpath);

                }
                Item item = new Item();
                item.Name = model.Name;
                item.Description = model.Description;
                item.ImageName = fillname;
                item.CategoryID = model.CategoryID;
                pizzaHatdata.Items.Add(item);
                pizzaHatdata.SaveChanges();
                return RedirectToAction("Index");
            }
            model.Categories = pizzaHatdata.Categories.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Item ItemFrom = pizzaHatdata.Items.Where(x => x.ItemID == id).FirstOrDefault();
            ItemModel ItemModel = new ItemModel();
            ItemModel.ItemID = ItemFrom.ItemID;
            ItemModel.Name = ItemFrom.Name;
            ItemModel.Description = ItemFrom.Description;
            ItemModel.ImageName = ItemFrom.ImageName;
            ItemModel.CategoryID = ItemFrom.CategoryID;
            ItemModel.Categories = pizzaHatdata.Categories.ToList();
            return View(ItemModel);
        }
        [HttpPost]
        public ActionResult Edit(ItemModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string fillname = "";
                if (file != null)
                {
                    fillname = file.FileName;
                    string fillnamefullpath = Path.Combine(Server.MapPath("~/content/img"), fillname);
                    file.SaveAs(fillnamefullpath);
                }
                Item RecouardEdit = pizzaHatdata.Items.Where(x => x.ItemID == model.ItemID).FirstOrDefault();
               RecouardEdit.Name = model.Name;
               RecouardEdit.Description = model.Description;
               RecouardEdit.ImageName = fillname;
                RecouardEdit.CategoryID = model.CategoryID;
                pizzaHatdata.SaveChanges();
                return RedirectToAction("Index", "Item");
            }
            
            model.Categories = pizzaHatdata.Categories.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Item Delete = pizzaHatdata.Items.Where(x => x.ItemID == id).FirstOrDefault();
           
            try
            {
                pizzaHatdata.Items.Remove(Delete);
                pizzaHatdata.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
            return RedirectToAction("Index", "Item");
        }
    }
}

