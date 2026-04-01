using MVC_AYAT.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_AYAT.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        PizzaHatEntities pizzaHatdata = new PizzaHatEntities();
        public ActionResult Index()
        {
            List<Category> Categories = pizzaHatdata.Categories.ToList();
           
            return View(Categories);
        }
        [HttpGet]
        public ActionResult Create()
        {

            CategoryModel categoryModel = new CategoryModel();
           
            return View(categoryModel);
        }

        [HttpPost]
        public ActionResult Create(CategoryModel model)
        {

            if (ModelState.IsValid)
            {
               
                Category category = new Category();
                category.Name = model.Name;
                pizzaHatdata.Categories.Add(category);
                pizzaHatdata.SaveChanges();
                return RedirectToAction("Index");
            }
         
            return View(model);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category category = pizzaHatdata.Categories.Where(x => x.CategoryID == id).FirstOrDefault();
            CategoryModel categoryModel = new CategoryModel();
            categoryModel.CategoryID = category.CategoryID;
            categoryModel.Name = category.Name;
         
            
            return View(categoryModel);
        }

        [HttpPost]
        public ActionResult Edit(CategoryModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                Category RecouardEdit = pizzaHatdata.Categories.Where(x => x.CategoryID == model.CategoryID).FirstOrDefault();

                RecouardEdit.Name = model.Name;

                pizzaHatdata.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Category RecouardDelete = pizzaHatdata.Categories.Where(x => x.CategoryID == id).FirstOrDefault();
            pizzaHatdata.Categories.Remove(RecouardDelete);
            pizzaHatdata.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}