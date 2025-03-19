using System.Windows;

namespace waerp_management.mainGUI
{
    /// <summary>
    /// Interaction logic for LoadingScreenWindow.xaml
    /// </summary>
    public partial class LoadingScreenWindow : Window
    {
        public LoadingScreenWindow()
        {
            InitializeComponent();
            string LoadingAction = LoadingScreenModel.LoadingType.ToString();


        }
    }
}
