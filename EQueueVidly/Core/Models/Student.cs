using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EQueueVidly.Models
{
    public class Student
    {
        public Student()
        {
            this.Attendees = new HashSet<Attendee>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string AccountId { get; set; }
        public ApplicationUser Account { get; set; }
        public HashSet<Attendee> Attendees { get; set; }


    }
}