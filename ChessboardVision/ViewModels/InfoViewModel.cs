using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChessboardVision.ViewModels
{
    class InfoViewModel
    {
        public Command DeveloperCommand { get; set; }
        public InfoViewModel()
        {
            DeveloperCommand = new Command(async () => await Browser.OpenAsync("https://andreagrisafi.it/", BrowserLaunchMode.SystemPreferred));
        }
    }
}
