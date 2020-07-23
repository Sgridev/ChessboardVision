using ChessboardVision.ViewModels;
using Xamarin.Forms;

namespace ChessboardVision.Views
{

    public partial class InfoPage : ContentPage
    {
        public InfoPage()
        {
            InitializeComponent();
            BindingContext = new InfoViewModel();

        }
    }
}