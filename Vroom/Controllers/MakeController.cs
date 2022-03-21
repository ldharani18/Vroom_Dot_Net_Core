using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vroom.AppDBContext;
using Vroom.Models;

namespace Vroom.Controllers
{
    [Authorize(Roles ="Admin,Executive")]
    public class MakeController : Controller
    {
        private readonly VroomDBContext _context;
        public MakeController(VroomDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Makes.ToList());
        }
        //Http GET Method
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Make make)
        {
            if(ModelState.IsValid)
            {
                _context.Add(make);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(make);
        }
       
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var make = _context.Makes.FirstOrDefault(m=>m.Id == id);
            if(make == null)
            {
                return NotFound();
            }
            _context.Makes.Remove(make);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
		{
            var make = _context.Makes.Find(id);
            if(make==null)
			{
                return NotFound();
			}
            return View(make);
		}

        [HttpPost]
        public IActionResult Edit(Make make)
        {
            if (ModelState.IsValid)
            {
                _context.Update(make);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(make);
        }


        /*  //BASICS
         [Route("Make")]
         [Route("Make/Bikes")]
         public IActionResult Bikes()
         {
             Make make = new Make() { Id = 1, Name = "Albert" };
             return View(make);
             //ContentResult cr = new ContentResult() { Content = "Hello World" };
             //return cr;
             //return Content("This is a content displaying view");
             //return Redirect("/home");
             //return RedirectToAction("Index","Home");
             //return new EmptyResult();
         }
         [Route("make/ByYearMonth/{year:int:length(4)}/{month:int:range(1,12)}")]
         public IActionResult ByYearMonth(int year,int month)
         {
             return Content("Year: "+year + " ; Month " + month) ;
         }*/
    }
}
