using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EQueueVidly.Models
{
    public class Teacher
    {
        public Teacher()
        {
            this.Appointments = new HashSet<Appointment>();
        }

        [Key]
        public int Id { get; set; }

        public string AccountId { get; set; }

        public ApplicationUser account { get; set; }

        public HashSet<Appointment> Appointments { get; set; }
    }
}