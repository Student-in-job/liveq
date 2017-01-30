using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQueueVidly.Domain;
using EQueueVidly.Models;
using Glimpse.AspNet.Tab;
using PagedList;

namespace EQueueVidly.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new ApplicationDbContext());
        public ActionResult Index(string searchString, int page = 1)
        {
            int pageSize = 6;
            var teachers = unitOfWork.Teachers.GetAllTeachersWithAccount();

            if (!String.IsNullOrEmpty(searchString))
            {
                teachers = unitOfWork.Teachers.Find(p => 
                    (
                    p.account.FirstName.ToLower().Contains(searchString) || 
                    p.account.LastName.ToLower().Contains(searchString)
                    )).OrderBy(t=>t.AccountId);
            }


            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("TeacherList", teachers.ToPagedList(page, pageSize))
                : View(teachers.ToPagedList(page, pageSize));
        }

    }
}