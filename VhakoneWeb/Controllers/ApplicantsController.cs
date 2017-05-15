using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VhakoneWeb.Models;

namespace VhakoneWeb.Controllers
{
    [Authorize]
    public class ApplicantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            PersonalDetail personalDetails = db.PersonalDetails.FirstOrDefault(x => x.UserId == userId);

            if (personalDetails == null)
            {
                ViewBag.canCreate = true;
            }
            else
            {
                ViewBag.canCreate = false;
            }
            return View();
        }
        // GET: Applicants
        public JsonResult GetApplicant()
        { 
            var  userId = User.Identity.GetUserId();
            // PersonalDetail personalDetails = db.PersonalDetails.FirstOrDefault(x => x.UserId == userId);
            List<PersonalDetail> personalDetails = db.PersonalDetails.ToList();
            var query = from a in personalDetails
                        where a.UserId == userId
                        select a;
            return Json(query.ToList(), JsonRequestBehavior.AllowGet);
        
        }

        public string CreatePersonalDetail(PersonalDetail personalDetail)
        {
            if (personalDetail != null)
            {
                var userId = User.Identity.GetUserId();
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
                db.SaveChanges();
                    return "Personal details Added Successfully";
                
            }
            else
            {
                return "Personal details Not Inserted! Try Again";
            }
        }

        public string EditPersonalDetail(PersonalDetail personalDetail)
        {
            if (personalDetail != null)
            {
                var userId = User.Identity.GetUserId();
                var personalDetail_ = db.Entry(personalDetail);
                PersonalDetail personalDetails = db.PersonalDetails.FirstOrDefault(x => x.UserId == userId);

                personalDetails.FirstName = personalDetail.FirstName;
                personalDetails.LastName = personalDetail.LastName;
                personalDetails.Title = personalDetail.Title;
                personalDetails.DateOfBirth = personalDetail.DateOfBirth;
                personalDetails.IdNumber = personalDetail.IdNumber;
                personalDetails.Gender = personalDetail.Gender;
                personalDetails.Disable = personalDetail.Disable;
                personalDetails.UserId = userId;
                
                db.SaveChanges();
                return "PersonalDetail Updated Successfully";
            }
            else
            {
                return "PersonalDetail Not Updated! Try Again";
            }
        }
    }
}