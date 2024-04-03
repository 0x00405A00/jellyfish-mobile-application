﻿using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ControlExtension;
using Presentation.Model;
using Presentation.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModel
{

    public class StatusPageViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;

        private readonly IServiceProvider _serviceProvider;
        private bool _selectedView = false;
        public bool SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }


        public StatusPageViewModel(IServiceProvider serviceProvider, NavigationService navigationService)
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;


        }


    }
}
