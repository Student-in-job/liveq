using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQueueVidly.Models
{
    public class Attendee
    {

        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int StudentId { get; set; }
        public DateTime JoinDate { get; set; }
        public Appointment Appointment { get; set; }
        public Student Student { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}