using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VhakoneWeb.Models;
using Microsoft.AspNet.Identity;

namespace VhakoneWeb.Controllers
{
    [Authorize]
    public class PersonalDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PersonalDetails
        public async Task<ActionResult> Index()
        {
            return View(await db.PersonalDetails.ToListAsync());
        }

        // GET: PersonalDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalDetail personalDetail = await db.PersonalDetails.FindAsync(id);
            if (personalDetail == null)
            {
                return HttpNotFound();
            }
            return View(personalDetail);
        }

        // GET: PersonalDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonalDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Title,DateOfBirth,IdNumber,Gender,Disable")] PersonalDetail personalDetail)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                var obj = new PersonalDetail()
                {
                    FirstName = personalDetail.FirstName,
                    LastName = personalDetail.LastName,
                    Title = personalDetail.Title,
                    DateOfBirth = personalDetail.DateOfBirth,
                    IdNumber = personalDetail.IdNumber,
                    Gender = personalDetail.Gender,
                    Disable = personalDetail.Disable,
                    UserId = userId
                };
                db.PersonalDetails.Add(obj);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

               
            }

            return View(personalDetail);
        }

        // GET: PersonalDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalDetail personalDetail = await db.PersonalDetails.FindAsync(id);
            if (personalDetail == null)
            {
                return HttpNotFound();
            }
            return View(personalDetail);
        }

        // POST: PersonalDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: PersonalDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalDetail personalDetail = await db.PersonalDetails.FindAsync(id);
            if (personalDetail == null)
            {
                return HttpNotFound();
            }
            return View(personalDetail);
        }

        // POST: PersonalDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PersonalDetail personalDetail = await db.PersonalDetails.FindAsync(id);
            db.PersonalDetails.Remove(personalDetail);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
