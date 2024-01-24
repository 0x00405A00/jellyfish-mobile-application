using Presentation.ViewModel;

namespace Presentation.View;

public partial class StatusPage : ContentPage
{
	public StatusPage(StatusPageViewModel statusPageViewModel)
	{
		InitializeComponent();
		BindingContext = statusPageViewModel;
	}
}