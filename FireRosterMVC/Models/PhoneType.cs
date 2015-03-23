﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireRosterMVC.Models
{
    public class PhoneType
    {
        [Key]
        public int ID { get; set; }
        public string Label { get; set; }

    }
}