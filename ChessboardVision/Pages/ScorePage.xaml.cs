using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChessboardVision
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScorePage : ContentPage
    {
        private readonly bool _timePressure;
        private readonly int _savedBestScore;
        private bool _isRunning = false;

        public ScorePage(int gameScore, bool timePressure)
        {
            InitializeComponent();
            this._timePressure = timePressure;
            _savedBestScore = timePressure ? Preferences.Get("best_score_time_pressure", 0) : Preferences.Get("best_score_standard", 0);
            currentScore.Text = gameScore.ToString();
            if(gameScore > _savedBestScore)
            {
                _savedBestScore = gameScore;
                Preferences.Set(timePressure ? "best_score_time_pressure" : "best_score_standard", _savedBestScore);
                newBest.IsVisible = true;
            }
            bestScore.Text = _savedBestScore.ToString();
            currentScore.Text = gameScore.ToString();
         
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void PlayAgain_OnClicked(object sender, EventArgs e)
        {
            if (_isRunning)
                return;
            else
                _isRunning = true;
            await Navigation.PushAsync(new GamePage(_timePressure));
            _isRunning = false;
        }
        private async void Menu_OnClicked(object sender, EventArgs e)
        {
            if (_isRunning)
                return;
            else
                _isRunning = true;
            await Navigation.PopToRootAsync();
            _isRunning = false;
        }
    }
}