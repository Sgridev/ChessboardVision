using ChessboardVision.ViewModels;
using Xamarin.Forms;

namespace ChessboardVision.Views
{


    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(Navigation);
        }

    }
}
