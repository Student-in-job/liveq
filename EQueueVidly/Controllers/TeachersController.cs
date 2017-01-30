using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EQueueVidly.Domain;
using EQueueVidly.Models;
using EQueueVidly.ViewModels;
using Microsoft.AspNet.Identity;

namespace EQueueVidly.Controllers
{
    public class TeachersController : BaseController
    {

        private UnitOfWork unitOfWork = new UnitOfWork(new ApplicationDbContext());

        //
        // GET: /Teachers/
        public ActionResult Index(int id)
        {
            var user = unitOfWork.Teachers.GetTeacherById(id).account;
            var model = new TeacherScheduleVM()
            {
                TeacherName = user.FirstName + " "+user.LastName,
                TeacherId = id,
                AccountId = user.Id
            };
            return View(model);
        }

       /* public IEnumerable<ApplicationUser> GetTeacherById(string id)
        {
            return from role in _context.Roles
                   where role.Name == "Teacher"
                   from userRoles in role.Users
                   join user in _context.Users
                   on userRoles.UserId equals id
                   select user;
        }*/
        
    }
}