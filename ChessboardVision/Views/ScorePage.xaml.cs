using ChessboardVision.ViewModels;
using Xamarin.Forms;

namespace ChessboardVision.Views
{

    public partial class ScorePage : ContentPage
    {      
      
        public ScorePage(int gameScore, bool timePressure)
        {
            InitializeComponent();
            BindingContext = new ScoreViewModel(gameScore, timePressure, Navigation);    
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }


    }
}