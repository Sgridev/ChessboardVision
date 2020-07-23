using ChessboardVision.Views;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChessboardVision.ViewModels
{
    class ScoreViewModel
    {
        private readonly bool _timePressure;
        private readonly int _savedBestScore;
        private bool _isRunning;

        public Command PlayAgainCommand { get; }
        public Command MenuCommand { get; }

        public INavigation Navigation { get; set; }
        public string CurrentScore { get; set; }
        public string BestScore { get; set; }
        public bool IsNewBestScore { get; set; }

        public ScoreViewModel(int gameScore, bool timePressure, INavigation navigation)
        {
            Navigation = navigation;
            PlayAgainCommand = new Command(async () => await NavigateToGamePage());
            MenuCommand = new Command(async () => await NavigateToMenu());
            _timePressure = timePressure;
            _savedBestScore = timePressure ? Preferences.Get("best_score_time_pressure", 0) : Preferences.Get("best_score_standard", 0);
            CurrentScore = gameScore.ToString();
            if (gameScore > _savedBestScore)
            {
                _savedBestScore = gameScore;
                Preferences.Set(timePressure ? "best_score_time_pressure" : "best_score_standard", _savedBestScore);
                IsNewBestScore = true;
            }
            else
                IsNewBestScore = false;
            CurrentScore = gameScore.ToString();
            BestScore = _savedBestScore.ToString();

        }

        private async Task NavigateToMenu()
        {
            if (_isRunning)
                return;
            else
                _isRunning = true;
            await Navigation.PopToRootAsync();
            _isRunning = false;
        }

        private async Task NavigateToGamePage()
        {
            if (_isRunning)
                return;
            else
                _isRunning = true;
            await Navigation.PushAsync(new GamePage(_timePressure));
            _isRunning = false;
        }


    }
}
