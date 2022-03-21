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
using System.IO;
using Microsoft.AspNetCore.Hosting.Internal;

namespace Vroom.Controllers
{
    [Authorize(Roles = "Admin,Executive")]
    public class BikeController : Controller
    {
        private readonly VroomDBContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        [BindProperty]
        public BikeViewModel BikeVM { get; set; }

        public BikeController(VroomDBContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            BikeVM = new BikeViewModel()
            {
                Makes = _context.Makes.ToList(),
                Models = _context.Models.ToList(),
                Bike = new Models.Bike()
            };
        }
        public IActionResult Index()
        {
            var bike = _context.Bikes.Include(m => m.Make).Include(m=>m.Model);
            return View(bike);
        }
        //HTTP GET
        public IActionResult Create()
        {
            return View(BikeVM);
        }
        //HTTP POST
        [HttpPost, ActionName("Create")]
        public IActionResult CreatePost()
        {
            if (ModelState.IsValid)
            {
                return View(BikeVM);
            }
            _context.Add(BikeVM.Bike);
           // _context.SaveChanges();


            ///////////
            ///Save Bike Logic 
            /////////
            
            //Get Bike Id where we saved in Database
            var BikeId=BikeVM.Bike.Id;

            //Get wwwrootpath to save the file on server
            string wwwrootpath = _hostingEnvironment.WebRootPath;

            //Get the uploaded files
            var files=HttpContext.Request.Form.Files;

            //Get the reference of DBSet for the bike we just have saves in database
            var savedbike = _context.Bikes.Find(BikeId);

            //upload the files on the server and save the image path of user who have uploaded any file
            if(files.Count!=0)
            {
                var ImagePath = @"Images\bikes\";
                var Extension=Path.GetExtension(files[0].FileName);
                var RelativeImagePath = ImagePath + BikeId + Extension;
                var AbsImgPath=Path.Combine(wwwrootpath, RelativeImagePath);

                //Upload the file on server
                using(FileStream fs=new FileStream(AbsImgPath,FileMode.Create))
                {
                    files[0].CopyTo(fs);
                }
                //Set the image path on database
                savedbike.ImagePath = RelativeImagePath;
                _context.SaveChanges();
            }   



            return RedirectToAction(nameof(Index));

        }

        //public IActionResult Edit(int id)
        //{
        //    BikeVM.Bike = _context.Bikes.Include(m => m.Make).SingleOrDefault(m => m.Id == id);
        //    if (BikeVM.Bike == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(BikeVM);
        //}
        //[HttpPost, ActionName("Edit")]
        //public IActionResult EditPost()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return View(BikeVM);
        //    }
        //    _context.Update(BikeVM.Bike);
        //    _context.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}
        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    var bike = _context.Bikes.Find(id);
        //    if (bike == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Remove(bike);
        //    _context.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
