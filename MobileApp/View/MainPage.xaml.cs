using Presentation.Controls;
using Presentation.Service;
using Presentation.ViewModel;

namespace Presentation
{
    public partial class MainPage : CustomContentPage
    {


        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            this.BindingContext = mainPageViewModel;
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {

        }

    }
}