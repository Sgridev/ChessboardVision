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
        private int gameResult;
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
                await Navigation.PushModalAsync(new ScorePage(currentScore, timePressure));
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

        private void CheckColorInput(bool isLight)
        {
            if (Chessboard.IsColorCorrect(square.Text, isLight))
            {
                ++currentScore;
                score.Text = currentScore.ToString();

            }
            else
                RemoveLife();

            square.Text = Chessboard.GetRandomSquare();
        }

        private async void RemoveLife()
        {
            currentLifes--;
            if (currentLifes == 2)
                life3.IsVisible = false;
            else if(currentLifes == 1)
                life2.IsVisible = false;
            else
            {

                life1.IsVisible = false;
                await Navigation.PushModalAsync(new ScorePage(currentScore, timePressure));
            }

        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}