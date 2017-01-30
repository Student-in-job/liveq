using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EQueueVidly.Models;

namespace EQueueVidly.Dtos
{
    public class AppintmentDto
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime WorkDate { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        /*[DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        public String IteratioType { get; set; }
        public int IterationTypeId { get; set; }*/
        public TimeSpan WorkStart { get; set; }
        public TimeSpan WorkFinish { get; set; }
        /*[DataType(DataType.Date)]
        public DateTime IterationFinish { get; set; }
        public double TimePerClient { get; set; }*/

        public int Subscriptions { get; set; }
        public int Total { get; set; }

    }
}