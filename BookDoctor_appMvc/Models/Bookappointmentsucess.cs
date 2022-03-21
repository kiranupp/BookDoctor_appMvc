using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookDoctor_appMvc.Models
{
    public class Bookappointmentsucess
    {
        public int userid { get; set; }
        public int doctorid { get; set; }

        public int timeslotid { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }

    }
}