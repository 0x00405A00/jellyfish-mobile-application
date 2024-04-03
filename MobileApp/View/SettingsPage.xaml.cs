using Presentation.Controls;
using Presentation.Services;
using Presentation.ViewModel;

namespace Presentation.View;

public partial class SettingsPage : CustomContentPage
{
	public SettingsPage(SettingsPageViewModel settingsPageViewModel)
    {
		InitializeComponent();
		this.BindingContext = settingsPageViewModel;	
	}
}