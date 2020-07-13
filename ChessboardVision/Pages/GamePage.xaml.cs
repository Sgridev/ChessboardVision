using ChessboardVision.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChessboardVision
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        private readonly bool timePressure;
        private Task progressBarFinish;
        private int currentScore = 0;
        private int currentLifes = 3;
        public GamePage()
        {
            InitializeComponent();
        }

        public GamePage(bool timePressure)
        {
            InitializeComponent();
            this.timePressure = timePressure;
            

        }

        protected async override void OnAppearing()
        {

            if (timePressure)
            {
                progressBar.IsVisible = true;
                progressBarFinish = WaitProgressBarAsync();

            }

            square.Text = Chessboard.GetRandomSquare();

            if (timePressure)
            {
                await progressBarFinish;
                await Navigation.PushAsync(new ScorePage(currentScore, timePressure));
            }

        }

        private async Task WaitProgressBarAsync()
        {
            await progressBar.ProgressTo(1, 60000, Easing.Linear);

        }

        private void IsLight_OnClicked(object sender, EventArgs e)
        {
            CheckColorInput(true);
          

        }
        private void IsDark_OnClicked(object sender, EventArgs e)
        {
            CheckColorInput(false);

        }

        private async void CheckColorInput(bool isLightPressed)
        {
            if (Chessboard.IsColorCorrect(square.Text, isLightPressed))
            {
                ++currentScore;
                score.Text = currentScore.ToString();
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
            square.Text = Chessboard.GetRandomSquare();
        }

        private async void RemoveLife()
        {
            currentLifes--;
            if (currentLifes == 2)
            {
                await life3.FadeTo(0, 200);
                life3.IsVisible = false;
            }
            else if (currentLifes == 1)
            {
                await life2.FadeTo(0, 200);
                life2.IsVisible = false;
            }
            else
            {
                await life1.FadeTo(0, 200);
                life1.IsVisible = false;
                await Navigation.PushAsync(new ScorePage(currentScore, timePressure));
            }

        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("Alert", "Do you really want to go back to the main menu?", "Yes", "No");
                if (result) await Navigation.PushAsync(new MainPage());
            });
            base.OnBackButtonPressed();
            return true;
        }
    }
}