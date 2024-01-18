﻿using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class LanguageModel : BaseViewModel
    {
        public string PhonePrefix
        {
            get;
            set;
        }
        public string Country
        {
            get;
            set;
        }
    }
}
