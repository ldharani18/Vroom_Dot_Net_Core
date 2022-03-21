using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vroom.AppDBContext;
using Vroom.Models;
using Vroom.Models.ViewModel;

namespace Vroom.Controllers
{
    [Authorize(Roles = "Admin,Executive")]
    public class ModelController : Controller
    {
        private readonly VroomDBContext _context;

        [BindProperty]
        public ModelViewModel ModelVM { get; set; }

        public ModelController(VroomDBContext context)
        {
            _context = context;
            ModelVM = new ModelViewModel()
            {
                Makes = _context.Makes.ToList(),
                Model = new Models.Model()
            };
        }
        public IActionResult Index()
        {
            var model = _context.Models.Include(m => m.Make);
            return View(model);
        }
        //HTTP GET
        public IActionResult Create()
        {
            return View(ModelVM);
        }
        [HttpPost, ActionName("Create")]
        public IActionResult CreatePost()
        {
            if (ModelState.IsValid)
            {
                return View(ModelVM);
            }
            _context.Add(ModelVM.Model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        
        public IActionResult Edit(int id)
        {
            ModelVM.Model = _context.Models.Include(m => m.Make).SingleOrDefault(m => m.Id == id);
            if (ModelVM.Model == null)
            {
                return NotFound();
            }
            return View(ModelVM);
        }
        [HttpPost,ActionName("Edit")]
        public IActionResult EditPost()
        {
            if (ModelState.IsValid)
            {
                return View(ModelVM);
            }
            _context.Update(ModelVM.Model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var model=_context.Models.Find(id);
            if(model==null)
            {
                return NotFound();
            }
            _context.Remove(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
