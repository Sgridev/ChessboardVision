using ChessboardVision.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChessboardVision.ViewModels
{
    class MainViewModel
    {
        private bool _isRunning = false;
        public Command StandardGameCommand { get; }
        public Command TimePressureGameCommand { get; }
        public Command InfoCommand { get; }
        public Command ProfileCommand { get; }
        public INavigation Navigation { get; set; }


        public MainViewModel(INavigation navigation)
        {
            Navigation = navigation;
            StandardGameCommand = new Command(async () => await NavigateToGamePage(false));
            TimePressureGameCommand = new Command(async () => await NavigateToGamePage(true));
            InfoCommand = new Command(async () => await NavigateToInfoPage());
            ProfileCommand = new Command(async () => await NavigateToProfilePage());

        }

        private async Task NavigateToGamePage(bool timePressure)
        {
            if (_isRunning)
                return;
            else
                _isRunning = true;

            await Navigation.PushAsync(new GamePage(timePressure));
            _isRunning = false;
        }

        private async Task NavigateToInfoPage()
        {
            if (_isRunning)
                return;
            else
                _isRunning = true;

            await Navigation.PushAsync(new InfoPage());
            _isRunning = false;
        }

        private async Task NavigateToProfilePage()
        {
            if (_isRunning)
                return;
            else
                _isRunning = true;

            await Navigation.PushAsync(new ProfilePage());
            _isRunning = false;
        }
    }
}
