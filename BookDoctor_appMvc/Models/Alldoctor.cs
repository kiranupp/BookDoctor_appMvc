﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BookDoctor_appMvc.Models
{
    public class Alldoctor
    {

        public int Id { get; set; }



        [DisplayName("Name")]
        public string Doctorname { get; set; }

        public string Speciality { get; set; }

        public string Location { get; set; }
    }
}