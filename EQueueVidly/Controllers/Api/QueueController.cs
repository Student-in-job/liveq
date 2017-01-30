using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using EQueueVidly.Dtos;
using EQueueVidly.Models;
using EQueueVidly.ViewModels;
using Microsoft.AspNet.Identity;

namespace EQueueVidly.Controllers
{
    public class QueueController : ApiController
    {
        private ApplicationDbContext _context;

        public QueueController()
        {
            _context = new ApplicationDbContext();
        }
/*
        [Route("api/user/items")]
        public IHttpActionResult GetUserQueue()
        {
            var user_id = User.Identity.GetUserId();
            var works = _context.Appointments;
            var queueItems = _context.Attendees.Where(q => q.UserId == user_id);
            var users = _context.Users;
            IQueryable<QueueItemDto> result = from q in queueItems
                         join w in works on q.WorkTimeId equals w.Id
                         join u in users on w.User_Id equals u.Id
                            select new QueueItemDto()
                            {
                                /*FirstName = u.FirstName,
                                LastName = u.LastName,
                                EndTime = q.EndTime,
                                StartTime = q.StartTime,
                                WorkDate = w.AppointmentDate#1#
                            };
            /*var customerDtos = result
                .ToList()
                .Select(Mapper.Map<QueueItem, TeacherViewModel>);#1#
            return Ok(result.ToList());

        }*/
    }
}
