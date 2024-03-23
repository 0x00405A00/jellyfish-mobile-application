using Presentation.View;
using Presentation.ViewModel;

namespace Presentation.ApplicationSpecific
{
    public class PageInstanceFactory
    {
        private readonly ChatsPageViewModel _chatsPageViewModel;
        private readonly StatusPageViewModel _statusPageViewModel;
        private readonly CallsPageViewModel _callsPageViewModel;
        private readonly MainPageViewModel _mainPageViewModel;
        private readonly LoginPageViewModel _loginPageViewModel;
        private readonly ResetPasswordContentPageViewModel _resetPasswordContentPageViewModel;
        private readonly ChatPageViewModel _chatPageViewModel;
        private readonly UserSelectionPageViewModel _userSelectionPageViewModel;
        private readonly ProfilePageViewModel _profilePageViewModel;
        private readonly CameraHandlerPageViewModel _cameraHandlerPageViewModel;
        private readonly RegisterContentPageViewModel _registerContentPageViewModel;
        private readonly SettingsPageViewModel _settingsPageViewModel;

        public PageInstanceFactory(
            ChatsPageViewModel chatsPageViewModel,
            StatusPageViewModel statusPageViewModel,
            CallsPageViewModel callsPageViewModel,
            MainPageViewModel mainPageViewModel,
            LoginPageViewModel loginPageViewModel,
            ResetPasswordContentPageViewModel resetPasswordContentPageViewModel,
            ChatPageViewModel chatPageViewModel,
            UserSelectionPageViewModel userSelectionPageViewModel,
            ProfilePageViewModel profilePageViewModel,
            CameraHandlerPageViewModel cameraHandlerPageViewModel,
            RegisterContentPageViewModel registerContentPageViewModel,
            SettingsPageViewModel settingsPageViewModel)
        {
            _chatsPageViewModel = chatsPageViewModel;
            _statusPageViewModel = statusPageViewModel;
            _callsPageViewModel = callsPageViewModel;
            _mainPageViewModel = mainPageViewModel;
            _loginPageViewModel = loginPageViewModel;
            _resetPasswordContentPageViewModel = resetPasswordContentPageViewModel;
            _chatPageViewModel = chatPageViewModel;
            _userSelectionPageViewModel = userSelectionPageViewModel;
            _profilePageViewModel = profilePageViewModel;
            _cameraHandlerPageViewModel = cameraHandlerPageViewModel;
            _registerContentPageViewModel = registerContentPageViewModel;
            _settingsPageViewModel = settingsPageViewModel;
        }

    }

}
