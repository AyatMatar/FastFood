using MVC_AYAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_AYAT.Controllers
{
    public class ItemSizePricController : Controller
    {
        PizzaHatEntities pizzaHatdata = new PizzaHatEntities();
        // GET: ItemSizePric
        public ActionResult Index()
        {
            List<ItemSizePric> ItemSizePrics = pizzaHatdata.ItemSizePrics.ToList();
            return View(ItemSizePrics);
        }
        [HttpGet]
        public ActionResult Create()
        {

            ItemSizeModel Model = new ItemSizeModel();
            Model.Sizes = pizzaHatdata.Sizes.ToList();
            Model.Items = pizzaHatdata.Items.ToList();
            return View(Model);
        }
        [HttpPost]
        public ActionResult Create(ItemSizeModel model)
        {
            if (ModelState.IsValid)
            {
                ItemSizePric ItemSize = new ItemSizePric();
                ItemSize.ItemID = model.ItemID;
                ItemSize.SizeID = model.SizeID;
                ItemSize.pric = model.pric;
                pizzaHatdata.ItemSizePrics.Add(ItemSize);
                pizzaHatdata.SaveChanges();
                return RedirectToAction("Index");
            }
                 model.Items = pizzaHatdata.Items.ToList();
                 model.Sizes = pizzaHatdata.Sizes.ToList();
                return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ItemSizePric itemSize = pizzaHatdata.ItemSizePrics.Where(x => x.ItemSizePricID == id).FirstOrDefault();
            ItemSizeModel itemSizeModel = new ItemSizeModel();
            itemSizeModel.ItemSizePricID = itemSize.ItemSizePricID;
            itemSizeModel.ItemID = itemSize.ItemID;
            itemSizeModel.SizeID = itemSize.SizeID;
            itemSizeModel.pric = itemSize.pric;

            itemSizeModel.Items = pizzaHatdata.Items.ToList();
            itemSizeModel.Sizes = pizzaHatdata.Sizes.ToList();
            return View(itemSizeModel);
        }

        [HttpPost]
        public ActionResult Edit(ItemSizeModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
              
                ItemSizePric RecouardEdit = pizzaHatdata.ItemSizePrics.Where(x => x.ItemSizePricID == model.ItemSizePricID).FirstOrDefault();
                
                RecouardEdit.pric = model.pric;
                
                pizzaHatdata.SaveChanges();
                return RedirectToAction("Index");
            }

            model.Items = pizzaHatdata.Items.ToList();
            model.Sizes = pizzaHatdata.Sizes.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ItemSizePric RecouardDelete = pizzaHatdata.ItemSizePrics.Where(x => x.ItemSizePricID == id).FirstOrDefault();
            pizzaHatdata.ItemSizePrics.Remove(RecouardDelete);
            pizzaHatdata.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}