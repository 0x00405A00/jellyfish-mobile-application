using Presentation.ViewModel;

namespace Presentation.View;

public partial class ChatsPage : ContentPage
{
	public ChatsPage(ChatsPageViewModel chatsPageViewModel)
	{
		InitializeComponent();
        BindingContext = chatsPageViewModel;
	}

    /// <summary>
    /// Fixes the CollectionView rerendering
    /// </summary>
    internal void RefreshChats()
    {
        Chats.ScrollTo(0, 0);
    }

}