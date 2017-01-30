using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQueueVidly.Domain;
using EQueueVidly.Models;
using EQueueVidly.ViewModels;

namespace EQueueVidly.Controllers
{
    public class StudentController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new ApplicationDbContext());

        //
        // GET: /Student/
        public ActionResult Index(int Id)
        {
            var user = unitOfWork.Teachers.GetTeacherById(Id).account;
            var model = new IndexViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
                Email = user.Email,
            };

            return View();
        }
#region delete
        public bool DeleteEvent(int id)
        {
            try
            {
                Attendee app = unitOfWork.Attendees.Find(a=>a.Id==id).FirstOrDefault();
                unitOfWork.Attendees.Remove(app);
                unitOfWork.Complete();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
#endregion
    }
}