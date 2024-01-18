﻿using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.Model
{
    public class MenuItemModel : BaseViewModel
    {
        public string Title { get; set; }
        public ICommand ExecCommand { get; set; }
    }
}
