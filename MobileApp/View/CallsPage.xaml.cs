using Presentation.ViewModel;

namespace Presentation.View;

public partial class CallsPage : ContentPage
{
	public CallsPage(CallsPageViewModel callsPageViewModel)
	{
		InitializeComponent();
		BindingContext = callsPageViewModel;

	}
}