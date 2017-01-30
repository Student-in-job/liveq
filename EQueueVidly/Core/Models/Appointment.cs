using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using DHTMLX.Scheduler;

namespace EQueueVidly.Models
{
    public class Appointment
    {
        public Appointment()
        {
            this.Attendees = new HashSet<Attendee>();
        }
    
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public Nullable<DateTime> CreationDate { get; set; }
        public int? IterationTypeId { get; set; }
        public int Durration { get; set; }
        public DateTime EndDate { get; set; }
        public int TimeLimit { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public int Capacity {get; set;}
        public int StatusId {get; set;}
        public IrerationType IterationType { get; set; }
        public ICollection<Attendee> Attendees { get; set; }
        public Teacher Teacher { get; set; }



    }
}