﻿using CommunityToolkit.Mvvm.Messaging;
using MobileApp.Controls;
using MobileApp.Handler.AppConfig;
using MobileApp.Handler.Data;
using MobileApp.Handler.Device.Media.Contact;
using MobileApp.Handler.Device.Vibrate;
using MobileApp.Model;
using MobileApp.Service;
using MobileApp.View;
using Shared.Infrastructure.Backend.Api;
using Shared.Infrastructure.Backend.SignalR;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MobileApp.ViewModel
{

    public class MainPageViewModel : BaseViewModel
    {
        private readonly SettingsPageViewModel _settingsPageViewModel;
        private readonly ApplicationConfigHandler _applicationConfigHandler;
        private readonly NavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;
        private readonly DeviceContactHandler _deviceContactHandler;
        private readonly JellyfishSqlliteDatabaseHandler _jellyfishSqlliteDatabaseHandler;
        public ChatsPageViewModel ChatsPageViewModel
        {
            get;
            private set;
        }
        public StatusPageViewModel StatusPageViewModel
        {
            get;
            private set;
        }
        public CallsPageViewModel CallsPageViewModel
        {
            get;
            private set;
        }

        public ICommand LoadViewCommand { get; private set; }
        public ICommand BindingContextChangedCommand { get; private set; }
        public ICommand SwipeLeftCommand { get; private set; }
        public ICommand SwipeRightCommand { get; private set; }
        public ICommand BackButtonCommand { get; private set; }
        public ICommand ExpandSettingsMenuIsExpandedCommand { get; private set; }
        public ICommand ExpandSearchIsExpandedCommand { get; private set; }
        public ICommand CreateNewGroupCommand { get; private set; }
        public ICommand CreateNewChatCommand { get; private set; }
        public ICommand OpenSettingsPageCommand { get; private set; }

        public int CalcHeigtByItems { get; private set; }
        private bool _expandSettingsMenuIsExpanded = false;
        public bool ExpandSettingsMenuIsExpanded
        {
            get { return _expandSettingsMenuIsExpanded; }
            set
            {
                _expandSettingsMenuIsExpanded = value;
                OnPropertyChanged();
            }
        }
        private bool _isSearchExpanded = false;
        public bool IsSearchExpanded
        {
            get { return _isSearchExpanded; }
            set
            {
                _isSearchExpanded = value;
                OnPropertyChanged();
            }
        }
        private bool _blockBackSwitch;
        public bool BlockBackSwitch
        {
            get { return _blockBackSwitch; }
            set
            {
                _blockBackSwitch = value;
                OnPropertyChanged();
            }
        }

        private ViewTemplateModel _selectedViewTemplate;
        public ViewTemplateModel SelectedViewTemplate
        {
            get
            {

                return _selectedViewTemplate;
            }
            set
            {
                if (value == null)
                    return;
                if (_selectedViewTemplate != null)
                    _selectedViewTemplate.IsSelected = false;
                _selectedViewTemplate = value;
                value.IsSelected = true;    

                OnPropertyChanged(nameof(SelectedViewTemplate));
                if(value.ContentViewModelType == typeof(ChatsPageViewModel))
                {
                    this.ChatsPageViewModel.SelectedView = true;
                    this.StatusPageViewModel.SelectedView = false;
                    this.CallsPageViewModel.SelectedView = false;
                }
                else if (value.ContentViewModelType == typeof(StatusPageViewModel))
                {

                    this.StatusPageViewModel.SelectedView = true;
                    this.ChatsPageViewModel.SelectedView = false;
                    this.CallsPageViewModel.SelectedView = false;
                }
                else if (value.ContentViewModelType == typeof(CallsPageViewModel))
                {

                    this.CallsPageViewModel.SelectedView = true;
                    this.StatusPageViewModel.SelectedView = false;
                    this.ChatsPageViewModel.SelectedView = false;
                }
            }
        }
        public ViewTemplateModel[] ViewTemplates
        {
            get;
            private set;
        }
        public ObservableCollection<MenuItemModel> MenuItems { get;private set; }
        public readonly string UserSelectionNewChatQueue = nameof(MainPageViewModel) + "_UserSelectionNewChat";
        public readonly string UserSelectionNewGroupChatQueue = nameof(MainPageViewModel) + "_UserSelectionNewGroupChat";

        public MainPageViewModel(IServiceProvider serviceProvider,
            SettingsPageViewModel settingsPageViewModel,
            VibrateHandler vibrateHandler,
            JellyfishSqlliteDatabaseHandler jellyfishSqlliteDatabaseHandler,
            NavigationService navigationService,
            ChatsPageViewModel chatsPageViewModel,
            StatusPageViewModel statusPageViewModel,
            CallsPageViewModel callsPageViewModel,
            DeviceContactHandler deviceContactHandler,
            ApplicationConfigHandler applicationConfigHandler) : base()
        {
            _settingsPageViewModel = settingsPageViewModel;
            _jellyfishSqlliteDatabaseHandler = jellyfishSqlliteDatabaseHandler;
            _applicationConfigHandler = applicationConfigHandler;
            _deviceContactHandler = deviceContactHandler;
            ChatsPageViewModel = chatsPageViewModel;
            StatusPageViewModel = statusPageViewModel;
            CallsPageViewModel = callsPageViewModel;
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
            BindingContextChangedCommand = new RelayCommand<object>(BindingContextChangedAction);
            SwipeLeftCommand = new RelayCommand<object>(SwipeLeftAction);
            SwipeRightCommand = new RelayCommand<object>(SwipeRightAction);

            ExpandSettingsMenuIsExpandedCommand = new RelayCommand(ExpandSettingsMenuIsExpandedCommandAction);
            ExpandSearchIsExpandedCommand = new RelayCommand(ExpandSearchIsExpandedCommandAction);
            LoadViewCommand = new RelayCommand(LoadViewCommandAction);
            BackButtonCommand = new RelayCommand(BackButtonAction);
            CreateNewGroupCommand = new RelayCommand(CreateNewGroupAction);
            OpenSettingsPageCommand = new RelayCommand(OpenSettingsPageAction);
            CreateNewChatCommand = new RelayCommand(CreateNewChatAction);

            MenuItems = new ObservableCollection<MenuItemModel>()
            {
                new MenuItemModel { Title = "New Chat", ExecCommand = CreateNewChatCommand },
                new MenuItemModel { Title = "New Group", ExecCommand = CreateNewGroupCommand },
                new MenuItemModel { Title = "Settings",ExecCommand = OpenSettingsPageCommand } };
            CalcHeigtByItems = MenuItems.Count * 45;
            InitViewModel();
        }
        public async void LoadViewCommandAction()
        {

        }

        public override void SignalrReconnectedAction()
        {
            base.SignalrReconnectedAction();
            NotificationHandler.ToastNotify("Reconnect to MobileApp Servers...");
        }

        public override async void InitViewModel()
        {
            base.InitViewModel();

            WeakReferenceMessenger.Default.Register<MessageBus.MessageModel>(this, async(r, m) =>
            {
                if (m.Message != null)
                {
                    if (m.Message == UserSelectionNewChatQueue)
                    {
                        if (m.Args != null && m.Args.Length == 1)
                        {
                            UserSelectionPageViewModel response = (UserSelectionPageViewModel)m.Args[0];
                            if (response != null)
                            {
                                //response.SelectedUserFriend
                                //mit dem ausgewählten User einen neuen Chat starten

                                try
                                {

                                    var chat = new Chat() 
                                    { 
                                        Name = response.SelectedUserFriend.NickName, 
                                        ChatMembers = new List<User> { response.SelectedUserFriend },
                                        Messages = new ObservableCollection<MessageGroup>() 
                                    };
                                    





                                    var vmFromDi = _serviceProvider.GetService<ChatPageViewModel>();
                                    ChatPage chatPage = new ChatPage(vmFromDi);
                                    var vm = chatPage.BindingContext as ChatPageViewModel;
                                    vm.SetChat(chat);
                                    await _navigationService.PushAsync(chatPage);
                                    vm.FocusLastMessage = false;
                                    vm.FocusLastMessage = true;
                                }
                                catch (Exception ex)
                                {
                                    NotificationHandler.DisplayAlert("exc", ex.Message + ":" + ex.Source, null, cancel: "cancel");
                                }
                            }
                        }
                    }
                    else if (m.Message == UserSelectionNewGroupChatQueue)
                    {
                        if (m.Args != null && m.Args.Length == 1)
                        {
                            UserSelectionPageViewModel response = (UserSelectionPageViewModel)m.Args[0];
                            if (response != null)
                            {
                                //response.SelectedUserFriend
                                //mit dem ausgewählten User einen neuen Chat starten
                            }
                        }
                    }
                }
            });
        }

        private async void CreateNewChatAction()
        {
            await _deviceContactHandler.OpenUserSelectionHandler(false, UserSelectionNewChatQueue);
            ExpandSettingsMenuIsExpandedCommand.Execute(null); 
            
        }
        private async void CreateNewGroupAction()
        {

            await _deviceContactHandler.OpenUserSelectionHandler(true, UserSelectionNewGroupChatQueue);
            ExpandSettingsMenuIsExpandedCommand.Execute(null);
        }
        private async void OpenSettingsPageAction()
        {
            SettingsPage settingsPage = new SettingsPage(_settingsPageViewModel);
            await _navigationService.PushAsync(settingsPage);
            ExpandSettingsMenuIsExpandedCommand.Execute(null);
        }
        private void ExpandSearchIsExpandedCommandAction()
        {

            IsSearchExpanded = !IsSearchExpanded;
            BlockBackSwitch = IsSearchExpanded;
            ExpandSettingsMenuIsExpanded = false;
        }
        private void ExpandSettingsMenuIsExpandedCommandAction()
        {

            ExpandSettingsMenuIsExpanded = !ExpandSettingsMenuIsExpanded;
            BlockBackSwitch = ExpandSettingsMenuIsExpanded;
            IsSearchExpanded = false;
        }
        public async void BackButtonAction()
        {
            if (BlockBackSwitch && !(ExpandSettingsMenuIsExpanded || IsSearchExpanded))
            {
                BlockBackSwitch = false;
            }
            else if (BlockBackSwitch && (ExpandSettingsMenuIsExpanded || IsSearchExpanded))
            {
                ExpandSettingsMenuIsExpanded = false;
                IsSearchExpanded = false;
            }
            else//Modal with question if user want to logout
            {
                BlockBackSwitch = true;
                bool response = await NotificationHandler.DisplayAlert("Logout?", "Do you want to logout?","Yes","No");
                if(response)
                {
                    BlockBackSwitch = false;
                    _navigationService.CloseCurrentPage();
                }
                else
                {
                    BlockBackSwitch = false;
                }
            }
        }
        private int CalculateIndex(ViewTemplateModel selectedObjectOfList, ViewTemplateModel[] items)
        {
            int index = 0;

            foreach (var item in items)
            {
                if (item == null)
                    continue;
                if(selectedObjectOfList != null && item.Title == selectedObjectOfList.Title) 
                {
                    return index;
                }
                index++;
            }
            return 0;
        }
        public void BindingContextChangedAction(object collection)
        {
            if (collection == null)
                return;
            ViewTemplateModel[] items = (ViewTemplateModel[])collection;
            SelectedViewTemplate = items.FirstOrDefault();  
            if(ViewTemplates == null)
            {
                ViewTemplates = items;
            }
        }
        public void SwipeLeftAction(object collection)
        {
            if (collection == null)
                return;
            ViewTemplateModel[] items = (ViewTemplateModel[])collection;
            int calcSelIndex = CalculateIndex(SelectedViewTemplate, items);
            if (calcSelIndex < items.Length - 1)
            {
                calcSelIndex++;
            }
            SelectedViewTemplate = items[calcSelIndex];
        }
        public void SwipeRightAction(object collection)
        {
            if (collection == null)
                return;
            ViewTemplateModel[] items = (ViewTemplateModel[])collection;
            int calcSelIndex = CalculateIndex(SelectedViewTemplate, items);
            if (calcSelIndex > 0)
            {
                calcSelIndex--;
            }
            SelectedViewTemplate = items[calcSelIndex];
        }

    }
}
