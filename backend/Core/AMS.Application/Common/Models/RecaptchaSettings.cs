﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Common.Models
{
    public class RecaptchaSettings
    {
        public string SecretKey { get; set; }
        public string URL { get; set; }
    }
}
