﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookDoctor_appMvc.Models
{
    public class Hospital_location
    {

        public int id { get; set; }
        public string Hospitallocation { get; set; }
        public List<SelectListItem> Hospitallocations { get; set; }
    }
}