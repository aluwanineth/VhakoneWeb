using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VhakoneWeb.Models;

namespace VhakoneWeb.Controllers
{
    public class CandidatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            string userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.Identity.GetUserId();
                /* var personalDetail = from a in db.PersonalDetails
                             where a.UserId == userId 
                             select a;*/
               
                PersonalDetail personalDetail = await db.PersonalDetails.FirstOrDefaultAsync(x => x.UserId == userId);
                if (personalDetail == null)
                {
                    ViewBag.IsCreate = true;
                    return PartialView("_CreatePersonalDetail");
                }
                else
                {
                    ViewBag.IsCreate = false;
                    return View(personalDetail);
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Title,DateOfBirth,IdNumber,Gender,Disable,UserId")] PersonalDetail personalDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(personalDetail);
        }
    }
}