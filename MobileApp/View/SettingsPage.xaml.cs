using Presentation.Controls;
using Presentation.Service;
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