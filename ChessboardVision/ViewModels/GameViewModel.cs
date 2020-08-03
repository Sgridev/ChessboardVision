using ChessboardVision.Model;
using ChessboardVision.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChessboardVision.ViewModels
{
    class GameViewModel : INotifyPropertyChanged
    {
        private readonly bool _timePressure;
        private readonly INavigation Navigation;
        private bool _isRunning;
        private bool _isRemovingLife;
        private int _currentScore;
        private int _currentLifes;
        private bool _isLife1Visible;
        private bool _isLife2Visible;
        private bool _isLife3Visible;
        private string _squareText;
        private string _scoreText;
        private bool _isLightEnabled;
        private bool _isDarkEnabled;
        private bool _isProgressbarVisible;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LightCommand { get; }
        public ICommand DarkCommand { get; }
        public ICommand BackPressCommand { get; }
        public string SquareText { get { return _squareText; } set { _squareText = value; OnPropertyChanged(); } }
        public string ScoreText { get { return _scoreText; } set { _scoreText = value; OnPropertyChanged(); } }
        public bool IsLife1Visible { get { return _isLife1Visible; } set { _isLife1Visible = value; OnPropertyChanged(); } }
        public bool IsLife2Visible { get { return _isLife2Visible; } set { _isLife2Visible = value; OnPropertyChanged(); } }
        public bool IsLife3Visible { get { return _isLife3Visible; } set { _isLife3Visible = value; OnPropertyChanged(); } }
        public bool IsLightEnabled { get { return _isLightEnabled; } set { _isLightEnabled = value; OnPropertyChanged(); } }
        public bool IsDarkEnabled { get { return _isDarkEnabled; } set { _isDarkEnabled = value; OnPropertyChanged(); } }
        public bool IsProgressBarVisible { get { return _isProgressbarVisible; } set { _isProgressbarVisible = value; OnPropertyChanged(); } }
        public bool IsLightPressedCorrect { get; set; }
        public bool IsLightPressedWrong { get; set; }
        public bool IsDarkPressedCorrect { get; set; }
        public bool IsDarkPressedWrong { get; set; }



        public GameViewModel(bool timePressure, INavigation navigation)
        {
            _currentScore = 0;
            _currentLifes = 3;
            ScoreText = "0";
            SquareText = Chessboard.GetRandomSquare();
            IsLife1Visible = true;
            IsLife2Visible = true;
            IsLife3Visible = true;
            IsLightEnabled = true;
            IsDarkEnabled = true;
            _timePressure = timePressure;
            Navigation = navigation;
            Preferences.Set("games_played", Preferences.Get("games_played", 0) + 1);
            LightCommand = new Command(x => CheckAnswer(true));
            DarkCommand = new Command(x => CheckAnswer(false));
            BackPressCommand = new Command(async () => await Navigation.PopToRootAsync());
            if (timePressure)
            {
                IsProgressBarVisible = true;
                Device.StartTimer(TimeSpan.FromSeconds(60), () =>
                {
                    Navigation.PushAsync(new ScorePage(_currentScore, _timePressure));
                    return false;
                });
            }
        }


        private async void CheckAnswer(bool isLightPressed)
        {
            if (_isRunning)
                return;
            else
                _isRunning = true;
            await CheckColorInput(isLightPressed);
            SquareText = Chessboard.GetRandomSquare();
            _isRunning = false;
        }

        private async Task CheckColorInput(bool isLightPressed)
        {
            if (Chessboard.IsColorCorrect(SquareText, isLightPressed))
            {
                ++_currentScore;
                ScoreText = _currentScore.ToString();
                Preferences.Set("correct_answers", Preferences.Get("correct_answers", 0) + 1);
                if (isLightPressed)
                    OnPropertyChanged("IsLightPressedCorrect");
                else
                    OnPropertyChanged("IsDarkPressedCorrect");

            }
            else
            {
                await RemoveLife();
                Preferences.Set("wrong_answers", Preferences.Get("wrong_answers", 0) + 1);
                if (isLightPressed)
                    OnPropertyChanged("IsLightPressedWrong");
                else
                    OnPropertyChanged("IsDarkPressedWrong");

            }

        }
        private async Task RemoveLife()
        {
            if (_isRemovingLife)
                return;
            else
                _isRemovingLife = true;
            _currentLifes--;
            if (_currentLifes == 2)
                IsLife3Visible = false;

            else if (_currentLifes == 1)
                IsLife2Visible = false;

            else
            {
                IsLife1Visible = false;
                IsLightEnabled = false;
                IsDarkEnabled = false;
                await Navigation.PushAsync(new ScorePage(_currentScore, _timePressure));
            }
            _isRemovingLife = false;

        }

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
