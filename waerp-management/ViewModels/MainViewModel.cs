namespace waerp_management.ViewModels
{
    internal class MainViewModel
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel()
        {
            CurrentViewModel = new HomeViewModel();
        }
    }
}
