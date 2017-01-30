using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EQueueVidly.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Working date")]
        public DateTime WorkDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Till")]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Time)]
        [Display(Name="Start time")]
        public TimeSpan StartTime { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "End time")]
        public TimeSpan EndTime { get; set; }
        
        [Display(Name="Limit")]
        [DataType(DataType.Duration)]
        public int limit { get; set; }

        [Display(Name = "Iteration Type")]
        public int SelectedIterationTypeId { get; set; }
        public IEnumerable<SelectListItem> IterationTypes { get; set; }

    }

    public class AppointmentDetailsViewModel
    {
        public AppointmentDetailsViewModel()
        {
            Attendees = new List<AttendeeViewModel>();
        }

        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<AttendeeViewModel> Attendees { get; set; }
        public int Capacity { get; set; }
        public int TimeLimit { get; set; }
    }

    public class AttendeeViewModel
    {
        public string FullName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class ScheduleEventVM
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Durration{ get; set; }
        public int TimeLimit { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Title { get; set; }
        public int Capacity {get; set;}
        public int QueueItemsCount { get; set; }
    }

    public class ScheduleEvent
    {
        public string id { get; set; }
        public string title { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string start { get; set; }
        public int duration { get; set; }
        public string end { get; set; }
        public int timeLimit { get; set; }
        public int queueIiems { get; set; }
        public int capacity { get; set; }
        public string resourceId { get; set; }
        public string color { get; set; }
        public string imageurl { get; set; }
    }

   
}