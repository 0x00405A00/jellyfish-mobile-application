using Presentation.Controls;
using Presentation.Services;
using Presentation.ViewModel;

namespace Presentation.View;

public partial class UserSelectionPage : CustomContentPage
{
	public UserSelectionPage(UserSelectionPageViewModel contactsPageViewModel) 
    {
		InitializeComponent();
		this.BindingContext = contactsPageViewModel;	
	}
}