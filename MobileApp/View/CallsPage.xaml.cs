using Presentation.ViewModel;

namespace Presentation.View;

public partial class CallsPage : ContentPage
{
    public CallsPage()
    {
        InitializeComponent();
    }
    public CallsPage(CallsPageViewModel callsPageViewModel) : this()
	{
		BindingContext = callsPageViewModel;

	}
}