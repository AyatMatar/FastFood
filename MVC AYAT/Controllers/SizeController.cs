using MVC_AYAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_AYAT.Controllers
{
    public class SizeController : Controller
    {
        // GET: Size
        PizzaHatEntities pizzaHatdata = new PizzaHatEntities();
        public ActionResult Index()
        {
            List<Size> sizes = pizzaHatdata.Sizes.ToList();
            return View(sizes);
        }
        [HttpGet]
        public ActionResult Create()
        {

            SizeModel sizeModel = new SizeModel();
         
            return View(sizeModel);
        }


        [HttpPost]
        public ActionResult Create(SizeModel model)
        {

            if (ModelState.IsValid)
            {

                Size size = new Size();
                size.Name = model.Name;
                pizzaHatdata.Sizes.Add(size);
                pizzaHatdata.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            Size size = pizzaHatdata.Sizes.Where(x => x.SizeID == id).FirstOrDefault();
            SizeModel sizeModel = new SizeModel();
            sizeModel.SizeID = size.SizeID;
            sizeModel.Name = size.Name;


            return View(sizeModel);
        }
        [HttpPost]
        public ActionResult Edit(SizeModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                Size RecouardEdit = pizzaHatdata.Sizes.Where(x => x.SizeID == model.SizeID).FirstOrDefault();

                RecouardEdit.Name = model.Name;

                pizzaHatdata.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Size RecouardDelete = pizzaHatdata.Sizes.Where(x => x.SizeID == id).FirstOrDefault();
            pizzaHatdata.Sizes.Remove(RecouardDelete);
            pizzaHatdata.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}