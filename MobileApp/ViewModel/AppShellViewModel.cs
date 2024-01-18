using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class AppShellViewModel : BaseViewModel
    {
        public ICommand BackButtonCommand { get; private set; }
        public AppShellViewModel()
        {
            BackButtonCommand = new RelayCommand(() => { 
            
            });
        }
    }
}
