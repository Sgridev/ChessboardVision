using ChessboardVision.BusinessObjects;
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
    public partial class GamePage : ContentPage
    {
        private readonly bool _timePressure;
        private Task _progressBarFinish;
        private int _currentScore = 0;
        private int _currentLifes = 3;
        private bool _isRunning = false;
        private bool _isRemovingLife = false;
        public GamePage()
        {
            InitializeComponent();
            Preferences.Set("games_played", Preferences.Get("games_played", 0) + 1);
        }

        public GamePage(bool timePressure)
        {
            InitializeComponent();
            Preferences.Set("games_played", Preferences.Get("games_played", 0) + 1);
            this._timePressure = timePressure;
            

        }

        protected async override void OnAppearing()
        {

            if (_timePressure)
            {
                progressBar.IsVisible = true;
                _progressBarFinish = WaitProgressBarAsync();

            }

            square.Text = Chessboard.GetRandomSquare();

            if (_timePressure)
            {
                await _progressBarFinish;
                await Navigation.PushAsync(new ScorePage(_currentScore, _timePressure));
            }

        }

        private async Task WaitProgressBarAsync()
        {
            await progressBar.ProgressTo(1, 60000, Easing.Linear);

        }

        private void IsLight_OnClicked(object sender, EventArgs e)
        {
            if (_isRunning)
                return;
            else
                _isRunning = true;
            CheckColorInput(true);
            square.Text = Chessboard.GetRandomSquare();
            _isRunning = false;

        }
        private void IsDark_OnClicked(object sender, EventArgs e)
        {
            if (_isRunning)
                return;
            else
                _isRunning = true;
            CheckColorInput(false);
            square.Text = Chessboard.GetRandomSquare();
            _isRunning = false;

        }

        private async void CheckColorInput(bool isLightPressed)
        {
            if (Chessboard.IsColorCorrect(square.Text, isLightPressed))
            {
                ++_currentScore;
                score.Text = _currentScore.ToString();
                Preferences.Set("correct_answers", Preferences.Get("correct_answers", 0) + 1);
                if (isLightPressed)
                {
                    await btnLight.ChangeBackgroundColorTo(Color.Green, 250, Easing.CubicOut);
                    await btnLight.ChangeBackgroundColorTo(Color.White, 250, Easing.CubicOut);

                }
                else
                {
                    await btnDark.ChangeBackgroundColorTo(Color.Green, 250, Easing.CubicOut);
                    await btnDark.ChangeBackgroundColorTo(Color.Black, 250, Easing.CubicOut);

                }
            }
            else
            {
                RemoveLife();
                Preferences.Set("wrong_answers", Preferences.Get("wrong_answers", 0) + 1);
                if (isLightPressed)
                {
                    await btnLight.ChangeBackgroundColorTo(Color.Red, 250, Easing.CubicOut);
                    await btnLight.ChangeBackgroundColorTo(Color.White, 250, Easing.CubicOut);

                }
                else
                {
                    await btnDark.ChangeBackgroundColorTo(Color.Red, 250, Easing.CubicOut);
                    await btnDark.ChangeBackgroundColorTo(Color.Black, 250, Easing.CubicOut);

                }
            }
           
        }

        private async void RemoveLife()
        {
            if (_isRemovingLife)
                return;
            else
                _isRemovingLife = true;
            _currentLifes--;
            if (_currentLifes == 2)
            {
                await life3.FadeTo(0, 200);
                life3.IsVisible = false;
            }
            else if (_currentLifes == 1)
            {
                await life2.FadeTo(0, 200);
                life2.IsVisible = false;
            }
            else
            {
                await life1.FadeTo(0, 200);
                life1.IsVisible = false;
                btnLight.IsEnabled = false;
                btnDark.IsEnabled = false;
                await Navigation.PushAsync(new ScorePage(_currentScore, _timePressure));
            }
            _isRemovingLife = false;
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("Alert", "Do you really want to go back to the main menu?", "Yes", "No");
                if (result) await Navigation.PopToRootAsync();
            });
            return true;
        }
    }
}