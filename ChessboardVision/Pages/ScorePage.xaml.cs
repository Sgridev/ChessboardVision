﻿using System;
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
        private readonly bool timePressure;
        private readonly int savedBestScore;


        public ScorePage(int gameScore, bool timePressure)
        {
            InitializeComponent();
            this.timePressure = timePressure;
            savedBestScore = timePressure ? Preferences.Get("best_score_time_pressure", 0) : Preferences.Get("best_score_standard", 0);
            currentScore.Text = gameScore.ToString();
            if(gameScore > savedBestScore)
            {
                savedBestScore = gameScore;
                Preferences.Set(timePressure ? "best_score_time_pressure" : "best_score_standard", savedBestScore);
                newBest.IsVisible = true;
            }
            bestScore.Text = savedBestScore.ToString();
            currentScore.Text = gameScore.ToString();
         
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void PlayAgain_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage(timePressure));
        }
        private async void Menu_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}