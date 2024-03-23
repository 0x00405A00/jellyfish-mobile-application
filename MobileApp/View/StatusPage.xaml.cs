using Presentation.ViewModel;

namespace Presentation.View;

public partial class StatusPage : ContentPage
{
    public StatusPage()
    {

        InitializeComponent();
    }
    public StatusPage(StatusPageViewModel statusPageViewModel):this()
	{
		BindingContext = statusPageViewModel;
	}
}