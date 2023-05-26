﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AVN.Common.Enums
{
    public enum Citizenship
    {
        [Display(Name = "Неопределённый")] Undefined = 0,
        [Display(Name = "Кыргызстан")] Kyrgyzstan = 1,
        [Display(Name = "Казахстан")] Kazakhstan = 2,
        [Display(Name = "Россия")] Russia = 3,
        [Display(Name = "Узбекистан")] Uzbekistan = 4
    }
}