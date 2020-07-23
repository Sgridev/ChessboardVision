using ChessboardVision.ViewModels;
using Xamarin.Forms;

namespace ChessboardVision.Views
{

    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel();

        }
    }
}