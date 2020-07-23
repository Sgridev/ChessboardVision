using Xamarin.Essentials;

namespace ChessboardVision.ViewModels
{
    class ProfileViewModel
    {
        public ProfileViewModel()
        {
            BestScoreStandard = Preferences.Get("best_score_standard", 0).ToString();
            BestScoreTimePressure = Preferences.Get("best_score_time_pressure", 0).ToString();
            GamesPlayed = Preferences.Get("games_played", 0).ToString();
            CorrectAnswers = Preferences.Get("correct_answers", 0).ToString();
            WrongAnswers = Preferences.Get("wrong_answers", 0).ToString();
        }


        public string BestScoreStandard { get; set; }
        public string BestScoreTimePressure { get; set; }
        public string GamesPlayed { get; set; }
        public string CorrectAnswers { get; set; }
        public string WrongAnswers { get; set; }
    }
}
