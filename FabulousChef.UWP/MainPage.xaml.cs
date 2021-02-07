using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace FabulousChef.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadApplication(new FabulousChef.App());
        }
    }
}
