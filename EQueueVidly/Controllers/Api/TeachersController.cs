using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using EQueueVidly.Models;
using EQueueVidly.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EQueueVidly.Controllers.Api
{
    public class TeachersController : ApiController
    {
        private ApplicationDbContext _context;

        public TeachersController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetTeachers()
        {
            var workQuery = GetApplicationUsersInRole("Teacher").ToList();

            var customerDtos = workQuery
                .ToList()
                .Select(Mapper.Map<ApplicationUser, TeacherViewModel>);
            return Ok(customerDtos);
        }
        [Route("api/resources")]
        public IHttpActionResult GetResources()
        {
            var resourceData = new List<ScheduleResource>();


            var teachers = GetApplicationUsersInRole("Teacher").ToList();


            foreach (var teacher in teachers)
            {
                var teacherRes = new ScheduleResource();
                teacherRes.id = teacher.Id;
                teacherRes.title = teacher.FirstName+" "+teacher.LastName;
                resourceData.Add(teacherRes);
            }


            return Json(resourceData);
        }

        public IEnumerable<ApplicationUser> GetApplicationUsersInRole(string roleName)
        {
            return from role in _context.Roles
                where role.Name == roleName
                from userRoles in role.Users
                join user in _context.Users
                on userRoles.UserId equals user.Id
                select user;
        }

       /* public IEnumerable<ApplicationUser> GetUserQueue(string userId)
        {
            
                var works = _context.Appointments;
                var queueItems = _context.Attendees.Where(q => q.UserId == userId);
                var users = _context.Users;

            var result = from q in queueItems
                         join w in works on q.WorkTimeId equals w.Id
                         join u in users on w.User_Id equals u.Id
                            select new
                            {
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                /*EndTime = q.EndTime,
                                StartTime = q.StartTime,
                                WorkDate = w.AppointmentDate#1#
                            };
            return null;

        }*/
    }
}
