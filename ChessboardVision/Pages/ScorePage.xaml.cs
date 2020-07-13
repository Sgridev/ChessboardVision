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
    public partial class ScorePage : ContentPage
    {
        private int gameScore;
        private bool timePressure;



        public ScorePage(int currentScore1, bool timePressure)
        {
            InitializeComponent();
            this.gameScore = currentScore1;
            this.timePressure = timePressure;
            
            currentScore.Text = gameScore.ToString();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}